using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal abstract class RanksDataProviderBase : BinDataProvider
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override bool AllowFloats
        {
            get { return true; }
        }

        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                if (Program.IsMarioKart8Deluxe)
                {
                    // Valid point ranks.
                    for (int i = 0; i < DataGroup.Count; i++)
                    {
                        yield return new TextImagePair($"{i} Point{(i == 1 ? null : "s")}");
                    }
                }
                else
                {
                    // Valid point ranks and one for the fallback value.
                    for (int i = 0; i < DataGroup.Count; i++)
                    {
                        if (i == DataGroup.Count - 1)
                        {
                            yield return new TextImagePair("Fallback");
                        }
                        else
                        {
                            yield return new TextImagePair($"{i + 1} Point{(i == 0 ? null : "s")}");
                        }
                    }
                }
            }
        }
    }
}
