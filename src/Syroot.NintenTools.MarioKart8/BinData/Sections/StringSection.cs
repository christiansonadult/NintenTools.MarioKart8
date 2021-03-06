using System;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a <see cref="SectionBase"/> storing <see cref="String"/> values.
    /// </summary>
    public class StringSection : SectionBase
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="StringSection"/> class read from the given 
        /// <paramref name="reader"/> with the provided settings.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data with.</param>
        /// <param name="sectionLength">The length of the section in bytes.</param>
        /// <param name="name">The identifier of the section.</param>
        /// <param name="groupCount">The number of groups in this section.</param>
        /// <param name="elementCount">The number of elements in a group.</param>
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
