using System;
using System.Reflection;
using System.Windows.Forms;
using Syroot.BinaryData;
using Syroot.NintenTools.MarioKart8.BinData;
using Syroot.NintenTools.MarioKart8.EditorUI;

namespace Syroot.NintenTools.MarioKart8.ItemEditor
{
    /// <summary>
    /// Represents the main class of the application containing the program entry point.
    /// </summary>
    internal static class Program
    {
        // ---- EVENTS -------------------------------------------------------------------------------------------------

        /// <summary>
        /// Raised when the <see cref="File"/> instance changed.
        /// </summary>
        internal static event EventHandler FileChanged;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the <see cref="ResourceLoader"/> which can be used to access resources in this assembly.
        /// </summary>
        internal static ResourceLoader R { get; private set; }

        /// <summary>
        /// Gets the name of the <see cref="File"/>.
        /// </summary>
        internal static string FileName { get; private set; }

        /// <summary>
        /// Gets the <see cref="BinFile"/> instance representing the performance data.
        /// </summary>
        internal static BinFile File { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="File"/> instance represents data from Mario Kart 8 Deluxe.
        /// </summary>
        internal static bool IsMarioKart8Deluxe
        {
            get
            {
                return File?.ByteOrder == ByteOrder.LittleEndian;
            }
        }
        
        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Opens the file with the given <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The name of the file to open.</param>
        internal static void OpenFile(string fileName)
        {
            FileName = fileName;
            File = new BinFile(fileName);
            FileChanged?.Invoke(null, EventArgs.Empty);
        }

        /// <summary>
        /// Saves the file under the given <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="openAfterSave">Reopens the stored file after saving.</param>
        internal static void SaveFile(string fileName, bool openAfterSave)
        {
            File.Save(fileName);
            if (openAfterSave)
            {
                OpenFile(fileName);
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        [STAThread]
        private static void Main(string[] args)
        {
            R = new ResourceLoader(Assembly.GetEntryAssembly(),
                "Syroot.NintenTools.MarioKart8.ItemEditor.Resources");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain(args));
        }
    }
}
