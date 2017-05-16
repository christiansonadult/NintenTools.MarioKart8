using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Syroot.BinaryData;
using Syroot.Maths;
using Syroot.NintenTools.MarioKart8.IO;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a model referenced in a <see cref="KclFile"/> which can hold up to 65535 triangles.
    /// </summary>
    public class KclModel : ILoadableFile, ISaveableFile
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="KclModel"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public KclModel(Stream stream, bool loadOctree = true)
        {
            Load(stream, loadOctree);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KclModel"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public KclModel(string fileName, bool loadOctree = true)
        {
            Load(fileName, loadOctree);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets an unknown value.
        /// </summary>
        public float Unknown1 { get; private set; }

        /// <summary>
        /// Gets or sets the smallest coordinate of the cube spanned by the model.
        /// </summary>
        public Vector3F MinCoordinate { get; private set; }

        /// <summary>
        /// Gets the coordinate mask required to compute indices into the octree.
        /// </summary>
        public Vector3 CoordinateMask { get; private set; }

        /// <summary>
        /// Gets the coordinate shift required to compute indices into the octree.
        /// </summary>
        public Vector3 CoordinateShift { get; private set; }

        /// <summary>
        /// Gets an unknown value.
        /// </summary>
        public float Unknown2 { get; private set; }

        /// <summary>
        /// Gets the array of vertex positions.
        /// </summary>
        public Vector3F[] Positions { get; private set; }

        /// <summary>
        /// Gets the array of vertex normals.
        /// </summary>
        public Vector3F[] Normals { get; private set; }

        /// <summary>
        /// Gets the array of triangles.
        /// </summary>
        public Triangle[] Triangles { get; private set; }

        /// <summary>
        /// Gets the root nodes of the model triangle octree. Can be <c>null</c> if no octree was loaded or created yet.
        /// </summary>
        public ModelOctreeNode[] ModelOctreeRoots { get; private set; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Loads the data from the given <paramref name="stream"/>, including the octree.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to load the data from.</param>
        public void Load(Stream stream)
        {
            Load(stream, true);
        }

        /// <summary>
        /// Loads the data from the given file, including the octree.
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from.</param>
        public void Load(string fileName)
        {
            Load(fileName, true);
        }

        /// <summary>
        /// Loads the data from the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to load the data from.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public void Load(Stream stream, bool loadOctree)
        {
            using (BinaryDataReader reader = new BinaryDataReader(stream, true) { ByteOrder = ByteOrder.BigEndian })
            {
                long modelPosition = reader.Position;

                // Read the header.
                int positionArrayOffset = reader.ReadInt32();
                int normalArrayOffset = reader.ReadInt32();
                int triangleArrayOffset = reader.ReadInt32();
                int octreeOffset = reader.ReadInt32();
                Unknown1 = reader.ReadSingle();
                MinCoordinate = reader.ReadVector3F();
                CoordinateMask = reader.ReadVector3();
                CoordinateShift = reader.ReadVector3();
                Unknown2 = reader.ReadSingle();

                // Read the positions.
                reader.Position = modelPosition + positionArrayOffset; // Mostly unrequired, data is successive.
                int positionCount = (normalArrayOffset - positionArrayOffset) / Vector3F.SizeInBytes;
                Positions = reader.ReadVector3Fs(positionCount);

                // Read the normals.
                reader.Position = modelPosition + normalArrayOffset; // Mostly unrequired, data is successive.
                int normalCount = (triangleArrayOffset - normalArrayOffset) / Vector3F.SizeInBytes;
                Normals = reader.ReadVector3Fs(normalCount);

                // Read the triangles.
                reader.Position = modelPosition + triangleArrayOffset; // Mostly unrequired, data is successive.
                int triangleCount = (octreeOffset - triangleArrayOffset) / Marshal.SizeOf<Triangle>();
                Triangles = reader.ReadTriangles(triangleCount);

                // Read the octree.
                if (loadOctree)
                {
                    reader.Position = modelPosition + octreeOffset; // Mostly unrequired, data is successive.
                    int nodeCount
                         = ((~CoordinateMask.X >> CoordinateShift.X) + 1)
                         * ((~CoordinateMask.Y >> CoordinateShift.X) + 1)
                         * ((~CoordinateMask.Z >> CoordinateShift.X) + 1);
                    ModelOctreeRoots = new ModelOctreeNode[nodeCount];
                    for (int i = 0; i < nodeCount; i++)
                    {
                        ModelOctreeRoots[i] = new ModelOctreeNode(null, reader, modelPosition + octreeOffset);
                    }
                    // Reader is now behind the last octree key, not at end of the model / behind the last separator.
                }
            }
        }

        /// <summary>
        /// Loads the data from the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public void Load(string fileName, bool loadOctree)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(stream, loadOctree);
            }
        }

        /// <summary>
        /// Saves the data into the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to save the data to.</param>
        public void Save(Stream stream)
        {
            using (BinaryDataWriter writer = new BinaryDataWriter(stream, true) { ByteOrder = ByteOrder.BigEndian })
            {
                long modelPosition = writer.Position;

                // Write the header.
                Offset positionArrayOffset = writer.ReserveOffset();
                Offset normalArrayOffset = writer.ReserveOffset();
                Offset triangleArrayOffset = writer.ReserveOffset();
                Offset octreeOffset = writer.ReserveOffset();
                writer.Write(Unknown1);
                writer.Write(MinCoordinate);
                writer.Write(CoordinateMask);
                writer.Write(CoordinateShift);
                writer.Write(Unknown2);

                // Write the positions.
                positionArrayOffset.Satisfy((int)(writer.Position - modelPosition));
                writer.Write(Positions);

                // Write the normals.
                normalArrayOffset.Satisfy((int)(writer.Position - modelPosition));
                writer.Write(Normals);

                // Write the triangles.
                triangleArrayOffset.Satisfy((int)(writer.Position - modelPosition));
                writer.Write(Triangles);

                // Write the octree.
                int octreeOffsetValue = (int)(writer.Position - modelPosition);
                octreeOffset.Satisfy(octreeOffsetValue);
                // Write the node keys, and compute the correct offsets into the triangle lists or to child nodes.
                // Nintendo writes child nodes behind the current node, so the children need to be queued.
                // In this implementation, empty triangle lists point to the same terminator behind the last node.
                // This could be further optimized by reusing equal parts of lists as Nintendo apparently did it.
                int emptyListPos = GetNodeCount(ModelOctreeRoots) * sizeof(uint);
                int triangleListPos = emptyListPos + sizeof(ushort);
                Queue<ModelOctreeNode[]> queuedNodes = new Queue<ModelOctreeNode[]>();
                queuedNodes.Enqueue(ModelOctreeRoots);
                while (queuedNodes.Count > 0)
                {
                    ModelOctreeNode[] nodes = queuedNodes.Dequeue();
                    long offset = writer.Position - modelPosition - octreeOffsetValue;
                    foreach (ModelOctreeNode node in nodes)
                    {
                        if (node.Children == null)
                        {
                            // Node is a leaf and points to triangle index list.
                            int listPos;
                            if (node.TriangleIndices.Count == 0)
                            {
                                listPos = emptyListPos;
                            }
                            else
                            {
                                listPos = triangleListPos;
                                triangleListPos += (node.TriangleIndices.Count + 1) * sizeof(ushort);
                            }
                            node.Key = (uint)ModelOctreeNode.Flags.Values | (uint)(listPos - offset - sizeof(ushort));
                        }
                        else
                        {
                            // Node is a branch and points to 8 children.
                            node.Key = (uint)(nodes.Length + queuedNodes.Count * 8) * sizeof(uint);
                            queuedNodes.Enqueue(node.Children);
                        }
                        writer.Write(node.Key);
                    }
                }
                // Iterate through the nodes again and write their triangle lists now.
                writer.Write((ushort)0xFFFF); // Terminator for all empty lists.
                queuedNodes.Enqueue(ModelOctreeRoots);
                while (queuedNodes.Count > 0)
                {
                    ModelOctreeNode[] nodes = queuedNodes.Dequeue();
                    foreach (ModelOctreeNode node in nodes)
                    {
                        if (node.Children == null)
                        {
                            if (node.TriangleIndices.Count > 0)
                            {
                                // Node is a leaf and points to triangle index list.
                                writer.Write(node.TriangleIndices);
                                writer.Write((ushort)0xFFFF);
                            }
                        }
                        else
                        {
                            // Node is a branch and points to 8 children.
                            queuedNodes.Enqueue(node.Children);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the data in the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the data in.</param>
        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                Save(stream);
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private int GetNodeCount(ModelOctreeNode[] nodes)
        {
            int count = nodes.Length;
            foreach (ModelOctreeNode node in nodes)
            {
                if (node.Children != null)
                {
                    count += GetNodeCount(node.Children);
                }
            }
            return count;
        }
    }
}