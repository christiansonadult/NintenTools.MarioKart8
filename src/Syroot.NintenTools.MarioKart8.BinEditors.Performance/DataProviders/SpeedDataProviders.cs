using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Performance
{
    internal abstract class SpeedDataProviderBase : RanksDataProviderBase
    {
        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("No Coins");
                yield return new TextImagePair("10 Coins");
            }
        }
    }

    internal class SpeedGroundDataProvider : SpeedDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.Editor.BinFile[(int)Section.SpeedGroundStats][0]; }
        }
    }

    internal class SpeedWaterDataProvider : SpeedDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.Editor.BinFile[(int)Section.SpeedWaterStats][0]; }
        }
    }

    internal class SpeedAntigravityDataProvider : SpeedDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.Editor.BinFile[(int)Section.SpeedAntigravityStats][0]; }
        }
    }

    internal class SpeedGlidingDataProvider : SpeedDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.Editor.BinFile[(int)Section.SpeedAirStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Any Coins");
            }
        }
    }
}
