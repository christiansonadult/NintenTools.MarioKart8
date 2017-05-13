using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.BinEditors.DataProvider
{
    /// <summary>
    /// Represents a data storage storing <see cref="Dword"/> values which can be displayed in a table.
    /// </summary>
    public abstract class DwordDataProvider : DataProviderBase<Dword>
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets a value whether floating point values are retrieved and stored or integer values.
        /// </summary>
        protected internal virtual bool UseFloats { get; }
    }
}
