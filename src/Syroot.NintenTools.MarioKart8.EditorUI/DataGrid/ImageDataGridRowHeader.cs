using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a <see cref="DataGridViewRowHeaderCell"/> which can display an image together with text.
    /// </summary>
    internal class ImageDataGridRowHeader : DataGridViewRowHeaderCell
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _width = 110;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private string _text;
        private Image _image;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageDataGridRowHeader"/> displaying the given
        /// <paramref name="text"/> and <paramref name="image"/>.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="image">The <see cref="Image"/> to display, or <c>null</c> to only display text.</param>
        internal ImageDataGridRowHeader(string text, Image image)
        {
            _text = text;
            _image = image;
        }
        
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex,
            Size constraintSize)
        {
            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            Size textSize = TextRenderer.MeasureText(graphics, _text, cellStyle.Font);
            Size imageSize = _image == null ? Size.Empty : _image.Size;
            
            // Fix width of headers for now, as it looks best when switching between tables.
            //preferredSize.Width = cellStyle.Padding.Horizontal
            //    + Math.Max(textSize.Width, imageSize.Width / 2);
            preferredSize.Width = _width;
            preferredSize.Height = (imageSize.Height / 2) + cellStyle.Padding.Vertical
                + cellStyle.Padding.Top + textSize.Height;
            
            return preferredSize;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // Draw the background.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue,
                errorText, cellStyle, advancedBorderStyle, paintParts);

            // Draw the image.
            Rectangle imageBounds;
            if (_image == null)
            {
                imageBounds = new Rectangle(
                    cellBounds.X + (cellBounds.Width / 2),
                    cellBounds.Y + cellStyle.Padding.Top,
                    0,
                    0);
            }
            else
            {
                imageBounds = new Rectangle(
                    cellBounds.X + (cellBounds.Width / 2) - (_image.Width / 4),
                    cellBounds.Y + cellStyle.Padding.Top,
                    _image.Width / 2,
                    _image.Height / 2);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.DrawImage(_image, imageBounds);
            }

            // Draw the text.
            Rectangle textBounds = new Rectangle(
                cellBounds.X,
                imageBounds.Bottom,
                cellBounds.Width,
                cellBounds.Bottom - imageBounds.Bottom);
            TextRenderer.DrawText(graphics, _text, cellStyle.Font, textBounds, cellStyle.ForeColor, cellStyle.BackColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
