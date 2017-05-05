using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointSetDataGridView"/> which has columns for displaying points of a driver, kart, tire
    /// or glider.
    /// </summary>
    public class PointSetDataGridView : IntegerSectionDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public PointSetDataGridView()
        {
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            ColumnHeadersHeight = 44;
            RowHeadersDefaultCellStyle.Padding = new Padding(4);

            AddColumn("Weight");
            AddColumn("Acceleration");
            AddColumn("On-Road Traction");
            AddColumn("Off-Road Traction");
            AddColumn("Turbo");
            AddColumn($"Speed{Environment.NewLine}Ground");
            AddColumn($"Speed{Environment.NewLine}Water");
            AddColumn($"Speed{Environment.NewLine}Anti-Gravity");
            AddColumn($"Speed{Environment.NewLine}Gliding");
            AddColumn($"Handling{Environment.NewLine}Ground");
            AddColumn($"Handling{Environment.NewLine}Water");
            AddColumn($"Handling{Environment.NewLine}Anti-Gravity");
            AddColumn($"Handling{Environment.NewLine}Gliding");

            NameImageValue[] nameImageValues = GetRowNameImageValues();
            Rows.Add(nameImageValues.Length);
            for (int i = 0; i < nameImageValues.Length; i++)
            {
                DataGridViewRow row = Rows[i];
                row.HeaderCell = new NameImageRowHeaderCell(nameImageValues[i]);
            }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected virtual NameImageValue[] GetRowNameImageValues()
        {
            return null;
        }
        
        protected sealed override void FillData()
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

    public class NameImageRowHeaderCell : DataGridViewRowHeaderCell
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private NameImageValue _nameImageValue;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        public NameImageRowHeaderCell()
        {
        }

        internal NameImageRowHeaderCell(NameImageValue nameImageValue)
        {
            _nameImageValue = nameImageValue;
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex,
            Size constraintSize)
        {
            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (_nameImageValue?.Image == null)
            {
                return preferredSize;
            }

            Size textSize = TextRenderer.MeasureText(graphics, _nameImageValue.Name, cellStyle.Font);

            preferredSize.Width = cellStyle.Padding.Horizontal
                + Math.Max(textSize.Width, _nameImageValue.Image.Width / 2);
            preferredSize.Height = (_nameImageValue.Image.Height / 2) + cellStyle.Padding.Vertical
                + cellStyle.Padding.Top + textSize.Height;
            return preferredSize;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (_nameImageValue?.Image == null)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue,
                    errorText, cellStyle, advancedBorderStyle, paintParts);
                return;
            }

            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue,
                errorText, cellStyle, advancedBorderStyle, paintParts);

            Rectangle imageBounds = new Rectangle(
                cellBounds.X + (cellBounds.Width / 2) - (_nameImageValue.Image.Width / 4),
                cellBounds.Y + cellStyle.Padding.Top,
                _nameImageValue.Image.Width / 2,
                _nameImageValue.Image.Height / 2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.DrawImage(_nameImageValue.Image, imageBounds);

            Rectangle textBounds = new Rectangle(
                cellBounds.X,
                imageBounds.Bottom,
                cellBounds.Width,
                cellBounds.Bottom - imageBounds.Bottom);
            TextRenderer.DrawText(graphics, _nameImageValue.Name, cellStyle.Font, textBounds, cellStyle.ForeColor,
                cellStyle.BackColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
