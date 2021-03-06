using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Syroot.NintenTools.MarioKart8.BinEditors.UI
{
    /// <summary>
    /// Represents a small input dialog to enter a calculation number.
    /// </summary>
    internal partial class FormCalculation : Form
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private static float _lastValue;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="FormCalculation"/> class.
        /// </summary>
        internal FormCalculation()
        {
            InitializeComponent();
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets a value indicating whether floating point values can be entered.
        /// </summary>
        internal bool AllowFloats
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value entered.
        /// </summary>
        internal float Value
        {
            get { return Single.Parse(_tbValue.Text); }
            set { _tbValue.Text = value.ToString(); }
        }

        // ---- METHODS (INTERNAL) -------------------------------------------------------------------------------------

        /// <summary>
        /// Shows the input dialog with the given title and value settings.
        /// </summary>
        /// <param name="caption">The text in the title bar of the input box.</param>
        /// <param name="allowFloat"><c>true</c> to allow floating point numbers.</param>
        /// <returns>The value the user entered or <c>null</c> if he canceled.</returns>
        internal static float? Show(string caption, bool allowFloat)
        {
            FormCalculation form = new FormCalculation()
            {
                Text = caption,
                AllowFloats = allowFloat
            };

            // Set the last default value.
            if (form.AllowFloats)
            {
                form.Value = _lastValue;
            }
            else
            {
                form.Value = (int)_lastValue;
            }

            // Show the dialog and return the value.
            if (form.ShowDialog() == DialogResult.OK)
            {
                _lastValue = form.Value;
                return form.Value;
            }
            return null;
        }

        // ---- EVENTHANDLERS ------------------------------------------------------------------------------------------

        private void _tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            string keyString = e.KeyChar.ToString();

            bool valid = (Char.IsControl(e.KeyChar)
                || Char.IsDigit(e.KeyChar)
                || keyString == numberFormat.NegativeSign
                || (AllowFloats && keyString == numberFormat.NumberDecimalSeparator));
            e.Handled = !valid;
        }

        private void _tbValue_Validating(object sender, CancelEventArgs e)
        {
            if (AllowFloats)
            {
                e.Cancel = !Single.TryParse(_tbValue.Text, out float result);
            }
            else
            {
                e.Cancel = !Int32.TryParse(_tbValue.Text, out int result);
            }
        }
    }
}
