using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankIntegerDataGridView"/> displaying PRMT values.
    /// </summary>
    public class PhysicsTurboDataGridView : PointRankIntegerDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Mini-Turbo");
            yield return new TextImagePair("Super-Turbo");
            if (Program.IsMarioKart8Deluxe)
            {
                yield return new TextImagePair("Ultra-Turbo");
            }
        }
    }
}
