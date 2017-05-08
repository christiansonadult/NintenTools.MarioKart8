using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRTL, PRTW or PRTA values.
    /// </summary>
    public class HandlingDataGridView : PointRankFloatDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------
        
        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Steer");
            yield return new TextImagePair("Drift");
            yield return new TextImagePair("Charge");
        }
    }
}
