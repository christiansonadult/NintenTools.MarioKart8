using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    public static class ControlExtensions
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const int WM_SETREDRAW = 11;

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        public static void SuspendDrawing(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, false, 0);
        }

        public static void ResumeDrawing(this Control control)
        {
            SendMessage(control.Handle, WM_SETREDRAW, true, 0);
            control.Refresh();
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
    }
}
