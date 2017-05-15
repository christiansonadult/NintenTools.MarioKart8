using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a node in a model triangle octree.
    /// </summary>
    [DebuggerDisplay(nameof(ModelOctreeNode) + " {TriangleIndices?.Length} triangles")]
    public class ModelOctreeNode : IEnumerable<ModelOctreeNode>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _childCount = 8;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        internal ModelOctreeNode(ModelOctreeNode parent, long parentOffset, uint key, BinaryDataReader reader)
        {
            Parent = parent;
            Children = new ModelOctreeNode[_childCount];

            // Get and seek to the data offset in bytes relative to the parent node's start.
            long offset = parentOffset + key & 0b00111111_11111111_11111111_11111111;
            using (reader.TemporarySeek(offset, SeekOrigin.Begin))
            {
                if (key >> 31 == 1)
                {
                    // Node is a leaf and key points to triangle list starting with a separator.
                    reader.Seek(sizeof(ushort)); // Skip the starting separator.
                    List<ushort> indices = new List<ushort>();
                    ushort index;
                    while ((index = reader.ReadUInt16()) != 0xFFFF)
                    {
                        indices.Add(index);
                    }
                    TriangleIndices = indices.ToArray();
                }
                else
                {
                    // Node is a branch and points to 8 child nodes.
                    for (int i = 0; i < _childCount; i++)
                    {
                        Children[i] = new ModelOctreeNode(this, offset, reader.ReadUInt32(), reader);
                    }
                }
            }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the parent node containing this instance, or <c>null</c> if this is a root node.
        /// </summary>
        public ModelOctreeNode Parent
        {
            get; private set;
        }

        /// <summary>
        /// Gets the eight children of this node.
        /// </summary>
        public ModelOctreeNode[] Children
        {
            get;
        }

        /// <summary>
        /// Gets the indices to triangles of the model to check in this cube.
        /// </summary>
        public ushort[] TriangleIndices { get; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public IEnumerator<ModelOctreeNode> GetEnumerator()
        {
            return ((IEnumerable<ModelOctreeNode>)Children).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------

        [Flags]
        private enum Flags : byte
        {
            Divide = 0,
            TriangleArrayOffset = 1
        }
    }
}
