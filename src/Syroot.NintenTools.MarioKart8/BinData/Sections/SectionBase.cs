using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a section in a <see cref="BinFile"/>. A section is identified by a name and then consists of 1 or
    /// more groups storing elements of different type.
    /// </summary>
    [DebuggerDisplay(nameof(SectionBase) + " {Name}, {Count} groups")]
    public abstract class SectionBase : IList<GroupBase>
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<GroupBase> _list;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionBase"/> class read from the given
        /// <paramref name="reader"/> with the provided settings.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data with.</param>
        /// <param name="sectionLength">The length of the section in bytes.</param>
        /// <param name="name">The identifier of the section.</param>
        /// <param name="groupCount">The number of groups in this section.</param>
        /// <param name="elementCount">The number of elements in a group.</param>
        /// <param name="sectionType">The <see cref="SectionType"/> identifier of the section.</param>
        internal SectionBase(BinaryDataReader reader, int sectionLength, string name, short groupCount,
            short elementCount, SectionType sectionType)
        {
            Name = name;
            _list = new List<GroupBase>(groupCount);
            ElementCount = elementCount;
            SectionType = sectionType;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the <see cref="GroupBase"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="GroupBase"/> to get or set.</param>
        /// <returns>The <see cref="GroupBase"/> at the specified index.</returns>
        public GroupBase this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the number of elements in this group as reserved from the constructor.
        /// </summary>
        internal int Capacity
        {
            get { return _list.Capacity; }
        }

        /// <summary>
        /// Gets the number of <see cref="GroupBase"/> instances in this section.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the section is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets a 4 character long ASCII section identifier. It basically determines the meaning of the entries.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the number of elements in a group.
        /// </summary>
        public short ElementCount { get; }

        /// <summary>
        /// Gets the <see cref="SectionType"/>, determining the data this section provides.
        /// </summary>
        public SectionType SectionType { get; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a <see cref="GroupBase"/> to the section.
        /// </summary>
        /// <param name="item">The <see cref="GroupBase"/> to add to the section.</param>
        public void Add(GroupBase item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// Removes all <see cref="GroupBase"/> instances from the section.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Determines whether the section contains a specific <see cref="GroupBase"/>.
        /// </summary>
        /// <param name="item">The <see cref="GroupBase"/> to locate in the section.</param>
        /// <returns><c>true</c> if item is found in the section; otherwise, <c>false</c>.</returns>
        public bool Contains(GroupBase item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the section to an <see cref="Array"/>, starting at a particular <see cref="Array"/>
        /// index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied
        /// from the section. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(GroupBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="GroupBase"/> instances of the section.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<GroupBase> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="GroupBase"/> instances of the section.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific <see cref="GroupBase"/> in the file.
        /// </summary>
        /// <param name="item">The <see cref="GroupBase"/> to locate in the file.</param>
        /// <returns>The index of <see cref="GroupBase"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(GroupBase item)
        {
            return _list.IndexOf(item);
        }

        /// <summary>
        /// Inserts a <see cref="GroupBase"/> to the section at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The <see cref="GroupBase"/> to insert into the section.</param>
        public void Insert(int index, GroupBase item)
        {
            _list.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="GroupBase"/> from section.
        /// </summary>
        /// <param name="item">The <see cref="GroupBase"/> to remove from the section.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the section; otherwise,
        /// <c>false</c>. This method also returns <c>false</c> if <paramref name="item"/> is not found in the original
        /// section.</returns>
        public bool Remove(GroupBase item)
        {
            return _list.Remove(item);
        }

        /// <summary>
        /// Removes the <see cref="GroupBase"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="GroupBase"/> to remove.</param>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Creates a correctly typed <see cref="SectionBase"/> instance from the given <paramref name="reader"/> with
        /// the provided <paramref name="sectionLength"/> in bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data from.</param>
        /// <param name="sectionLength">The length of the section in bytes.</param>
        /// <returns>A correctly typed <see cref="SectionBase"/> representing the section data.</returns>
        internal static SectionBase Create(BinaryDataReader reader, int sectionLength)
        {
            // Create a strongly-typed section class depending on the section type.
            string name = reader.ReadString(4);
            short elementCount = reader.ReadInt16();
            short groupCount = reader.ReadInt16();
            SectionType sectionType = reader.ReadEnum<SectionType>(false);
            switch (sectionType)
            {
                case SectionType.DwordArray:
                    return new DwordArraySection(reader, sectionLength, name, groupCount, elementCount);
                case SectionType.String:
                    return new StringSection(reader, sectionLength, name, groupCount, elementCount);
            }
            return new RawSection(reader, sectionLength, name, groupCount, elementCount, sectionType);
        }
    }
    
    /// <summary>
    /// Represents the known <see cref="SectionBase"/> types.
    /// </summary>
    public enum SectionType : int
    {
        /// <summary>
        /// A section containing <see cref="DwordArrayGroup"/> instances.
        /// </summary>
        DwordArray = 0x00000000,

        /// <summary>
        /// A section containing <see cref="StringGroup"/> instances.
        /// </summary>
        String = 0x000000A0
    }
}
