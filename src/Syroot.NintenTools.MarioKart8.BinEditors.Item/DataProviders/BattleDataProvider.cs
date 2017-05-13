using System;
using System.Collections.Generic;
using Syroot.NintenTools.MarioKart8.BinData.Item;
using Syroot.NintenTools.MarioKart8.BinEditors.UI;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Item
{
    internal class BattleDataProvider : ItemDataProviderBase
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private bool _isAISet;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal BattleDataProvider(Section section, BattleItemSet set, bool isAISet)
            : base(section, (int)set)
        {
            _isAISet = isAISet;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        protected override IEnumerable<TextImagePair> Rows
        {
            get
            {
                yield return new TextImagePair($"> 2 Minutes{Environment.NewLine}> 2 Balloons");
                yield return new TextImagePair($"> 2 Minutes{Environment.NewLine}2 Balloons");
                yield return new TextImagePair($"> 2 Minutes{Environment.NewLine}1 Balloon");
                yield return new TextImagePair($"> 1 Minute{Environment.NewLine}> 2 Balloons");
                yield return new TextImagePair($"> 1 Minute{Environment.NewLine}2 Balloons");
                yield return new TextImagePair($"> 1 Minute{Environment.NewLine}1 Balloon");
                yield return new TextImagePair($"> 0 Minutes{Environment.NewLine}> 2 Balloons");
                yield return new TextImagePair($"> 0 Minutes{Environment.NewLine}2 Balloons");
                yield return new TextImagePair($"> 0 Minutes{Environment.NewLine}1 Balloon");
                yield return new TextImagePair($"Any Minute{Environment.NewLine}0 Balloons");
            }
        }

        protected override string RowHeaderTitle
        {
            get { return "State"; }
        }
    }
}
