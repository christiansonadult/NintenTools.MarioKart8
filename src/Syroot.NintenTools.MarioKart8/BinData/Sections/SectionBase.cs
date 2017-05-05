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
        /// Initializes a new instance of the <see cref="Section"/> class, reading groups and elements from the
        /// given <paramref name="reader"/> and having the provided size in bytes.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the section from.</param>
        /// <param name="header">The <see cref="SectionHeader"/> providing information about this section.</param>
        /// <param name="sectionLength">The size of the section data in bytes.</param>
        internal SectionBase(BinaryDataReader reader, int sectionLength, string name, short groupCount,
            short elementCount, SectionType sectionType)
        {
            Name = name;
            _list = new List<GroupBase>(groupCount);
            ElementCount = elementCount;
            SectionType = sectionType;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

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

        public int Count
        {
            get { return _list.Count; }
        }

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

        public void Add(GroupBase item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(GroupBase item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(GroupBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GroupBase> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        public int IndexOf(GroupBase item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, GroupBase item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(GroupBase item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

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
    /// Represents the known <see cref="Section"/> types.
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
