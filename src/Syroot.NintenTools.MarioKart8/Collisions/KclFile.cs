using System.Collections.Generic;
using System.IO;
using Syroot.BinaryData;
using Syroot.Maths;
using Syroot.NintenTools.MarioKart8.IO;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents the collision model of a course which sorts polygons into an octree to allow fast spatial lookups.
    /// </summary>
    public class KclFile : ILoadableFile, ISaveableFile
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _signature = 0x02020000;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        /// <summary>
        /// Initializes a new instance of the <see cref="KclFile"/> class.
        /// </summary>
        public KclFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KclFile"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public KclFile(Stream stream, bool loadOctree = true)
        {
            Load(stream, loadOctree);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KclFile"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing triangles.</param>
        public KclFile(string fileName, bool loadOctree = true)
        {
            Load(fileName, loadOctree);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the smallest coordinate spanned by the octree in this file.
        /// </summary>
        public Vector3F MinCoordinate { get; private set; }

        /// <summary>
        /// Gets the biggest coordinate spanned by the octree in this file.
        /// </summary>
        public Vector3F MaxCoordinate { get; private set; }

        /// <summary>
        /// Gets the coordinate shift required to compute indices into the octree.
        /// </summary>
        public Vector3 CoordinateShift { get; private set; }

        /// <summary>
        /// Gets an unknown value.
        /// </summary>
        public int Unknown { get; private set; }

        /// <summary>
        /// Gets the root node of the course model octree. Can be <c>null</c> if no octree was loaded or created yet.
        /// </summary>
        public CourseOctreeNode CourseOctreeRoot { get; private set; }

        /// <summary>
        /// Gets the list of <see cref="KclModel"/> instances referenced by the course octree.
        /// </summary>
        public List<KclModel> Models { get; private set; }

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
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing models.</param>
        public void Load(Stream stream, bool loadOctree)
        {
            using (BinaryDataReader reader = new BinaryDataReader(stream, true) { ByteOrder = ByteOrder.BigEndian })
            {
                // Read the header.
                if (reader.ReadInt32() != _signature)
                {
                    throw new InvalidDataException("Invalid KCL file signature.");
                }
                int octreeOffset = reader.ReadInt32();
                int modelOffsetArrayOffset = reader.ReadInt32();
                int modelCount = reader.ReadInt32();
                MinCoordinate = reader.ReadVector3F();
                MaxCoordinate = reader.ReadVector3F();
                CoordinateShift = reader.ReadVector3();
                Unknown = reader.ReadInt32();

                // Read the model octree.
                if (loadOctree)
                {
                    reader.Position = octreeOffset; // Mostly unrequired, data is successive.
                    CourseOctreeRoot = new CourseOctreeNode();
                    for (int i = 0; i < CourseOctreeNode.ChildCount; i++)
                    {
                        CourseOctreeRoot.Children[i] = new CourseOctreeNode(CourseOctreeRoot, reader);
                    }
                }

                // Read the model offsets.
                reader.Position = modelOffsetArrayOffset; // Mostly unrequired, data is successive.
                int[] modelOffsets = reader.ReadInt32s(modelCount);

                // Read the models.
                Models = new List<KclModel>(modelCount);
                foreach (int modelOffset in modelOffsets)
                {
                    reader.Position = modelOffset; // Required as loading a model does not position reader at its end.
                    Models.Add(new KclModel(stream, loadOctree));
                }
            }
        }

        /// <summary>
        /// Loads the data from the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from.</param>
        /// <param name="loadOctree"><c>true</c> to also load the octree referencing models.</param>
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
                // Write the header.
                writer.Write(_signature);
                Offset octreeOffset = writer.ReserveOffset();
                Offset modelOffsetArrayOffset = writer.ReserveOffset();
                writer.Write(Models.Count);
                writer.Write(MinCoordinate);
                writer.Write(MaxCoordinate);
                writer.Write(CoordinateShift);
                writer.Write(Unknown);

                // Write the model octree.
                octreeOffset.Satisfy();
                foreach (CourseOctreeNode rootChild in CourseOctreeRoot)
                {
                    rootChild.Save(writer);
                }

                // Write the model offsets.
                modelOffsetArrayOffset.Satisfy();
                Offset[] modelOffsets = writer.ReserveOffset(Models.Count);

                // Write the models.
                int i = 0;
                foreach (KclModel model in Models)
                {
                    modelOffsets[i++].Satisfy();
                    model.Save(stream);
                    writer.Align(4);
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
    }
}
