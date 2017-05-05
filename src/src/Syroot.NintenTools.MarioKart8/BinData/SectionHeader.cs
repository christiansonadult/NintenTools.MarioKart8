using System.Diagnostics;
using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents the header of any <see cref="Section"/> in a <see cref="BinFile"/>, providing the required
    /// information to load it into memory.
    /// </summary>
    [DebuggerDisplay("Name={Name}")]
    public struct SectionHeader
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// The size of the header in bytes.
        /// </summary>
        internal const int SizeInBytes = 12;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets a 4 character long ASCII section identifier. It basically determines the type of the entries.
        /// </summary>
        [BinaryMember(StringFormat = BinaryStringFormat.NoPrefixOrTermination, Length = 4)]
        public string Name { get; set; }

        /// <summary>
        /// Gets the (in this class non-enforced) number of elements each group must have. This number is not enforced
        /// in the API, but must be respected to create valid files.
        /// </summary>
        public short ElementCount { get; set; }

        /// <summary>
        /// Gets or sets the number of groups this section must have. This number is not enforced in the API, but must
        /// be respected to create valid files.
        /// </summary>
        public short GroupCount { get; set; }

        /// <summary>
        /// Gets or sets a value determining the section data type.
        /// </summary>
        public SectionType SectionType { get; set; }
    }
}
