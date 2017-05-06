using System.Collections.Generic;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRAC values.
    /// </summary>
    public class PhysicsAccelerationDataGridView : PointRankFloatDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Limiter");
            yield return new TextImagePair("Strength");
        }
    }
}
