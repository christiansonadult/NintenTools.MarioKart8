using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents the untyped contents of a *.bin file. Such files consist of several <see cref="Section"/>
    /// instances which consist of a set number of groups (sometimes just 1). Those in turn contain several elements,
    /// all of same structure per section.
    /// </summary>
    /// <remarks>
    /// The meaning of the elements is hardcoded in the game as it simply pulls the untyped data into C structures, with
    /// which it also determines the size of each such element. Thanks to the main header containing the offsets to each
    /// section, the size of the elements can be computed by this class to provide at least untyped byte arrays.
    /// </remarks>
    [DebuggerDisplay(nameof(BinFile) + " {Identifier}, {Count} sections")]
    public class BinFile : IList<SectionBase>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        internal const int _sectionHeaderSize = 12;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<SectionBase> _list;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BinFile"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        public BinFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(stream);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinFile"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        public BinFile(Stream stream)
        {
            Load(stream);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the number of <see cref="SectionBase"/> instances in this file.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets the endianness in which data was read or will be saved.
        /// </summary>
        public ByteOrder ByteOrder { get; set; }

        /// <summary>
        /// Gets or sets a 4 character long ASCII file identifier.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets an unknown value.
        /// </summary>
        public short Unknown { get; set; }

        /// <summary>
        /// Gets or sets the version of the file format. Only 1000 is known.
        /// </summary>
        public int Version { get; set; }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the <see cref="SectionBase"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="SectionBase"/> to get or set.</param>
        /// <returns>The <see cref="SectionBase"/> at the specified index.</returns>
        public SectionBase this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="Section"/> with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="Section"/> to get or set.</param>
        /// <returns>The <see cref="Section"/> with the given name.</returns>
        public SectionBase this[string name]
        {
            get
            {
                SectionBase section = GetSectionByName(name);
                if (section == null)
                {
                    throw new IndexOutOfRangeException(nameof(name));
                }
                return section;
            }
            set
            {
                int index = GetSectionIndexByName(name);
                if (index == -1)
                {
                    throw new IndexOutOfRangeException(nameof(name));
                }
                this[index] = value;
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public void Add(SectionBase item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(SectionBase item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Gets a value indicating whether a <see cref="Section"/> with the given <paramref name="name"/> exists.
        /// </summary>
        /// <param name="name">The name of the <see cref="Section"/> to check for existence.</param>
        /// <returns><c>true</c> if the <see cref="Section"/> exists, otherwise <c>false</c>.</returns>
        public bool Contains(string name)
        {
            return GetSectionByName(name) != null;
        }

        public void CopyTo(SectionBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SectionBase> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        
        /// <summary>
        /// Determines the index of a specific <see cref="SectionBase"/> in the file.
        /// </summary>
        /// <param name="item">The <see cref="SectionBase"/> to locate in the file.</param>
        /// <returns>The index of <see cref="SectionBase"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(SectionBase item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, SectionBase item)
        {
            _list.Insert(index, item);
        }

        public bool Remove(SectionBase item)
        {
            return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        /// <summary>
        /// Saves the data into the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file in which the data will be stored.</param>
        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Saves the data into the the given stream.
        /// </summary>
        /// <param name="stream">The stream in which the data will be stored.</param>
        public void Save(Stream stream)
        {
            using (BinaryDataWriter writer = new BinaryDataWriter(stream, Encoding.ASCII, true))
            {
                writer.ByteOrder = ByteOrder;

                // Write the file header.
                writer.Write(Identifier, BinaryStringFormat.NoPrefixOrTermination);
                Offset fileSizeOffset = writer.ReserveOffset();
                writer.Write((short)Count);
                writer.Write(Unknown);
                writer.Write(Version);
                Offset[] sectionOffsets = writer.ReserveOffset(Count);
                int headerLength = (int)writer.Position;

                // Write all the sections.
                int sectionIndex = 0;
                foreach (SectionBase section in this)
                {
                    // Fill in the offset to this section.
                    sectionOffsets[sectionIndex].Satisfy((int)writer.Position - headerLength);

                    // Write the section header.
                    writer.Write(section.Name, BinaryStringFormat.NoPrefixOrTermination);
                    writer.Write(section.ElementCount);
                    writer.Write((short)section.Count);
                    writer.Write(section.SectionType, false);

                    // Write the section groups, each of them is 4-byte aligned.
                    foreach (GroupBase group in section)
                    {
                        group.Save(writer);
                        writer.Align(4);
                    }

                    sectionIndex++;
                }
                
                fileSizeOffset.Satisfy();
            }
        }
        
        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void Load(Stream stream)
        {
            using (BinaryDataReader reader = new BinaryDataReader(stream, Encoding.ASCII, true))
            {
                // Detect the byte order with the version number.
                using (reader.TemporarySeek(12))
                {
                    int version = reader.ReadInt32();
                    ByteOrder = version == 0x000003E8 ? ByteOrder.LittleEndian : ByteOrder.BigEndian;
                }
                reader.ByteOrder = ByteOrder;

                // Read the file header providing information about the section count and offsets.
                Identifier = reader.ReadString(4);
                int fileSize = reader.ReadInt32(); // Slightly off at times. Not stored in the class.
                int sectionCount = reader.ReadInt16();
                Unknown = reader.ReadInt16();
                Version = reader.ReadInt32();
                int[] sectionOffsets = reader.ReadInt32s(sectionCount);

                // Read in all the sections.
                _list = new List<SectionBase>();
                int fileHeaderSize = (int)reader.Position;
                for (int i = 0; i < sectionCount; i++)
                {
                    // Compute the length of the section data (without its header) and instantiate it.
                    int dataStart = fileHeaderSize + sectionOffsets[i] + _sectionHeaderSize;
                    int dataEnd;
                    if (i < sectionCount - 1)
                    {
                        dataEnd = fileHeaderSize + sectionOffsets[i + 1];
                    }
                    else
                    {
                        dataEnd = (int)reader.Length;
                    }
                    int sectionLength = dataEnd - dataStart;
                    Add(SectionBase.Create(reader, sectionLength));
                }
            }
        }
        
        private SectionBase GetSectionByName(string name)
        {
            foreach (SectionBase section in this)
            {
                if (section.Name == name)
                {
                    return section;
                }
            }
            return null;
        }

        private int GetSectionIndexByName(string name)
        {
            int i = 0;
            foreach (SectionBase section in this)
            {
                if (section.Name == name)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}
