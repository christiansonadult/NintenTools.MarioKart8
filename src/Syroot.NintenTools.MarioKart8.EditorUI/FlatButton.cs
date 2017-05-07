using System;
using System.Drawing;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a button in a flat design.
    /// </summary>
    public class FlatButton : Button
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatButton"/> class displaying the given
        /// <paramref name="text"/>  and attaching to the provided <paramref name="clickEventHandler"/>.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="clickEventHandler">The click <see cref="EventHandler"/> to attach to.</param>
        public FlatButton(string text, EventHandler clickEventHandler)
        {
            BackColor = Color.FromArgb(64, 64, 64);
            Dock = DockStyle.Fill;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(76, 76, 76);
            FlatAppearance.MouseDownBackColor = Color.FromArgb(76, 76, 76);
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.White;
            Margin = new Padding(0);
            Text = text;
            UseVisualStyleBackColor = false;
            Click += clickEventHandler;
        }
    }
}
