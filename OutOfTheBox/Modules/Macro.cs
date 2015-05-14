using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OutOfTheBox.Common;
using OutOfTheBox.ModuleTree.Macro;

namespace OutOfTheBox.Modules
{
    [Module("Macro", 1, 0, 0)]
    public partial class Macro : Form
    {
        NotifyIcon sysTray;

        bool pass = false, flag = true;

        public Macro()
        {
            InitializeComponent();

            ImageList il = new ImageList();
            il.ImageSize = new Size(16, 16);
            il.Images.Add(Properties.Resources.MACRO_off1);
            il.Images.Add(Properties.Resources.MACRO_on1);

            list.LargeImageList = il;
            list.SmallImageList = il;

            list.Items.Add(new ListViewItem("Words") { ImageIndex = 1 });

            //ShowInTaskbar = false;
            //WindowState = FormWindowState.Minimized;

            FormClosing += new FormClosingEventHandler(delegate(object s, FormClosingEventArgs evt)
            {
                if (!pass)
                {
                    WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = false;
                    evt.Cancel = true;
                }
                else sysTray.Dispose();
                //if (MessageBox.Show("Are you sure you want to terminate Macro?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                //    evt.Cancel = true;
                //else
                //    sysTray.Dispose();
            });

            this.Disposed += delegate { sysTray.Dispose(); };
            //this.FormClosing += delegate { sysTray.Dispose(); };

            //this.ResizeBegin += delegate { list.Scrollable = false; };
            //this.Resize += delegate { column.Width = list.Width - 3; }; column.Width = list.Width - 3;
            //this.ResizeEnd += delegate { list.Scrollable = true; };

            //list.ExtType = Common.ListViewExtendedType.Clean;
            colName.Width = -1;
            colHotkey.Width = -1;

            /* Context menu */
            ContextMenuStrip cm = new ContextMenuStrip();

            ToolStripMenuItem m1 = new ToolStripMenuItem("Add macro...");
            m1.Click += delegate
            {
                AddMacro f = new AddMacro();
                f.ShowDialog();
            };
            cm.Items.Add(m1);

            list.ContextMenuStrip = cm;

            /* System tray */
            #region System tray
            sysTray = new NotifyIcon();
            sysTray.Icon = Properties.Resources.MACRO_on;
            sysTray.Visible = true;
            sysTray.Text = "Macro";
            sysTray.MouseDoubleClick += delegate
            {
                ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
            };

            sysTray.ShowBalloonTip(3000, "Macro", "Macro is now running", ToolTipIcon.Info);
            sysTray.BalloonTipClicked += delegate
            {
                ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
            };

            ContextMenu menu = new ContextMenu();

            MenuItem i1 = new MenuItem("Show...");
            i1.Click += delegate
            {
                ShowInTaskbar = true;
                WindowState = FormWindowState.Normal;
            };
            i1.DefaultItem = true;
            menu.MenuItems.Add(i1);

            MenuItem i2 = new MenuItem("Disable");
            i2.Click += delegate
            {
                flag = !flag;
                i2.Text = (flag ? "Disable" : "Enable");
                sysTray.Icon = (flag ? Properties.Resources.MACRO_on : Properties.Resources.MACRO_off);
            };
            menu.MenuItems.Add(i2);

            menu.MenuItems.Add("-");

            MenuItem i3 = new MenuItem("Exit");
            i3.Click += delegate
            {
                pass = true;
                this.Close();
            };
            menu.MenuItems.Add(i3);

            sysTray.ContextMenu = menu;
            #endregion
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
