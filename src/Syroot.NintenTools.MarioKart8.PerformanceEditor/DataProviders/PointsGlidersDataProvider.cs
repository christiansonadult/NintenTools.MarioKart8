using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal class PointsGlidersDataProvider : PointsDataProviderBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairs = new List<TextImagePair>()
        {
            new TextImagePair("Super Glider", Program.R.GetBitmap("Gliders.SuperGlider.png")),
            new TextImagePair("Cloud Glider", Program.R.GetBitmap("Gliders.CloudGlider.png")),
            new TextImagePair("Wario Wing", Program.R.GetBitmap("Gliders.WarioWing.png")),
            new TextImagePair("Waddle Wing", Program.R.GetBitmap("Gliders.WaddleWing.png")),
            new TextImagePair("Peach Parasol", Program.R.GetBitmap("Gliders.PeachParasol.png")),
            new TextImagePair("Parachute", Program.R.GetBitmap("Gliders.Parachute.png")),
            new TextImagePair("Parafoil", Program.R.GetBitmap("Gliders.Parafoil.png")),
            new TextImagePair("Flower Glider", Program.R.GetBitmap("Gliders.FlowerGlider.png")),
            new TextImagePair("Bowser Kite", Program.R.GetBitmap("Gliders.BowserKite.png")),
            new TextImagePair("Plane Glider", Program.R.GetBitmap("Gliders.PlaneGlider.png")),
            new TextImagePair("MKTV Parafoil", Program.R.GetBitmap("Gliders.MktvParafoil.png")),
            new TextImagePair("Gold Glider", Program.R.GetBitmap("Gliders.GoldGlider.png")),
            new TextImagePair("Hylian Kite", Program.R.GetBitmap("Gliders.HylianKite.png")),
            new TextImagePair("Paper Glider", Program.R.GetBitmap("Gliders.PaperGlider.png"))
        };

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.GliderPoints][0]; }
        }
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get { return _textImagePairs.Take(DataGroup.Count); }
        }

        protected override string RowHeaderTitle
        {
            get { return "Glider"; }
        }
    }
}
