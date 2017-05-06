using System;
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
    /// Represents a group in a <see cref="SectionBase"/> which stores 1 or more elements. The exact number of elements
    /// and their data has to be computed by the group itself.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    public abstract class GroupBase<T> : GroupBase, IList<T>
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<T> _list;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupBase"/> instance read from the given
        /// <paramref name="reader"/> with the provided settings.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data with.</param>
        /// <param name="section">The <see cref="SectionBase"/> to which this group belongs.</param>
        /// <param name="sectionLength">The length of the full section to which this group belongs.</param>
        public GroupBase(BinaryDataReader reader, SectionBase section, int sectionLength)
        {
            _list = new List<T>();
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the element at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
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

        /// <summary>
        /// Gets a value indicating whether the group is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Adds an element to the group.
        /// </summary>
        /// <param name="item">The element to add to the group.</param>
        public void Add(T item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// Removes all element instances from the group.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }
        
        /// <summary>
        /// Determines whether the group contains a specific element.
        /// </summary>
        /// <param name="item">The element to locate in the group.</param>
        /// <returns><c>true</c> if element is found in the group; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the group to an <see cref="Array"/>, starting at a particular <see cref="Array"/>
        /// index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied
        /// from the group. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the element instances of the group.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the element instances of the group.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific element in the file.
        /// </summary>
        /// <param name="item">The element to locate in the file.</param>
        /// <returns>The index of element if found in the list; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts an element to the group at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The element to insert into the group.</param>
        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific element from the group.
        /// </summary>
        /// <param name="item">The element to remove from the group.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the group; otherwise,
        /// <c>false</c>. This method also returns <c>false</c> if <paramref name="item"/> is not found in the original
        /// group.</returns>
        public bool Remove(T item)
        {
            return _list.Remove(item);
        }

        /// <summary>
        /// Removes the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }
    }
}
