using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinEditors.DataProvider;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Performance
{
    internal abstract class RanksDataProviderBase : DwordArrayGroupDataProvider
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override bool UseFloats
        {
            get { return true; }
        }

        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                if (Program.Editor.IsMK8Deluxe)
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

        protected override string RowHeaderTitle
        {
            get { return "Total Points"; }
        }
    }
}
