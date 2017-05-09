using System;
using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    internal abstract class DistanceDataProviderBase : BinDataProvider
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal DistanceDataProviderBase(Section section)
        {
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

    }

    internal class RaceDistanceDataProvider : BinDataProvider
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private int _raceDistanceType;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal RaceDistanceDataProvider()
        {
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("No AI Racers");
                yield return new TextImagePair("With AI Racers");
            }
        }

        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                for (int i = 0; i < DataGroup.Count; i++)
                {
                    yield return new TextImagePair(i.ToString());
                }
            }
        }

        protected override string RowHeaderTitle
        {
            get { return "Index"; }
        }

        protected override DwordArrayGroup DataGroup
        {
            get { return (DwordArrayGroup)Program.File[(int)Section.RaceDistances][_raceDistanceType]; }
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        protected override Dword GetValue(int x, int y)
        {
            _raceDistanceType = x;
            return DataGroup[y][0];
        }

        protected override void SetValue(int x, int y, Dword value)
        {
            _raceDistanceType = x;
            DataGroup[y][0] = value;
        }
    }
}
