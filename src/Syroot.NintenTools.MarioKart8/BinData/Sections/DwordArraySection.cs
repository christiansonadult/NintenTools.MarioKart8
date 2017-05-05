using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    public class DwordArraySection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public DwordArraySection(BinaryDataReader reader, int sectionLength, string name, short groupCount,
            short elementCount) : base(reader, sectionLength, name, groupCount, elementCount, SectionType.DwordArray)
        {
            for (int i = 0; i < groupCount; i++)
            {
                Add(new DwordArrayGroup(reader, this, sectionLength));
            }
        }
    }
}
