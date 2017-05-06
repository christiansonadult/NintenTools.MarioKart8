using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTWG values.
    /// </summary>
    public class PointGlidersDataGridView : PointSetDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            yield return new TextImagePair("Super Glider", Resources.Glider_SuperGlider);
            yield return new TextImagePair("Cloud Glider", Resources.Glider_CloudGlider);
            yield return new TextImagePair("Wario Wing", Resources.Glider_WarioWing);
            yield return new TextImagePair("Waddle Wing", Resources.Glider_WaddleWing);
            yield return new TextImagePair("Peach Parasol", Resources.Glider_PeachParasol);
            yield return new TextImagePair("Parachute", Resources.Glider_Parachute);
            yield return new TextImagePair("Parafoil", Resources.Glider_Parafoil);
            yield return new TextImagePair("Flower Glider", Resources.Glider_FlowerGlider);
            yield return new TextImagePair("Bowser Kite", Resources.Glider_BowserKite);
            yield return new TextImagePair("Plane Glider", Resources.Glider_PlaneGlider);
            yield return new TextImagePair("MKTV Parafoil", Resources.Glider_MktvParafoil);
            yield return new TextImagePair("Gold Glider", Resources.Glider_GoldGlider);
            yield return new TextImagePair("Hylian Kite", Resources.Glider_HylianKite);
            yield return new TextImagePair("Paper Glider", Resources.Glider_PaperGlider);
        }
    }
}
