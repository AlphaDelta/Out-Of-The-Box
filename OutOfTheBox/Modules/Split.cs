using System;
using System.IO;
using System.Windows.Forms;
using OutOfTheBox.Common;

namespace OutOfTheBox.Modules
{
    [Module("Split file", 1, 0, 0)]
    public partial class Split : Form
    {
        ToolTip info = new ToolTip();
        public Split()
        {
            InitializeComponent();

            ddSize.SelectedIndex = 2;

            info.InitialDelay = 5000; //This may or may not be wrong (s as opposed to ms), but it really doesn't matter.
            info.ShowAlways = true;
            txtFormat.MouseHover += (object sender, EventArgs e) => { info.Show("^N = File name\n^P = Part number\n^E = Extension", txtFormat, 0, txtFormat.Height); };
            txtFormat.MouseLeave += (object sender, EventArgs e) => { info.Hide(txtFormat); };

            string format = "";
            if (Settings.Get<string>("SPLIT_format", ref format)) txtFormat.Text = format;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            Settings.Set<string>("SPLIT_format", txtFormat.Text);

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName)) MessageBox.Show("File no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    FileInfo finfo = new FileInfo(ofd.FileName);
                    long psize = (int)numSize.Value * (long)Math.Pow(1024, ddSize.SelectedIndex);
                    int parts = (int)Math.Ceiling((double)finfo.Length / (double)psize);

                    if (parts < 2) MessageBox.Show("Part size must be less than the size of the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        if (MessageBox.Show("Are you sure you would like to split this file into " + parts + " parts?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            string dir = Path.GetDirectoryName(ofd.FileName) + "\\";
                            string file = txtFormat.Text.Replace("^N", Path.GetFileNameWithoutExtension(ofd.FileName)).Replace("^E", Path.GetExtension(ofd.FileName).Replace(".", ""));

                            int part = 0;
                            int i = 0;
                            FileStream fs = null;
                            FileStream input = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                            try
                            {

                                int cbyte = -1;
                                while ((cbyte = input.ReadByte()) != -1)
                                {
                                    if (i % psize == 0)
                                    {
                                        part++;
                                        if(fs != null) fs.Close();
                                        fs = File.Open(dir + file.Replace("^P", part.ToString()), FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                                    }
                                    fs.WriteByte((byte)cbyte);
                                    i++;
                                }
                                fs.Close();
                                input.Close();
                                MessageBox.Show("Split file successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (UnauthorizedAccessException) { MessageBox.Show("Could not access file, make sure it is not open in another program", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            catch (FileNotFoundException) { MessageBox.Show("File no longer exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            catch (IOException ex) { MessageBox.Show((part == 0 ? ex.ToString() : "Could not write to " + file.Replace("^P", part.ToString() + "") + ", make sure it doesn't already exist."), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            if (fs != null) fs.Close();
                            if (input != null) input.Close();
                        }
                    }
                }
            }
            ofd.Dispose();
        }
    }
}
