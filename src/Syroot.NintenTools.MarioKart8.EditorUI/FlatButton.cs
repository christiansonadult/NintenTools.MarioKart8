using System;
using System.Drawing;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a button in a flat design.
    /// </summary>
    public class FlatButton : Control
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private Color _backColorHovered;
        private bool _isHovered;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatButton"/> class displaying the given
        /// <paramref name="text"/>  and attaching to the provided <paramref name="clickEventHandler"/>.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="clickEventHandler">The click <see cref="EventHandler"/> to attach to.</param>
        public FlatButton(string text, EventHandler clickEventHandler)
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw,
                true);
            _backColorHovered = Color.FromArgb(76, 76, 76);
            BackColor = Color.FromArgb(64, 64, 64);
            Dock = DockStyle.Fill;
            ForeColor = Color.White;
            Margin = new Padding(0);
            Text = text;
            Click += clickEventHandler;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the background color when hovered with the mouse.
        /// </summary>
        public Color BackColorHovered
        {
            get
            {
                return _backColorHovered;
            }
            set
            {
                _backColorHovered = value;
                Refresh();
            }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when the mouse pointer enters the control bounds.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Refresh();
        }

        /// <summary>
        /// Raised when the mouse pointer leaves the control bounds.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            Refresh();
        }

        /// <summary>
        /// Raised when the control has to paint its contents.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/>.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Color backColor = _isHovered ? _backColorHovered : BackColor;
            e.Graphics.Clear(backColor);

            TextRenderer.DrawText(e.Graphics, Text, Font, new Rectangle(Point.Empty, ClientSize), ForeColor,
                backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
