using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    public class StringSection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public StringSection(BinaryDataReader reader, int sectionLength, string name, short groupCount,
            short elementCount) : base(reader, sectionLength, name, groupCount, elementCount, SectionType.String)
        {   
            for (int i = 0; i < groupCount; i++)
            {
                Add(new StringGroup(reader, this, sectionLength));
            }
        }
    }
}
