using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    public class RawSection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

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
