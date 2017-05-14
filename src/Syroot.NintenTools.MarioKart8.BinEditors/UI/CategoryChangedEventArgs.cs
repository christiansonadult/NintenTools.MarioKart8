using System;

namespace Syroot.NintenTools.MarioKart8.BinEditors.UI
{
    internal class CategoryChangedEventArgs : EventArgs
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------
        
        internal CategoryChangedEventArgs(int previous, int current)
        {
            Previous = previous;
            Current = current;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        internal int Previous { get; }

        internal int Current { get; }
    }
}