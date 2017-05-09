using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    internal class PhysicsWeightDataProvider : RanksDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.WeightStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Low");
                yield return new TextImagePair("High");
                yield return new TextImagePair("Unknown");
            }
        }
    }

    internal class PhysicsAccelerationDataProvider : RanksDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.AccelerationStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Limiter");
                yield return new TextImagePair("Strength");
            }
        }
    }

    internal class PhysicsOnroadDataProvider : RanksDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.OnroadStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Road");
            }
        }
    }

    internal abstract class PhysicsOffroadDataProviderBase : RanksDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.OffroadStats][0]; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
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
        }
    }

    internal class PhysicsOffroadSlipDataProvider : PhysicsOffroadDataProviderBase
    {
    }

    internal class PhysicsOffroadBrakeDataProvider : PhysicsOffroadDataProviderBase
    {
        protected override Dword GetValue(int x, int y)
        {
            return DataGroup[y][x + 9];
        }

        protected override void SetValue(int x, int y, Dword value)
        {
            DataGroup[y][x + 9] = value;
        }
    }

    internal class PhysicTurboDataProvider : RanksDataProviderBase
    {
        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.TurboStats][0]; }
        }

        protected override bool AllowFloats
        {
            get { return false; }
        }

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Mini-Turbo");
                yield return new TextImagePair("Super-Turbo");
                if (Program.IsMarioKart8Deluxe)
                {
                    yield return new TextImagePair("Ultra-Turbo");
                }
            }
        }
    }
}
