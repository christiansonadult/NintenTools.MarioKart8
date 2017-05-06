using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTDV values.
    /// </summary>
    public class PointDriversDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairsMK8 = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Resources.Driver_Mario),
            new TextImagePair("Luigi", Resources.Driver_Luigi),
            new TextImagePair("Peach", Resources.Driver_Peach),
            new TextImagePair("Daisy", Resources.Driver_Daisy),
            new TextImagePair("Yoshi", Resources.Driver_Yoshi),
            new TextImagePair("Toad", Resources.Driver_Toad),
            new TextImagePair("Toadette", Resources.Driver_Toadette),
            new TextImagePair("Koopa", Resources.Driver_Koopa),
            new TextImagePair("Bowser", Resources.Driver_Bowser),
            new TextImagePair("Donkey Kong", Resources.Driver_DonkeyKong),
            new TextImagePair("Wario", Resources.Driver_Wario),
            new TextImagePair("Waluigi", Resources.Driver_Waluigi),
            new TextImagePair("Rosalina", Resources.Driver_Rosalina),
            new TextImagePair("Metal Mario", Resources.Driver_MetalMario),
            new TextImagePair("Pink Gold Peach", Resources.Driver_PinkGoldPeach),
            new TextImagePair("Lakitu", Resources.Driver_Lakitu),
            new TextImagePair("Shy Guy", Resources.Driver_ShyGuy),
            new TextImagePair("Baby Mario", Resources.Driver_BabyMario),
            new TextImagePair("Baby Luigi", Resources.Driver_BabyLuigi),
            new TextImagePair("Baby Peach", Resources.Driver_BabyPeach),
            new TextImagePair("Baby Daisy", Resources.Driver_BabyDaisy),
            new TextImagePair("Baby Rosalina", Resources.Driver_BabyRosalina),
            new TextImagePair("Larry", Resources.Driver_Larry),
            new TextImagePair("Lemmy", Resources.Driver_Lemmy),
            new TextImagePair("Wendy", Resources.Driver_Wendy),
            new TextImagePair("Ludwig", Resources.Driver_Ludwig),
            new TextImagePair("Iggy", Resources.Driver_Iggy),
            new TextImagePair("Roy", Resources.Driver_Roy),
            new TextImagePair("Morton", Resources.Driver_Morton),
            new TextImagePair("Mii", Resources.Driver_Mii),
            new TextImagePair("Tanooki Mario", Resources.Driver_TanookiMario),
            new TextImagePair("Link", Resources.Driver_Link),
            new TextImagePair("Villager Male", Resources.Driver_VillagerMale),
            new TextImagePair("Isabelle", Resources.Driver_Isabelle),
            new TextImagePair("Cat Peach", Resources.Driver_CatPeach),
            new TextImagePair("Dry Bowser", Resources.Driver_DryBowser),
            new TextImagePair("Villager Female", Resources.Driver_VillagerFemale)
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Resources.Driver_Mario),
            new TextImagePair("Luigi", Resources.Driver_Luigi),
            new TextImagePair("Peach", Resources.Driver_Peach),
            new TextImagePair("Daisy", Resources.Driver_Daisy),
            new TextImagePair("Yoshi", Resources.Driver_Yoshi),
            new TextImagePair("Toad", Resources.Driver_Toad),
            new TextImagePair("Toadette", Resources.Driver_Toadette),
            new TextImagePair("Koopa", Resources.Driver_Koopa),
            new TextImagePair("Bowser", Resources.Driver_Bowser),
            new TextImagePair("Donkey Kong", Resources.Driver_DonkeyKong),
            new TextImagePair("Wario", Resources.Driver_Wario),
            new TextImagePair("Waluigi", Resources.Driver_Waluigi),
            new TextImagePair("Rosalina", Resources.Driver_Rosalina),
            new TextImagePair("Metal Mario", Resources.Driver_MetalMario),
            new TextImagePair("Pink Gold Peach", Resources.Driver_PinkGoldPeach),
            new TextImagePair("Lakitu", Resources.Driver_Lakitu),
            new TextImagePair("Shy Guy", Resources.Driver_ShyGuy),
            new TextImagePair("Baby Mario", Resources.Driver_BabyMario),
            new TextImagePair("Baby Luigi", Resources.Driver_BabyLuigi),
            new TextImagePair("Baby Peach", Resources.Driver_BabyPeach),
            new TextImagePair("Baby Daisy", Resources.Driver_BabyDaisy),
            new TextImagePair("Baby Rosalina", Resources.Driver_BabyRosalina),
            new TextImagePair("Larry", Resources.Driver_Larry),
            new TextImagePair("Lemmy", Resources.Driver_Lemmy),
            new TextImagePair("Wendy", Resources.Driver_Wendy),
            new TextImagePair("Ludwig", Resources.Driver_Ludwig),
            new TextImagePair("Iggy", Resources.Driver_Iggy),
            new TextImagePair("Roy", Resources.Driver_Roy),
            new TextImagePair("Morton", Resources.Driver_Morton),
            new TextImagePair("Mii", Resources.Driver_Mii),
            new TextImagePair("Tanooki Mario", Resources.Driver_TanookiMario),
            new TextImagePair("Link", Resources.Driver_Link),
            new TextImagePair("Villager Male", Resources.Driver_VillagerMale),
            new TextImagePair("Isabelle", Resources.Driver_Isabelle),
            new TextImagePair("Cat Peach", Resources.Driver_CatPeach),
            new TextImagePair("Dry Bowser", Resources.Driver_DryBowser),
            new TextImagePair("Villager Female", Resources.Driver_VillagerFemale),
            new TextImagePair("King Boo", Resources.Driver_KingBoo),
            new TextImagePair("Dry Bones", Resources.Driver_DryBones),
            new TextImagePair("Bowser Jr.", Resources.Driver_BowserJr),
            new TextImagePair("Inkling Boy", Resources.Driver_InklingMale),
            new TextImagePair("Inkling Girl", Resources.Driver_InklingFemale)
        };

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
            return pairs.Take(DataGroup.Count);
        }
    }
}
