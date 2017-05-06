using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

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
            new TextImagePair("Super Glider", Resources.Glider_SuperGlider),
            new TextImagePair("Cloud Glider", Resources.Glider_CloudGlider),
            new TextImagePair("Wario Wing", Resources.Glider_WarioWing),
            new TextImagePair("Waddle Wing", Resources.Glider_WaddleWing),
            new TextImagePair("Peach Parasol", Resources.Glider_PeachParasol),
            new TextImagePair("Parachute", Resources.Glider_Parachute),
            new TextImagePair("Parafoil", Resources.Glider_Parafoil),
            new TextImagePair("Flower Glider", Resources.Glider_FlowerGlider),
            new TextImagePair("Bowser Kite", Resources.Glider_BowserKite),
            new TextImagePair("Plane Glider", Resources.Glider_PlaneGlider),
            new TextImagePair("MKTV Parafoil", Resources.Glider_MktvParafoil),
            new TextImagePair("Gold Glider", Resources.Glider_GoldGlider),
            new TextImagePair("Hylian Kite", Resources.Glider_HylianKite),
            new TextImagePair("Paper Glider", Resources.Glider_PaperGlider),
        };

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            return _textImagePairs.Take(DataGroup.Count);
        }
    }
}
