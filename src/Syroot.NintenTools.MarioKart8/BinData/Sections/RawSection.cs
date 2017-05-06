using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a <see cref="SectionBase"/> of unknown format, thus storing only the raw data in its
    /// <see cref="Data"/> property, and no groups.
    /// </summary>
    public class RawSection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="RawSection"/> class read from the given
        /// <paramref name="reader"/> with the provided settings.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data with.</param>
        /// <param name="sectionLength">The length of the section in bytes.</param>
        /// <param name="name">The identifier of the section.</param>
        /// <param name="groupCount">The number of groups in this section.</param>
        /// <param name="elementCount">The number of elements in a group.</param>
        /// <param name="sectionType">The <see cref="SectionType"/> identifier of the section.</param>
        public RawSection(BinaryDataReader reader, int sectionLength, string name, short groupCount, short elementCount,
            SectionType sectionType) : base(reader, sectionLength, name, groupCount, elementCount, sectionType)
        {
            Data = reader.ReadBytes(sectionLength);
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the raw data of the seection. No groups were loaded.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
