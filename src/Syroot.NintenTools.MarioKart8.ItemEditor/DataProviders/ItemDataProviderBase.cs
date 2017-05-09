using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    internal abstract class ItemDataProviderBase : DwordArrayGroupDataProvider
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private Section _section;
        private int _set;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal ItemDataProviderBase(Section section, int set)
        {
            _section = section;
            _set = set;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Banana", Program.R.GetBitmap("Items.Banana.png"));
                yield return new TextImagePair("Green Shell", Program.R.GetBitmap("Items.ShellGreen.png"));
                yield return new TextImagePair("Red Shell", Program.R.GetBitmap("Items.ShellRed.png"));
                yield return new TextImagePair("Mushroom", Program.R.GetBitmap("Items.Mushroom.png"));
                yield return new TextImagePair("Bob-omb", Program.R.GetBitmap("Items.Bobomb.png"));
                yield return new TextImagePair("Blooper", Program.R.GetBitmap("Items.Blooper.png"));
                yield return new TextImagePair("Spiny Shell", Program.R.GetBitmap("Items.ShellBlue.png"));
                yield return new TextImagePair("3 Mushrooms", Program.R.GetBitmap("Items.Mushroom3.png"));
                yield return new TextImagePair("Star", Program.R.GetBitmap("Items.Star.png"));
                yield return new TextImagePair("Bullet Bill", Program.R.GetBitmap("Items.Bullet.png"));
                yield return new TextImagePair("Lightning Bolt", Program.R.GetBitmap("Items.Lightning.png"));
                yield return new TextImagePair("Gold Mushroom", Program.R.GetBitmap("Items.MushroomGold.png"));
                yield return new TextImagePair("Fire Flower", Program.R.GetBitmap("Items.Flower.png"));
                yield return new TextImagePair("Piranha Plant", Program.R.GetBitmap("Items.Piranha.png"));
                yield return new TextImagePair("Super Horn", Program.R.GetBitmap("Items.Horn.png"));
                yield return new TextImagePair("Boomerang", Program.R.GetBitmap("Items.Boomerang.png"));
                yield return new TextImagePair("Coin", Program.R.GetBitmap("Items.Coin.png"));
                yield return new TextImagePair("3 Bananas", Program.R.GetBitmap("Items.Banana3.png"));
                yield return new TextImagePair("3 Green Shells", Program.R.GetBitmap("Items.ShellGreen3.png"));
                yield return new TextImagePair("3 Red Shells", Program.R.GetBitmap("Items.ShellRed3.png"));
                if (Program.IsMarioKart8Deluxe)
                {
                    yield return new TextImagePair("Crazy Eight", Program.R.GetBitmap("Items.EightDeluxe.png"));
                    yield return new TextImagePair("Boo", Program.R.GetBitmap("Items.Boo.png"));
                    yield return new TextImagePair("Feather", Program.R.GetBitmap("Items.Feather.png"));
                }
                else
                {
                    yield return new TextImagePair("Crazy Eight", Program.R.GetBitmap("Items.Eight.png"));
                }
            }
        }

        protected override string RowHeaderTitle
        {
            get { return "Distance"; }
        }

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)_section][_set]; }
        }
    }
}
