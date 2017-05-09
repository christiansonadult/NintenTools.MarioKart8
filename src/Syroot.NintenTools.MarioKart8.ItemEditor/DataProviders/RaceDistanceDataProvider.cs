using System;
using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    internal class RaceDistanceDataProvider : DwordArrayGroupDataProvider
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private int _raceDistanceType;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        protected override IEnumerable<TextImagePair> Columns
        {
            get
            {
                yield return new TextImagePair("Human Racer");
                yield return new TextImagePair("Software Racer");
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
