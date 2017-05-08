using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal class PointsKartsDataProvider : PointsDataProviderBase
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairsMK8 = new List<TextImagePair>()
        {
            new TextImagePair("Standard Kart", Program.R.GetBitmap("Karts.StandardKart.png")),
            new TextImagePair("Pipe Frame", Program.R.GetBitmap("Karts.PipeFrame.png")),
            new TextImagePair("Mach 8", Program.R.GetBitmap("Karts.Mach8.png")),
            new TextImagePair("Steel Driver", Program.R.GetBitmap("Karts.SteelDriver.png")),
            new TextImagePair("Cat Cruiser", Program.R.GetBitmap("Karts.CatCruiser.png")),
            new TextImagePair("Circuit Special", Program.R.GetBitmap("Karts.CircuitSpecial.png")),
            new TextImagePair("Tri-Speeder", Program.R.GetBitmap("Karts.TriSpeeder.png")),
            new TextImagePair("Badwagon", Program.R.GetBitmap("Karts.Badwagon.png")),
            new TextImagePair("Prancer", Program.R.GetBitmap("Karts.Prancer.png")),
            new TextImagePair("Biddybuggy", Program.R.GetBitmap("Karts.Biddybuggy.png")),
            new TextImagePair("Landship", Program.R.GetBitmap("Karts.Landship.png")),
            new TextImagePair("Sneeker", Program.R.GetBitmap("Karts.Sneeker.png")),
            new TextImagePair("Sports Coupe", Program.R.GetBitmap("Karts.SportsCoupe.png")),
            new TextImagePair("Gold Standard", Program.R.GetBitmap("Karts.GoldStandard.png")),
            new TextImagePair("Standard Bike", Program.R.GetBitmap("Karts.StandardBike.png")),
            new TextImagePair("Comet", Program.R.GetBitmap("Karts.Comet.png")),
            new TextImagePair("Sport Bike", Program.R.GetBitmap("Karts.SportBike.png")),
            new TextImagePair("The Duke", Program.R.GetBitmap("Karts.TheDuke.png")),
            new TextImagePair("Flame Rider", Program.R.GetBitmap("Karts.FlameRider.png")),
            new TextImagePair("Varmint", Program.R.GetBitmap("Karts.Varmint.png")),
            new TextImagePair("Mr. Scooty", Program.R.GetBitmap("Karts.MrScooty.png")),
            new TextImagePair("Jet Bike", Program.R.GetBitmap("Karts.JetBike.png")),
            new TextImagePair("Yoshi Bike", Program.R.GetBitmap("Karts.YoshiBike.png")),
            new TextImagePair("Standard ATV", Program.R.GetBitmap("Karts.StandardAtv.png")),
            new TextImagePair("Wild Wiggler", Program.R.GetBitmap("Karts.WildWiggler.png")),
            new TextImagePair("Teddy Buggy", Program.R.GetBitmap("Karts.TeddyBuggy.png")),
            new TextImagePair("GLA", Program.R.GetBitmap("Karts.Gla.png")),
            new TextImagePair("W 25 Silver Arrow", Program.R.GetBitmap("Karts.W25SilverArrow.png")),
            new TextImagePair("300 SL Roadster", Program.R.GetBitmap("Karts.300SlRoadster.png")),
            new TextImagePair("Blue Falcon", Program.R.GetBitmap("Karts.BlueFalcon.png")),
            new TextImagePair("Tanooki Kart", Program.R.GetBitmap("Karts.TanookiKart.png")),
            new TextImagePair("B Dasher", Program.R.GetBitmap("Karts.BDasher.png")),
            new TextImagePair("Master Cycle", Program.R.GetBitmap("Karts.MasterCycle.png")),
            new TextImagePair("Unused 1", Program.R.GetBitmap("Karts.Unknown.png")),
            new TextImagePair("Unused 2", Program.R.GetBitmap("Karts.Unknown.png")),
            new TextImagePair("Streetle", Program.R.GetBitmap("Karts.Streetle.png")),
            new TextImagePair("P-Wing", Program.R.GetBitmap("Karts.PWing.png")),
            new TextImagePair("City Tripper", Program.R.GetBitmap("Karts.CityTripper.png")),
            new TextImagePair("Bone Rattler", Program.R.GetBitmap("Karts.BoneRattler.png"))
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Standard Kart", Program.R.GetBitmap("Karts.StandardKart.png")),
            new TextImagePair("Pipe Frame", Program.R.GetBitmap("Karts.PipeFrame.png")),
            new TextImagePair("Mach 8", Program.R.GetBitmap("Karts.Mach8.png")),
            new TextImagePair("Steel Driver", Program.R.GetBitmap("Karts.SteelDriver.png")),
            new TextImagePair("Cat Cruiser", Program.R.GetBitmap("Karts.CatCruiser.png")),
            new TextImagePair("Circuit Special", Program.R.GetBitmap("Karts.CircuitSpecial.png")),
            new TextImagePair("Tri-Speeder", Program.R.GetBitmap("Karts.TriSpeeder.png")),
            new TextImagePair("Badwagon", Program.R.GetBitmap("Karts.Badwagon.png")),
            new TextImagePair("Prancer", Program.R.GetBitmap("Karts.Prancer.png")),
            new TextImagePair("Biddybuggy", Program.R.GetBitmap("Karts.Biddybuggy.png")),
            new TextImagePair("Landship", Program.R.GetBitmap("Karts.Landship.png")),
            new TextImagePair("Sneeker", Program.R.GetBitmap("Karts.Sneeker.png")),
            new TextImagePair("Sports Coupe", Program.R.GetBitmap("Karts.SportsCoupe.png")),
            new TextImagePair("Gold Standard", Program.R.GetBitmap("Karts.GoldStandard.png")),
            new TextImagePair("Standard Bike", Program.R.GetBitmap("Karts.StandardBike.png")),
            new TextImagePair("Comet", Program.R.GetBitmap("Karts.Comet.png")),
            new TextImagePair("Sport Bike", Program.R.GetBitmap("Karts.SportBike.png")),
            new TextImagePair("The Duke", Program.R.GetBitmap("Karts.TheDuke.png")),
            new TextImagePair("Flame Rider", Program.R.GetBitmap("Karts.FlameRider.png")),
            new TextImagePair("Varmint", Program.R.GetBitmap("Karts.Varmint.png")),
            new TextImagePair("Mr. Scooty", Program.R.GetBitmap("Karts.MrScooty.png")),
            new TextImagePair("Jet Bike", Program.R.GetBitmap("Karts.JetBike.png")),
            new TextImagePair("Yoshi Bike", Program.R.GetBitmap("Karts.YoshiBike.png")),
            new TextImagePair("Standard ATV", Program.R.GetBitmap("Karts.StandardAtv.png")),
            new TextImagePair("Wild Wiggler", Program.R.GetBitmap("Karts.WildWiggler.png")),
            new TextImagePair("Teddy Buggy", Program.R.GetBitmap("Karts.TeddyBuggy.png")),
            new TextImagePair("GLA", Program.R.GetBitmap("Karts.Gla.png")),
            new TextImagePair("W 25 Silver Arrow", Program.R.GetBitmap("Karts.W25SilverArrow.png")),
            new TextImagePair("300 SL Roadster", Program.R.GetBitmap("Karts.300SlRoadster.png")),
            new TextImagePair("Blue Falcon", Program.R.GetBitmap("Karts.BlueFalcon.png")),
            new TextImagePair("Tanooki Kart", Program.R.GetBitmap("Karts.TanookiKart.png")),
            new TextImagePair("B Dasher", Program.R.GetBitmap("Karts.BDasher.png")),
            new TextImagePair("Master Cycle", Program.R.GetBitmap("Karts.MasterCycle.png")),
            new TextImagePair("Inkstriker", Program.R.GetBitmap("Karts.Inkstriker.png")),
            new TextImagePair("Splat Buggy", Program.R.GetBitmap("Karts.SplatBuggy.png")),
            new TextImagePair("Streetle", Program.R.GetBitmap("Karts.Streetle.png")),
            new TextImagePair("P-Wing", Program.R.GetBitmap("Karts.PWing.png")),
            new TextImagePair("City Tripper", Program.R.GetBitmap("Karts.CityTripper.png")),
            new TextImagePair("Bone Rattler", Program.R.GetBitmap("Karts.BoneRattler.png")),
            new TextImagePair("Koopa Clown", Program.R.GetBitmap("Karts.KoopaClown.png"))
        };

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.KartPoints][0]; }
        }
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
                return pairs.Take(DataGroup.Count);
            }
        }
    }
}
