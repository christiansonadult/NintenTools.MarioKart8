namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRTL, PRTW or PRTA values.
    /// </summary>
    public class HandlingDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public HandlingDataGridView()
        {
            AddColumn("Steer");
            AddColumn("Drift");
            AddColumn("Charge");
        }
    }
}
