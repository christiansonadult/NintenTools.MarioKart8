using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinEditors.DataProvider;

namespace Syroot.NintenTools.MarioKart8.BinEditors.UI
{
    /// <summary>
    /// Represents a <see cref="DwordArrayGroup"/> of a <see cref="BinFile"/> in a tabular layout.
    /// </summary>
    public class BinDataGrid : DataGridView
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private DwordDataProvider _dataProvider;
        private int _minimumColumnWidth;
        private bool _isRefillingData;
        private bool _isSizingColumns;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="BinDataGrid"/> class.
        /// </summary>
        public BinDataGrid()
        {
            InitializeUI();
            ContextMenuStrip = new CalculationContextMenu();
            VerticalScrollBar.VisibleChanged += (s, e) => AutoSizeColumns();
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the <see cref="DwordDataProvider"/> which controls which and how data is display and edited.
        /// </summary>
        public DwordDataProvider DataProvider
        {
            get { return _dataProvider; }
            set
            {
                // Apply any ongoing changes (if possible).
                EndEdit();

                // Set the new data provider and refill the grid.
                _dataProvider = value;
                Refill();
            }
        }

        /// <summary>
        /// Gets or sets the minimum width of a column in pixels.
        /// </summary>
        public int MinimumColumnWidth
        {
            get
            {
                return _minimumColumnWidth;
            }
            set
            {
                _minimumColumnWidth = value;
                AutoSizeColumns();
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Forces the datagrid to refresh the displayed columns, rows and data.
        /// </summary>
        public void Refill()
        {
            // Suspend expensive layouting and drawing operations.
            _isRefillingData = true;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Clear existing columns and rows.
            TopLeftHeaderCell.Value = null;
            Columns.Clear();
            Rows.Clear();
            if (_dataProvider != null)
            {
                TopLeftHeaderCell.Value = _dataProvider.RowHeaderTitle;

                // Fill in the columns to display.
                foreach (TextImagePair header in _dataProvider.Columns)
                {
                    Columns.Add(CreateColumn(header));
                }

                // Fill in the rows to display.
                List<TextImagePair> headers = new List<TextImagePair>(_dataProvider.Rows);
                Rows.Add(headers.Count);
                int i = 0;
                foreach (TextImagePair header in headers)
                {
                    ConfigureRow(Rows[i++], header);
                }

                // Fill in the data.
                for (int y = 0; y < Rows.Count; y++)
                {
                    for (int x = 0; x < Columns.Count; x++)
                    {
                        if (_dataProvider.UseFloats)
                        {
                            Rows[y].Cells[x].Value = _dataProvider.GetValue(x, y).Single;
                        }
                        else
                        {
                            Rows[y].Cells[x].Value = _dataProvider.GetValue(x, y).Int32;
                        }
                    }
                }
            }

            // Resume layouting and drawing operations.
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            AutoSizeColumns();
            _isRefillingData = false;
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------
        
        /// <summary>
        /// Raised when the input of a user has to be converted to a valid value object.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellParsingEventArgs"/>.</param>
        protected override void OnCellParsing(DataGridViewCellParsingEventArgs e)
        {
            // Discard invalid inputs and convert valid ones.
            if (_dataProvider.UseFloats)
            {
                if (Single.TryParse((string)e.Value, out float f))
                {
                    e.Value = f;
                }
                else
                {
                    e.Value = _dataProvider.GetValue(e.ColumnIndex, e.RowIndex).Single;
                }
            }
            else
            {
                if (Int32.TryParse((string)e.Value, out int i))
                {
                    e.Value = i;
                }
                else
                {
                    e.Value = _dataProvider.GetValue(e.ColumnIndex, e.RowIndex).Int32;
                }
            }

            // Always apply parsing to update the value object in the cell.
            e.ParsingApplied = true;
        }

        /// <summary>
        /// Raised when the value of a cell has been changed (also programmatically).
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/>.</param>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);

            // Write the value back into the data provider.
            if (_isRefillingData || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            object value = Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (_dataProvider.UseFloats)
            {
                _dataProvider.SetValue(e.ColumnIndex, e.RowIndex, (float)value);
            }
            else
            {
                _dataProvider.SetValue(e.ColumnIndex, e.RowIndex, (int)value);
            }
        }

        /// <summary>
        /// Raised when the client size was changed.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/>.</param>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            AutoSizeColumns();
        }

        /// <summary>
        /// Raised when the editing control for a cell is about to be displayed.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/>.</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= EditingControl_KeyPress;
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress += EditingControl_KeyPress;
            }
        }

