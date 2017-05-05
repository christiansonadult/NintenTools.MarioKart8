using System.Collections;
using System.Collections.Generic;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents the non-generic base class for all <see cref="GroupBase{T}"/>.
    /// </summary>
    public abstract class GroupBase
    {
        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Stores the data of the group with the given <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryDataWriter"/> to write the data with.</param>
        internal abstract void Save(BinaryDataWriter writer);
    }

    /// <summary>
    /// Represents a group in a <see cref="Section"/> which stores 1 or more elements. The exact number of elements and
    /// their data has to be computed by the group itself.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public abstract class GroupBase<T> : GroupBase, IList<T>
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<T> _list;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public GroupBase(BinaryDataReader reader, SectionBase section, int sectionLength)
        {
            _list = new List<T>();
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets the number of elements in this group.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public void Add(T item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }
    }
}
