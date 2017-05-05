using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a group consisting of string elements.
    /// </summary>
    public class StringGroup : GroupBase<string>
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="StringGroup"/> class, reading data with the given parameters.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data from.</param>
        /// <param name="section">The <see cref="SectionBase"/> this group belongs to.</param>
        /// <param name="sectionLength">The size of the section data in bytes.</param>
        internal StringGroup(BinaryDataReader reader, SectionBase section, int sectionLength)
            : base(reader, section, sectionLength)
        {
            // Get the offsets and read the strings at them.
            int[] offsets = reader.ReadInt32s(section.ElementCount);
            long stringArrayOffset = reader.Position;
            for (int i = 0; i < offsets.Length; i++)
            {
                reader.Seek(stringArrayOffset + offsets[i], SeekOrigin.Begin);
                Add(reader.ReadString(BinaryStringFormat.ZeroTerminated));
            }
            reader.Align(4);
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Stores the data of the group with the given <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryDataWriter"/> to write the data with.</param>
        internal override void Save(BinaryDataWriter writer)
        {
            // Write the offsets to the strings.
            int offset = 0;
            foreach (string element in this)
            {
                writer.Write(offset);
                offset += element.Length + 1;
            }
            // Write the strings.
            foreach (string element in this)
            {
                writer.Write(element, BinaryStringFormat.ZeroTerminated);
            }
        }
    }
}
