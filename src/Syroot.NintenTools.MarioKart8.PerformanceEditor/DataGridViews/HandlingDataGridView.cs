using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
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
