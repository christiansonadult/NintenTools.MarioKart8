using System.Collections;
using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents the base for an octree node.
    /// </summary>
    /// <typeparam name="T">The type of the octree node.</typeparam>
    public abstract class OctreeNodeBase<T> : IEnumerable<T>
        where T : OctreeNodeBase<T>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _childCount = 8;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="OctreeNodeBase{T}"/> class with the given
        /// <paramref name="parent"/> and octree node <paramref name="key"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="OctreeNodeBase{T}"/>, if any.</param>
        /// <param name="key">The octree node key with which the node can be referenced.</param>
        protected OctreeNodeBase(T parent, uint key)
        {
            Parent = parent;
            Key = key;
            Children = new T[_childCount];
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the parent node containing this instance, or <c>null</c> if this is a root node.
        /// </summary>
        public T Parent { get; protected set; }

        /// <summary>
        /// Gets the octree key used to reference this node.
        /// </summary>
        public uint Key { get; }

        /// <summary>
        /// Gets the eight children of this node.
        /// </summary>
        public T[] Children { get; protected set; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------
        
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)Children).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Children.GetEnumerator();
        }
    }
}
