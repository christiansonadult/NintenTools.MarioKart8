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
        private static readonly DwordDataProvider[][][] _dataProviders = new DwordDataProvider[][][]
        {
            null,
            new DwordDataProvider[][]
            {
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.All, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.AllAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Mushrooms, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.MushroomsAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Shells, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.ShellsAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Bananas, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.BananasAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Bobombs, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.BobombsAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.Frantic, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.FranticAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.GrandPrix, false),
                    new RaceDataProvider(Section.RaceSets, RaceItemSet.GrandPrixAI, true)
                },
                new DwordDataProvider[]
                {
                    new RaceDistanceDataProvider()
                }
            },
            new DwordDataProvider[][]
            {
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.All, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.AllAI, true)
                },
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Mushrooms, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.MushroomsAI, true)
                },
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Shells, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.ShellsAI, true)
                },
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Bananas, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.BananasAI, true)
                },
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Bobombs, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.BobombsAI, true)
                },
                new DwordDataProvider[]
                {
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.Frantic, false),
                    new BattleDataProvider(Section.BattleSets, BattleItemSet.FranticAI, true)
                }
            }
        };

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private CategoryRow _crMain;
        private CategoryRow _crItemVersus;
        private CategoryRow _crItemBattle;
        private CategoryRow _crPlayerType;
        private TableLayoutPanel _tlpFile;
        private FlatButton _fbOpen;
        private FlatButton _fbSave;
        private FlatButton _fbSaveAs;
        private BinDataGrid _binDataGrid;

        private int _itemSet;
        private int _playerType;

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
            SuspendLayout();

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
            _crMain.AddCategory("Versus Races");
            _crMain.AddCategory("Battle Mode");
            _crMain.SelectedCategoryChanged += _crMain_SelectedCategoryChanged;

            _crItemVersus = new CategoryRow(1, _accentColor);
            _crItemVersus.AddCategory("All Items");
            _crItemVersus.AddCategory("Mushrooms Only");
            _crItemVersus.AddCategory("Shells Only");
            _crItemVersus.AddCategory("Bananas Only");
            _crItemVersus.AddCategory("Bob-ombs Only");
            _crItemVersus.AddCategory("Frantic Mode");
            _crItemVersus.AddCategory("Grand Prix");
            _crItemVersus.AddCategory("Distances");
            _crItemVersus.SelectedCategoryChanged += _crItemSet_SelectedCategoryChanged;

            _crItemBattle = new CategoryRow(1, _accentColor);
            _crItemBattle.AddCategory("All Items");
            _crItemBattle.AddCategory("Mushrooms Only");
            _crItemBattle.AddCategory("Shells Only");
            _crItemBattle.AddCategory("Bananas Only");
            _crItemBattle.AddCategory("Bob-ombs Only");
            _crItemBattle.AddCategory("Frantic Mode");
            _crItemBattle.SelectedCategoryChanged += _crItemSet_SelectedCategoryChanged;

            _crPlayerType = new CategoryRow(2, _accentColor);
            _crPlayerType.AddCategory("Human Racer");
            _crPlayerType.AddCategory("Software Racer");
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

            // Add the controls in reversed order so that docking works as expected.
            Controls.Add(_binDataGrid);
            Controls.Add(_tlpFile);
            Controls.Add(_crPlayerType);
            Controls.Add(_crItemBattle);
            Controls.Add(_crItemVersus);
            Controls.Add(_crMain);
            
            UpdateUI();

            ResumeLayout();
        }

        private void UpdateUI()
        {
            SuspendLayout();

            // Show the correct data (do this as first operation as it takes the longest time).
            switch ((CategoryMain)_crMain.SelectedCategory)
            {
                case CategoryMain.Versus:
                    if (_crItemVersus.SelectedCategory == (int)CategoryVersus.Distances)
                    {
                        _binDataGrid.DataProvider
                            = _dataProviders[(int)CategoryMain.Versus][(int)CategoryVersus.Distances][0];
                    }
                    else
                    {
                        _binDataGrid.DataProvider = _dataProviders[(int)CategoryMain.Versus][_itemSet][_playerType];
                    }
                    break;
                case CategoryMain.Battle:
                    _binDataGrid.DataProvider = _dataProviders[(int)CategoryMain.Battle][_itemSet][_playerType];
                    break;
            }

            // Enable categories and buttons when a file is open.
            bool fileOpen = Program.File != null;
            _crMain.EnableCategory((int)CategoryMain.Versus, fileOpen);
            _crMain.EnableCategory((int)CategoryMain.Battle, fileOpen);
            _fbSave.Visible = fileOpen;
            _fbSaveAs.Visible = fileOpen;

            // Show the file section or the data grid.
            bool isDataView = _crMain.SelectedCategory != (int)CategoryMain.File;
            _tlpFile.Visible = !isDataView;
            _binDataGrid.Visible = isDataView;

            // Show the correct secondary and tertiary rows.
            switch ((CategoryMain)_crMain.SelectedCategory)
            {
                case CategoryMain.File:
                    _crItemVersus.Visible = false;
                    _crItemBattle.Visible = false;
                    _crPlayerType.Visible = false;
                    break;
                case CategoryMain.Versus:
                    _crItemVersus.Visible = true;
                    _crItemBattle.Visible = false;
                    _crPlayerType.Visible = _itemSet != (int)CategoryVersus.Distances;
                    break;
                case CategoryMain.Battle:
                    _crItemVersus.Visible = false;
                    _crItemBattle.Visible = true;
                    _crPlayerType.Visible = true;
                    break;
            }
            // Fix ridiculously uncontrollable Windows Forms docking order.
            _crPlayerType.BringToFront();
            _binDataGrid.BringToFront();

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
                _crMain.SelectedCategory = (int)CategoryMain.Versus;
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
            if (_crMain.SelectedCategory == (int)CategoryMain.Battle)
            {
                _itemSet = Math.Min(_itemSet, (int)CategoryBattle.Frantic);
            }
            _crItemVersus.SelectedCategory = _itemSet;
            _crItemBattle.SelectedCategory = _itemSet;
            UpdateUI();
        }

        private void _crItemSet_SelectedCategoryChanged(object sender, EventArgs e)
        {
            CategoryRow crItemSet = (CategoryRow)sender;
            _itemSet = crItemSet.SelectedCategory;
            UpdateUI();
        }

        private void _crPlayerType_SelectedCategoryChanged(object sender, EventArgs e)
        {
            _playerType = _crPlayerType.SelectedCategory;
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
            Battle
        }

        private enum CategoryVersus
        {
            AllItems,
            MushroomsOnly,
            ShellsOnly,
            BananasOnly,
            BobombsOnly,
            Frantic,
            GrandPrix,
            Distances
        }

        private enum CategoryBattle
        {
            AllItems,
            MushroomsOnly,
            ShellsOnly,
            BananasOnly,
            BobombsOnly,
            Frantic
        }
    }
}
