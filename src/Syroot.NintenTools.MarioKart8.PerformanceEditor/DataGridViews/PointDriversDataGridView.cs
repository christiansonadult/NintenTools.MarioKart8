using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTDV values.
    /// </summary>
    public class PointDriversDataGridView : PointSetDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            yield return new TextImagePair("Mario", Resources.Driver_Mario);
            yield return new TextImagePair("Luigi", Resources.Driver_Luigi);
            yield return new TextImagePair("Peach", Resources.Driver_Peach);
            yield return new TextImagePair("Daisy", Resources.Driver_Daisy);
            yield return new TextImagePair("Yoshi", Resources.Driver_Yoshi);
            yield return new TextImagePair("Toad", Resources.Driver_Toad);
            yield return new TextImagePair("Toadette", Resources.Driver_Toadette);
            yield return new TextImagePair("Koopa", Resources.Driver_Koopa);
            yield return new TextImagePair("Bowser", Resources.Driver_Bowser);
            yield return new TextImagePair("Donkey Kong", Resources.Driver_DonkeyKong);
            yield return new TextImagePair("Wario", Resources.Driver_Wario);
            yield return new TextImagePair("Waluigi", Resources.Driver_Waluigi);
            yield return new TextImagePair("Rosalina", Resources.Driver_Rosalina);
            yield return new TextImagePair("Metal Mario", Resources.Driver_MetalMario);
            yield return new TextImagePair("Pink Gold Peach", Resources.Driver_PinkGoldPeach);
            yield return new TextImagePair("Lakitu", Resources.Driver_Lakitu);
            yield return new TextImagePair("Shy Guy", Resources.Driver_ShyGuy);
            yield return new TextImagePair("Baby Mario", Resources.Driver_BabyMario);
            yield return new TextImagePair("Baby Luigi", Resources.Driver_BabyLuigi);
            yield return new TextImagePair("Baby Peach", Resources.Driver_BabyPeach);
            yield return new TextImagePair("Baby Daisy", Resources.Driver_BabyDaisy);
            yield return new TextImagePair("Baby Rosalina", Resources.Driver_BabyRosalina);
            yield return new TextImagePair("Larry", Resources.Driver_Larry);
            yield return new TextImagePair("Lemmy", Resources.Driver_Lemmy);
            yield return new TextImagePair("Wendy", Resources.Driver_Wendy);
            yield return new TextImagePair("Ludwig", Resources.Driver_Ludwig);
            yield return new TextImagePair("Iggy", Resources.Driver_Iggy);
            yield return new TextImagePair("Roy", Resources.Driver_Roy);
            yield return new TextImagePair("Morton", Resources.Driver_Morton);
            yield return new TextImagePair("Mii", Resources.Driver_Mii);
            yield return new TextImagePair("Tanooki Mario", Resources.Driver_TanookiMario);
            yield return new TextImagePair("Link", Resources.Driver_Link);
            yield return new TextImagePair("Villager Male", Resources.Driver_VillagerMale);
            yield return new TextImagePair("Isabelle", Resources.Driver_Isabelle);
            yield return new TextImagePair("Cat Peach", Resources.Driver_CatPeach);
            yield return new TextImagePair("Dry Bowser", Resources.Driver_DryBowser);
            yield return new TextImagePair("Villager Female", Resources.Driver_VillagerFemale);
        }
    }
}
