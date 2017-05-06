using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.EditorUI;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTBD values.
    /// </summary>
    public class PointKartsDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairsMK8 = new List<TextImagePair>()
        {
            new TextImagePair("Standard Kart", Resources.Kart_StandardKart),
            new TextImagePair("Pipe Frame", Resources.Kart_PipeFrame),
            new TextImagePair("Mach 8", Resources.Kart_Mach8),
            new TextImagePair("Steel Driver", Resources.Kart_SteelDriver),
            new TextImagePair("Cat Cruiser", Resources.Kart_CatCruiser),
            new TextImagePair("Circuit Special", Resources.Kart_CircuitSpecial),
            new TextImagePair("Tri-Speeder", Resources.Kart_TriSpeeder),
            new TextImagePair("Badwagon", Resources.Kart_Badwagon),
            new TextImagePair("Prancer", Resources.Kart_Prancer),
            new TextImagePair("Biddybuggy", Resources.Kart_Biddybuggy),
            new TextImagePair("Landship", Resources.Kart_Landship),
            new TextImagePair("Sneeker", Resources.Kart_Sneeker),
            new TextImagePair("Sports Coupe", Resources.Kart_SportsCoupe),
            new TextImagePair("Gold Standard", Resources.Kart_GoldStandard),
            new TextImagePair("Standard Bike", Resources.Kart_StandardBike),
            new TextImagePair("Comet", Resources.Kart_Comet),
            new TextImagePair("Sport Bike", Resources.Kart_SportBike),
            new TextImagePair("The Duke", Resources.Kart_TheDuke),
            new TextImagePair("Flame Rider", Resources.Kart_FlameRider),
            new TextImagePair("Varmint", Resources.Kart_Varmint),
            new TextImagePair("Mr. Scooty", Resources.Kart_MrScooty),
            new TextImagePair("Jet Bike", Resources.Kart_JetBike),
            new TextImagePair("Yoshi Bike", Resources.Kart_YoshiBike),
            new TextImagePair("Standard ATV", Resources.Kart_StandardAtv),
            new TextImagePair("Wild Wiggler", Resources.Kart_WildWiggler),
            new TextImagePair("Teddy Buggy", Resources.Kart_TeddyBuggy),
            new TextImagePair("GLA", Resources.Kart_Gla),
            new TextImagePair("W 25 Silver Arrow", Resources.Kart_W25SilverArrow),
            new TextImagePair("300 SL Roadster", Resources.Kart_300SlRoadster),
            new TextImagePair("Blue Falcon", Resources.Kart_BlueFalcon),
            new TextImagePair("Tanooki Kart", Resources.Kart_TanookiKart),
            new TextImagePair("B Dasher", Resources.Kart_BDasher),
            new TextImagePair("Master Cycle", Resources.Kart_MasterCycle),
            new TextImagePair("Unused 1", Resources.Kart_Unknown),
            new TextImagePair("Unused 2", Resources.Kart_Unknown),
            new TextImagePair("Streetle", Resources.Kart_Streetle),
            new TextImagePair("P-Wing", Resources.Kart_PWing),
            new TextImagePair("City Tripper", Resources.Kart_CityTripper),
            new TextImagePair("Bone Rattler", Resources.Kart_BoneRattler)
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Standard Kart", Resources.Kart_StandardKart),
            new TextImagePair("Pipe Frame", Resources.Kart_PipeFrame),
            new TextImagePair("Mach 8", Resources.Kart_Mach8),
            new TextImagePair("Steel Driver", Resources.Kart_SteelDriver),
            new TextImagePair("Cat Cruiser", Resources.Kart_CatCruiser),
            new TextImagePair("Circuit Special", Resources.Kart_CircuitSpecial),
            new TextImagePair("Tri-Speeder", Resources.Kart_TriSpeeder),
            new TextImagePair("Badwagon", Resources.Kart_Badwagon),
            new TextImagePair("Prancer", Resources.Kart_Prancer),
            new TextImagePair("Biddybuggy", Resources.Kart_Biddybuggy),
            new TextImagePair("Landship", Resources.Kart_Landship),
            new TextImagePair("Sneeker", Resources.Kart_Sneeker),
            new TextImagePair("Sports Coupe", Resources.Kart_SportsCoupe),
            new TextImagePair("Gold Standard", Resources.Kart_GoldStandard),
            new TextImagePair("Standard Bike", Resources.Kart_StandardBike),
            new TextImagePair("Comet", Resources.Kart_Comet),
            new TextImagePair("Sport Bike", Resources.Kart_SportBike),
            new TextImagePair("The Duke", Resources.Kart_TheDuke),
            new TextImagePair("Flame Rider", Resources.Kart_FlameRider),
            new TextImagePair("Varmint", Resources.Kart_Varmint),
            new TextImagePair("Mr. Scooty", Resources.Kart_MrScooty),
            new TextImagePair("Jet Bike", Resources.Kart_JetBike),
            new TextImagePair("Yoshi Bike", Resources.Kart_YoshiBike),
            new TextImagePair("Standard ATV", Resources.Kart_StandardAtv),
            new TextImagePair("Wild Wiggler", Resources.Kart_WildWiggler),
            new TextImagePair("Teddy Buggy", Resources.Kart_TeddyBuggy),
            new TextImagePair("GLA", Resources.Kart_Gla),
            new TextImagePair("W 25 Silver Arrow", Resources.Kart_W25SilverArrow),
            new TextImagePair("300 SL Roadster", Resources.Kart_300SlRoadster),
            new TextImagePair("Blue Falcon", Resources.Kart_BlueFalcon),
            new TextImagePair("Tanooki Kart", Resources.Kart_TanookiKart),
            new TextImagePair("B Dasher", Resources.Kart_BDasher),
            new TextImagePair("Master Cycle", Resources.Kart_MasterCycle),
            new TextImagePair("Inkstriker", Resources.Kart_Inkstriker),
            new TextImagePair("Splat Buggy", Resources.Kart_SplatBuggy),
            new TextImagePair("Streetle", Resources.Kart_Streetle),
            new TextImagePair("P-Wing", Resources.Kart_PWing),
            new TextImagePair("City Tripper", Resources.Kart_CityTripper),
            new TextImagePair("Bone Rattler", Resources.Kart_BoneRattler),
            new TextImagePair("Koopa Clown", Resources.Kart_KoopaClown)
        };
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
            return pairs.Take(DataGroup.Count);
        }
    }
}
