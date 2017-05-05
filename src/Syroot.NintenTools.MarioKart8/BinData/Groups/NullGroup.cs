using Syroot.BinaryData;

namespace Syroot.NintenTools.MarioKart8.BinData
{
    /// <summary>
    /// Represents a dummy group not holding any data.
    /// </summary>
    public class NullGroup : GroupBase<string>
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="NullGroup"/> class, reading data with the given parameters.
        /// </summary>
        /// <param name="reader">The <see cref="BinaryDataReader"/> to read the data from.</param>
        /// <param name="section">The <see cref="SectionBase"/> this group belongs to.</param>
        /// <param name="sectionLength">The size of the section data in bytes.</param>
        internal NullGroup(BinaryDataReader reader, SectionBase section, int sectionLength)
            : base(reader, section, sectionLength)
        {
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Stores the data of the group with the given <paramref name="writer"/>.
        /// </summary>
        /// <param name="writer">The <see cref="BinaryDataWriter"/> to write the data with.</param>
        internal override void Save(BinaryDataWriter writer)
        {
        }
    }
}
