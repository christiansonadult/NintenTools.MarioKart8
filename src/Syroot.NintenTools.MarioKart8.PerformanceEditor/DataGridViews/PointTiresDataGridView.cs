using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTTR values.
    /// </summary>
    public class PointTiresDataGridView : PointSetDataGridView
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            yield return new TextImagePair("Standard", Resources.Tire_Standard);
            yield return new TextImagePair("Monster", Resources.Tire_Monster);
            yield return new TextImagePair("Roller", Resources.Tire_Roller);
            yield return new TextImagePair("Slim", Resources.Tire_Slim);
            yield return new TextImagePair("Slick", Resources.Tire_Slick);
            yield return new TextImagePair("Metal", Resources.Tire_Metal);
            yield return new TextImagePair("Button", Resources.Tire_Button);
            yield return new TextImagePair("Off-Road", Resources.Tire_OffRoad);
            yield return new TextImagePair("Sponge", Resources.Tire_Sponge);
            yield return new TextImagePair("Wood", Resources.Tire_Wood);
            yield return new TextImagePair("Cushion", Resources.Tire_Cushion);
            yield return new TextImagePair("Blue Standard", Resources.Tire_BlueStandard);
            yield return new TextImagePair("Hot Monster", Resources.Tire_HotMonster);
            yield return new TextImagePair("Azure Roller", Resources.Tire_AzureRoller);
            yield return new TextImagePair("Crimson Slim", Resources.Tire_CrimsonSlim);
            yield return new TextImagePair("Cyber Slick", Resources.Tire_CyberSlick);
            yield return new TextImagePair("Retro Off-Road", Resources.Tire_RetroOffRoad);
            yield return new TextImagePair("Gold Tires", Resources.Tire_GoldTires);
            yield return new TextImagePair("GLA Tires", Resources.Tire_GlaTires);
            yield return new TextImagePair("Triforce Tires", Resources.Tire_TriforceTires);
            yield return new TextImagePair("Leaf Tires", Resources.Tire_LeafTires);
        }
    }
}
