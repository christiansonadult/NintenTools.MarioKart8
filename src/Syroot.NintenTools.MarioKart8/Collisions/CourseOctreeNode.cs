using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a node in a course model octree.
    /// </summary>
    [DebuggerDisplay(nameof(CourseOctreeNode) + " ModelIndex={ModelIndex}")]
    public class CourseOctreeNode : IEnumerable<CourseOctreeNode>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _childCount = 8;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        internal CourseOctreeNode(CourseOctreeNode parent, uint key, BinaryDataReader reader)
        {
            Parent = parent;
            Children = new CourseOctreeNode[_childCount];

            Flags flags = (Flags)(key >> 30);
            switch (flags)
            {
                case Flags.Divide:
                    for (int i = 0; i < _childCount; i++)
                    {
                        Children[i] = new CourseOctreeNode(this, reader.ReadUInt32(), reader);
                    }
                    break;
                case Flags.Index:
                    ModelIndex = (int)(key & 0b00111111_11111111_11111111_11111111);
                    break;
                case Flags.NoData:
                    break;
                default:
                    throw new ArgumentException($"Course octree node key has invalid flags {flags}.", nameof(key));
            }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the parent node containing this instance, or <c>null</c> if this is a root node.
        /// </summary>
        public CourseOctreeNode Parent
        {
            get; private set;
        }

        /// <summary>
        /// Gets the eight children of this node.
        /// </summary>
        public CourseOctreeNode[] Children
        {
            get;
        }

        /// <summary>
        /// Gets the index of the model to check in this cube.
        /// </summary>
        public int? ModelIndex { get; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public IEnumerator<CourseOctreeNode> GetEnumerator()
        {
            return ((IEnumerable<CourseOctreeNode>)Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------
        
        private enum Flags
        {
            Divide = 0,
            Index = 2,
            NoData = 3
        }
    }
}
