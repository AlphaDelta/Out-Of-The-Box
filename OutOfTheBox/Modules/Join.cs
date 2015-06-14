using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OutOfTheBox.Common;

namespace OutOfTheBox.Modules
{
    [Module("Join files", 1, 0, 0)]
    public partial class Join : Form
    {
        Font rFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);

        public Join()
        {
            InitializeComponent();

            listFiles.ExtType = ListViewExtendedType.Clean;
            listFiles.SingleColumn = true;

            /* Context menu for list */
            #region Context menu
            ContextMenuStrip cms = new ContextMenuStrip();

            ToolStripItem tsiAdd = new ToolStripMenuItem("Add...");
            tsiAdd.Click += (object sender, EventArgs e) =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

                listFiles.BeginUpdate();
                foreach (string s in ofd.FileNames) AddFile(s);
                listFiles.EndUpdate();
            };
            cms.Items.Add(tsiAdd);

            ToolStripItem tsiRemove = new ToolStripMenuItem("Remove");
            tsiRemove.Enabled = false;
            tsiRemove.Click += (object sender, EventArgs e) => { foreach (ListViewItem i in listFiles.SelectedItems) i.Remove(); if(listFiles.Items.Count < 1) btnJoin.Enabled = false; };
            cms.Items.Add(tsiRemove);

            cms.Items.Add("-");

            ToolStripItem tsiUp = new ToolStripMenuItem("Move up");
            tsiUp.Enabled = false;
            tsiUp.Click += (object sender, EventArgs e) =>
            {
                if (listFiles.SelectedIndices.Count < 1) return;
                if (listFiles.SelectedIndices[0] < 1) return;

                listFiles.BeginUpdate();
                foreach (ListViewItem i in listFiles.SelectedItems)
                {
                    int index = i.Index - 1;
                    listFiles.Items.Remove(i);
                    listFiles.Items.Insert(index, i);
                }
                listFiles.EndUpdate();
            };
            cms.Items.Add(tsiUp);

            ToolStripItem tsiDown = new ToolStripMenuItem("Move down");
            tsiDown.Enabled = false;
            tsiDown.Click += (object sender, EventArgs e) =>
            {
                if (listFiles.SelectedIndices.Count < 1) return;
                if (listFiles.SelectedIndices[listFiles.SelectedIndices.Count - 1] >= listFiles.Items.Count - 1) return;

                listFiles.BeginUpdate();

                Stack<ListViewItem> generic = new Stack<ListViewItem>();
                foreach (ListViewItem i in listFiles.SelectedItems) generic.Push(i);
                foreach (ListViewItem i in generic)
                {
                    int index = i.Index + 1;
                    listFiles.Items.Remove(i);
                    listFiles.Items.Insert(index, i);
                }
                listFiles.EndUpdate();
                generic.Clear();
            };
            cms.Items.Add(tsiDown);

            listFiles.ContextMenuStrip = cms;
            #endregion

            listFiles.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                tsiRemove.Enabled = tsiUp.Enabled = tsiDown.Enabled = listFiles.SelectedItems.Count > 0;
            };

            listFiles.AllowDrop = true;
            listFiles.DragEnter += (object sender, DragEventArgs e) => { if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy; else e.Effect = DragDropEffects.None; };
            listFiles.DragDrop += (object sender, DragEventArgs e) =>
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string s in files) AddFile(s);
            };
        }

        public void AddFile(string path)
        {
            btnJoin.Enabled = true;
            if (!File.Exists(path)) return;
            listFiles.Items.Add(new ListViewItemGradient(path) { BackColor = Color.White, Font = rFont });
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            FileStream output = null;
            FileStream input = null;
            try
            {
                output = File.Open(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.Read);

                foreach (ListViewItem i in listFiles.Items)
                {
                    input = File.Open(i.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
                    while (true)
                    {
                        byte[] buffer = new byte[Internals.BUFFER_SIZE];
                        int read = input.Read(buffer, 0, Internals.BUFFER_SIZE);
                        if (read < 1) break;
                        output.Write(buffer, 0, read);
                        if (read < Internals.BUFFER_SIZE) break;
                    }
                    input.Close();
                }
                MessageBox.Show("Joined " + listFiles.Items.Count + " files successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException) { MessageBox.Show("Could not access file, make sure it is not open in another program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (FileNotFoundException) { MessageBox.Show("File no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (output != null) output.Close();
            if (input != null) input.Close();
        }
    }
}
