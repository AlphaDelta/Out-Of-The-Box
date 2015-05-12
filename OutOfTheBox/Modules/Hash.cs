using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace OutOfTheBox.Modules
{
    [Module("Hash data (Checksum)", 1, 0, 0)]
    public partial class Hash : Form
    {
        public Hash()
        {
            InitializeComponent();

            pMain.Visible = false;

            this.AllowDrop = true;
            this.DragEnter += (object sender, DragEventArgs e) => { e.Effect = DragDropEffects.Copy; };
            this.DragDrop += (object sender, DragEventArgs e) =>
            {
                try
                {
                    if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    {
                        string data = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                        txtData.Text = data;
                        if (!File.Exists(data)) { MessageBox.Show("File no longer exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        if (new FileInfo(data).Length > Int32.MaxValue) { MessageBox.Show("File is too large, must be less than 2GB in size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        byte[] buffer = File.ReadAllBytes(data);
                        HexDump(buffer);
                        ComputeHashes(buffer);
                    }
                    if (e.Data.GetDataPresent(DataFormats.UnicodeText))
                    {
                        string data = (string)e.Data.GetData(DataFormats.UnicodeText);
                        txtData.Text = "Text";
                        byte[] buffer = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("Windows-1252"), System.Text.Encoding.UTF8.GetBytes(data));
                        HexDump(buffer);
                        ComputeHashes(buffer);
                    }

                    imgBox.Visible = lDrop.Visible = false;
                    pMain.Visible = true;
                    this.Size = new Size(525, 398);
                }
                catch (IOException) { MessageBox.Show("File could not be accessed, make sure another program is not already using it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            };
            this.KeyPreview = true;
            this.KeyDown += (object sender, KeyEventArgs e) =>
            {
                if (e.Control && e.KeyCode == Keys.V)
                {
                    if (Clipboard.ContainsText())
                    {
                        imgBox.Visible = lDrop.Visible = false;
                        pMain.Visible = true;
                        this.Size = new Size(525, 398);

                        string data = Clipboard.GetText();
                        txtData.Text = "Text";
                        byte[] buffer = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("Windows-1252"), System.Text.Encoding.UTF8.GetBytes(data));
                        HexDump(buffer);
                        ComputeHashes(buffer);
                    }
                    else if (Clipboard.ContainsFileDropList())
                    {
                        try
                        {
                            string data = Clipboard.GetFileDropList()[0];
                            txtData.Text = data;
                            if (!File.Exists(data)) { MessageBox.Show("File no longer exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            if (new FileInfo(data).Length > Int32.MaxValue) { MessageBox.Show("File is too large, must be less than 2GB in size", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            byte[] buffer = File.ReadAllBytes(data);
                            HexDump(buffer);
                            ComputeHashes(buffer);

                            imgBox.Visible = lDrop.Visible = false;
                            pMain.Visible = true;
                            this.Size = new Size(525, 398);
                        }
                        catch (IOException) { MessageBox.Show("File could not be accessed, make sure another program is not already using it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            };
        }

        const int HEX_DUMP_SIZE = 16; //HAKEHAKEHAKE
        void HexDump(byte[] data)
        {
            /* Abandon all ye who enter here, fer code spaghetti awaits ye */

            if (data.Length > 2097152) { txtHex.Text = "Hex dump unavailable";  return; }
            txtHex.Clear();
            string datastr = Encoding.Default.GetString(data);

            StringBuilder sb = new StringBuilder(data.Length * 4);
            /* Hex dump */
            int i = 0;
            for (; i < data.Length; i++)
            {
                if (i % HEX_DUMP_SIZE == 0 && i != 0)
                {
                    for (int j = HEX_DUMP_SIZE; j > 0; j--) { int n = i - j; sb.Append((j == HEX_DUMP_SIZE ? "   " : "") + (CheckChar(datastr[n]) ? '.' : datastr[n])); }
                    sb.Append("\r\n");
                }
                sb.Append((i % HEX_DUMP_SIZE == 0 || i == 0 ? "" : " ") + data[i].ToString("X2"));

            }

            /* Final string at the end, I could have probably implemented this somehow in the above loop but effort though */
            string spaces = "";
            int mod = (i % HEX_DUMP_SIZE == 0 ? HEX_DUMP_SIZE : i % HEX_DUMP_SIZE);
            for (int j = ((HEX_DUMP_SIZE - mod) * 3 + 3); j > 0; j--) spaces += " ";

            for (int j = mod; j > 0; j--)
            {
                int n = i - j;
                sb.Append((j == mod ? spaces : "") + (CheckChar(datastr[n]) ? '.' : datastr[n]));
            }
            txtHex.Text = sb.ToString();
        }

        bool CheckChar(char a)
        {
            if (
                a <= 0x1F ||
                a == 0x7F ||
                a == 0x81 ||
                a == 0x8D ||
                a == 0x8F ||
                a == 0x90 ||
                a == 0xAD)
                return true;
            return false;
        }

        MD5 md5p = new MD5CryptoServiceProvider();
        SHA1 sha1p = new SHA1CryptoServiceProvider();
        SHA256 sha256p = SHA256Managed.Create();
        SHA512 sha512p = SHA512Managed.Create();
        void ComputeHashes(byte[] buffer)
        {
            /* TODO: Instead of getting the file entirely in a byte[] get it as a stream then use TransformBlock and TransformFinalBlock to support files larger than 4GiB. */
            byte[] md5b = md5p.ComputeHash(buffer);
            string md5 = "";
            for (int i = 0; i < md5b.Length; i++) md5 += md5b[i].ToString("X2").ToLower();
            txtMD5.Text = md5;

            byte[] sha1b = sha1p.ComputeHash(buffer);
            string sha1 = "";
            for (int i = 0; i < sha1b.Length; i++) sha1 += sha1b[i].ToString("X2").ToLower();
            txtSHA1.Text = sha1;

            byte[] sha256b = sha256p.ComputeHash(buffer);
            string sha256 = "";
            for (int i = 0; i < sha256b.Length; i++) sha256 += sha256b[i].ToString("X2").ToLower();
            txtSHA256.Text = sha256;

            byte[] sha512b = sha512p.ComputeHash(buffer);
            string sha512 = "";
            for (int i = 0; i < sha512b.Length; i++) sha512 += sha512b[i].ToString("X2").ToLower();
            txtSHA512.Text = sha512;
        }
    }
}
