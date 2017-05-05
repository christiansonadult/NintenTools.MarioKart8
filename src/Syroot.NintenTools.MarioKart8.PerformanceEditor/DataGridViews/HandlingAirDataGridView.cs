namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRTG values.
    /// </summary>
    public class HandlingAirDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public HandlingAirDataGridView()
        {
            AddColumn("Roll");
            AddColumn("Move");
        }
    }
}
