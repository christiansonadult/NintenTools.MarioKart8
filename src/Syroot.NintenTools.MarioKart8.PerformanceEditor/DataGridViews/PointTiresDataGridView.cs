using System.Collections.Generic;
using System.Linq;
using Syroot.NintenTools.MarioKart8.PerformanceEditor.Properties;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PTTR values.
    /// </summary>
    public class PointTiresDataGridView : PointSetDataGridView
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private static readonly List<TextImagePair> _textImagePairs = new List<TextImagePair>()
        {
            new TextImagePair("Standard", Resources.Tire_Standard),
            new TextImagePair("Monster", Resources.Tire_Monster),
            new TextImagePair("Roller", Resources.Tire_Roller),
            new TextImagePair("Slim", Resources.Tire_Slim),
            new TextImagePair("Slick", Resources.Tire_Slick),
            new TextImagePair("Metal", Resources.Tire_Metal),
            new TextImagePair("Button", Resources.Tire_Button),
            new TextImagePair("Off-Road", Resources.Tire_OffRoad),
            new TextImagePair("Sponge", Resources.Tire_Sponge),
            new TextImagePair("Wood", Resources.Tire_Wood),
            new TextImagePair("Cushion", Resources.Tire_Cushion),
            new TextImagePair("Blue Standard", Resources.Tire_BlueStandard),
            new TextImagePair("Hot Monster", Resources.Tire_HotMonster),
            new TextImagePair("Azure Roller", Resources.Tire_AzureRoller),
            new TextImagePair("Crimson Slim", Resources.Tire_CrimsonSlim),
            new TextImagePair("Cyber Slick", Resources.Tire_CyberSlick),
            new TextImagePair("Retro Off-Road", Resources.Tire_RetroOffRoad),
            new TextImagePair("Gold Tires", Resources.Tire_GoldTires),
            new TextImagePair("GLA Tires", Resources.Tire_GlaTires),
            new TextImagePair("Triforce Tires", Resources.Tire_TriforceTires),
            new TextImagePair("Leaf Tires", Resources.Tire_LeafTires)
        };

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetRowHeaders()
        {
            return _textImagePairs.Take(DataGroup.Count);
        }
    }
}
