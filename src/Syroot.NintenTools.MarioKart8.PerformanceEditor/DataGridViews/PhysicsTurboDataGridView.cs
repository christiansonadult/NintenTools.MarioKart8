namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankIntegerDataGridView"/> displaying PRMT values.
    /// </summary>
    public class PhysicsTurboDataGridView : PointRankIntegerDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PhysicsTurboDataGridView()
        {
            AddColumn("Mini-Turbo");
            AddColumn("Super-Turbo");
        }
    }
}
