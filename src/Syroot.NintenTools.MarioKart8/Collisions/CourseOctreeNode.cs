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
            switch ((Flags)(Key & _flagMask))
            {
                case Flags.Divide:
                    // Node is a branch subdivided into 8 children.
                    Children = new CourseOctreeNode[ChildCount];
                    for (int i = 0; i < ChildCount; i++)
                    {
                        Children[i] = new CourseOctreeNode(this, reader);
                    }
                    break;
                case Flags.Values:
                    // Node points to a model in the file's model array.
                    ModelIndex = Key & ~_flagMask;
                    break;
            }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the index to the model referenced by this node in the model array of the file this node belongs to.
        /// </summary>
        public uint? ModelIndex { get; }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal void Save(BinaryDataWriter writer)
        {
            if (Children == null)
            {
                if (ModelIndex.HasValue)
                {
                    // Node points to a model in the file's model array.
                    Key = (uint)Flags.Values | ModelIndex.Value;
                }
                else
                {
                    // Node is an empty cube.
                    Key = (uint)Flags.NoData;
                }
                writer.Write(Key);
            }
            else
            {
                // Node is a branch subdivided into 8 children.
                Key = 8;
                writer.Write(Key);
                foreach (CourseOctreeNode child in Children)
                {
                    child.Save(writer);
                }
            }
        }
    }
}
