using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OutOfTheBox.ModuleTree.SQL
{
    public partial class Main : Form
    {
        public SQLType SQL;
        public string version = "";
        public static ulong queuetimer = 0;
        public Main(SQLType sql)
        {
            InitializeComponent();

            this.Icon = OutOfTheBox.Properties.Resources.SQL_icon;

            ImageList imglist = new ImageList();
            imglist.ImageSize = new Size(16, 16);
            imglist.Images.Add(Properties.Resources.SQL_database);
            imglist.Images.Add(Properties.Resources.SQL_table);
            imglist.Images.Add(Properties.Resources.SQL_hourglass);
            imglist.Images.Add(Properties.Resources.SQL_table_error);
            treeSchemata.ImageList = imglist;
            
            this.SQL = sql;

            this.FormClosed += delegate { SQL.Dispose(); };

            if (sql.GetType() == typeof(MySQL))
            {
                SQLResult q = SQL.Query("SELECT VERSION()");
                if (q.Rows.Length == 1 && q.Rows[0].Length == 1 && q.Rows[0][0].GetType() == typeof(string))
                {
                    version = (string)q.Rows[0][0];
                    //label1.Text = version;
                    string[] ver = version.Split('.');
                    int v = 0, vminor = 0;
                    if (Int32.TryParse(ver[0].ToString(), out v) && Int32.TryParse(ver[1].ToString(), out v) && (v > 4 || (v == 4 && vminor > 12))) ((MySQL)sql).schemata = true;
                }
                //else label1.Text = "Unknown";

                Common.Async.StartAsync((Common.Action)delegate
                {
                    sql.PopulateTree(treeSchemata);
                    BeginInvoke((Common.Action)delegate { lSchemata.Visible = false; });
                });
            }


            #region Events
            treeSchemata.BeforeExpand += (object sender, TreeViewCancelEventArgs e) =>
            {
                if(e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "Populating...")
                Common.Async.StartAsync((Common.Action)delegate {
                    sql.PopulateNode(e.Node);
                    BeginInvoke((Common.Action)delegate { e.Node.FirstNode.Remove(); });
                });
            };

            int state = -1;
            treeSchemata.AfterSelect += (object sender, TreeViewEventArgs e) =>
            {
                if (Main.queuetimer > 0) { Main.queuetimer = 5; }
                if (e.Node.Text == "Populating...") return;
                
                if (state != e.Node.Level) { foreach (TabPage p in tabs.TabPages) if(p != tabResults) tabs.TabPages.Remove(p); }
                if (e.Node.Level == 0)
                {
                    if (state != e.Node.Level)
                    {
                        tabRows.Text = "Tables";
                        tabs.TabPages.Add(tabRows);
                    }
                    menuFileDumpDatabase.Enabled = true;
                    menuFileDumpTable.Enabled = menuEditInsert.Enabled = false;
                }
                else if (e.Node.Level == 1)
                {
                    if (state != e.Node.Level)
                    {
                        tabRows.Text = "Rows";
                        tabs.TabPages.Add(tabRows);
                        tabs.TabPages.Add(tabStructure);
                    }
                    menuEditInsert.Enabled = menuFileDumpDatabase.Enabled = menuFileDumpTable.Enabled = true;

                    Common.Async.StartAsync((Common.Action)delegate
                    {
                        sql.PopulateTable(listRows, e.Node, e.Node.Text, e.Node.Parent.Text);
                        Main.queuetimer = 0;
                    });
                }
                else
                {
                    menuEditInsert.Enabled =
                    menuFileDumpDatabase.Enabled =
                    menuFileDumpTable.Enabled = false;
                }
                state = e.Node.Level;
                (sender as TreeView).Focus();
            };

            listRows.SelectedIndexChanged += delegate
            {
                menuEditEdit.Enabled = menuEditRemove.Enabled = listRows.SelectedItems.Count > 0;
            };
            #endregion

            tabs.SelectedIndexChanged += delegate { linkClose.Visible = tabs.SelectedTab == tabResults; };

            foreach (TabPage p in tabs.TabPages) tabs.TabPages.Remove(p);

            SQL.OnInitialized(this);
        }

        private void menuActionQuery_Click(object sender, EventArgs e)
        {
            ModuleTree.SQL.Query q = new ModuleTree.SQL.Query(this);
            if (SQL.GetType() == typeof(MySQLi)) q.Text = "Manual injection";
            q.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabs.TabPages.Remove(tabResults);
            listResults.Clear();
        }

        public static string Sanitize(string inpt)
        {
            return inpt.Replace("`", "\\`").Replace(@"\", @"\\");
        }
    }

    public class SQLResult
    {
        public int Affected = -1;
        public int Result = -1;

        public object[][] Rows;
        public string[] Columns;
    }

    public class SQLType
    {
        public string Version = "Null";
        public virtual SQLResult Query(string q) { return null; }
        public virtual void Dispose() { }

        public virtual void OnInitialized(Main m) { }

        public virtual void PopulateTree(TreeView t) { }
        public virtual void PopulateNode(TreeNode n) { }
        public virtual void PopulateTable(ListView l, TreeNode node, string table, string database) { }
    }

    public class MySQL : SQLType
    {
        MySqlConnection connection;
        public bool schemata = false;

        public MySQL(MySqlConnection c)
        {
            connection = c;
        }

        public override SQLResult Query(string q)
        {
            SQLResult result = new SQLResult();
            connection.Open();
            
            try
            {
                MySqlCommand cmd = new MySqlCommand(q, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                result.Affected = reader.RecordsAffected;
                result.Result = 0;
                if (reader.HasRows)
                {
                    int num = 0;
                    List<object[]> rowlist = new List<object[]>(16);
                    List<string> columns = new List<string>(16);
                    bool readcolumns = false;
                    while (reader.Read())
                    {
                        if (!readcolumns)
                        {
                            for (int i = 0; i < reader.VisibleFieldCount; i++) columns.Add(reader.GetName(i));
                            result.Columns = columns.ToArray();
                            readcolumns = true;
                        }
                        num++;
                        List<object> row = new List<object>(16);

                        for (int i = 0; i < reader.VisibleFieldCount; i++) row.Add(reader.GetValue(i));

                        rowlist.Add(row.ToArray());
                    }

                    result.Rows = rowlist.ToArray();
                    result.Result = num;
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception e) { connection.Close(); throw e; }
            connection.Close();

            return result;
        }

        public override void PopulateTree(TreeView t)
        {
            if (schemata)
            {
                SQLResult q = this.Query("SELECT `schema_name` FROM `information_schema`.`SCHEMATA`");

                foreach (object[] row in q.Rows)
                    if (row.Length > 0)
                        t.BeginInvoke((Common.Action)delegate {
                            TreeNode node = new TreeNode((string)row[0]);
                            node.ImageIndex = 0;
                            node.Nodes.Add(new TreeNode("Populating...") { ImageIndex = 2, SelectedImageIndex = 2 });
                            t.Nodes.Add(node);
                        });
            }
        }

        public override void PopulateNode(TreeNode n)
        {
            SQLResult q = this.Query("SELECT `table_name` FROM `information_schema`.`TABLES` WHERE table_schema='" + Main.Sanitize(n.Text) + "'");

            foreach (object[] row in q.Rows)
                if (row.Length > 0)
                    n.TreeView.BeginInvoke((Common.Action)delegate
                    {
                        TreeNode node = new TreeNode((string)row[0]);
                        node.ImageIndex = 1;
                        node.SelectedImageIndex = 1;
                        n.Nodes.Add(node);
                    });
        }

        Object threadlock = new Object();
        int PopulateTable_iteration = 0;
        
        public override void PopulateTable(ListView l, TreeNode node, string table, string database)
        {
            PopulateTable_iteration++;
            int store = PopulateTable_iteration;
            Main.queuetimer = 5;
            lock (threadlock)
            {
                try
                {
                    if (PopulateTable_iteration != store) return;
                    l.Invoke((Common.Action)delegate { l.Columns.Clear(); l.Items.Clear(); });

                    SQLResult columns = this.Query("SELECT `column_name` FROM `information_schema`.`COLUMNS` WHERE table_name='" + Main.Sanitize(table) + "'");
                    foreach (object[] col in columns.Rows) l.BeginInvoke((Common.Action)delegate { l.Columns.Add((string)col[0]); });

                    SQLResult rows = this.Query("SELECT * FROM `" + Main.Sanitize(database) + "`.`" + Main.Sanitize(table) + "` LIMIT 0,50");
                    l.Invoke((Common.Action)delegate { l.BeginUpdate(); });
                    foreach (object[] row in rows.Rows)
                        l.Invoke((Common.Action)delegate
                        {
                            ListViewItem i = new ListViewItem(row[0].ToString());
                            bool temp = true;
                            foreach (object obj in row)
                            {
                                string tobj = obj.ToString();
                                if (tobj.Length > 100) tobj = tobj.Substring(0, 100) + "...";
                                if (temp) temp = false; else i.SubItems.Add(tobj);
                            }
                            l.Items.Add(i);
                            
                        });

                    l.Invoke((Common.Action)delegate
                    {
                        foreach (ColumnHeader h in l.Columns) h.Width = -1;
                        l.EndUpdate();
                    });
                    while (Main.queuetimer > 0) { Thread.Sleep(100); Main.queuetimer--; };
                }
                catch { l.BeginInvoke((Common.Action)delegate { l.EndUpdate(); node.ImageIndex = 3; node.SelectedImageIndex = 3; }); }
            }
        }

        public override void Dispose()
        {
            if (connection != null) { connection.Close(); connection.Dispose(); }
        }
    }

    public class MySQLi : SQLType
    {
        public override void OnInitialized(Main m)
        {
            //m.menuActionQuery.Text = "Inject";
            m.Enabled = false; //I don't see any way for this to be possible, so I'll just disable it
        }
    }
}
