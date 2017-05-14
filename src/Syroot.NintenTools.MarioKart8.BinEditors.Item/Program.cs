using System;
using System.Reflection;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.BinEditors.Item
{
    /// <summary>
    /// Represents the main class of the application containing the program entry point.
    /// </summary>
    internal static class Program
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the <see cref="EditorAppBase"/> representing this application.
        /// </summary>
        internal static EditorAppBase Editor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="ResourceLoader"/> which can be used to access resources in this assembly.
        /// </summary>
        internal static ResourceLoader R { get; private set; }
        
        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        [STAThread]
        private static void Main(string[] args)
        {
            R = new ResourceLoader(Assembly.GetEntryAssembly(),
                "Syroot.NintenTools.MarioKart8.BinEditors.Item.Resources");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Editor = new ItemEditor(args))
            {
                Application.Run(Editor);
            }
        }
    }
}
