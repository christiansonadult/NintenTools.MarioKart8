using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a group consisting of byte array elements of a specific structure which is known by the game's code.
    /// </summary>
    public class DwordArrayGroup : GroupBase<Dword[]>
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="DwordArrayGroup"/> class, reading data with the given
        /// parameters.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data from.</param>
        /// <param name="section">The <see cref="SectionBase"/> this group belongs to.</param>
        /// <param name="sectionLength">The size of the section data in bytes.</param>
        internal DwordArrayGroup(BinaryDataReader reader, SectionBase section, int sectionLength)
            : base(reader, section, sectionLength)
        {
            // Compute the element array size.
            int totalElements = section.Capacity * section.ElementCount;
            if (sectionLength % totalElements != 0)
            {
                throw new InvalidDataException("Could not compute element array size without a remainder.");
            }
            int arrayElements = sectionLength / totalElements / sizeof(int);
            // Read the element arrays.
            for (int i = 0; i < section.ElementCount; i++)
            {
                Dword[] dwords = new Dword[arrayElements];
                for (int j = 0; j < arrayElements; j++)
                {
                    dwords[j] = new Dword() { Int32 = reader.ReadInt32() };
                }
                Add(dwords);
            }
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Stores the data of the group with the given <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryDataWriter"/> to write the data with.</param>
        internal override void Save(BinaryDataWriter writer)
        {
            foreach (Dword[] dwordArray in this)
            {
                foreach (Dword dword in dwordArray)
                {
                    writer.Write(dword.Int32);
                }
            }
        }
    }
}
