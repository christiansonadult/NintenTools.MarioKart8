using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.BinEditors.UI
{
    /// <summary>
    /// Represents a <see cref="DataGridViewColumnHeaderCell"/> which can display an image together with text.
    /// </summary>
    internal class ImageDataGridColumnHeader : DataGridViewColumnHeaderCell
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private string _text;
        private Image _image;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageDataGridColumnHeader"/> displaying the given
        /// <paramref name="text"/> and <paramref name="image"/>.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="image">The <see cref="Image"/> to display, or <c>null</c> to only display text.</param>
        internal ImageDataGridColumnHeader(string text, Image image)
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

            // Fix height of headers for now to limit the size of images.
            preferredSize.Width = cellStyle.Padding.Horizontal + Math.Max(textSize.Width, imageSize.Width / 2);
            preferredSize.Height = cellStyle.Padding.Vertical + textSize.Height;
            if (_image != null)
            {
                preferredSize.Height += imageSize.Height / 4 + cellStyle.Padding.Top;
            }

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
                    cellBounds.Y,
                    0,
                    0);
            }
            else
            {
                imageBounds = new Rectangle(
                    cellBounds.X + (cellBounds.Width / 2) - (_image.Width / 8),
                    cellBounds.Y + cellStyle.Padding.Top,
                    _image.Width / 4,
                    _image.Height / 4);
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
