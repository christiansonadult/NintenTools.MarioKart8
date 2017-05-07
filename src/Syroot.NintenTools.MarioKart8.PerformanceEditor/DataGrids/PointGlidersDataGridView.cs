using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTWG values.
    /// </summary>
    public class PointGlidersDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairs = new List<TextImagePair>()
        {
            new TextImagePair("Super Glider", Program.R.GetBitmap("Glider_SuperGlider.png")),
            new TextImagePair("Cloud Glider", Program.R.GetBitmap("Glider_CloudGlider.png")),
            new TextImagePair("Wario Wing", Program.R.GetBitmap("Glider_WarioWing.png")),
            new TextImagePair("Waddle Wing", Program.R.GetBitmap("Glider_WaddleWing.png")),
            new TextImagePair("Peach Parasol", Program.R.GetBitmap("Glider_PeachParasol.png")),
            new TextImagePair("Parachute", Program.R.GetBitmap("Glider_Parachute.png")),
            new TextImagePair("Parafoil", Program.R.GetBitmap("Glider_Parafoil.png")),
            new TextImagePair("Flower Glider", Program.R.GetBitmap("Glider_FlowerGlider.png")),
            new TextImagePair("Bowser Kite", Program.R.GetBitmap("Glider_BowserKite.png")),
            new TextImagePair("Plane Glider", Program.R.GetBitmap("Glider_PlaneGlider.png")),
            new TextImagePair("MKTV Parafoil", Program.R.GetBitmap("Glider_MktvParafoil.png")),
            new TextImagePair("Gold Glider", Program.R.GetBitmap("Glider_GoldGlider.png")),
            new TextImagePair("Hylian Kite", Program.R.GetBitmap("Glider_HylianKite.png")),
            new TextImagePair("Paper Glider", Program.R.GetBitmap("Glider_PaperGlider.png"))
        };

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            return _textImagePairs.Take(DataGroup.Count);
        }
    }
}
