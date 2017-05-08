using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    internal partial class FormMain : Form
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly Color _accentColor = Color.FromArgb(200, 66, 0);
        private static readonly BinDataProvider[][][] _dataProviders = new BinDataProvider[][][]
        {
            null,
            new BinDataProvider[][]
            {
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.GrandPrix, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.GrandPrixAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.All, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.AllAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Mushrooms, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.MushroomsAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Shells, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.ShellsAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Bananas, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.BananasAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Bobombs, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.BobombsAI, true)
                },
                new BinDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Frantic, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.FranticAI, true)
                }
            },
            new BinDataProvider[][]
            {
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.All, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.AllAI, true)
                },
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Mushrooms, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.MushroomsAI, true)
                },
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Shells, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.ShellsAI, true)
                },
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Bananas, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.BananasAI, true)
                },
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Bobombs, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.BobombsAI, true)
                },
                new BinDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Frantic, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.FranticAI, true)
                }
            }
        };

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private CategoryRow _crMain;
        private CategoryRow _crItemVersusSet;
        private CategoryRow _crItemBattleSet;
        private CategoryRow _crPlayerType;
        private TableLayoutPanel _tlpFile;
        private FlatButton _fbOpen;
        private FlatButton _fbSave;
        private FlatButton _fbSaveAs;
        private BinDataGrid _binDataGrid;

        private int _category1;
        private int _category2;
        private int _category3;
        private int _lastCategory1 = (int)CategoryMain.Versus;
        
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
            ClientSize = new Size(1000, 630);
            DoubleBuffered = true;
            Font = SystemFonts.MessageBoxFont;
            Icon = Program.R.GetIcon("Icon.ico");
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mario Kart 8 Item Editor";
            DragEnter += FormMain_DragEnter;
            DragDrop += FormMain_DragDrop;

            // Set up category rows.
            _crMain = new CategoryRow(0, _accentColor);
            _crMain.AddCategory("File");
            _crMain.AddCategory("Versus Races", false);
            _crMain.AddCategory("Battle Mode", false);
            //_crMain.AddCategory("Distances", false);
            _crMain.SelectedCategoryChanged += _crMain_SelectedCategoryChanged;

            _crItemVersusSet = new CategoryRow(1, _accentColor);
            _crItemVersusSet.AddCategory("Grand Prix");
            _crItemVersusSet.AddCategory("All Items");
            _crItemVersusSet.AddCategory("Mushrooms Only");
            _crItemVersusSet.AddCategory("Shells Only");
            _crItemVersusSet.AddCategory("Bananas Only");
            _crItemVersusSet.AddCategory("Bob-ombs Only");
            _crItemVersusSet.AddCategory("Frantic Mode");
            _crItemVersusSet.SelectedCategoryChanged += _crItemSet_SelectedCategoryChanged;

            _crItemBattleSet = new CategoryRow(1, _accentColor);
            _crItemBattleSet.AddCategory("All Items");
            _crItemBattleSet.AddCategory("Mushrooms Only");
            _crItemBattleSet.AddCategory("Shells Only");
            _crItemBattleSet.AddCategory("Bananas Only");
            _crItemBattleSet.AddCategory("Bob-ombs Only");
            _crItemBattleSet.AddCategory("Frantic Mode");
            _crItemBattleSet.SelectedCategoryChanged += _crItemSet_SelectedCategoryChanged;

            _crPlayerType = new CategoryRow(2, _accentColor);
            _crPlayerType.AddCategory("No AI Racers");
            _crPlayerType.AddCategory("With AI Racers");
            _crPlayerType.SelectedCategoryChanged += _crPlayerType_SelectedCategoryChanged;

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
            _binDataGrid.MinimumColumnWidth = 90;
            _binDataGrid.TopLeftHeaderCell.Value = "blabla";

            // Add the controls in reversed order so that docking works as expected.
            Controls.Add(_binDataGrid);
            Controls.Add(_tlpFile);
            Controls.Add(_crPlayerType);
            Controls.Add(_crItemBattleSet);
            Controls.Add(_crItemVersusSet);
            Controls.Add(_crMain);
            
            UpdateUI();
        }

        private void UpdateUI()
        {
            SuspendLayout();

            // Show the correct data (do this as first operation as it takes the longest time).
            _binDataGrid.DataProvider = _dataProviders[_category1]?[_category2][_category3];

            // Enable categories and buttons when a file is open.
            bool fileOpen = Program.File != null;
            _crMain.EnableCategory((int)CategoryMain.Versus, fileOpen);
            _crMain.EnableCategory((int)CategoryMain.Battle, fileOpen);
            //_crMain.EnableCategory((int)CategoryMain.Distances, fileOpen);
            _fbSave.Visible = fileOpen;
            _fbSaveAs.Visible = fileOpen;

            // Show the file section or the data grid.
            bool isDataView = _category1 != (int)CategoryMain.File;
            _tlpFile.Visible = !isDataView;
            _binDataGrid.Visible = isDataView;

            // Show the correct secondary and tertiary rows.
            switch ((CategoryMain)_category1)
            {
                case CategoryMain.File:
                    _crPlayerType.Visible = false;
                    _crItemVersusSet.Visible = false;
                    _crItemBattleSet.Visible = false;
                    break;
                case CategoryMain.Versus:
                    _crPlayerType.Visible = true;
                    _crItemVersusSet.Visible = true;
                    _crItemBattleSet.Visible = false;
                    break;
                case CategoryMain.Battle:
                    _crPlayerType.Visible = true;
                    _crItemVersusSet.Visible = false;
                    _crItemBattleSet.Visible = true;
                    break;
                case CategoryMain.Distances:
                    _crPlayerType.Visible = true;
                    _crItemVersusSet.Visible = false;
                    _crItemBattleSet.Visible = false;
                    break;
            }
            
            _binDataGrid.Focus();

            ResumeLayout();
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
                case CategoryMain.Versus:
                    _category2 = _crItemVersusSet.SelectedCategory;
                    break;
                case CategoryMain.Battle:
                    _category2 = _crItemBattleSet.SelectedCategory;
                    break;
                case CategoryMain.Distances:
                    //_category2 = _crSpeedHandling.SelectedCategory;
                    break;
            }
            UpdateUI();
        }

        private void _crItemSet_SelectedCategoryChanged(object sender, EventArgs e)
        {
            CategoryRow categoryRow = (CategoryRow)sender;
            _category2 = categoryRow.SelectedCategory;
            UpdateUI();
        }

        private void _crPlayerType_SelectedCategoryChanged(object sender, EventArgs e)
        {
            CategoryRow categoryRow = (CategoryRow)sender;
            _category3 = categoryRow.SelectedCategory;
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
            Versus,
            Battle,
            Distances
        }
    }
}
