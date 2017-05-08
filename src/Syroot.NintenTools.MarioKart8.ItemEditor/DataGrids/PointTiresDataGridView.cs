using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTTR values.
    /// </summary>
    public class PointTiresDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairs = new List<TextImagePair>()
        {
            new TextImagePair("Standard", Program.R.GetBitmap("Tire_Standard.png")),
            new TextImagePair("Monster", Program.R.GetBitmap("Tire_Monster.png")),
            new TextImagePair("Roller", Program.R.GetBitmap("Tire_Roller.png")),
            new TextImagePair("Slim", Program.R.GetBitmap("Tire_Slim.png")),
            new TextImagePair("Slick", Program.R.GetBitmap("Tire_Slick.png")),
            new TextImagePair("Metal", Program.R.GetBitmap("Tire_Metal.png")),
            new TextImagePair("Button", Program.R.GetBitmap("Tire_Button.png")),
            new TextImagePair("Off-Road", Program.R.GetBitmap("Tire_OffRoad.png")),
            new TextImagePair("Sponge", Program.R.GetBitmap("Tire_Sponge.png")),
            new TextImagePair("Wood", Program.R.GetBitmap("Tire_Wood.png")),
            new TextImagePair("Cushion", Program.R.GetBitmap("Tire_Cushion.png")),
            new TextImagePair("Blue Standard", Program.R.GetBitmap("Tire_BlueStandard.png")),
            new TextImagePair("Hot Monster", Program.R.GetBitmap("Tire_HotMonster.png")),
            new TextImagePair("Azure Roller", Program.R.GetBitmap("Tire_AzureRoller.png")),
            new TextImagePair("Crimson Slim", Program.R.GetBitmap("Tire_CrimsonSlim.png")),
            new TextImagePair("Cyber Slick", Program.R.GetBitmap("Tire_CyberSlick.png")),
            new TextImagePair("Retro Off-Road", Program.R.GetBitmap("Tire_RetroOffRoad.png")),
            new TextImagePair("Gold Tires", Program.R.GetBitmap("Tire_GoldTires.png")),
            new TextImagePair("GLA Tires", Program.R.GetBitmap("Tire_GlaTires.png")),
            new TextImagePair("Triforce Tires", Program.R.GetBitmap("Tire_TriforceTires.png")),
            new TextImagePair("Leaf Tires", Program.R.GetBitmap("Tire_LeafTires.png"))
        };

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            return _textImagePairs.Take(DataGroup.Count);
        }
    }
}
