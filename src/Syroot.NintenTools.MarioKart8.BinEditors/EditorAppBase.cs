using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Syroot.BinaryData;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinEditors.DataProvider;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors
{
    /// <summary>
    /// Represents the common user interface and behavior for a BIN editor program.
    /// </summary>
    public abstract class EditorAppBase : Form
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private string _fileName;
        private CategoryRow[] _categoryRows;
        private TableLayoutPanel _tlpFile;
        private FlatButton _fbOpen;
        private FlatButton _fbSave;
        private FlatButton _fbSaveAs;
        private BinDataGrid _binDataGrid;
        private bool _handleCategoryEvents;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorAppBase"/> class with the given command line
        /// <paramref name="args"/>.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        protected EditorAppBase(string[] args)
        {
            InitializeUI();

            // Open a file passed as command line argument.
            string file = args.Length == 1 ? args[0] : null;
            if (!String.IsNullOrEmpty(file) && File.Exists(file))
            {
                OpenFile(file);
            }
        }
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the <see cref="BinData.BinFile"/> instance representing the performance data.
        /// </summary>
        public BinFile BinFile { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="BinFile"/> instance represents data from Mario Kart 8 Deluxe.
        /// </summary>
        public bool IsMK8Deluxe
        {
            get
            {
                return BinFile?.ByteOrder == ByteOrder.LittleEndian;
            }
        }
        
        /// <summary>
        /// Gets the <see cref="Icon"/> to display in the title bar of the window.
        /// </summary>
        protected new abstract Icon Icon { get; }

        /// <summary>
        /// Gets the name of the editor application.
        /// </summary>
        protected abstract string Title { get; }

        /// <summary>
        /// Gets the accentual color used for category rows in the user interface.
        /// </summary>
        protected abstract Color AccentColor { get; }

        /// <summary>
        /// Gets the number of category rows available.
        /// </summary>
        protected abstract int CategoryRowCount { get; }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when a drag &amp; drop operation enters this window's bounds.
        /// </summary>
        /// <param name="e">The <see cref="DragEventArgs"/>.</param>
        protected override void OnDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// Raised when a drag &amp; drop operation ends inside this window's bounds.
        /// </summary>
        /// <param name="e">The <see cref="DragEventArgs"/>.</param>
        protected override void OnDragDrop(DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            OpenFile(file);
        }

        /// <summary>
        /// Called when the BIN file should be checked if it can be edited.
        /// </summary>
        protected abstract void CheckBinFile();

        /// <summary>
        /// Gets the <see cref="DwordDataProvider"/> for the selected category indices.
        /// </summary>
        /// <param name="categoryIndices">The category indices for which to retrieve the data provider.</param>
        /// <returns>A <see cref="DwordDataProvider"/> instance to display in the data grid, or <c>null</c> to display
        /// the file selection section.</returns>
        protected abstract DwordDataProvider GetDataProvider(int[] categoryIndices);

        /// <summary>
        /// Allows custom logic to be applied when switching between categories.
        /// </summary>
        /// <param name="categoryRows">The <see cref="CategoryRow"/> controls.</param>
        /// <param name="changedRow">The changed category row index in the array.</param>
        /// <param name="previous">The previously selected category index.</param>
        /// <param name="current">The newly selected category index.</param>
        protected abstract void UpdateCategories(CategoryRow[] categoryRows, int changedRow, int previous, int current);

        /// <summary>
        /// Allows custom logic to be applied when the first category is selected.
        /// </summary>
        protected virtual void ResetCategories()
        {
            SuspendLayout();
            _handleCategoryEvents = false;
            // Update the internal indices.
            for (int i = 0; i < _categoryRows.Length; i++)
            {
                _categoryRows[i].SelectedCategory = i == 0 ? 1 : 0;
            }
            // Let the editor application optimize the index switching.
            UpdateCategories(_categoryRows, 0, 0, 1);
            for (int i = 1; i < _categoryRows.Length; i++)
            {
                UpdateCategories(_categoryRows, i, 0, 0);
            }
            _handleCategoryEvents = true;
            ResumeLayout();
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void OpenFile(string fileName)
        {
            try
            {
                _fileName = fileName;
                BinFile = new BinFile(_fileName);
                CheckBinFile();
                Text = $"{Path.GetFileName(_fileName)} ({Path.GetDirectoryName(_fileName)}) - {Title}";
                ResetCategories();
                UpdateUI();
            }
            catch (Exception ex)
            {
                _fileName = null;
                BinFile = null;
                Text = Title;
                UpdateUI();
                UpdateCategoriesInternal(0, 0, 0);
                MessageBox.Show($"Could not open \"{fileName}\":{Environment.NewLine}{ex.Message}", Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void SaveFile(string fileName)
        {
            try
            {
                BinFile.Save(fileName);
                OpenFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save \"{fileName}\":{Environment.NewLine}{ex.Message}", Title,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void InitializeUI()
        {
            SuspendLayout();

            // Set up the main form.
            AllowDrop = true;
            BackColor = Color.White;
            ClientSize = new Size(1000, 560);
            DoubleBuffered = true;
            Font = SystemFonts.MessageBoxFont;
            base.Icon = Icon;
            StartPosition = FormStartPosition.CenterScreen;
            Text = Title;

            // Set up category rows.
            _categoryRows = new CategoryRow[CategoryRowCount];
            for (int i = 0; i < _categoryRows.Length; i++)
            {
                CategoryRow categoryRow = new CategoryRow(i, AccentColor);
                categoryRow.SelectedCategoryChanged += CategoryRow_SelectedCategoryChanged;
                _categoryRows[i] = categoryRow;
                Controls.Add(categoryRow);
            }

            // Set up the file section.
            _fbOpen = new FlatButton("Open", _fbOpen_Click);
            _fbSave = new FlatButton("Save", _fbSave_Click);
            _fbSaveAs = new FlatButton("Save As", _fbSaveAs_Click);
            _tlpFile = new TableLayoutPanel();
            _tlpFile.Margin = new Padding(0);
            _tlpFile.Dock = DockStyle.Fill;
            for (int i = 0; i < 3; i++)
            {
                _tlpFile.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            }
            _tlpFile.Controls.Add(_fbOpen);
            _tlpFile.Controls.Add(_fbSave);
            _tlpFile.Controls.Add(_fbSaveAs);
            Controls.Add(_tlpFile);

            // Set up the data grid.
            _binDataGrid = new BinDataGrid();
            _binDataGrid.MinimumColumnWidth = 95;
            Controls.Add(_binDataGrid);

            UpdateCategoriesInternal(0, 0, 0);
            UpdateUI();

            ResumeLayout();
        }

        private void UpdateUI()
        {
            SuspendLayout();

            // Show the file selection if no data provider was returned.
            DwordDataProvider dataProvider = GetDataProvider(_categoryRows.Select(x => x.SelectedCategory).ToArray());
            _tlpFile.Visible = dataProvider == null;
            _binDataGrid.Visible = dataProvider != null;
            _binDataGrid.DataProvider = dataProvider;

            // Hide Save buttons if no file is open.
            bool fileOpen = BinFile != null;
            _fbSave.Visible = fileOpen;
            _fbSaveAs.Visible = fileOpen;

            // Fix Windows Forms' hilariously uncontrollable dock order.
            foreach (CategoryRow categoryRow in _categoryRows)
            {
                categoryRow.BringToFront();
            }
            _tlpFile.BringToFront();
            _binDataGrid.BringToFront();

            // Focus the data grid if it is visible.
            if (_binDataGrid.Visible)
            {
                _binDataGrid.Focus();
            }

            ResumeLayout();
        }

        private void UpdateCategoriesInternal(int changedRow, int previous, int current)
        {
            _handleCategoryEvents = false;
            UpdateCategories(_categoryRows, changedRow, previous, current);
            _handleCategoryEvents = true;
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void CategoryRow_SelectedCategoryChanged(object sender, CategoryChangedEventArgs e)
        {
            if (_handleCategoryEvents)
            {
                UpdateCategoriesInternal(Array.IndexOf(_categoryRows, sender), e.Previous, e.Current);
                UpdateUI();
            }
        }

        private void _fbOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open File";
                openFileDialog.InitialDirectory = Path.GetDirectoryName(_fileName);
                openFileDialog.FileName = Path.GetFileName(_fileName);
                openFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(openFileDialog.FileName);
                }
            }
        }

        private void _fbSave_Click(object sender, EventArgs e)
        {
            SaveFile(_fileName);
        }

        private void _fbSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save File";
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(_fileName);
                saveFileDialog.FileName = Path.GetFileName(_fileName);
                saveFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveFile(saveFileDialog.FileName);
                }
            }
        }
    }
}
