using System;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a node in a course model octree.
    /// </summary>
    public class CourseOctreeNode : OctreeNodeBase<CourseOctreeNode>
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseOctreeNode"/> class with no parent, an initialized child
        /// array and empty key.
        /// </summary>
        internal CourseOctreeNode()
            : base(null, 0)
        {
            Children = new CourseOctreeNode[ChildCount];
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
            // If this is a branch, it is subdivided into 8 child nodes.
            if ((Flags)(Key >> 30) == Flags.Divide)
            {
                CourseOctreeNode[] children = new CourseOctreeNode[ChildCount];
                for (int i = 0; i < ChildCount; i++)
                {
                    children[i] = new CourseOctreeNode(this, reader);
                }
                Children = children;
            }
        }
        
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the index of the model this node points to if it is a leaf node.
        /// </summary>
        /// <returns>The index of the model if this is a leaf node.</returns>
        public int GetModelIndex()
        {
            if ((Flags)(Key >> 30) == Flags.Index)
            {
                return (int)(Key & 0b00111111_11111111_11111111_11111111);
            }
            throw new InvalidOperationException("This node does not represent a model index.");
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal void Save(BinaryDataWriter writer)
        {
            // Write the key.
            writer.Write(Key);

            // If this is a branch, write the children.
            if ((Flags)(Key >> 30) == Flags.Divide)
            {
                foreach (CourseOctreeNode child in Children)
                {
                    child.Save(writer);
                }
            }
        }

        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------

        private enum Flags
        {
            Divide = 0b00,
            Index = 0b10,
            NoData = 0b11
        }
    }
}
