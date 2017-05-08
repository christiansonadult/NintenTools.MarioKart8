using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal class PointsDriversDataProvider : PointsDataProviderBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairsMK8 = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Program.R.GetBitmap("Drivers.Mario.png")),
            new TextImagePair("Luigi", Program.R.GetBitmap("Drivers.Luigi.png")),
            new TextImagePair("Peach", Program.R.GetBitmap("Drivers.Peach.png")),
            new TextImagePair("Daisy", Program.R.GetBitmap("Drivers.Daisy.png")),
            new TextImagePair("Yoshi", Program.R.GetBitmap("Drivers.Yoshi.png")),
            new TextImagePair("Toad", Program.R.GetBitmap("Drivers.Toad.png")),
            new TextImagePair("Toadette", Program.R.GetBitmap("Drivers.Toadette.png")),
            new TextImagePair("Koopa", Program.R.GetBitmap("Drivers.Koopa.png")),
            new TextImagePair("Bowser", Program.R.GetBitmap("Drivers.Bowser.png")),
            new TextImagePair("Donkey Kong", Program.R.GetBitmap("Drivers.DonkeyKong.png")),
            new TextImagePair("Wario", Program.R.GetBitmap("Drivers.Wario.png")),
            new TextImagePair("Waluigi", Program.R.GetBitmap("Drivers.Waluigi.png")),
            new TextImagePair("Rosalina", Program.R.GetBitmap("Drivers.Rosalina.png")),
            new TextImagePair("Metal Mario", Program.R.GetBitmap("Drivers.MetalMario.png")),
            new TextImagePair("Pink Gold Peach", Program.R.GetBitmap("Drivers.PinkGoldPeach.png")),
            new TextImagePair("Lakitu", Program.R.GetBitmap("Drivers.Lakitu.png")),
            new TextImagePair("Shy Guy", Program.R.GetBitmap("Drivers.ShyGuy.png")),
            new TextImagePair("Baby Mario", Program.R.GetBitmap("Drivers.BabyMario.png")),
            new TextImagePair("Baby Luigi", Program.R.GetBitmap("Drivers.BabyLuigi.png")),
            new TextImagePair("Baby Peach", Program.R.GetBitmap("Drivers.BabyPeach.png")),
            new TextImagePair("Baby Daisy", Program.R.GetBitmap("Drivers.BabyDaisy.png")),
            new TextImagePair("Baby Rosalina", Program.R.GetBitmap("Drivers.BabyRosalina.png")),
            new TextImagePair("Larry", Program.R.GetBitmap("Drivers.Larry.png")),
            new TextImagePair("Lemmy", Program.R.GetBitmap("Drivers.Lemmy.png")),
            new TextImagePair("Wendy", Program.R.GetBitmap("Drivers.Wendy.png")),
            new TextImagePair("Ludwig", Program.R.GetBitmap("Drivers.Ludwig.png")),
            new TextImagePair("Iggy", Program.R.GetBitmap("Drivers.Iggy.png")),
            new TextImagePair("Roy", Program.R.GetBitmap("Drivers.Roy.png")),
            new TextImagePair("Morton", Program.R.GetBitmap("Drivers.Morton.png")),
            new TextImagePair("Mii", Program.R.GetBitmap("Drivers.Mii.png")),
            new TextImagePair("Tanooki Mario", Program.R.GetBitmap("Drivers.TanookiMario.png")),
            new TextImagePair("Link", Program.R.GetBitmap("Drivers.Link.png")),
            new TextImagePair("Villager Male", Program.R.GetBitmap("Drivers.VillagerMale.png")),
            new TextImagePair("Isabelle", Program.R.GetBitmap("Drivers.Isabelle.png")),
            new TextImagePair("Cat Peach", Program.R.GetBitmap("Drivers.CatPeach.png")),
            new TextImagePair("Dry Bowser", Program.R.GetBitmap("Drivers.DryBowser.png")),
            new TextImagePair("Villager Female", Program.R.GetBitmap("Drivers.VillagerFemale.png"))
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Program.R.GetBitmap("Drivers.Mario.png")),
            new TextImagePair("Luigi", Program.R.GetBitmap("Drivers.Luigi.png")),
            new TextImagePair("Peach", Program.R.GetBitmap("Drivers.Peach.png")),
            new TextImagePair("Daisy", Program.R.GetBitmap("Drivers.Daisy.png")),
            new TextImagePair("Yoshi", Program.R.GetBitmap("Drivers.Yoshi.png")),
            new TextImagePair("Toad", Program.R.GetBitmap("Drivers.Toad.png")),
            new TextImagePair("Toadette", Program.R.GetBitmap("Drivers.Toadette.png")),
            new TextImagePair("Koopa", Program.R.GetBitmap("Drivers.Koopa.png")),
            new TextImagePair("Bowser", Program.R.GetBitmap("Drivers.Bowser.png")),
            new TextImagePair("Donkey Kong", Program.R.GetBitmap("Drivers.DonkeyKong.png")),
            new TextImagePair("Wario", Program.R.GetBitmap("Drivers.Wario.png")),
            new TextImagePair("Waluigi", Program.R.GetBitmap("Drivers.Waluigi.png")),
            new TextImagePair("Rosalina", Program.R.GetBitmap("Drivers.Rosalina.png")),
            new TextImagePair("Metal Mario", Program.R.GetBitmap("Drivers.MetalMario.png")),
            new TextImagePair("Pink Gold Peach", Program.R.GetBitmap("Drivers.PinkGoldPeach.png")),
            new TextImagePair("Lakitu", Program.R.GetBitmap("Drivers.Lakitu.png")),
            new TextImagePair("Shy Guy", Program.R.GetBitmap("Drivers.ShyGuy.png")),
            new TextImagePair("Baby Mario", Program.R.GetBitmap("Drivers.BabyMario.png")),
            new TextImagePair("Baby Luigi", Program.R.GetBitmap("Drivers.BabyLuigi.png")),
            new TextImagePair("Baby Peach", Program.R.GetBitmap("Drivers.BabyPeach.png")),
            new TextImagePair("Baby Daisy", Program.R.GetBitmap("Drivers.BabyDaisy.png")),
            new TextImagePair("Baby Rosalina", Program.R.GetBitmap("Drivers.BabyRosalina.png")),
            new TextImagePair("Larry", Program.R.GetBitmap("Drivers.Larry.png")),
            new TextImagePair("Lemmy", Program.R.GetBitmap("Drivers.Lemmy.png")),
            new TextImagePair("Wendy", Program.R.GetBitmap("Drivers.Wendy.png")),
            new TextImagePair("Ludwig", Program.R.GetBitmap("Drivers.Ludwig.png")),
            new TextImagePair("Iggy", Program.R.GetBitmap("Drivers.Iggy.png")),
            new TextImagePair("Roy", Program.R.GetBitmap("Drivers.Roy.png")),
            new TextImagePair("Morton", Program.R.GetBitmap("Drivers.Morton.png")),
            new TextImagePair("Mii", Program.R.GetBitmap("Drivers.Mii.png")),
            new TextImagePair("Tanooki Mario", Program.R.GetBitmap("Drivers.TanookiMario.png")),
            new TextImagePair("Link", Program.R.GetBitmap("Drivers.Link.png")),
            new TextImagePair("Villager Male", Program.R.GetBitmap("Drivers.VillagerMale.png")),
            new TextImagePair("Isabelle", Program.R.GetBitmap("Drivers.Isabelle.png")),
            new TextImagePair("Cat Peach", Program.R.GetBitmap("Drivers.CatPeach.png")),
            new TextImagePair("Dry Bowser", Program.R.GetBitmap("Drivers.DryBowser.png")),
            new TextImagePair("Villager Female", Program.R.GetBitmap("Drivers.VillagerFemale.png")),
            new TextImagePair("King Boo", Program.R.GetBitmap("Drivers.KingBoo.png")),
            new TextImagePair("Dry Bones", Program.R.GetBitmap("Drivers.DryBones.png")),
            new TextImagePair("Bowser Jr.", Program.R.GetBitmap("Drivers.BowserJr.png")),
            new TextImagePair("Inkling Boy", Program.R.GetBitmap("Drivers.InklingMale.png")),
            new TextImagePair("Inkling Girl", Program.R.GetBitmap("Drivers.InklingFemale.png"))
        };
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.DriverPoints][0]; }
        }
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
                return pairs.Take(DataGroup.Count);
            }
        }

        protected override string RowHeaderTitle
        {
            get { return "Driver"; }
        }
    }
}
