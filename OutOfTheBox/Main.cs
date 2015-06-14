using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using OutOfTheBox.Common;
using OutOfTheBox.ModuleTree.SQL;

namespace OutOfTheBox
{
    public partial class Main : Form
    {
        List<Type> Types = new List<Type>(16);
        List<Module> Modules = new List<Module>(16);
        List<Form> Instances = new List<Form>(16);

        public Main()
        {
            InitializeComponent();

            this.Icon = OutOfTheBox.Properties.Resources.icon;

            columnHeader1.Width = list.Width - 4;

            Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

            Font rFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular); //Font for the list items (For line spacing reasons)

            /* Self-reflection to get everything in <root>.Modules and populate Types, Modules, Instances, and the ListView */
            list.BeginUpdate();
            foreach (Type c in classes)
            {
                if (c.BaseType != typeof(Form) || c.Namespace != Internals.ROOT_NAMESPACE + ".Modules") continue; //If it isn't a form or it isn't in <root>.Modules disregard it

                string name = c.Name;
                object[] mods = c.GetCustomAttributes(typeof(Module), false); //Get the custom attributes so that we can get the module information
                Module mod;
                if (mods.Length > 0)
                {
                    mod = (Module)mods[0]; Modules.Add(mod); name = mod.Name + (Internals.DEBUG ? " - 0x" + mod.Version.ToString("X6") : ""); 
                    list.Items.Add(new ListViewItemGradient(name) { BackColor = Color.White, Font = rFont });
                    Types.Add(c);
                }
            }
            list.EndUpdate();

            /* Events */
            this.FormClosing += (object sender, FormClosingEventArgs e) => {
                if (Instances.Count > 0 && MessageBox.Show("There are still modules running, are you sure you want to exit and terminate the running modules?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
                    e.Cancel = true;
            };

            list.SingleColumn = true;
            //this.ResizeBegin += (object sender, EventArgs e) => { list.Scrollable = false; };
            //this.ResizeEnd += (object sender, EventArgs e) => { list.Scrollable = true; };

            list.DoubleClick += (object sender, EventArgs e) =>
            {
                if (list.SelectedIndices.Count < 1) return;
                if (list.SelectedItems[0].BackColor == ListViewExtended.cInuse) { MessageBox.Show("That module is already running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                ListViewItemGradient itm = (ListViewItemGradient)list.SelectedItems[0];
                itm.VirtualBackColor = ListViewExtended.cInuse;
                Form instance = (Form)Activator.CreateInstance(Types[list.SelectedIndices[0]]);
                instance.Disposed += (object s, EventArgs arg) => { itm.VirtualBackColor = Color.White; Instances.Remove((Form)s); };
                instance.Icon = OutOfTheBox.Properties.Resources.iconclear;
                instance.Show();
                itm.Selected = false;
                Instances.Add(instance);
            };

            list.ExtType = ListViewExtendedType.Clean;
        }
    }

    /* I should problably move this into the ListViewExtended, someone remind me to later */
    class ListViewItemGradient : ListViewItem
    {
        public Pen Extreme;
        public LinearGradientBrush Gradient;
        public bool Processed = false;
        //Color _BackgroundColor;
        public Color VirtualBackColor { get { return BackColor; } set { BackColor = value; Processed = false; } }

        public bool Focus = false;

        public ListViewItemGradient(string Name)
        {
            this.Text = Name;
        }
    }
}
