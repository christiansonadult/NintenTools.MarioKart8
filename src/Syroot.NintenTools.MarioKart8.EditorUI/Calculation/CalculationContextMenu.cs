using System;
using System.Reflection;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a context menu offering typical calculation methods for a <see cref="SectionDataGridView"/>.
    /// </summary>
    public class CalculationContextMenu : ContextMenuStrip
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculationContextMenu"/> class.
        /// </summary>
        public CalculationContextMenu()
        {
            ResourceLoader loader = new ResourceLoader(Assembly.GetExecutingAssembly(),
                "Syroot.NintenTools.MarioKart8.EditorUI.Resources");
            Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Set...", loader.GetBitmap("Menu_Set.png"), Set_Clicked),
                new ToolStripSeparator(),
                new ToolStripMenuItem("Add...", loader.GetBitmap("Menu_Add.png"), Add_Clicked),
                new ToolStripMenuItem("Subtract...", loader.GetBitmap("Menu_Subtract.png"), Subtract_Clicked),
                new ToolStripMenuItem("Multiply...", loader.GetBitmap("Menu_Multiply.png"), Multiply_Clicked),
                new ToolStripMenuItem("Divide...", loader.GetBitmap("Menu_Divide.png"), Divide_Clicked),
            });
            Renderer = new VisualStudioRenderer();
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void Set_Clicked(object sender, EventArgs e)
        {
            DataGridView dataGridView = SourceControl as DataGridView;
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

        private void Add_Clicked(object sender, EventArgs e)
        {
            DataGridView dataGridView = SourceControl as DataGridView;
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

        private void Subtract_Clicked(object sender, EventArgs e)
        {
            DataGridView dataGridView = SourceControl as DataGridView;
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

        private void Multiply_Clicked(object sender, EventArgs e)
        {
            DataGridView dataGridView = SourceControl as DataGridView;
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

        private void Divide_Clicked(object sender, EventArgs e)
        {
            DataGridView dataGridView = SourceControl as DataGridView;
            bool isFloatGrid = dataGridView is FloatSectionDataGridView;

            float? value = FormCalculation.Show("Divide", isFloatGrid);
            if (value.HasValue && value != 0)
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
