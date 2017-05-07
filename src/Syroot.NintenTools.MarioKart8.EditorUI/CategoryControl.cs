using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents tab-like control with an array of entries at the top and changing content depending on the selected
    /// control.
    /// </summary>
    [ProvideProperty("Title", typeof(Control))]
    public partial class CategoryControl : ContainerControl
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private ControlLayoutEngine _layoutEngine;
        private Dictionary<Control, string> _controlTitles;
        private Control _selectedControl;
        private Control _hoveredControl;
        private int _headerHeight;
        private Color _headerBackColor;
        private Color _headerBackColorHovered;
        private Color _headerBackColorSelected;
        private Color _headerForeColor;
        private Color _headerForeColorDisabled;
        private SolidBrush _brHeaderBackColor;
        private SolidBrush _brHeaderHoveredBackColor;
        private SolidBrush _brHeaderSelectedBackColor;
        private SolidBrush _brHeaderForeColor;
        private SolidBrush _brHeaderDisabledForeColor;
        private StringFormat _sfText;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryControl"/> class.
        /// </summary>
        public CategoryControl(int level, Color accent)
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw,
                true);
            Margin = new Padding(0);
            Dock = DockStyle.Fill;

            _controlTitles = new Dictionary<Control, string>();
            _headerHeight = 30;

            int gain = level * 32;
            _headerBackColor = Color.FromArgb(32 + gain, 32 + gain, 32 + gain);
            _headerBackColorHovered = Color.FromArgb(16, 255, 255, 255);
            _headerBackColorSelected = Color.FromArgb(accent.R + gain, accent.G + gain, accent.B + gain);
            _headerForeColor = Color.White;
            _headerForeColorDisabled = Color.FromArgb(64, 64, 64);

            _sfText = new StringFormat(StringFormatFlags.NoWrap)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            CreateHeaderBackColorBrush();
            CreateHeaderHoveredBackColorBrush();
            CreateHeaderSelectedBackColorBrush();
            CreateHeaderForeColorBrush();
            CreateHeaderDisabledForeColorBrush();
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the height of the header in pixels.
        /// </summary>
        public int HeaderHeight
        {
            get
            {
                return _headerHeight;
            }
            set
            {
                _headerHeight = value;
                Refresh();
                PerformLayout();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the header.
        /// </summary>
        public Color HeaderBackColor
        {
            get
            {
                return _headerBackColor;
            }
            set
            {
                _headerBackColor = value;
                CreateHeaderBackColorBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the currently hovered control area.
        /// </summary>
        public Color HeaderBackColorHovered
        {
            get
            {
                return _headerBackColorHovered;
            }
            set
            {
                _headerBackColorHovered = value;
                CreateHeaderHoveredBackColorBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the background color of the control area of the currently selected control.
        /// </summary>
        public Color HeaderBackColorSelected
        {
            get
            {
                return _headerBackColorSelected;
            }
            set
            {
                _headerBackColorSelected = value;
                CreateHeaderSelectedBackColorBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the font color of enabled controls.
        /// </summary>
        public Color HeaderForeColor
        {
            get
            {
                return _headerForeColor;
            }
            set
            {
                _headerForeColor = value;
                CreateHeaderForeColorBrush();
                Refresh();
            }
        }
        
        /// <summary>
        /// Gets or sets the font color of disabled controls.
        /// </summary>
        public Color HeaderForeColorDisabled
        {
            get
            {
                return _headerForeColorDisabled;
            }
            set
            {
                _headerForeColorDisabled = value;
                CreateHeaderDisabledForeColorBrush();
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the currently active and displayed <see cref="Control"/>.
        /// </summary>
        public Control SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                _selectedControl = value;
                Refresh();
                PerformLayout();
            }
        }

        /// <summary>
        /// Gets the <see cref="LayoutEngine"/> used to layout controls in this container.
        /// </summary>
        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (DesignMode)
                {
                    return base.LayoutEngine;
                }
                else
                {
                    if (_layoutEngine == null)
                    {
                        _layoutEngine = new ControlLayoutEngine();
                    }
                    return _layoutEngine;
                }
            }
        }
        
        private Control HoveredControl
        {
            get
            {
                return _hoveredControl;
            }
            set
            {
                if (_hoveredControl != value)
                {
                    _hoveredControl = value;
                    Refresh();
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets the title property for the extended control.
        /// </summary>
        /// <param name="control">The control to retrieve the title property for.</param>
        /// <returns>The title for the control or <c>null</c> if it has not set a title yet.</returns>
        public string GetTitle(Control control)
        {
            if (_controlTitles.TryGetValue(control, out string title))
            {
                return title;
            }
            return null;
        }

        /// <summary>
        /// Sets the title property for the extended control.
        /// </summary>
        /// <param name="control">The control to remember the title property for.</param>
        /// <param name="value">The title to set.</param>
        public void SetTitle(Control control, string value)
        {
            _controlTitles[control] = value;
            Refresh();
        }
        
        /// <summary>
        /// Resets the title property for the given control.
        /// </summary>
        /// <param name="control">The control which title will be reset.</param>
        private void ResetTitle(Control control)
        {
            _controlTitles.Remove(control);
            Refresh();
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            e.Control.EnabledChanged += Control_EnabledChanged;
            if (SelectedControl == null)
            {
                SelectedControl = e.Control;
            }
            else
            {
                Refresh();
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            e.Control.EnabledChanged -= Control_EnabledChanged;
            if (SelectedControl == e.Control)
            {
                SelectedControl = null;
            }
            else
            {
                Refresh();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            HoveredControl = GetCategoryAt(PointToClient(Cursor.Position));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            HoveredControl = GetCategoryAt(e.Location);
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            HoveredControl = null;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Must be inside the header.
            if (e.Location.Y >= 0 && e.Location.Y < HeaderHeight && e.Button == MouseButtons.Left
                && HoveredControl?.Enabled == true)
            {
                SelectedControl = HoveredControl;
            }

            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            float buttonWidth = Width / (float)Controls.Count;
            float currentX = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                Rectangle buttonRect = new Rectangle(
                    (int)Math.Ceiling(currentX), 0, (int)Math.Ceiling(buttonWidth), HeaderHeight);

                SolidBrush br = control == SelectedControl ? _brHeaderSelectedBackColor : _brHeaderBackColor;
                e.Graphics.FillRectangle(br, buttonRect);
                if (control.Enabled && HoveredControl == control)
                {
                    e.Graphics.FillRectangle(_brHeaderHoveredBackColor, buttonRect);
                }

                SolidBrush brFore = control.Enabled ? _brHeaderForeColor : _brHeaderDisabledForeColor;
                string text = GetTitle(control) ?? control.Text ?? control.Name;
                e.Graphics.DrawString(text, Font, brFore, buttonRect, _sfText);

                currentX += buttonWidth;
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void CreateHeaderBackColorBrush()
        {
            _brHeaderBackColor = new SolidBrush(HeaderBackColor);
        }

        private void CreateHeaderHoveredBackColorBrush()
        {
            _brHeaderHoveredBackColor = new SolidBrush(HeaderBackColorHovered);
        }

        private void CreateHeaderSelectedBackColorBrush()
        {
            _brHeaderSelectedBackColor = new SolidBrush(HeaderBackColorSelected);
        }

        private void CreateHeaderForeColorBrush()
        {
            _brHeaderForeColor = new SolidBrush(HeaderForeColor);
        }

        private void CreateHeaderDisabledForeColorBrush()
        {
            _brHeaderDisabledForeColor = new SolidBrush(HeaderForeColorDisabled);
        }

        private Control GetCategoryAt(Point position)
        {
            // Must be inside the header.
            if (position.Y < 0 || position.Y >= HeaderHeight)
            {
                return null;
            }

            // Must be on a category button.
            int index = position.X / (int)Math.Ceiling(Width / (float)Controls.Count);
            if (index < 0 || index >= Controls.Count)
            {
                return null;
            }
            return Controls[index];
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void Control_EnabledChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        // ---- CLASSES ------------------------------------------------------------------------------------------------

        private class ControlLayoutEngine : LayoutEngine
        {
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                CategoryControl parent = (CategoryControl)container;

                foreach (Control control in parent.Controls)
                {
                    if (parent.SelectedControl == control)
                    {
                        control.Location = new Point(
                            parent.Padding.Left + control.Margin.Left,
                            parent.Padding.Top + parent.HeaderHeight + control.Margin.Top);
                        control.Size = new Size(
                            parent.Width - parent.Padding.Horizontal - control.Margin.Horizontal,
                            parent.Height - parent.Padding.Vertical - parent.HeaderHeight - control.Margin.Vertical);
                        control.Visible = true;
                    }
                    else
                    {
                        control.Visible = false;
                    }
                }

                return false;
            }
        }
    }
}
