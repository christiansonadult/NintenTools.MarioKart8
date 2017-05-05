namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRWG values.
    /// </summary>
    public class PhysicsWeightDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PhysicsWeightDataGridView()
        {
            AddColumn("Low");
            AddColumn("High");
            AddColumn("Unknown");
        }
    }
}
