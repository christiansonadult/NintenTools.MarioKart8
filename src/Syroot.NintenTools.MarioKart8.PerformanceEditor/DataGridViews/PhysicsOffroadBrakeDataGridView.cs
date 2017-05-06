using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents a <see cref="PointRankFloatDataGridView"/> displaying PROF values.
    /// </summary>
    public class PhysicsOffroadBrakeDataGridView : PointRankFloatDataGridView
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> GetColumnHeaders()
        {
            yield return new TextImagePair("Dirt Light");
            yield return new TextImagePair("Dirt Medium");
            yield return new TextImagePair("Dirt Heavy");
            yield return new TextImagePair("Sand Light");
            yield return new TextImagePair("Sand Medium");
            yield return new TextImagePair("Sand Heavy");
            yield return new TextImagePair("Ice Light");
            yield return new TextImagePair("Ice Medium");
            yield return new TextImagePair("Ice Heavy");
        }

        /// <summary>
        /// Called when the whole grid has to be filled with columns, rows and values from the <see cref="DataGroup"/>.
        /// </summary>
        protected override void FillData()
        {
            // Only show the first half holding the brake stats.
            for (int y = 0; y < DataGroup.Count; y++)
            {
                Dword[] dwords = DataGroup[y];
                for (int x = 0; x < 9; x++)
                {
                    Rows[y].Cells[x].Value = dwords[x].Single;
                }
            }
        }
    }
}