        /// <summary>
        /// Raised when the rows have been painted.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewRowPostPaintEventArgs"/>.</param>
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            // Draw the header manually to get rid of this ugly triangle.
            e.PaintHeader(DataGridViewPaintParts.Background);
            object headerValue = Rows[e.RowIndex].HeaderCell.Value;
            Rectangle headerRect = new Rectangle(e.RowBounds.Left, e.RowBounds.Top,
                RowHeadersWidth, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, $"{headerValue}", RowHeadersDefaultCellStyle.Font, headerRect,
                RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void InitializeUI()
        {
            // General
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.None;
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            GridColor = Color.FromArgb(255, 255, 255);
            Margin = Padding.Empty;
            SelectionMode = DataGridViewSelectionMode.CellSelect;

            // Columns
            AllowUserToResizeColumns = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(236, 236, 236),
                ForeColor = Color.FromArgb(64, 64, 64),
                Padding = new Padding(4, 7, 4, 6),
                WrapMode = DataGridViewTriState.True
            };
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            EnableHeadersVisualStyles = false;

            // Rows
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(250, 250, 250),
                SelectionBackColor = Color.FromArgb(184, 207, 255),
                SelectionForeColor = ForeColor
            };
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            RowTemplate.Height = 30;
            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            RowHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(236, 236, 236),
                ForeColor = Color.FromArgb(64, 64, 64),
                Padding = new Padding(4)
            };
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            RowsDefaultCellStyle = new DataGridViewCellStyle()
            {
                BackColor = Color.FromArgb(255, 255, 255)
            };
            ShowEditingIcon = false;

            // Cells
            CellBorderStyle = DataGridViewCellBorderStyle.None;
            DefaultCellStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleRight,
                BackColor = Color.FromArgb(255, 255, 255),
                SelectionBackColor = Color.FromArgb(192, 213, 255),
                SelectionForeColor = ForeColor
            };
        }

        private void AutoSizeColumns()
        {
            if (!_isSizingColumns)
            {
                _isSizingColumns = true;

                // Get the available width for the columns.
                int width = ClientSize.Width - RowHeadersWidth;
                if (VerticalScrollBar.Visible)
                {
                    width -= SystemInformation.VerticalScrollBarWidth;
                }

                // Resize the columns.
                int currentColumn = 0;
                foreach (DataGridViewColumn column in Columns)
                {
                    int columnWidth = (int)Math.Ceiling(width / ((float)Columns.Count - currentColumn));
                    column.Width = Math.Max(_minimumColumnWidth, columnWidth);
                    width -= columnWidth;
                    currentColumn++;
                }

                _isSizingColumns = false;
            }
        }

        private DataGridViewTextBoxColumn CreateColumn(TextImagePair header)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = header.Text;
            column.HeaderCell = new ImageDataGridColumnHeader(header.Text, header.Image);
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
            if (_dataProvider.UseFloats)
            {
                column.DefaultCellStyle.Format = "0.0000#####";
            }
            return column;
        }

        private void ConfigureRow(DataGridViewRow row, TextImagePair header)
        {
            row.HeaderCell = new ImageDataGridRowHeader(header.Text, header.Image);
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyString = e.KeyChar.ToString();
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;

            bool valid = (Char.IsControl(e.KeyChar)
                || Char.IsDigit(e.KeyChar)
                || keyString == numberFormat.NegativeSign
                || (_dataProvider.UseFloats && keyString == numberFormat.NumberDecimalSeparator));
            e.Handled = !valid;
        }
    }
}
