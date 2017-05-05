using System;
using Syroot.BinaryData;
using Syroot.NintenTools.MarioKart8.BinData.Performance;

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

        internal ByteOrder FileByteOrder { get; private set; }

        internal PerformanceData PerformanceData { get; private set; }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        internal void OpenFile(string fileName, ByteOrder byteOrder)
        {
            FileName = fileName;
            FileByteOrder = byteOrder;
            PerformanceData = new PerformanceData(fileName, FileByteOrder);
            FileChanged?.Invoke(this, EventArgs.Empty);
        }

        internal void SaveFile(string fileName)
        {
            PerformanceData.Save(fileName, FileByteOrder);
            OpenFile(fileName, FileByteOrder);
        }

        internal void SaveFile(string fileName, ByteOrder byteOrder)
        {
            PerformanceData.Save(fileName, byteOrder);
            OpenFile(fileName, byteOrder);
        }
    }
}
