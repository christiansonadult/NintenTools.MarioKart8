using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRSL, PRSW or PRSA values.
    /// </summary>
    public class SpeedDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("No Coins");
            yield return new TextImagePair("10 Coins");
        }
    }
}
