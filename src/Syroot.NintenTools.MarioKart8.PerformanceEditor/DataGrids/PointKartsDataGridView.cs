using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.EditorUI;

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
            new TextImagePair("Standard Kart", Program.R.GetBitmap("Kart_StandardKart.png")),
            new TextImagePair("Pipe Frame", Program.R.GetBitmap("Kart_PipeFrame.png")),
            new TextImagePair("Mach 8", Program.R.GetBitmap("Kart_Mach8.png")),
            new TextImagePair("Steel Driver", Program.R.GetBitmap("Kart_SteelDriver.png")),
            new TextImagePair("Cat Cruiser", Program.R.GetBitmap("Kart_CatCruiser.png")),
            new TextImagePair("Circuit Special", Program.R.GetBitmap("Kart_CircuitSpecial.png")),
            new TextImagePair("Tri-Speeder", Program.R.GetBitmap("Kart_TriSpeeder.png")),
            new TextImagePair("Badwagon", Program.R.GetBitmap("Kart_Badwagon.png")),
            new TextImagePair("Prancer", Program.R.GetBitmap("Kart_Prancer.png")),
            new TextImagePair("Biddybuggy", Program.R.GetBitmap("Kart_Biddybuggy.png")),
            new TextImagePair("Landship", Program.R.GetBitmap("Kart_Landship.png")),
            new TextImagePair("Sneeker", Program.R.GetBitmap("Kart_Sneeker.png")),
            new TextImagePair("Sports Coupe", Program.R.GetBitmap("Kart_SportsCoupe.png")),
            new TextImagePair("Gold Standard", Program.R.GetBitmap("Kart_GoldStandard.png")),
            new TextImagePair("Standard Bike", Program.R.GetBitmap("Kart_StandardBike.png")),
            new TextImagePair("Comet", Program.R.GetBitmap("Kart_Comet.png")),
            new TextImagePair("Sport Bike", Program.R.GetBitmap("Kart_SportBike.png")),
            new TextImagePair("The Duke", Program.R.GetBitmap("Kart_TheDuke.png")),
            new TextImagePair("Flame Rider", Program.R.GetBitmap("Kart_FlameRider.png")),
            new TextImagePair("Varmint", Program.R.GetBitmap("Kart_Varmint.png")),
            new TextImagePair("Mr. Scooty", Program.R.GetBitmap("Kart_MrScooty.png")),
            new TextImagePair("Jet Bike", Program.R.GetBitmap("Kart_JetBike.png")),
            new TextImagePair("Yoshi Bike", Program.R.GetBitmap("Kart_YoshiBike.png")),
            new TextImagePair("Standard ATV", Program.R.GetBitmap("Kart_StandardAtv.png")),
            new TextImagePair("Wild Wiggler", Program.R.GetBitmap("Kart_WildWiggler.png")),
            new TextImagePair("Teddy Buggy", Program.R.GetBitmap("Kart_TeddyBuggy.png")),
            new TextImagePair("GLA", Program.R.GetBitmap("Kart_Gla.png")),
            new TextImagePair("W 25 Silver Arrow", Program.R.GetBitmap("Kart_W25SilverArrow.png")),
            new TextImagePair("300 SL Roadster", Program.R.GetBitmap("Kart_300SlRoadster.png")),
            new TextImagePair("Blue Falcon", Program.R.GetBitmap("Kart_BlueFalcon.png")),
            new TextImagePair("Tanooki Kart", Program.R.GetBitmap("Kart_TanookiKart.png")),
            new TextImagePair("B Dasher", Program.R.GetBitmap("Kart_BDasher.png")),
            new TextImagePair("Master Cycle", Program.R.GetBitmap("Kart_MasterCycle.png")),
            new TextImagePair("Unused 1", Program.R.GetBitmap("Kart_Unknown.png")),
            new TextImagePair("Unused 2", Program.R.GetBitmap("Kart_Unknown.png")),
            new TextImagePair("Streetle", Program.R.GetBitmap("Kart_Streetle.png")),
            new TextImagePair("P-Wing", Program.R.GetBitmap("Kart_PWing.png")),
            new TextImagePair("City Tripper", Program.R.GetBitmap("Kart_CityTripper.png")),
            new TextImagePair("Bone Rattler", Program.R.GetBitmap("Kart_BoneRattler.png"))
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Standard Kart", Program.R.GetBitmap("Kart_StandardKart.png")),
            new TextImagePair("Pipe Frame", Program.R.GetBitmap("Kart_PipeFrame.png")),
            new TextImagePair("Mach 8", Program.R.GetBitmap("Kart_Mach8.png")),
            new TextImagePair("Steel Driver", Program.R.GetBitmap("Kart_SteelDriver.png")),
            new TextImagePair("Cat Cruiser", Program.R.GetBitmap("Kart_CatCruiser.png")),
            new TextImagePair("Circuit Special", Program.R.GetBitmap("Kart_CircuitSpecial.png")),
            new TextImagePair("Tri-Speeder", Program.R.GetBitmap("Kart_TriSpeeder.png")),
            new TextImagePair("Badwagon", Program.R.GetBitmap("Kart_Badwagon.png")),
            new TextImagePair("Prancer", Program.R.GetBitmap("Kart_Prancer.png")),
            new TextImagePair("Biddybuggy", Program.R.GetBitmap("Kart_Biddybuggy.png")),
            new TextImagePair("Landship", Program.R.GetBitmap("Kart_Landship.png")),
            new TextImagePair("Sneeker", Program.R.GetBitmap("Kart_Sneeker.png")),
            new TextImagePair("Sports Coupe", Program.R.GetBitmap("Kart_SportsCoupe.png")),
            new TextImagePair("Gold Standard", Program.R.GetBitmap("Kart_GoldStandard.png")),
            new TextImagePair("Standard Bike", Program.R.GetBitmap("Kart_StandardBike.png")),
            new TextImagePair("Comet", Program.R.GetBitmap("Kart_Comet.png")),
            new TextImagePair("Sport Bike", Program.R.GetBitmap("Kart_SportBike.png")),
            new TextImagePair("The Duke", Program.R.GetBitmap("Kart_TheDuke.png")),
            new TextImagePair("Flame Rider", Program.R.GetBitmap("Kart_FlameRider.png")),
            new TextImagePair("Varmint", Program.R.GetBitmap("Kart_Varmint.png")),
            new TextImagePair("Mr. Scooty", Program.R.GetBitmap("Kart_MrScooty.png")),
            new TextImagePair("Jet Bike", Program.R.GetBitmap("Kart_JetBike.png")),
            new TextImagePair("Yoshi Bike", Program.R.GetBitmap("Kart_YoshiBike.png")),
            new TextImagePair("Standard ATV", Program.R.GetBitmap("Kart_StandardAtv.png")),
            new TextImagePair("Wild Wiggler", Program.R.GetBitmap("Kart_WildWiggler.png")),
            new TextImagePair("Teddy Buggy", Program.R.GetBitmap("Kart_TeddyBuggy.png")),
            new TextImagePair("GLA", Program.R.GetBitmap("Kart_Gla.png")),
            new TextImagePair("W 25 Silver Arrow", Program.R.GetBitmap("Kart_W25SilverArrow.png")),
            new TextImagePair("300 SL Roadster", Program.R.GetBitmap("Kart_300SlRoadster.png")),
            new TextImagePair("Blue Falcon", Program.R.GetBitmap("Kart_BlueFalcon.png")),
            new TextImagePair("Tanooki Kart", Program.R.GetBitmap("Kart_TanookiKart.png")),
            new TextImagePair("B Dasher", Program.R.GetBitmap("Kart_BDasher.png")),
            new TextImagePair("Master Cycle", Program.R.GetBitmap("Kart_MasterCycle.png")),
            new TextImagePair("Inkstriker", Program.R.GetBitmap("Kart_Inkstriker.png")),
            new TextImagePair("Splat Buggy", Program.R.GetBitmap("Kart_SplatBuggy.png")),
            new TextImagePair("Streetle", Program.R.GetBitmap("Kart_Streetle.png")),
            new TextImagePair("P-Wing", Program.R.GetBitmap("Kart_PWing.png")),
            new TextImagePair("City Tripper", Program.R.GetBitmap("Kart_CityTripper.png")),
            new TextImagePair("Bone Rattler", Program.R.GetBitmap("Kart_BoneRattler.png")),
            new TextImagePair("Koopa Clown", Program.R.GetBitmap("Kart_KoopaClown.png"))
        };
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
            return pairs.Take(DataGroup.Count);
        }
    }
}
