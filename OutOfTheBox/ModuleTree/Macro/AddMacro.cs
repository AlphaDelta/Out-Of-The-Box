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
            gbAction.Enabled = btnAdd.Enabled = btnCancel.Enabled = txtName.Enabled = flag;
            flag = !flag;

            if (flag)
            {
                WinAPI.KeyboardHook.Hook();
                WinAPI.KeyboardHook.DownEvent += KeyDownEvent;
            }
            else
            {
                WinAPI.KeyboardHook.DownEvent -= KeyDownEvent;
                WinAPI.KeyboardHook.Unhook();
            }
        }

        void KeyDownEvent(WinAPI.KeyboardHook.DownEventArgs e)
        {
            MessageBox.Show(WinAPI.KeyboardHook.Names[e.Key]);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
