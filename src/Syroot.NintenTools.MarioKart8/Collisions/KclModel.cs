using System;
using System.Diagnostics;
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
            using (BinaryDataReader reader = new BinaryDataReader(stream, true))
            {
                reader.ByteOrder = ByteOrder.BigEndian;
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
    }
}