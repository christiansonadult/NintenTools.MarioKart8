using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents a <see cref="IntegerSectionDataGridView"/> which has 21 rows to display the values for each point
    /// rank.
    /// </summary>
    public abstract class PointRankIntegerDataGridView : IntegerSectionDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            if (Program.IsMarioKart8Deluxe)
            {
                // Valid point ranks.
                for (int i = 0; i < DataGroup.Count; i++)
                {
                    yield return new TextImagePair($"{i} Point{(i == 1 ? null : "s")}");
                }
            }
            else
            {
                // Valid point ranks and one for the fallback value.
                for (int i = 0; i < DataGroup.Count; i++)
                {
                    if (i == DataGroup.Count - 1)
                    {
                        yield return new TextImagePair("Fallback");
                    }
                    else
                    {
                        yield return new TextImagePair($"{i + 1} Point{(i == 0 ? null : "s")}");
                    }
                }
            }
        }

        protected override void FillData()
        {
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
    }
}
