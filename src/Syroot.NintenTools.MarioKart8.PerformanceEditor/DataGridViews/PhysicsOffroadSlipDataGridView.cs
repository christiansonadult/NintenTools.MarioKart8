using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PROF values.
    /// </summary>
    public class PhysicsOffroadSlipDataGridView : PointRankFloatDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PhysicsOffroadSlipDataGridView()
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
            // Only show the second half holding the slip stats.
            AddPointRankRows();
            for (int y = 0; y < DataGroup.Count; y++)
            {
                Dword[] dwords = DataGroup[y];
                for (int x = 9; x < dwords.Length; x++)
                {
                    Rows[y].Cells[x - 9].Value = dwords[x].Single;
                }
            }
        }

        protected override void SetDataValue(int row, int column, float value)
        {
            // Set the second half holding the slip stats.
            DataGroup[row][column + 9] = value;
        }
    }
}
