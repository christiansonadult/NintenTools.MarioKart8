namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRAC values.
    /// </summary>
    public class PhysicsAccelerationDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PhysicsAccelerationDataGridView()
        {
            AddColumn("Limiter");
            AddColumn("Strength");
        }
    }
}
