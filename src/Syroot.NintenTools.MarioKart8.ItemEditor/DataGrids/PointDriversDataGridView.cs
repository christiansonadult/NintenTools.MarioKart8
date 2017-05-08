using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTDV values.
    /// </summary>
    public class PointDriversDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairsMK8 = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Program.R.GetBitmap("Driver_Mario.png")),
            new TextImagePair("Luigi", Program.R.GetBitmap("Driver_Luigi.png")),
            new TextImagePair("Peach", Program.R.GetBitmap("Driver_Peach.png")),
            new TextImagePair("Daisy", Program.R.GetBitmap("Driver_Daisy.png")),
            new TextImagePair("Yoshi", Program.R.GetBitmap("Driver_Yoshi.png")),
            new TextImagePair("Toad", Program.R.GetBitmap("Driver_Toad.png")),
            new TextImagePair("Toadette", Program.R.GetBitmap("Driver_Toadette.png")),
            new TextImagePair("Koopa", Program.R.GetBitmap("Driver_Koopa.png")),
            new TextImagePair("Bowser", Program.R.GetBitmap("Driver_Bowser.png")),
            new TextImagePair("Donkey Kong", Program.R.GetBitmap("Driver_DonkeyKong.png")),
            new TextImagePair("Wario", Program.R.GetBitmap("Driver_Wario.png")),
            new TextImagePair("Waluigi", Program.R.GetBitmap("Driver_Waluigi.png")),
            new TextImagePair("Rosalina", Program.R.GetBitmap("Driver_Rosalina.png")),
            new TextImagePair("Metal Mario", Program.R.GetBitmap("Driver_MetalMario.png")),
            new TextImagePair("Pink Gold Peach", Program.R.GetBitmap("Driver_PinkGoldPeach.png")),
            new TextImagePair("Lakitu", Program.R.GetBitmap("Driver_Lakitu.png")),
            new TextImagePair("Shy Guy", Program.R.GetBitmap("Driver_ShyGuy.png")),
            new TextImagePair("Baby Mario", Program.R.GetBitmap("Driver_BabyMario.png")),
            new TextImagePair("Baby Luigi", Program.R.GetBitmap("Driver_BabyLuigi.png")),
            new TextImagePair("Baby Peach", Program.R.GetBitmap("Driver_BabyPeach.png")),
            new TextImagePair("Baby Daisy", Program.R.GetBitmap("Driver_BabyDaisy.png")),
            new TextImagePair("Baby Rosalina", Program.R.GetBitmap("Driver_BabyRosalina.png")),
            new TextImagePair("Larry", Program.R.GetBitmap("Driver_Larry.png")),
            new TextImagePair("Lemmy", Program.R.GetBitmap("Driver_Lemmy.png")),
            new TextImagePair("Wendy", Program.R.GetBitmap("Driver_Wendy.png")),
            new TextImagePair("Ludwig", Program.R.GetBitmap("Driver_Ludwig.png")),
            new TextImagePair("Iggy", Program.R.GetBitmap("Driver_Iggy.png")),
            new TextImagePair("Roy", Program.R.GetBitmap("Driver_Roy.png")),
            new TextImagePair("Morton", Program.R.GetBitmap("Driver_Morton.png")),
            new TextImagePair("Mii", Program.R.GetBitmap("Driver_Mii.png")),
            new TextImagePair("Tanooki Mario", Program.R.GetBitmap("Driver_TanookiMario.png")),
            new TextImagePair("Link", Program.R.GetBitmap("Driver_Link.png")),
            new TextImagePair("Villager Male", Program.R.GetBitmap("Driver_VillagerMale.png")),
            new TextImagePair("Isabelle", Program.R.GetBitmap("Driver_Isabelle.png")),
            new TextImagePair("Cat Peach", Program.R.GetBitmap("Driver_CatPeach.png")),
            new TextImagePair("Dry Bowser", Program.R.GetBitmap("Driver_DryBowser.png")),
            new TextImagePair("Villager Female", Program.R.GetBitmap("Driver_VillagerFemale.png"))
        };

        private static readonly List<TextImagePair> _textImagePairsMK8D = new List<TextImagePair>()
        {
            new TextImagePair("Mario", Program.R.GetBitmap("Driver_Mario.png")),
            new TextImagePair("Luigi", Program.R.GetBitmap("Driver_Luigi.png")),
            new TextImagePair("Peach", Program.R.GetBitmap("Driver_Peach.png")),
            new TextImagePair("Daisy", Program.R.GetBitmap("Driver_Daisy.png")),
            new TextImagePair("Yoshi", Program.R.GetBitmap("Driver_Yoshi.png")),
            new TextImagePair("Toad", Program.R.GetBitmap("Driver_Toad.png")),
            new TextImagePair("Toadette", Program.R.GetBitmap("Driver_Toadette.png")),
            new TextImagePair("Koopa", Program.R.GetBitmap("Driver_Koopa.png")),
            new TextImagePair("Bowser", Program.R.GetBitmap("Driver_Bowser.png")),
            new TextImagePair("Donkey Kong", Program.R.GetBitmap("Driver_DonkeyKong.png")),
            new TextImagePair("Wario", Program.R.GetBitmap("Driver_Wario.png")),
            new TextImagePair("Waluigi", Program.R.GetBitmap("Driver_Waluigi.png")),
            new TextImagePair("Rosalina", Program.R.GetBitmap("Driver_Rosalina.png")),
            new TextImagePair("Metal Mario", Program.R.GetBitmap("Driver_MetalMario.png")),
            new TextImagePair("Pink Gold Peach", Program.R.GetBitmap("Driver_PinkGoldPeach.png")),
            new TextImagePair("Lakitu", Program.R.GetBitmap("Driver_Lakitu.png")),
            new TextImagePair("Shy Guy", Program.R.GetBitmap("Driver_ShyGuy.png")),
            new TextImagePair("Baby Mario", Program.R.GetBitmap("Driver_BabyMario.png")),
            new TextImagePair("Baby Luigi", Program.R.GetBitmap("Driver_BabyLuigi.png")),
            new TextImagePair("Baby Peach", Program.R.GetBitmap("Driver_BabyPeach.png")),
            new TextImagePair("Baby Daisy", Program.R.GetBitmap("Driver_BabyDaisy.png")),
            new TextImagePair("Baby Rosalina", Program.R.GetBitmap("Driver_BabyRosalina.png")),
            new TextImagePair("Larry", Program.R.GetBitmap("Driver_Larry.png")),
            new TextImagePair("Lemmy", Program.R.GetBitmap("Driver_Lemmy.png")),
            new TextImagePair("Wendy", Program.R.GetBitmap("Driver_Wendy.png")),
            new TextImagePair("Ludwig", Program.R.GetBitmap("Driver_Ludwig.png")),
            new TextImagePair("Iggy", Program.R.GetBitmap("Driver_Iggy.png")),
            new TextImagePair("Roy", Program.R.GetBitmap("Driver_Roy.png")),
            new TextImagePair("Morton", Program.R.GetBitmap("Driver_Morton.png")),
            new TextImagePair("Mii", Program.R.GetBitmap("Driver_Mii.png")),
            new TextImagePair("Tanooki Mario", Program.R.GetBitmap("Driver_TanookiMario.png")),
            new TextImagePair("Link", Program.R.GetBitmap("Driver_Link.png")),
            new TextImagePair("Villager Male", Program.R.GetBitmap("Driver_VillagerMale.png")),
            new TextImagePair("Isabelle", Program.R.GetBitmap("Driver_Isabelle.png")),
            new TextImagePair("Cat Peach", Program.R.GetBitmap("Driver_CatPeach.png")),
            new TextImagePair("Dry Bowser", Program.R.GetBitmap("Driver_DryBowser.png")),
            new TextImagePair("Villager Female", Program.R.GetBitmap("Driver_VillagerFemale.png")),
            new TextImagePair("King Boo", Program.R.GetBitmap("Driver_KingBoo.png")),
            new TextImagePair("Dry Bones", Program.R.GetBitmap("Driver_DryBones.png")),
            new TextImagePair("Bowser Jr.", Program.R.GetBitmap("Driver_BowserJr.png")),
            new TextImagePair("Inkling Boy", Program.R.GetBitmap("Driver_InklingMale.png")),
            new TextImagePair("Inkling Girl", Program.R.GetBitmap("Driver_InklingFemale.png"))
        };

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            List<TextImagePair> pairs = Program.IsMarioKart8Deluxe ? _textImagePairsMK8D : _textImagePairsMK8;
            return pairs.Take(DataGroup.Count);
        }
    }
}
