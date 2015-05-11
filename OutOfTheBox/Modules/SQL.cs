using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OutOfTheBox.Modules
{
    [Module("SQL Manager", 1, 0, 0)]
    public partial class SQL : Form
    {
        Point pPosition = new Point(0, 129);
        Size pSize = new Size(400, 203);
        public SQL()
        {
            InitializeComponent();

            /* Set up pDBType tooltips for the radio buttons*/
            ToolTip ttMySQL = new ToolTip();
            ttMySQL.AutomaticDelay = 500;
            ttMySQL.SetToolTip(this.rMySQL, "Direct connection, could fail if access is allowed to localhost only");

            ToolTip ttMySQL_PHP = new ToolTip();
            ttMySQL_PHP.AutomaticDelay = 500;
            ttMySQL_PHP.SetToolTip(this.rMySQL_PHP, "PHP to Application SQL Service"); //Shh, dun' tell nobody, it's a sekwit

            ToolTip ttMySQLi = new ToolTip();
            ttMySQLi.AutomaticDelay = 500;
            ttMySQLi.SetToolTip(this.rMySQLi, "SQL injection vector");

            /* Set-up panels */
            pDBType.Location = pPosition;
            pDBType.Size = pSize;

            pConnecting.Location = pPosition;
            pConnecting.Size = pSize;
            pConnecting.Visible = false;

            /* Event handlers */
            EventHandler h = delegate { btnConnect.Enabled = (txtHost.Text.Length > 0 && txtUser.Text.Length > 0); };
            txtHost.TextChanged += h;
            txtUser.TextChanged += h;
            txtPassword.TextChanged += h;
            h(null, null);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (rMySQL.Checked || rMySQL_PHP.Checked || rMySQLi.Checked) lConnecting.Text = "Connecting to MySQL database";
            else if (rMSSQL.Checked) lConnecting.Text = "Connecting to MSSQL database";
            else return;

            pDBType.Visible = false;
            pConnecting.Visible = true;

            if (rMySQL.Checked)
            {
                string[] host = txtHost.Text.Split(':');

                int port;
                if (host.Length < 2) port = 3306;
                else if (!Int32.TryParse(host[1], out port)) { pDBType.Visible = true; pConnecting.Visible = false; MessageBox.Show("The host port must be a valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                this.ControlBox = false;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection("server='" + host[0] + "';port='" + port + "';uid='" + txtUser.Text.Replace("'", "\'") + "';pwd='" + txtPassword.Text.Replace("'", "\'") + "';");
                        connection.Open();
                        connection.Close();
                        BeginInvoke((Common.Action)delegate
                        {
                            ModuleTree.SQL.Main main = new ModuleTree.SQL.Main(new ModuleTree.SQL.MySQL(connection));
                            main.Show();
                            main.FormClosed += delegate { this.Close(); };
                            this.Hide();
                        });
                    }
                    catch (Exception mse)
                    {
                        if (!this.Disposing && !this.IsDisposed)
                        {
                            Invoke((Common.Action)delegate
                            {
                                pDBType.Visible = true;
                                pConnecting.Visible = false;
                                this.ControlBox = true;
                            });
                        }
                        MessageBox.Show(mse.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };
                worker.RunWorkerAsync();
            }
            if (rMySQLi.Checked)
            {
                ModuleTree.SQL.Main main = new ModuleTree.SQL.Main(new ModuleTree.SQL.MySQLi());
                main.Show();
                main.FormClosed += delegate { this.Close(); };
                this.Hide();
            }
        }
    }
}
