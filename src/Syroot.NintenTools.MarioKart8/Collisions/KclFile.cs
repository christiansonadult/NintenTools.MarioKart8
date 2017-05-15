using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public KclFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KclFile"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        public KclFile(Stream stream)
        {
            Load(stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KclFile"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        public KclFile(string fileName)
        {
            Load(fileName);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the smallest coordinate spanned by the octree in this file.
        /// </summary>
        public Vector3F MinCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the biggest coordinate spanned by the octree in this file.
        /// </summary>
        public Vector3F MaxCoordinate { get; set; }
        
        public Vector3 CoordinateShift { get; set; }

        /// <summary>
        /// Gets or sets an unknown value.
        /// </summary>
        public int Unknown { get; set; }

        /// <summary>
        /// Gets the root node of the course model octree.
        /// </summary>
        public CourseOctreeNode CourseOctreeRoot { get; private set; }

        /// <summary>
        /// Gets the list of <see cref="KclModel"/> instances referenced by the course octree.
        /// </summary>
        public List<KclModel> Models { get; private set; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Loads the data from the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to load the data from.</param>
        public void Load(Stream stream)
        {
            using (BinaryDataReader reader = new BinaryDataReader(stream, true))
            {
                reader.ByteOrder = ByteOrder.BigEndian;

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
                reader.Position = octreeOffset; // Mostly unrequired, data is successive.
                CourseOctreeRoot = new CourseOctreeNode(null, 0x00000008, reader);

                // Read the model offsets.
                reader.Position = modelOffsetArrayOffset; // Mostly unrequired, data is successive.
                int[] modelOffsets = reader.ReadInt32s(modelCount);

                // Read the models.
                Models = new List<KclModel>(modelCount);
                foreach (int modelOffset in modelOffsets)
                {
                    reader.Position = modelOffset; // Required as loading a model does not position reader at its end.
                    Models.Add(new KclModel(stream));
                }
            }
        }

        /// <summary>
        /// Loads the data from the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from.</param>
        public void Load(string fileName)
        {
            Load(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
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
