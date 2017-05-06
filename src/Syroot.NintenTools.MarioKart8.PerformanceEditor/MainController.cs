using System;
using Syroot.NintenTools.MarioKart8.BinData;

namespace Syroot.NintenTools.MarioKart8.PerformanceEditor
{
    /// <summary>
    /// Represents the main application controller.
    /// </summary>
    internal static class MainController
    {
        // ---- EVENTS -------------------------------------------------------------------------------------------------

        internal static event EventHandler FileChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        internal static string FileName { get; private set; }
        
        internal static BinFile PerformanceData { get; private set; }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal static void OpenFile(string fileName)
        {
            FileName = fileName;
            PerformanceData = new BinFile(fileName);
            FileChanged?.Invoke(null, EventArgs.Empty);
        }

        internal static void SaveFile(string fileName)
        {
            PerformanceData.Save(fileName);
            OpenFile(fileName);
        }
    }
}
