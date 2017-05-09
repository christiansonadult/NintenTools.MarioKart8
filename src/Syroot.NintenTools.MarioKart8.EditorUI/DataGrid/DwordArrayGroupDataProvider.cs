using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a data storage storing <see cref="Dword"/> values retrieved from a <see cref="DwordArrayGroup"/>
    /// which can be displayed in a table.
    /// </summary>
    public abstract class DwordArrayGroupDataProvider : DwordDataProvider
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the <see cref="DwordArrayGroup"/> from which data is retrieved and stored.
        /// </summary>
        protected internal abstract DwordArrayGroup DataGroup { get; }

        // ---- METHODS (PROTECTED INTERNAL) ---------------------------------------------------------------------------

        /// <summary>
        /// Gets a value for the cell at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <returns>The value for the cell.</returns>
        protected internal override Dword GetValue(int x, int y)
        {
            return DataGroup[y][x];
        }

        /// <summary>
        /// Sets the <paramref name="value"/> for the cell at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <param name="value">The value to set.</param>
        protected internal override void SetValue(int x, int y, Dword value)
        {
            DataGroup[y][x] = value;
        }
    }
}
