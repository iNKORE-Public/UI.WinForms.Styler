
using System;
using System.ComponentModel;
using System.Drawing;

namespace iNKORE.UI.WinForms.Styler.Controls
{
    [ToolboxBitmap(typeof(AdvTextBox))]
    public class AdvTextBox:System.Windows.Forms.TextBox
    {
        private string _cueBannerText = string.Empty;

		/// <summary>Gets or sets the cue text that is displayed on the TextBox control.</summary>
        [Description("Text that is displayed as Cue banner."), Category("Appearance"), DefaultValue("")]
        public string CueBannerText
        {
            get {
                return _cueBannerText;
            }
            set {
                _cueBannerText = value;
                this.SetCueText(ShowCueFocused);
            }
        }

		[Browsable(false)]
		public new bool Multiline {
			get { return base.Multiline; }
			set { base.Multiline = false; }
		}

		private bool _showCueFocused = false;

		/// <summary>Gets or sets whether the Cue text should be displyed even when the control has keybord focus.</summary>
		/// <remarks>If true, the Cue text will disappear as soon as the user starts typing.</remarks>
		[Description("If true, the Cue text will be displayed even when the control has keyboard focus."), Category("Appearance"), DefaultValue(false)]
		public bool ShowCueFocused {
			get { return _showCueFocused; }
			set {
				_showCueFocused = value;
				SetCueText(value);
			}
		}

        private void SetCueText(bool showFocus)
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.EM_SETCUEBANNER, new IntPtr((showFocus) ? 1 : 0), _cueBannerText);
        }
    }
}
