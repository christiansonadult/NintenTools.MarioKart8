using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a tool strip renderer for a Visual Studio 2015-like interface.
    /// </summary>
    internal class VisualStudioRenderer : ToolStripProfessionalRenderer
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioRenderer"/> class.
        /// </summary>
        internal VisualStudioRenderer() : base(new VisualStudioColorTable())
        {
            RoundedEdges = false;
        }

        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------

        /// <summary>
        /// </summary>
        /// <param name="e">A <see cref="ToolStripGripRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            using (HatchBrush brush = new HatchBrush(HatchStyle.Percent20, ColorTable.GripDark, ColorTable.GripLight))
            {
                Rectangle patternRectangle = e.GripBounds;
                patternRectangle.Inflate(2, -4);
                e.Graphics.FillRectangle(brush, patternRectangle);
            }
        }
    }
}
