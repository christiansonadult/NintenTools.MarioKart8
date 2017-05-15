using System;
using System.Diagnostics;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a node in a course model octree.
    /// </summary>
    [DebuggerDisplay(nameof(CourseOctreeNode) + " ModelIndex={ModelIndex}")]
    public class CourseOctreeNode : OctreeNodeBase<CourseOctreeNode>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _childCount = 8;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseOctreeNode"/> class with no parent and empty key.
        /// </summary>
        internal CourseOctreeNode()
            : base(null, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseOctreeNode"/> class with the given
        /// <paramref name="parent"/> and its key and data read from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="CourseOctreeNode"/>.</param>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the node data with.</param>
        internal CourseOctreeNode(CourseOctreeNode parent, BinaryDataReader reader)
            : base(parent, reader.ReadUInt32())
        {
            Flags flags = (Flags)(Key >> 30);
            switch (flags)
            {
                case Flags.Divide:
                    for (int i = 0; i < _childCount; i++)
                    {
                        Children[i] = new CourseOctreeNode(this, reader);
                    }
                    break;
                case Flags.Index:
                    ModelIndex = (int)(Key & 0b00111111_11111111_11111111_11111111);
                    break;
                case Flags.NoData:
                    break;
                default:
                    throw new ArgumentException($"Course octree node key has invalid flags {flags}.", nameof(Key));
            }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets the index of the model to check in this cube.
        /// </summary>
        public int? ModelIndex { get; }
        
        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------
        
        private enum Flags
        {
            Divide = 0,
            Index = 2,
            NoData = 3
        }
    }
}
