using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    public abstract class BinDataProvider
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected internal abstract DwordArrayGroup DataGroup { get; }

        protected internal virtual bool AllowFloats { get; }
        
        protected internal virtual string RowHeaderTitle { get; }

        protected internal abstract IEnumerable<TextImagePair> Columns { get; }

        protected internal abstract IEnumerable<TextImagePair> Rows { get; }

        // ---- METHODS (PROTECTED INTERNAL) ---------------------------------------------------------------------------

        protected internal virtual Dword GetValue(int x, int y)
        {
            return DataGroup[y][x];
        }

        protected internal virtual void SetValue(int x, int y, Dword value)
        {
            DataGroup[y][x] = value;
        }
    }
}
