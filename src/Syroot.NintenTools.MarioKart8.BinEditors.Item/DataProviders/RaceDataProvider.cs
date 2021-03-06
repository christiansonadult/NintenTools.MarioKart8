using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Item
{
    internal class RaceDataProvider : ItemDataProviderBase
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private bool _isAISet;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal RaceDataProvider(Section section, RaceItemSet set, bool isAISet)
            : base(section, (int)set)
        {
            _isAISet = isAISet;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                DwordArrayGroup distances = (DwordArrayGroup)Program.Editor.BinFile[(int)Section.RaceDistances]
                    [_isAISet ? 1 : 0];
                foreach (Dword[] distance in distances)
                {
                    yield return new TextImagePair(distance[0].Int32.ToString());
                }
            }
        }
    }
}
