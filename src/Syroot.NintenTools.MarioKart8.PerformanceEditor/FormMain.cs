using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    internal partial class FormMain : Form
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly Color _accentColor = Color.FromArgb(0, 66, 200);
        private static readonly BinDataProvider[][] _dataProviders = new BinDataProvider[][]
        {
            null,
            new BinDataProvider[]
            {
                new PointsDriversDataProvider(),
                new PointsKartsDataProvider(),
                new PointsTiresDataProvider(),
                new PointsGlidersDataProvider()
            },
            new BinDataProvider[]
            {
                new PhysicsWeightDataProvider(),
                new PhysicsAccelerationDataProvider(),
                new PhysicsOnroadDataProvider(),
                new PhysicsOffroadSlipDataProvider(),
                new PhysicsOffroadBrakeDataProvider(),
                new PhysicTurboDataProvider()
            },
            new BinDataProvider[]
            {
                new SpeedGroundDataProvider(),
                new SpeedWaterDataProvider(),
                new SpeedAntigravityDataProvider(),
                new SpeedGlidingDataProvider()
            },
            new BinDataProvider[]
            {
                new HandlingGroundDataProvider(),
                new HandlingWaterDataProvider(),
                new HandlingAntigravityDataProvider(),
                new HandlingGlidingDataProvider()
            }
        };

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private CategoryRow _crMain;
        private CategoryRow _crPoints;
        private CategoryRow _crPhysics;
        private CategoryRow _crSpeedHandling;
        private TableLayoutPanel _tlpFile;
        private FlatButton _fbOpen;
        private FlatButton _fbSave;
        private FlatButton _fbSaveAs;
        private BinDataGrid _binDataGrid;

        private int _category1;
        private int _category2;
        private int _lastCategory1 = (int)CategoryMain.Points;
        
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class with the given command line
        /// <paramref name="args"/>.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal FormMain(string[] args)
        {
            InitializeUI();
            Program.FileChanged += Program_FileChanged;

            // Open a file passed to the application.
            if (args.Length == 1 && File.Exists(args[0]))
            {
                string argFile = args[0];
                if (File.Exists(argFile))
                {
                    Program.OpenFile(argFile);
                }
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void InitializeUI()
        {
            AllowDrop = true;
            BackColor = Color.White;
            ClientSize = new Size(1000, 560);
            DoubleBuffered = true;
            Font = SystemFonts.MessageBoxFont;
            Icon = Program.R.GetIcon("Icon.ico");
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mario Kart 8 Performance Editor";
            DragEnter += FormMain_DragEnter;
            DragDrop += FormMain_DragDrop;

            // Set up category rows.
            _crMain = new CategoryRow(0, _accentColor);
            _crMain.AddCategory("File");
            _crMain.AddCategory("Points", false);
            _crMain.AddCategory("Physics", false);
            _crMain.AddCategory("Speed", false);
            _crMain.AddCategory("Handling", false);
            _crMain.SelectedCategoryChanged += _crMain_SelectedCategoryChanged;

            _crPoints = new CategoryRow(1, _accentColor);
            _crPoints.AddCategory("Drivers");
            _crPoints.AddCategory("Karts");
            _crPoints.AddCategory("Tires");
            _crPoints.AddCategory("Gliders");
            _crPoints.SelectedCategoryChanged += _crSecondary_SelectedCategoryChanged;

            _crPhysics = new CategoryRow(1, _accentColor);
            _crPhysics.AddCategory("Weight");
            _crPhysics.AddCategory("Acceleration");
            _crPhysics.AddCategory("On-Road Slip");
            _crPhysics.AddCategory("Off-Road Slip");
            _crPhysics.AddCategory("Off-Road Brake");
            _crPhysics.AddCategory("Turbo");
            _crPhysics.SelectedCategoryChanged += _crSecondary_SelectedCategoryChanged;

            _crSpeedHandling = new CategoryRow(1, _accentColor);
            _crSpeedHandling.AddCategory("Ground");
            _crSpeedHandling.AddCategory("Water");
            _crSpeedHandling.AddCategory("Anti-Gravity");
            _crSpeedHandling.AddCategory("Gliding");
            _crSpeedHandling.SelectedCategoryChanged += _crSecondary_SelectedCategoryChanged;

            // Set up file section.
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

            // Set up data grid.
            _binDataGrid = new BinDataGrid();
            _binDataGrid.MinimumColumnWidth = 95;

            // Add the controls in reversed order so that docking works as expected.
            Controls.Add(_binDataGrid);
            Controls.Add(_tlpFile);
            Controls.Add(_crSpeedHandling);
            Controls.Add(_crPhysics);
            Controls.Add(_crPoints);
            Controls.Add(_crMain);
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            this.SuspendDrawing();

            // Show the correct data (do this as first operation as it takes the longest time).
            _binDataGrid.DataProvider = _dataProviders[_category1]?[_category2];

            // Enable categories and buttons when a file is open.
            bool fileOpen = Program.File != null;
            _crMain.EnableCategory((int)CategoryMain.Points, fileOpen);
            _crMain.EnableCategory((int)CategoryMain.Physics, fileOpen);
            _crMain.EnableCategory((int)CategoryMain.Speed, fileOpen);
            _crMain.EnableCategory((int)CategoryMain.Handling, fileOpen);
            _fbSave.Visible = fileOpen;
            _fbSaveAs.Visible = fileOpen;

            // Show the file section or the data grid.
            bool isDataView = _category1 != (int)CategoryMain.File;
            _tlpFile.Visible = !isDataView;
            _binDataGrid.Visible = isDataView;

            // Show the correct secondary row.
            _crPoints.Visible = _category1 == (int)CategoryMain.Points;
            _crPhysics.Visible = _category1 == (int)CategoryMain.Physics;
            _crSpeedHandling.Visible = _category1 == (int)CategoryMain.Speed
                || _category1 == (int)CategoryMain.Handling;
            
            this.ResumeDrawing();

            _binDataGrid.Focus();
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void Program_FileChanged(object sender, EventArgs e)
        {
            // Update window title and switch to data view if a file was opened.
            bool fileOpen = Program.File != null;
            if (fileOpen)
            {
                string fileName = Program.FileName;
                Text = $"{Path.GetFileName(fileName)} ({Path.GetDirectoryName(fileName)}) - {Application.ProductName}";
                _crMain.SelectedCategory = _lastCategory1;
            }
            else
            {
                Text = Application.ProductName;
                _crMain.SelectedCategory = (int)CategoryMain.File;
            }

            UpdateUI();
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            Program.OpenFile(file);
        }

        private void _crMain_SelectedCategoryChanged(object sender, EventArgs e)
        {
            _category1 = _crMain.SelectedCategory;

            // Remember the last data category.
            if (_category1 > (int)CategoryMain.File)
            {
                _lastCategory1 = _category1;
            }

            // Switch to the correct subcategory.
            switch ((CategoryMain)_category1)
            {
                case CategoryMain.Points:
                    _category2 = _crPoints.SelectedCategory;
                    break;
                case CategoryMain.Physics:
                    _category2 = _crPhysics.SelectedCategory;
                    break;
                case CategoryMain.Handling:
                case CategoryMain.Speed:
                    _category2 = _crSpeedHandling.SelectedCategory;
                    break;
            }
            UpdateUI();
        }

        private void _crSecondary_SelectedCategoryChanged(object sender, EventArgs e)
        {
            CategoryRow categoryRow = (CategoryRow)sender;
            _category2 = categoryRow.SelectedCategory;
            UpdateUI();
        }

        private void _fbOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open File";
                openFileDialog.FileName = Program.FileName;
                openFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Program.OpenFile(openFileDialog.FileName);
                }
            }
        }

        private void _fbSave_Click(object sender, EventArgs e)
        {
            Program.SaveFile(Program.FileName, false);
        }

        private void _fbSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = Program.FileName;
                saveFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Program.SaveFile(saveFileDialog.FileName, true);
                }
            }
        }
        
        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------

        private enum CategoryMain
        {
            File,
            Points,
            Physics,
            Speed,
            Handling
        }
    }
}
