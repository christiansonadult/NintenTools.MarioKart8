using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="FloatSectionDataGridView"/> which has several rows to display the values for each point
    /// rank.
    /// </summary>
    public abstract class PointRankFloatDataGridView : FloatSectionDataGridView
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
                    Rows[y].Cells[x].Value = dwords[x].Single;
                }
            }
        }

        protected override void SetDataValue(int row, int column, float value)
        {
            DataGroup[row][column] = value;
        }
    }
}
