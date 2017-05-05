using System;
using System.Windows.Forms;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// The main window of the application.
    /// </summary>
    public partial class FormMain : Form
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private MainController _controller;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            _controller = new MainController();
            _controller.FileChanged += _controller_FileChanged;
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void UpdateDataGrids(Control parent)
        {
            BinFile performance = _controller.PerformanceData;
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
            _controller.OpenFile(file);
        }

        private void _controller_FileChanged(object sender, EventArgs e)
        {
            if (_controller.PerformanceData == null)
            {
                Text = Application.ProductName;
                _ccPoints.Enabled = false;
                _ccPhysics.Enabled = false;
                _ccSpeed.Enabled = false;
                _ccHandling.Enabled = false;
                _ccMain.SelectedControl = _tlpFile;
                _btSave.Visible = false;
                _btSaveAs.Visible = false;
            }
            else
            {
                Text = $"{_controller.FileName} - {Application.ProductName}";
                _ccPoints.Enabled = true;
                _ccPhysics.Enabled = true;
                _ccSpeed.Enabled = true;
                _ccHandling.Enabled = true;
                _ccMain.SelectedControl = _ccPoints;
                _btSave.Visible = true;
                _btSaveAs.Visible = true;
            }
            UpdateDataGrids(this);
        }

        private void _btOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open File";
                openFileDialog.FileName = _controller.FileName;
                openFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _controller.OpenFile(openFileDialog.FileName);
                }
            }
        }

        private void _btSave_Click(object sender, EventArgs e)
        {
            _controller.SaveFile(_controller.FileName);
        }

        private void _btSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save File";
                saveFileDialog.FileName = _controller.FileName;
                saveFileDialog.Filter = "Mario Kart 8 (Deluxe) BIN Files|*.bin|All Files|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _controller.SaveFile(saveFileDialog.FileName);
                }
            }
        }

        private void _cmsGridSet_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _cmsGrid.SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Set", isFloatGrid);
            if (value != null)
            {
                if (isFloatGrid)
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (float)value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (int)value;
                    }
                }
            }
        }

        private void _cmsGridAdd_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _cmsGrid.SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Add", isFloatGrid);
            if (value != null)
            {
                if (isFloatGrid)
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (float)cell.Value + value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (int)cell.Value + (int)value;
                    }
                }
            }
        }

        private void _cmsGridSubtract_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _cmsGrid.SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Subtract", isFloatGrid);
            if (value != null)
            {
                if (isFloatGrid)
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (float)cell.Value - value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (int)cell.Value - (int)value;
                    }
                }
            }

        }

        private void _cmsGridMultiply_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _cmsGrid.SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Multiply", isFloatGrid);
            if (value != null)
            {
                if (isFloatGrid)
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (float)cell.Value * value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (int)cell.Value * (int)value;
                    }
                }
            }
        }

        private void _cmsGridDivide_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _cmsGrid.SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Divide", isFloatGrid);
            if (value != null)
            {
                if (isFloatGrid)
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (float)cell.Value / value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in dataGridView.SelectedCells)
                    {
                        cell.Value = (int)cell.Value / (int)value;
                    }
                }
            }
        }
    }
}