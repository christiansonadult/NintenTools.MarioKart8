using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a row of buttons of which one can be selected.
    /// </summary>
    public partial class CategoryRow : Control
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private List<string> _categoryTexts;
        private List<bool> _categoryStates;
        private int _selectedCategory;
        private int _hoveredCategory;
        private Color _backColorHovered;
        private Color _backColorSelected;
        private Color _foreColorDisabled;
        private SolidBrush _brBackColor;
        private SolidBrush _brBackColorHovered;
        private SolidBrush _brBackColorSelected;
        private StringFormat _sfText;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRow"/> class.
        /// </summary>
        public CategoryRow(int level, Color accent)
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw,
                true);
            ClientSize = new Size(30, 30);
            Dock = DockStyle.Top;
            Margin = new Padding(0);

            _categoryTexts = new List<string>();
            _categoryStates = new List<bool>();

            int gain = level * 32;
            BackColor = Color.FromArgb(32 + gain, 32 + gain, 32 + gain);
            _backColorHovered = Color.FromArgb(16, 255, 255, 255);
            _backColorSelected = Color.FromArgb(
                Math.Min(255, accent.R + gain),
                Math.Min(255, accent.G + gain),
                Math.Min(255, accent.B + gain));
            ForeColor = Color.White;
            _foreColorDisabled = Color.FromArgb(64, 64, 64);

            _sfText = new StringFormat(StringFormatFlags.NoWrap);
            _sfText.Alignment = StringAlignment.Center;
            _sfText.LineAlignment = StringAlignment.Center;

            CreateBackColorBrush();
            CreateBackColorHoveredBrush();
            CreateBackColorSelectedBrush();
        }

        // ---- EVENTS -------------------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when another category has been selected.
        /// </summary>
        public event EventHandler SelectedCategoryChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                CreateBackColorBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the currently hovered category.
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
                CreateBackColorHoveredBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the currently active category.
        /// </summary>
        public Color HeaderBackColorSelected
        {
            get
            {
                return _backColorSelected;
            }
            set
            {
                _backColorSelected = value;
                CreateBackColorSelectedBrush();
                Refresh();
            }
        }
        
        /// <summary>
        /// Gets or sets the font color of disabled categories.
        /// </summary>
        public Color ForeColorDisabled
        {
            get
            {
                return _foreColorDisabled;
            }
            set
            {
                _foreColorDisabled = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently active category.
        /// </summary>
        public int SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                SelectedCategoryChanged?.Invoke(this, EventArgs.Empty);
                Refresh();
            }
        }

        private int HoveredCategory
        {
            get
            {
                return _hoveredCategory;
            }
            set
            {
                if (_hoveredCategory != value)
                {
                    _hoveredCategory = value;
                    Refresh();
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public void AddCategory(string text, bool enabled = true)
        {
            _categoryTexts.Add(text);
            _categoryStates.Add(enabled);
            Refresh();
        }

        public void EnableCategory(int index, bool enabled)
        {
            _categoryStates[index] = enabled;
            Refresh();
        }

        public void RemoveCategory(int index)
        {
            _categoryTexts.RemoveAt(index);
            _categoryStates.RemoveAt(index);
            Refresh();
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override void OnMouseEnter(EventArgs e)
        {
            HoveredCategory = GetCategoryAt(PointToClient(Cursor.Position));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HoveredCategory = GetCategoryAt(e.Location);
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HoveredCategory = -1;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Must be inside the header.
            if (e.Location.Y >= 0 && e.Location.Y < ClientSize.Height && e.Button == MouseButtons.Left
                && (_hoveredCategory > -1 && _categoryStates[_hoveredCategory]))
            {
                SelectedCategory = _hoveredCategory;
            }
            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float categoryWidth = Width / (float)_categoryTexts.Count;
            float currentX = 0;
            for (int i = 0; i < _categoryTexts.Count; i++)
            {
                string categoryText = _categoryTexts[i];
                bool categoryEnabled = _categoryStates[i];

                Rectangle categoryRect = new Rectangle(
                    (int)Math.Ceiling(currentX), 0, (int)Math.Ceiling(categoryWidth), ClientSize.Height);

                SolidBrush br = i == _selectedCategory ? _brBackColorSelected : _brBackColor;
                e.Graphics.FillRectangle(br, categoryRect);
                if (categoryEnabled && _hoveredCategory == i)
                {
                    e.Graphics.FillRectangle(_brBackColorHovered, categoryRect);
                }

                Color foreColor = categoryEnabled ? ForeColor : ForeColorDisabled;
                //e.Graphics.DrawString(categoryText, Font, brFore, categoryRect, _sfText);
                TextRenderer.DrawText(e.Graphics, categoryText, Font, categoryRect, foreColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                currentX += categoryWidth;
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void CreateBackColorBrush()
        {
            _brBackColor = new SolidBrush(BackColor);
        }

        private void CreateBackColorHoveredBrush()
        {
            _brBackColorHovered = new SolidBrush(BackColorHovered);
        }

        private void CreateBackColorSelectedBrush()
        {
            _brBackColorSelected = new SolidBrush(HeaderBackColorSelected);
        }
        
        private int GetCategoryAt(Point position)
        {
            // Must be inside the header.
            if (position.Y < 0 || position.Y >= ClientSize.Height)
            {
                return -1;
            }

            // Must be on a category.
            int index = position.X / (int)Math.Ceiling(Width / (float)_categoryTexts.Count);
            if (index < 0 || index >= _categoryTexts.Count)
            {
                return -1;
            }
            return index;
        }
    }
}
