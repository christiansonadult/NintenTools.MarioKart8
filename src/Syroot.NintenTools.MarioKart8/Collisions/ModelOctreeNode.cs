using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a node in a model triangle octree.
    /// </summary>
    [DebuggerDisplay(nameof(ModelOctreeNode) + " {TriangleIndices?.Count} Triangles")]
    public class ModelOctreeNode : OctreeNodeBase<ModelOctreeNode>
    {   
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseOctreeNode"/> class with the given
        /// <paramref name="parent"/> and its key and data read from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="CourseOctreeNode"/>.</param>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the node data with.</param>
        /// <param name="parentOffset">The required offset of the start of the parent node.</param>
        internal ModelOctreeNode(ModelOctreeNode parent, BinaryDataReader reader, long parentOffset)
            : base(parent, reader.ReadUInt32())
        {
            // Get and seek to the data offset in bytes relative to the parent node's start.
            long offset = parentOffset + Key & ~_flagMask;
            if ((Flags)(Key & _flagMask) == Flags.Values)
            {
                // Node is a leaf and key points to triangle list starting 2 bytes later.
                using (reader.TemporarySeek(offset + sizeof(ushort), SeekOrigin.Begin))
                {
                    TriangleIndices = new List<ushort>();
                    ushort index;
                    while ((index = reader.ReadUInt16()) != 0xFFFF)
                    {
                        TriangleIndices.Add(index);
                    }
                }
            }
            else
            {
                // Node is a branch and points to 8 child nodes.
                using (reader.TemporarySeek(offset, SeekOrigin.Begin))
                {
                    ModelOctreeNode[] children = new ModelOctreeNode[ChildCount];
                    for (int i = 0; i < ChildCount; i++)
                    {
                        children[i] = new ModelOctreeNode(this, reader, offset);
                    }
                    Children = children;
                }
            }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the indices to triangles of the model available in this cube.
        /// </summary>
        public List<ushort> TriangleIndices { get; }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal void Save(int parentOffset)
        {
            
        }
    }
}
