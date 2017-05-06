using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
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
        }
    }
}
