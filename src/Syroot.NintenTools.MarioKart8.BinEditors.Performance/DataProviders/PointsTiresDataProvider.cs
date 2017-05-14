using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Performance
{
    internal class PointsTiresDataProvider : PointsDataProviderBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairs = new List<TextImagePair>()
        {
            new TextImagePair("Standard", Program.R.GetBitmap("Tires.Standard.png")),
            new TextImagePair("Monster", Program.R.GetBitmap("Tires.Monster.png")),
            new TextImagePair("Roller", Program.R.GetBitmap("Tires.Roller.png")),
            new TextImagePair("Slim", Program.R.GetBitmap("Tires.Slim.png")),
            new TextImagePair("Slick", Program.R.GetBitmap("Tires.Slick.png")),
            new TextImagePair("Metal", Program.R.GetBitmap("Tires.Metal.png")),
            new TextImagePair("Button", Program.R.GetBitmap("Tires.Button.png")),
            new TextImagePair("Off-Road", Program.R.GetBitmap("Tires.OffRoad.png")),
            new TextImagePair("Sponge", Program.R.GetBitmap("Tires.Sponge.png")),
            new TextImagePair("Wood", Program.R.GetBitmap("Tires.Wood.png")),
            new TextImagePair("Cushion", Program.R.GetBitmap("Tires.Cushion.png")),
            new TextImagePair("Blue Standard", Program.R.GetBitmap("Tires.BlueStandard.png")),
            new TextImagePair("Hot Monster", Program.R.GetBitmap("Tires.HotMonster.png")),
            new TextImagePair("Azure Roller", Program.R.GetBitmap("Tires.AzureRoller.png")),
            new TextImagePair("Crimson Slim", Program.R.GetBitmap("Tires.CrimsonSlim.png")),
            new TextImagePair("Cyber Slick", Program.R.GetBitmap("Tires.CyberSlick.png")),
            new TextImagePair("Retro Off-Road", Program.R.GetBitmap("Tires.RetroOffRoad.png")),
            new TextImagePair("Gold Tires", Program.R.GetBitmap("Tires.GoldTires.png")),
            new TextImagePair("GLA Tires", Program.R.GetBitmap("Tires.GlaTires.png")),
            new TextImagePair("Triforce Tires", Program.R.GetBitmap("Tires.TriforceTires.png")),
            new TextImagePair("Leaf Tires", Program.R.GetBitmap("Tires.LeafTires.png"))
        };

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.Editor.BinFile[(int)Section.TirePoints][0]; }
        }
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get { return _textImagePairs.Take(DataGroup.Count); }
        }

        protected override string RowHeaderTitle
        {
            get { return "Tire"; }
        }
    }
}
