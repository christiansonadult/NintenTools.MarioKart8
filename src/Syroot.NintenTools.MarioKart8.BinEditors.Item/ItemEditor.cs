using System;
using System.Drawing;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.BinEditors.DataProvider;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Item
{
    /// <summary>
    /// Represents the main window of the application.
    /// </summary>
    internal partial class ItemEditor : EditorAppBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------
        
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
        
        private int _itemSet;
        private int _playerType;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemEditor"/> class with the given command line
        /// <paramref name="args"/>.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal ItemEditor(string[] args) : base(args)
        {
        }

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
            get { return "MK8 Item Editor"; }
        }

        /// <summary>
        /// Gets the accentual color used for category rows in the user interface.
        /// </summary>
        protected override Color AccentColor
        {
            get { return Color.FromArgb(200, 66, 0); }
        }

        /// <summary>
        /// Gets the number of category rows available.
        /// </summary>
        protected override int CategoryRowCount
        {
            get { return 3; }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        /// <summary>
        /// Called when the BIN file should be checked if it can be edited.
        /// </summary>
        protected override void CheckBinFile()
        {
            if (BinFile.Identifier != "ITEM")
            {
                throw new InvalidOperationException("This is not an Item BIN file.");
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
            switch ((CategoryMain)categoryIndices[0])
            {
                case CategoryMain.Versus:
                    if (categoryIndices[1] == (int)CategorySecond.Distances)
                    {
                        return _dataProviders[(int)CategoryMain.Versus][(int)CategorySecond.Distances][0];
                    }
                    else
                    {
                        return _dataProviders[(int)CategoryMain.Versus][_itemSet][_playerType];
                    }
                case CategoryMain.Battle:
                    return _dataProviders[(int)CategoryMain.Battle][_itemSet][_playerType];
            }
            return _dataProviders[categoryIndices[0]]?[categoryIndices[1]]?[categoryIndices[2]];
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
            CategoryRow crTertiary = categoryRows[2];

            if (changedRow == 0)
            {
                // Update the main categories.
                bool fileOpen = BinFile != null;
                CategoryMain category = (CategoryMain)current;
                CategoryMain categoryPrevious = (CategoryMain)previous;

                crMain.ClearCategories();
                crMain.AddCategory("File");
                crMain.AddCategory("Versus Races", fileOpen);
                crMain.AddCategory("Battle Mode", fileOpen);

                // Update the secondary categories.
                crSecondary.ClearCategories();
                switch (category)
                {
                    case CategoryMain.Versus:
                        crSecondary.AddCategory("All Items");
                        crSecondary.AddCategory("Mushrooms Only");
                        crSecondary.AddCategory("Shells Only");
                        crSecondary.AddCategory("Bananas Only");
                        crSecondary.AddCategory("Bob-ombs Only");
                        crSecondary.AddCategory("Frantic Mode");
                        crSecondary.AddCategory("Grand Prix");
                        crSecondary.AddCategory("Distances");
                        break;
                    case CategoryMain.Battle:
                        crSecondary.AddCategory("All Items");
                        crSecondary.AddCategory("Mushrooms Only");
                        crSecondary.AddCategory("Shells Only");
                        crSecondary.AddCategory("Bananas Only");
                        crSecondary.AddCategory("Bob-ombs Only");
                        crSecondary.AddCategory("Frantic Mode");
                        break;
                }
                if ((CategoryMain)crMain.SelectedCategory == CategoryMain.Battle)
                {
                    _itemSet = Math.Min((int)CategorySecond.Frantic, _itemSet);
                }
                crSecondary.SelectedCategory = _itemSet;
            }
            else if (changedRow == 1)
            {
                // Update the tertiary categories.
                crTertiary.Visible = (CategorySecond)current != CategorySecond.Distances;
                crTertiary.ClearCategories();
                crTertiary.AddCategory("Human Player");
                crTertiary.AddCategory("Software Player");
                crTertiary.SelectedCategory = _playerType;

                // Remember the newly selected secondary category.
                _itemSet = current;
            }
            else if (changedRow == 2)
            {
                // Remember the newly selected tertiary category.
                _playerType = current;
            }
            
            crSecondary.Visible = (CategoryMain)crMain.SelectedCategory != CategoryMain.File;
            crTertiary.Visible = crSecondary.Visible
                && (CategorySecond)crSecondary.SelectedCategory != CategorySecond.Distances;
        }

        /// <summary>
        /// Allows custom logic to be applied when the first category is selected.
        /// </summary>
        protected override void ResetCategories()
        {
            _itemSet = 0;
            _playerType = 0;

            base.ResetCategories();
        }
        
        // ---- ENUMERATIONS -------------------------------------------------------------------------------------------

        private enum CategoryMain
        {
            File,
            Versus,
            Battle
        }

        private enum CategorySecond
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
    }
}
