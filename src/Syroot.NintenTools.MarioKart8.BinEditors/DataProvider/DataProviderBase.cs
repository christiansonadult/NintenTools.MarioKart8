using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.DataProvider
{
    /// <summary>
    /// Represents a data storage of arbitrary source which can be displayed in a table.
    /// </summary>
    /// <typeparam name="T">The type of the values the data source stores.</typeparam>
    public abstract class DataProviderBase<T>
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets a text displayed at in top left header cell.
        /// </summary>
        protected internal virtual string RowHeaderTitle { get; }

        /// <summary>
        /// Gets or sets the enumeration of <see cref="TextImagePair"/> instances used to display column headers.
        /// </summary>
        protected internal abstract IEnumerable<TextImagePair> Columns { get; }

        /// <summary>
        /// Gets or sets the enumeration of <see cref="TextImagePair"/> instances used to display row headers.
        /// </summary>
        protected internal abstract IEnumerable<TextImagePair> Rows { get; }

        // ---- METHODS (PROTECTED INTERNAL) ---------------------------------------------------------------------------

        /// <summary>
        /// Gets a value for the cell at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <returns>The value for the cell.</returns>
        protected internal abstract T GetValue(int x, int y);

        /// <summary>
        /// Sets the <paramref name="value"/> for the cell at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the cell.</param>
        /// <param name="y">The Y coordinate of the cell.</param>
        /// <param name="value">The value to set.</param>
        protected internal abstract void SetValue(int x, int y, T value);
    }
}
