using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PROF values.
    /// </summary>
    public class PhysicsOffroadBrakeDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PhysicsOffroadBrakeDataGridView()
        {
            AddColumn("Light Dirt");
            AddColumn("Medium Dirt");
            AddColumn("Heavy Dirt");
            AddColumn("Light Sand");
            AddColumn("Medium Sand");
            AddColumn("Heavy Sand");
            AddColumn("Light Ice");
            AddColumn("Medium Ice");
            AddColumn("Heavy Ice");
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override void FillData()
        {
            // Only show the first half holding the brake stats.
            AddPointRankRows();
            for (int y = 0; y < DataGroup.Count; y++)
            {
                Dword[] dwords = DataGroup[y];
                for (int x = 0; x < 9; x++)
                {
                    Rows[y].Cells[x].Value = dwords[x].Single;
                }
            }
        }
    }
}
