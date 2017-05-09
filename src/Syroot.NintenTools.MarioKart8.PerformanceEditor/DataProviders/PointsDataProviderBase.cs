using System;
using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal abstract class PointsDataProviderBase : DwordArrayGroupDataProvider
    {
        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Weight");
                yield return new TextImagePair("Acceleration");
                yield return new TextImagePair($"On-Road{Environment.NewLine}Traction");
                yield return new TextImagePair($"Off-Road{Environment.NewLine}Traction");
                yield return new TextImagePair("Turbo");
                yield return new TextImagePair($"Speed{Environment.NewLine}Ground");
                yield return new TextImagePair($"Speed{Environment.NewLine}Water");
                yield return new TextImagePair($"Speed{Environment.NewLine}Anti-Gravity");
                yield return new TextImagePair($"Speed{Environment.NewLine}Gliding");
                yield return new TextImagePair($"Handling{Environment.NewLine}Ground");
                yield return new TextImagePair($"Handling{Environment.NewLine}Water");
                yield return new TextImagePair($"Handling{Environment.NewLine}Anti-Gravity");
                yield return new TextImagePair($"Handling{Environment.NewLine}Gliding");
            }
        }
    }
}
