using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="IntegerSectionDataGridView"/> which has 21 rows to display the values for each point
    /// rank.
    /// </summary>
    public abstract class PointRankIntegerDataGridView : IntegerSectionDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PointRankIntegerDataGridView()
        {
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override void FillData()
        {
            AddPointRankRows();
            for (int y = 0; y < DataGroup.Count; y++)
            {
                Dword[] dwords = DataGroup[y];
                for (int x = 0; x < dwords.Length; x++)
                {
                    Rows[y].Cells[x].Value = dwords[x].Int32;
                }
            }
        }

        protected override void SetDataValue(int row, int column, int value)
        {
            DataGroup[row][column] = value;
        }

        protected void AddPointRankRows()
        {
            // Add rows for the valid point ranks and one for the fallback value.
            Rows.Clear();
            for (int i = 0; i < DataGroup.Count; i++)
            {
                Rows.Add();
                if (i == DataGroup.Count - 1)
                {
                    Rows[i].HeaderCell.Value = "Fallback";
                }
                else
                {
                    Rows[i].HeaderCell.Value = $"{i + 1} Point{(i == 0 ? null : "s")}";
                }
            }
        }
    }
}
