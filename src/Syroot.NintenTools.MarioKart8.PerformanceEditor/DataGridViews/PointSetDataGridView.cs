using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointSetDataGridView"/> which has columns for displaying points of a driver, kart, tire
    /// or glider.
    /// </summary>
    public abstract class PointSetDataGridView : IntegerSectionDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="PointSetDataGridView"/> instance.
        /// </summary>
        public PointSetDataGridView()
        {
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            ColumnHeadersHeight = 44;
            RowHeadersDefaultCellStyle.Padding = new Padding(4);
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Weight");
            yield return new TextImagePair("Acceleration");
            yield return new TextImagePair($"On-Road{Environment.NewLine}Traction");
            yield return new TextImagePair($"Off-Road{Environment.NewLine}Traction");
            yield return new TextImagePair("Turbo");
            yield return new TextImagePair($"Speed{Environment.NewLine}Ground");
            yield return new TextImagePair($"Speed{Environment.NewLine}Water");
            yield return new TextImagePair($"Speed{Environment.NewLine}Anti-Gravity");
            yield return new TextImagePair($"Speed{Environment.NewLine}Gliding");
            yield return new TextImagePair($"Handling{Environment.NewLine}Ground");
            yield return new TextImagePair($"Handling{Environment.NewLine}Water");
            yield return new TextImagePair($"Handling{Environment.NewLine}Anti-Gravity");
            yield return new TextImagePair($"Handling{Environment.NewLine}Gliding");
        }

        protected override void FillData()
        {
            for (int y = 0; y < Rows.Count; y++)
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
