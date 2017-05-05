namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PRSL, PRSW or PRSA values.
    /// </summary>
    public class SpeedDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public SpeedDataGridView()
        {
            AddColumn("No Coins");
            AddColumn("10 Coins");
        }
    }
}
