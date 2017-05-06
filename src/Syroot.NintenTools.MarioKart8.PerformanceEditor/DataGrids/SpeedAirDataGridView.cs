using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRSG values.
    /// </summary>
    public class SpeedAirDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Any Coins");
        }
    }
}
