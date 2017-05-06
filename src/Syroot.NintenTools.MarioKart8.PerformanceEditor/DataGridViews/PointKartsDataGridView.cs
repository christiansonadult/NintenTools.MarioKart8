using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTBD values.
    /// </summary>
    public class PointKartsDataGridView : PointSetDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            yield return new TextImagePair("Standard Kart", Resources.Kart_StandardKart);
            yield return new TextImagePair("Pipe Frame", Resources.Kart_PipeFrame);
            yield return new TextImagePair("Mach 8", Resources.Kart_Mach8);
            yield return new TextImagePair("Steel Driver", Resources.Kart_SteelDriver);
            yield return new TextImagePair("Cat Cruiser", Resources.Kart_CatCruiser);
            yield return new TextImagePair("Circuit Special", Resources.Kart_CircuitSpecial);
            yield return new TextImagePair("Tri-Speeder", Resources.Kart_TriSpeeder);
            yield return new TextImagePair("Badwagon", Resources.Kart_Badwagon);
            yield return new TextImagePair("Prancer", Resources.Kart_Prancer);
            yield return new TextImagePair("Biddybuggy", Resources.Kart_Biddybuggy);
            yield return new TextImagePair("Landship", Resources.Kart_Landship);
            yield return new TextImagePair("Sneeker", Resources.Kart_Sneeker);
            yield return new TextImagePair("Sports Coupe", Resources.Kart_SportsCoupe);
            yield return new TextImagePair("Gold Standard", Resources.Kart_GoldStandard);
            yield return new TextImagePair("Standard Bike", Resources.Kart_StandardBike);
            yield return new TextImagePair("Comet", Resources.Kart_Comet);
            yield return new TextImagePair("Sport Bike", Resources.Kart_SportBike);
            yield return new TextImagePair("The Duke", Resources.Kart_TheDuke);
            yield return new TextImagePair("Flame Rider", Resources.Kart_FlameRider);
            yield return new TextImagePair("Varmint", Resources.Kart_Varmint);
            yield return new TextImagePair("Mr. Scooty", Resources.Kart_MrScooty);
            yield return new TextImagePair("Jet Bike", Resources.Kart_JetBike);
            yield return new TextImagePair("Yoshi Bike", Resources.Kart_YoshiBike);
            yield return new TextImagePair("Standard ATV", Resources.Kart_StandardAtv);
            yield return new TextImagePair("Wild Wiggler", Resources.Kart_WildWiggler);
            yield return new TextImagePair("Teddy Buggy", Resources.Kart_TeddyBuggy);
            yield return new TextImagePair("GLA", Resources.Kart_Gla);
            yield return new TextImagePair("W 25 Silver Arrow", Resources.Kart_W25SilverArrow);
            yield return new TextImagePair("300 SL Roadster", Resources.Kart_300SlRoadster);
            yield return new TextImagePair("Blue Falcon", Resources.Kart_BlueFalcon);
            yield return new TextImagePair("Tanooki Kart", Resources.Kart_TanookiKart);
            yield return new TextImagePair("B Dasher", Resources.Kart_BDasher);
            yield return new TextImagePair("Master Cycle", Resources.Kart_MasterCycle);
            yield return new TextImagePair("Unused 1", Resources.Kart_Unknown);
            yield return new TextImagePair("Unused 2", Resources.Kart_Unknown);
            yield return new TextImagePair("Streetle", Resources.Kart_Streetle);
            yield return new TextImagePair("P-Wing", Resources.Kart_PWing);
            yield return new TextImagePair("City Tripper", Resources.Kart_CityTripper);
            yield return new TextImagePair("Bone Rattler", Resources.Kart_BoneRattler);
        }
    }
}
