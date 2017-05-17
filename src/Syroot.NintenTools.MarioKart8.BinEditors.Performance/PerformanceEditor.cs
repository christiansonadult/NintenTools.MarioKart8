using System;
using System.Drawing;
using Syroot.NintenTools.MarioKart8.BinEditors.DataProvider;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Performance
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    internal partial class PerformanceEditor : EditorAppBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------
        
        private static readonly DwordDataProvider[][] _dataProviders = new DwordDataProvider[][]
        {
            null,
            new DwordDataProvider[]
            {
                new PointsDriversDataProvider(),
                new PointsKartsDataProvider(),
                new PointsTiresDataProvider(),
                new PointsGlidersDataProvider()
            },
            new DwordDataProvider[]
            {
                new PhysicsWeightDataProvider(),
                new PhysicsAccelerationDataProvider(),
                new PhysicsOnroadDataProvider(),
                new PhysicsOffroadSlipDataProvider(),
                new PhysicsOffroadBrakeDataProvider(),
                new PhysicTurboDataProvider()
            },
            new DwordDataProvider[]
            {
                new SpeedGroundDataProvider(),
                new SpeedWaterDataProvider(),
                new SpeedAntigravityDataProvider(),
                new SpeedGlidingDataProvider()
            },
            new DwordDataProvider[]
            {
                new HandlingGroundDataProvider(),
                new HandlingWaterDataProvider(),
                new HandlingAntigravityDataProvider(),
                new HandlingGlidingDataProvider()
            }
        };

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private int _categoryPoints;
        private int _categoryPhysics;
        private int _categorySpeedHandling;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the <see cref="Icon"/> to display in the title bar of the window.
        /// </summary>
        protected override Icon Icon
        {
            get { return Program.R.GetIcon("Icon.ico"); }
        }

        /// <summary>
        /// Gets the name of the editor application.
        /// </summary>
        protected override string Title
        {
            get { return "MK8 Performance Editor"; }
        }

        /// <summary>
        /// Gets the accentual color used for category rows in the user interface.
        /// </summary>
        protected override Color AccentColor
        {
            get { return Color.FromArgb(0, 66, 200); }
        }

        /// <summary>
        /// Gets the number of category rows available.
        /// </summary>
        protected override int CategoryRowCount
        {
            get { return 2; }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        /// <summary>
        /// Called when the BIN file should be checked if it can be edited.
        /// </summary>
        protected override void CheckBinFile()
        {
            if (BinFile.Identifier != "PERF")
            {
                throw new InvalidOperationException("This is not a Performance BIN file.");
            }
        }

        /// <summary>
        /// Gets the <see cref="DwordDataProvider"/> for the selected category indices.
        /// </summary>
        /// <param name="categoryIndices">The category indices for which to retrieve the data provider.</param>
        /// <returns>A <see cref="DwordDataProvider"/> instance to display in the data grid, or <c>null</c> to display
        /// the file selection section.</returns>
        protected override DwordDataProvider GetDataProvider(int[] categoryIndices)
        {
            return _dataProviders[categoryIndices[0]]?[categoryIndices[1]];
        }

        /// <summary>
        /// Allows custom logic to be applied when switching between categories.
        /// </summary>
        /// <param name="categoryRows">The <see cref="CategoryRow"/> controls.</param>
        /// <param name="changedRow">The changed category row index in the array.</param>
        /// <param name="previous">The previously selected category index.</param>
        /// <param name="current">The newly selected category index.</param>
        protected override void UpdateCategories(CategoryRow[] categoryRows, int changedRow, int previous, int current)
        {
            CategoryRow crMain = categoryRows[0];
            CategoryRow crSecondary = categoryRows[1];

            if (changedRow == 0)
            {
                // Update the main categories.
                bool fileOpen = BinFile != null;
                CategoryMain category = (CategoryMain)current;
                CategoryMain categoryPrevious = (CategoryMain)previous;

                crMain.ClearCategories();
                crMain.AddCategory("File");
                crMain.AddCategory("Points", fileOpen);
                crMain.AddCategory("Physics", fileOpen);
                crMain.AddCategory("Speed", fileOpen);
                crMain.AddCategory("Handling", fileOpen);

                // Update the secondary categories.
                crSecondary.Visible = category != CategoryMain.File;
                crSecondary.ClearCategories();
                switch (category)
                {
                    case CategoryMain.Points:
                        crSecondary.AddCategory("Drivers");
                        crSecondary.AddCategory("Karts");
                        crSecondary.AddCategory("Tires");
                        crSecondary.AddCategory("Gliders");
                        crSecondary.SelectedCategory = _categoryPoints;
                        break;
                    case CategoryMain.Physics:
                        crSecondary.AddCategory("Weight");
                        crSecondary.AddCategory("Acceleration");
                        crSecondary.AddCategory("On-Road Slip");
                        crSecondary.AddCategory("Off-Road Slip");
                        crSecondary.AddCategory("Off-Road Brake");
                        crSecondary.AddCategory("Turbo");
                        crSecondary.SelectedCategory = _categoryPhysics;
                        break;
                    case CategoryMain.Speed:
                    case CategoryMain.Handling:
                        crSecondary.AddCategory("Ground");
                        crSecondary.AddCategory("Water");
                        crSecondary.AddCategory("Anti-Gravity");
                        crSecondary.AddCategory("Gliding");
                        crSecondary.SelectedCategory = _categorySpeedHandling;
                        break;
                }
            }
            else
            {
                // Remember the newly selected secondary category.
                switch ((CategoryMain)crMain.SelectedCategory)
                {
                    case CategoryMain.Points:
                        _categoryPoints = current;
                        break;
                    case CategoryMain.Physics:
                        _categoryPhysics = current;
                        break;
                    case CategoryMain.Speed:
                    case CategoryMain.Handling:
                        _categorySpeedHandling = current;
                        break;
                }
            }
        }

        /// <summary>
        /// Allows custom logic to be applied when the first category is selected.
        /// </summary>
        protected override void ResetCategories()
        {
            _categoryPhysics = 0;
            _categoryPoints = 0;
            _categorySpeedHandling = 0;

            base.ResetCategories();
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
