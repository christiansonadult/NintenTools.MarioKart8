using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a <see cref="SectionBase"/> storing arrays with <see cref="Dword"/> elements.
    /// </summary>
    public class DwordArraySection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="DwordArraySection"/> class read from the given
        /// <paramref name="reader"/> with the provided settings.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data with.</param>
        /// <param name="sectionLength">The length of the section in bytes.</param>
        /// <param name="name">The identifier of the section.</param>
        /// <param name="groupCount">The number of groups in this section.</param>
        /// <param name="elementCount">The number of elements in a group.</param>
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
