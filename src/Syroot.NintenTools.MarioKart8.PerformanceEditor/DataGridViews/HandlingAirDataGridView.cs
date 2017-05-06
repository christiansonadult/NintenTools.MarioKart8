using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRTG values.
    /// </summary>
    public class HandlingAirDataGridView : PointRankFloatDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Roll");
            yield return new TextImagePair("Move");
        }
    }
}
