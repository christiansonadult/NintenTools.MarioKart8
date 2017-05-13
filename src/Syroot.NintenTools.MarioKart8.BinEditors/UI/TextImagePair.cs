using System.Drawing;

namespace Syroot.NintenTools.MarioKart8.BinEditors.UI
{
    /// <summary>
    /// Represents a pair holding text and a visualizing image.
    /// </summary>
    public struct TextImagePair
    {
        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="TextImagePair"/> structure.
        /// </summary>
        /// <param name="text">The text to hold.</param>
        /// <param name="image">The <see cref="Image"/> to hold.</param>
        public TextImagePair(string text, Image image = null)
        {
            Text = text;
            Image = image;
        }
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the text of the pair.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the <see cref="Image"/> of the pair.
        /// </summary>
        public Image Image { get; }
    }
}
