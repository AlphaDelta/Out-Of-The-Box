using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OutOfTheBox.Common;

namespace OutOfTheBox.ModuleTree.Macro
{
    public partial class AddMacro : Form
    {
        public AddMacro()
        {
            InitializeComponent();
        }

        bool flag = false;
        private void btnHotkey_Click(object sender, EventArgs e)
        {
            this.ControlBox = gbAction.Enabled = btnAdd.Enabled = btnCancel.Enabled = txtName.Enabled = flag;
            flag = !flag;

            if (flag)
            {
                WinAPI.KeyboardHook.Hook();
                WinAPI.KeyboardHook.DownEvent += KeyDownEvent;
                //WinAPI.KeyboardHook.UpEvent += KeyUpEvent;

                txtHotkey.Focus();
            }
            else
            {
                //WinAPI.KeyboardHook.UpEvent -= KeyUpEvent;
                WinAPI.KeyboardHook.DownEvent -= KeyDownEvent;
                WinAPI.KeyboardHook.Unhook();
            }
        }

        void KeyDownEvent(WinAPI.KeyboardHook.DownEventArgs e) { RefreshKeys(); }
        //void KeyUpEvent(WinAPI.KeyboardHook.UpEventArgs e) { RefreshKeys(); }

        bool[] tempstates = new bool[0xFF];
        void RefreshKeys()
        {
            bool first = true;
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < 0xFF; i++)
            {
                if (WinAPI.KeyboardHook.States[i] == false) { tempstates[i] = false; continue; }
                tempstates[i] = true;

                if (first) first = false;
                else b.Append(" + ");
                b.Append(WinAPI.KeyboardHook.FriendlyNames[i]);
            }
            Invoke((Action)delegate { txtHotkey.Text = b.ToString(); });
        }

        Color bad = Color.FromArgb(255, 200, 200);
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool good = false;

            if (txtName.Text == "") { txtName.BackColor = bad; good = false; }
            else txtName.BackColor = SystemColors.Window;

            if (txtHotkey.Text == "None") { txtHotkey.BackColor = bad; good = false; }
            else txtHotkey.BackColor = SystemColors.Window;

            if (!good) return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
