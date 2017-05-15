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
    /// Represents the untyped contents of a *.bin file. Such files consist of several <see cref="SectionBase"/>
    /// instances which consist of a set number of groups (sometimes just 1). Those in turn contain several elements,
    /// all of same structure per section.
    /// </summary>
    /// <remarks>
    /// The meaning of the elements is hardcoded in the game as it simply pulls the untyped data into C structures, with
    /// which it also determines the size of each such element. Thanks to the main header containing the offsets to each
    /// section, the size of the elements can be computed by this class to provide at least untyped byte arrays.
    /// </remarks>
    [DebuggerDisplay(nameof(BinFile) + " {Identifier}, {Count} sections")]
    public class BinFile : IList<SectionBase>, ILoadableFile, ISaveableFile
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------
        
        private const int _sectionHeaderSize = 12;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<SectionBase> _list;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BinFile"/> class from the given stream.
        /// </summary>
        /// <param name="stream">The stream from which the instance will be loaded.</param>
        public BinFile(Stream stream)
        {
            Load(stream);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinFile"/> class from the file with the given name.
        /// </summary>
        /// <param name="fileName">The name of the file from which the instance will be loaded.</param>
        public BinFile(string fileName)
        {
            Load(fileName);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the number of <see cref="SectionBase"/> instances in this file.
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the BIN file is read-only.
        /// </summary>
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
        /// Gets or sets the <see cref="SectionBase"/> with the given <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="SectionBase"/> to get or set.</param>
        /// <returns>The <see cref="SectionBase"/> with the given name.</returns>
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

        /// <summary>
        /// Loads the data from the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to load the data from.</param>
        public void Load(Stream stream)
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

        /// <summary>
        /// Loads the data from the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to load the data from.</param>
        public void Load(string fileName)
        {
            Load(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        /// <summary>
        /// Saves the data into the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to save the data to.</param>
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

        /// <summary>
        /// Saves the data in the given file.
        /// </summary>
        /// <param name="fileName">The name of the file to save the data in.</param>
        public void Save(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                Save(stream);
            }
        }

        /// <summary>
        /// Adds a <see cref="SectionBase"/> to the BIN file.
        /// </summary>
        /// <param name="item">The <see cref="SectionBase"/> to add to the BIN file.</param>
        public void Add(SectionBase item)
        {
            _list.Add(item);
        }

        /// <summary>
        /// Removes all <see cref="SectionBase"/> instances from the BIN file.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Determines whether the BIN file contains a specific <see cref="SectionBase"/>.
        /// </summary>
        /// <param name="item">The <see cref="SectionBase"/> to locate in the BIN file.</param>
        /// <returns><c>true</c> if <see cref="SectionBase"/> is found in the BIN file; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(SectionBase item)
        {
            return _list.Contains(item);
        }

        /// <summary>
        /// Determines whether the BIN file contains a specific <see cref="SectionBase"/> with the given
        /// <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the <see cref="SectionBase"/> to check for existence.</param>
        /// <returns><c>true</c> if <see cref="SectionBase"/> is found in the BIN file; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string name)
        {
            return GetSectionByName(name) != null;
        }

        /// <summary>
        /// Copies the elements of the BIN file to an <see cref="Array"/>, starting at a particular <see cref="Array"/>
        /// index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied
        /// from the BIN file. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(SectionBase[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="SectionBase"/> instances of the BIN file.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<SectionBase> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="SectionBase"/> instances of the BIN file.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
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

        /// <summary>
        /// Inserts a <see cref="SectionBase"/> to the BIN file at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The <see cref="SectionBase"/> to insert into the BIN file.</param>
        public void Insert(int index, SectionBase item)
        {
            _list.Insert(index, item);
        }

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="SectionBase"/> from the BIN file.
        /// </summary>
        /// <param name="item">The <see cref="SectionBase"/> to remove from the BIN file.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the BIN file; otherwise,
        /// <c>false</c>. This method also returns <c>false</c> if <paramref name="item"/> is not found in the original
        /// BIN file.</returns>
        public bool Remove(SectionBase item)
        {
            return _list.Remove(item);
        }

        /// <summary>
        /// Removes the <see cref="SectionBase"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="SectionBase"/> to remove.</param>
        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------
        
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
