using System;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents the main application controller.
    /// </summary>
    internal class MainController
    {
        // ---- EVENTS -------------------------------------------------------------------------------------------------

        internal event EventHandler FileChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        internal string FileName { get; private set; }
        
        internal BinFile PerformanceData { get; private set; }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal void OpenFile(string fileName)
        {
            FileName = fileName;
            PerformanceData = new BinFile(fileName);
            FileChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void SaveFile(string fileName)
        {
            PerformanceData.Save(fileName);
            OpenFile(fileName);
        }
    }
}
