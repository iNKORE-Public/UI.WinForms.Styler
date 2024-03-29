





using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace iNKORE.UI.WinForms.Styler.Controls
{
    [ToolboxBitmap(typeof(Button))]
    public class CommandLink: System.Windows.Forms.Button
    {
        public CommandLink()
        {
            this.FlatStyle = FlatStyle.System;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                //Fix for XP provided by jonpreece (http://iNKORE.UI.WinForms.Styler.codeplex.com/Thread/View.aspx?ThreadId=81391)
                if (OsSupport.IsVistaOrBetter)
                    cp.Style |= NativeMethods.BS_COMMANDLINK;
                else
                    cp.Style |= 1;
                return cp;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private bool useicon = true; //Checks if user wants to use an icon instead of a bitmap
        private Bitmap image_;
        
        //Image alignment is ignored at the moment. Property overrides inherited image property
        //Supports images other than bitmap, supports transparency on .NET 2.0
        [Description("Gets or sets the image that is displayed on a button control."), Category("Appearance"), DefaultValue(null)]
        public new Bitmap Image
        {
            get
            {
                return image_;
            }
            set
            {
                image_ = value;
                if (value != null)
                {
                    this.useicon = false;
                    this.Icon = null;
                }
                this.SetShield(false);
                SetImage();
            }
        }

        private Icon icon_;
        [Description("Gets or sets the icon that is displayed on a button control."), Category("Appearance"), DefaultValue(null)]
        public Icon Icon
        {
            get
            {
                return icon_;
            }
            set
            {
                icon_ = value;
                if (icon_ != null)
                {
                    this.useicon = true;
                }
                this.SetShield(false);
                SetImage();
            }
        }
        [Description("Refreshes the image displayed on the button.")]
        public void SetImage()
        {
            IntPtr iconhandle = IntPtr.Zero;
            if (!this.useicon)
            {
                if (this.image_ != null)
                {
                    iconhandle = image_.GetHicon(); //Gets the handle of the bitmap
                }
            }
            else
            {
                if (this.icon_ != null)
                {
                    iconhandle = this.Icon.Handle;
                }
            }

            //Set the button to use the icon. If no icon or bitmap is used, no image is set.
            SendMessage(this.Handle, NativeMethods.BM_SETIMAGE, 1, (int)iconhandle);
        }

        private bool showshield_ = false;
        [Description("Gets or sets whether if the control should use an elevated shield icon."), Category("Appearance"), DefaultValue(false)]
        public bool ShowShield
        {
            get
            {
                return showshield_;
            }
            set
            {
                showshield_ = value;
                this.SetShield(value);
                if (!value)
                {
                    this.SetImage();
                }
            }
        }

        public void SetShield(bool Value)
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.BCM_SETSHIELD, IntPtr.Zero, new IntPtr(showshield_ ? 1 : 0));
        }

        private string note_ = string.Empty;

		[Description("Gets or sets the note that is displayed on a button control."), Category("Appearance"), DefaultValue("")]
        public string Note
        {
            get
            {
                return this.note_;
            }
            set
            {
                this.note_ = value;
                this.SetNote(this.note_);
            }
        }

        [Description("Sets the note displayed on the button.")]
        private void SetNote(string NoteText)
        {
            //Sets the note
            NativeMethods.SendMessage(this.Handle, NativeMethods.BCM_SETNOTE, IntPtr.Zero, NoteText);
        }

    }
}
