using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents the non-generic base of <see cref="SectionDataGridView{T}"/>, which is required for the Windows
    /// Forms Designer.
    /// </summary>
    public abstract class SectionDataGridView : DataGridView
    {
        public abstract DwordArrayGroup DataGroup { get; set; }
    }

    /// <summary>
    /// Represents a <see cref="DataGridView"/> showing values from a <see cref="Section"/>. of the given type.
    /// </summary>
    /// <typeparam name="T">The type of the values.</typeparam>
    public abstract class SectionDataGridView<T> : SectionDataGridView
         where T : struct, IComparable, IConvertible
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int _minimumColumnWidth = 85;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private DwordArrayGroup _dataGroup;
        private bool _isSizingColumns;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionDataGridView"/> class.
        /// </summary>
        public SectionDataGridView()
        {
            // General
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.None;
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
                ForeColor = Color.FromArgb(64, 64, 64)
            };
            ColumnHeadersHeight = 30;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
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
            RowTemplate.Height = 30;
            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            RowHeadersDefaultCellStyle = new DataGridViewCellStyle()
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(236, 236, 236),
                ForeColor = Color.FromArgb(64, 64, 64)
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

            VerticalScrollBar.VisibleChanged += VerticalScrollBar_VisibleChanged;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the <see cref="DwordArrayGroup"/> to display and make editable.
        /// </summary>
        public override DwordArrayGroup DataGroup
        {
            get
            {
                return _dataGroup;
            }
            set
            {
                _dataGroup = value;
                Columns.Clear();
                Rows.Clear();

                if (_dataGroup != null)
                {
                    UpdateDataGridView();
                }
            }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

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
        /// Raised when the user input of a cell is being validated.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellValidatingEventArgs"/>.</param>
        protected override void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            e.Cancel = !ValidateTextValue(e.FormattedValue.ToString());
        }

        /// <summary>
        /// Raised when user input in a cell has been validated.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/>.</param>
        protected override void OnCellValidated(DataGridViewCellEventArgs e)
        {
            base.OnCellValidated(e);
            SetCellValue(e);
        }

        /// <summary>
        /// Raised when the value of a cell has been changed (also programmatically).
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/>.</param>
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            base.OnCellValueChanged(e);
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                SetCellValue(e);
            }
        }

        /// <summary>
        /// Raised when the editing control for a cell is about to be displayed.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/>.</param>
        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= EditingControlTextBox_KeyPress;
            if (e.Control is TextBox textBox)
            {
                textBox.KeyPress += EditingControlTextBox_KeyPress;
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

        /// <summary>
        /// Configures the specified column for use in the data grid.
        /// </summary>
        /// <param name="column">The <see cref="DataGridViewColumn"/> to configure.</param>
        /// <param name="header">The contents displayed in the column.</param>
        protected virtual void ConfigureColumn(DataGridViewColumn column, TextImagePair header)
        {
            column.HeaderText = header.Text;
            column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Configures the specified row for use in the data grid.
        /// </summary>
        /// <param name="column">The <see cref="DataGridViewRow"/> to configure.</param>
        /// <param name="header">The contents displayed in the row.</param>
        protected virtual void ConfigureRow(DataGridViewRow row, TextImagePair header)
        {
            row.HeaderCell = new ImageDataGridViewRowHeaderCell(header.Text, header.Image);
        }

        /// <summary>
        /// Called when the column header contents should be returned.
        /// </summary>
        /// <returns>The enumeration of column header contents.</returns>
        protected abstract IEnumerable<TextImagePair> GetColumnHeaders();

        /// <summary>
        /// Called when the row header contents should be returned.
        /// </summary>
        /// <returns>The enumeration of row header contents.</returns>
        protected abstract IEnumerable<TextImagePair> GetRowHeaders();

        /// <summary>
        /// Called when the whole grid has to be filled with columns, rows and values from the <see cref="DataGroup"/>.
        /// </summary>
        protected abstract void FillData();

        /// <summary>
        /// Called when the value has to be written back to data.
        /// </summary>
        /// <param name="row">The row of the data.</param>
        /// <param name="column">The column of the data.</param>
        /// <param name="value">The value of the data.</param>
        protected abstract void SetDataValue(int row, int column, T value);

        /// <summary>
        /// Called when the user inputs a character and it has to be validated.
        /// </summary>
        /// <param name="character">The character which was input.</param>
        /// <returns><c>true</c> to allow the character, otherwise <c>false</c>.</returns>
        protected abstract bool ValidateCharacterInput(char character);

        /// <summary>
        /// Called when the cell value text has to be validated.
        /// </summary>
        /// <param name="text">The value to validate.</param>
        /// <returns><c>true</c> to allow the text, otherwise <c>false</c>.</returns>
        protected abstract bool ValidateTextValue(string text);

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void UpdateDataGridView()
        {
            // Fill in the column headers.
            List<TextImagePair> columnHeaders = new List<TextImagePair>(GetColumnHeaders());
            DataGridViewColumn[] columns = new DataGridViewColumn[columnHeaders.Count];
            int i = 0;
            foreach (TextImagePair columnHeader in columnHeaders)
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                ConfigureColumn(column, columnHeader);
                columns[i++] = column;
            }
            Columns.AddRange(columns);

            // Fill in the row headers.
            List<TextImagePair> rowHeaders = new List<TextImagePair>(GetRowHeaders());
            Rows.Add(rowHeaders.Count);
            i = 0;
            foreach (TextImagePair rowHeader in rowHeaders)
            {
                ConfigureRow(Rows[i++], rowHeader);
            }

            // Fill the data values.
            FillData();

            AutoSizeColumns();
        }

        private void SetCellValue(DataGridViewCellEventArgs e)
        {
            // Write the value back to the performance data.
            object cellValue = Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            SetDataValue(e.RowIndex, e.ColumnIndex, (T)Convert.ChangeType(cellValue, typeof(T)));
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

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void EditingControlTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !ValidateCharacterInput(e.KeyChar);
        }

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            AutoSizeColumns();
        }
    }
}
