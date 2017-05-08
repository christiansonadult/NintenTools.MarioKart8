using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal abstract class HandlingDataProviderBase : RanksDataProviderBase
    {
        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Steer");
                yield return new TextImagePair("Drift");
                yield return new TextImagePair("Charge");
            }
        }
    }

    internal class HandlingGroundDataProvider : HandlingDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.HandlingGroundStats][0]; }
        }
    }

    internal class HandlingWaterDataProvider : HandlingDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.HandlingWaterStats][0]; }
        }
    }

    internal class HandlingAntigravityDataProvider : HandlingDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.HandlingAntigravityStats][0]; }
        }
    }

    internal class HandlingGlidingDataProvider : HandlingDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.HandlingAirStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Roll");
                yield return new TextImagePair("Move");
            }
        }
    }
}
