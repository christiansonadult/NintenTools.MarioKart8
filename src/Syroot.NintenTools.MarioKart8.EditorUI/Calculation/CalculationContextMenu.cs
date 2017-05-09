using System;
using System.Reflection;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a context menu offering typical calculation methods for a <see cref="BinDataGrid"/>.
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
                new ToolStripMenuItem("Set...", loader.GetBitmap("Menu.Set.png"), Set_Clicked),
                new ToolStripSeparator(),
                new ToolStripMenuItem("Add...", loader.GetBitmap("Menu.Add.png"), Add_Clicked),
                new ToolStripMenuItem("Subtract...", loader.GetBitmap("Menu.Subtract.png"), Subtract_Clicked),
                new ToolStripMenuItem("Multiply...", loader.GetBitmap("Menu.Multiply.png"), Multiply_Clicked),
                new ToolStripMenuItem("Divide...", loader.GetBitmap("Menu.Divide.png"), Divide_Clicked),
            });
            Renderer = new VisualStudioRenderer();
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void Set_Clicked(object sender, EventArgs e)
        {
            BinDataGrid binDataGrid = SourceControl as BinDataGrid;
            float? value = FormCalculation.Show("Set", binDataGrid.DataProvider.UseFloats);
            if (value != null)
            {
                if (binDataGrid.DataProvider.UseFloats)
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (float)value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (int)value;
                    }
                }
            }
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            BinDataGrid binDataGrid = SourceControl as BinDataGrid;
            float? value = FormCalculation.Show("Add", binDataGrid.DataProvider.UseFloats);
            if (value != null)
            {
                if (binDataGrid.DataProvider.UseFloats)
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (float)cell.Value + value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (int)cell.Value + (int)value;
                    }
                }
            }
        }

        private void Subtract_Clicked(object sender, EventArgs e)
        {
            BinDataGrid binDataGrid = SourceControl as BinDataGrid;
            float? value = FormCalculation.Show("Subtract", binDataGrid.DataProvider.UseFloats);
            if (value != null)
            {
                if (binDataGrid.DataProvider.UseFloats)
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (float)cell.Value - value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (int)cell.Value - (int)value;
                    }
                }
            }
        }

        private void Multiply_Clicked(object sender, EventArgs e)
        {
            BinDataGrid binDataGrid = SourceControl as BinDataGrid;
            float? value = FormCalculation.Show("Multiply", binDataGrid.DataProvider.UseFloats);
            if (value != null)
            {
                if (binDataGrid.DataProvider.UseFloats)
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (float)cell.Value * value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (int)cell.Value * (int)value;
                    }
                }
            }
        }

        private void Divide_Clicked(object sender, EventArgs e)
        {
            BinDataGrid binDataGrid = SourceControl as BinDataGrid;
            float? value = FormCalculation.Show("Divide", binDataGrid.DataProvider.UseFloats);
            if (value.HasValue && value != 0)
            {
                if (binDataGrid.DataProvider.UseFloats)
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (float)cell.Value / value;
                    }
                }
                else
                {
                    foreach (DataGridViewCell cell in binDataGrid.SelectedCells)
                    {
                        cell.Value = (int)cell.Value / (int)value;
                    }
                }
            }
        }
    }
}
