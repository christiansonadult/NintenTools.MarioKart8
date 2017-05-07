using System;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.EditorUI
{
    /// <summary>
    /// Represents a <see cref="SectionDataGridView"/> which only allows the input of float values.
    /// </summary>
    public abstract class FloatSectionDataGridView : SectionDataGridView<float>
    {
        // ---- METHODS (PROTECTED) ------------------------------------------------------------------------------------
        
        protected override void ConfigureColumn(DataGridViewColumn column, TextImagePair header)
        {
            base.ConfigureColumn(column, header);
            column.DefaultCellStyle.Format = "0.0000#####";
        }

        /// <summary>
        /// Called when the user inputs a character and it has to be validated.
        /// </summary>
        /// <param name="character">The character which was input.</param>
        /// <returns><c>true</c> to allow the character, otherwise <c>false</c>.</returns>
        protected override bool ValidateCharacterInput(char character)
        {
            return Char.IsControl(character)
                || Char.IsDigit(character)
                || Char.IsPunctuation(character);
        }

        /// <summary>
        /// Called when the cell value text has to be validated.
        /// </summary>
        /// <param name="text">The value to validate.</param>
        /// <returns><c>true</c> to allow the text, otherwise <c>false</c>.</returns>
        protected override bool ValidateTextValue(string text)
        {
            return Single.TryParse(text, out float f);
        }
    }
}
