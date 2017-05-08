using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    public abstract class BinDataProvider
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected internal abstract DwordArrayGroup DataGroup { get; }

        protected internal virtual bool AllowFloats { get; }

        protected internal virtual int FirstColumn { get; }

        protected internal abstract IEnumerable<TextImagePair> Columns { get; }

        protected internal abstract IEnumerable<TextImagePair> Rows { get; }
    }
}
