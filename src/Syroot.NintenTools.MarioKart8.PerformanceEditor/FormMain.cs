using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
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

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private CategoryControl _ccMain;
        private CalculationContextMenu _ccmAll;
        
        private TableLayoutPanel _tlpFile;
        private FlatButton _fbOpen;
        private FlatButton _fbSave;
        private FlatButton _fbSaveAs;
        
        private CategoryControl _ccPoints;
        private SectionDataGridView _dgvPointsDrivers;
        private SectionDataGridView _dgvPointsKarts;
        private SectionDataGridView _dgvPointsTires;
        private SectionDataGridView _dgvPointsGliders;

        private CategoryControl _ccPhysics;
        private SectionDataGridView _dgvPhysicsWeight;
        private SectionDataGridView _dgvPhysicsAcceleration;
        private SectionDataGridView _dgvPhysicsOnroad;
        private SectionDataGridView _dgvPhysicsOffroadBrake;
        private SectionDataGridView _dgvPhysicsOffroadSlip;
        private SectionDataGridView _dgvPhysicsTurbo;

        private CategoryControl _ccSpeed;
        private SectionDataGridView _dgvSpeedGround;
        private SectionDataGridView _dgvSpeedWater;
        private SectionDataGridView _dgvSpeedAntigravity;
        private SectionDataGridView _dgvSpeedGliding;
        
        private CategoryControl _ccHandling;
        private SectionDataGridView _dgvHandlingGround;
        private SectionDataGridView _dgvHandlingWater;
        private SectionDataGridView _dgvHandlingAntigravity;
        private SectionDataGridView _dgvHandlingGliding;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class with the given command line
        /// <paramref name="args"/>.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal FormMain(string[] args)
        {
            CreateInterface();
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

        private void CreateInterface()
        {
            BackColor = Color.White;
            ClientSize = new Size(930, 560);
            Font = SystemFonts.MessageBoxFont;
            Icon = Program.R.GetIcon("Icon.ico");
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mario Kart 8 Performance Editor";

            _ccmAll = new CalculationContextMenu();

            _ccMain = new CategoryControl(0, _accentColor);
            {
                _tlpFile = new TableLayoutPanel();
                _tlpFile.Margin = new Padding(0);
                for (int i = 0; i < 3; i++) _tlpFile.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                {
                    _fbOpen = new FlatButton("Open", _fbOpen_Click);
                    _tlpFile.Controls.Add(_fbOpen);
                    _fbSave = new FlatButton("Save", _fbSave_Click);
                    _tlpFile.Controls.Add(_fbSave);
                    _fbSaveAs = new FlatButton("Save As", _fbSaveAs_Click);
                    _tlpFile.Controls.Add(_fbSaveAs);
                }
                _ccMain.Controls.Add(_tlpFile);
                _ccMain.SetTitle(_tlpFile, "File");

                _ccPoints = new CategoryControl(1, _accentColor);
                _ccPoints.Enabled = false;
                {
                    _dgvPointsDrivers = CreateDataGrid<PointDriversDataGridView>(_ccPoints, "Drivers");
                    _dgvPointsKarts = CreateDataGrid<PointKartsDataGridView>(_ccPoints, "Karts");
                    _dgvPointsTires = CreateDataGrid<PointTiresDataGridView>(_ccPoints, "Tires");
                    _dgvPointsGliders = CreateDataGrid<PointGlidersDataGridView>(_ccPoints, "Gliders");
                }
                _ccMain.Controls.Add(_ccPoints);
                _ccMain.SetTitle(_ccPoints, "Points");

                _ccPhysics = new CategoryControl(1, _accentColor);
                _ccPhysics.Enabled = false;
                {
                    _dgvPhysicsWeight = CreateDataGrid<PhysicsWeightDataGridView>(_ccPhysics, "Weight");
                    _dgvPhysicsAcceleration = CreateDataGrid<PhysicsAccelerationDataGridView>(_ccPhysics, "Acceleration");
                    _dgvPhysicsOnroad = CreateDataGrid<PhysicsOnroadDataGridView>(_ccPhysics, "On-Road Slip");
                    _dgvPhysicsOffroadBrake = CreateDataGrid<PhysicsOffroadBrakeDataGridView>(_ccPhysics, "Off-Road Brake");
                    _dgvPhysicsOffroadSlip = CreateDataGrid<PhysicsOffroadSlipDataGridView>(_ccPhysics, "Off-Road Slip");
                    _dgvPhysicsTurbo = CreateDataGrid<PhysicsTurboDataGridView>(_ccPhysics, "Turbo");
                }
                _ccMain.Controls.Add(_ccPhysics);
                _ccMain.SetTitle(_ccPhysics, "Physics");

                _ccSpeed = new CategoryControl(1, _accentColor);
                _ccSpeed.Enabled = false;
                {
                    _dgvSpeedGround = CreateDataGrid<SpeedDataGridView>(_ccSpeed, "Ground");
                    _dgvSpeedWater = CreateDataGrid<SpeedDataGridView>(_ccSpeed, "Water");
                    _dgvSpeedAntigravity = CreateDataGrid<SpeedDataGridView>(_ccSpeed, "Anti-Gravity");
                    _dgvSpeedGliding = CreateDataGrid<SpeedAirDataGridView>(_ccSpeed, "Gliding");
                }
                _ccMain.Controls.Add(_ccSpeed);
                _ccMain.SetTitle(_ccSpeed, "Speed");

                _ccHandling = new CategoryControl(1, _accentColor);
                _ccHandling.Enabled = false;
                {
                    _dgvHandlingGround = CreateDataGrid<HandlingDataGridView>(_ccHandling, "Ground");
                    _dgvHandlingWater = CreateDataGrid<HandlingDataGridView>(_ccHandling, "Water");
                    _dgvHandlingAntigravity = CreateDataGrid<HandlingDataGridView>(_ccHandling, "Anti-Gravity");
                    _dgvHandlingGliding = CreateDataGrid<HandlingAirDataGridView>(_ccHandling, "Gliding");
                }
                _ccMain.Controls.Add(_ccHandling);
                _ccMain.SetTitle(_ccHandling, "Handling");
            }
            Controls.Add(_ccMain);
        }
        private SectionDataGridView CreateDataGrid<T>(CategoryControl parent, string title)
            where T : SectionDataGridView
        {
            SectionDataGridView dataGrid = Activator.CreateInstance<T>();
            dataGrid.ContextMenuStrip = _ccmAll;
            parent.Controls.Add(dataGrid);
            parent.SetTitle(dataGrid, title);
            return dataGrid;
        }

        private void UpdateDataGrids()
        {
            BinFile performance = Program.File;

            _dgvPointsDrivers.DataGroup = (DwordArrayGroup)performance[(int)Section.DriverPoints][0];
            _dgvPointsKarts.DataGroup = (DwordArrayGroup)performance[(int)Section.KartPoints][0];
            _dgvPointsTires.DataGroup = (DwordArrayGroup)performance[(int)Section.TirePoints][0];
            _dgvPointsGliders.DataGroup = (DwordArrayGroup)performance[(int)Section.GliderPoints][0];

            _dgvPhysicsWeight.DataGroup = (DwordArrayGroup)performance[(int)Section.WeightStats][0];
            _dgvPhysicsAcceleration.DataGroup = (DwordArrayGroup)performance[(int)Section.AccelerationStats][0];
            _dgvPhysicsOnroad.DataGroup = (DwordArrayGroup)performance[(int)Section.OnroadStats][0];
            _dgvPhysicsOffroadBrake.DataGroup = (DwordArrayGroup)performance[(int)Section.OffroadStats][0];
            _dgvPhysicsOffroadSlip.DataGroup = (DwordArrayGroup)performance[(int)Section.OffroadStats][0];
            _dgvPhysicsTurbo.DataGroup = (DwordArrayGroup)performance[(int)Section.TurboStats][0];

            _dgvSpeedGround.DataGroup = (DwordArrayGroup)performance[(int)Section.SpeedGroundStats][0];
            _dgvSpeedWater.DataGroup = (DwordArrayGroup)performance[(int)Section.SpeedWaterStats][0];
            _dgvSpeedAntigravity.DataGroup = (DwordArrayGroup)performance[(int)Section.SpeedAntigravityStats][0];
            _dgvSpeedGliding.DataGroup = (DwordArrayGroup)performance[(int)Section.SpeedAirStats][0];

            _dgvHandlingGround.DataGroup = (DwordArrayGroup)performance[(int)Section.HandlingGroundStats][0];
            _dgvHandlingWater.DataGroup = (DwordArrayGroup)performance[(int)Section.HandlingWaterStats][0];
            _dgvHandlingAntigravity.DataGroup = (DwordArrayGroup)performance[(int)Section.HandlingAntigravityStats][0];
            _dgvHandlingGliding.DataGroup = (DwordArrayGroup)performance[(int)Section.HandlingAirStats][0];
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void Program_FileChanged(object sender, EventArgs e)
        {
            bool fileOpen = Program.File != null;
            if (fileOpen)
            {
                string fileName = Program.FileName;
                Text = $"{Path.GetFileName(fileName)} ({Path.GetDirectoryName(fileName)}) - {Application.ProductName}";
                _ccMain.SelectedControl = _ccPoints;
            }
            else
            {
                Text = Application.ProductName;
                _ccMain.SelectedControl = _tlpFile;
            }
            _ccPoints.Enabled = fileOpen;
            _ccPhysics.Enabled = fileOpen;
            _ccSpeed.Enabled = fileOpen;
            _ccHandling.Enabled = fileOpen;
            _fbSave.Visible = fileOpen;
            _fbSaveAs.Visible = fileOpen;

            UpdateDataGrids();
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
    }
}
