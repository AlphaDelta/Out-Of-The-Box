using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace OutOfTheBox.ModuleTree.SQL
{
    public partial class Query : Form
    {
        Main m;
        public Query(Main m)
        {
            InitializeComponent();

            this.Icon = OutOfTheBox.Properties.Resources.SQL_query_icon;

            this.m = m;
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            string[] spl = Regex.Split(Regex.Replace(queryBox.Text, @";$", ""), @";[\r\n][\r\n]?");

            try
            {
                int affected = 0;
                int selected = 0;
                int queries = 0;
                List<SQLResult> results = new List<SQLResult>();
                foreach (string q in spl)
                {
                    if (q == "") continue;
                    queries++;

                    SQLResult res = m.SQL.Query(q);
                    if(res.Affected > 0) affected += res.Affected;
                    if (res.Result > 0) selected += res.Result;

                    results.Add(res);
                    //if (res != null) MessageBox.Show(res.Result + ":" + res.Affected);
                }

                if (selected > 0)
                {
                    m.listResults.Clear();

                    bool caught = false;
                    foreach (TabPage p in m.tabs.TabPages) if (p == m.tabResults) { caught = true; break; }
                    if (!caught) m.tabs.TabPages.Add(m.tabResults);
                    m.tabs.SelectedTab = m.tabResults;
                    m.tabResults.Select();

                    m.listResults.Select();
                    m.listResults.Focus();

                    this.Activate();

                    m.listResults.BeginUpdate();
                    foreach (SQLResult res in results)
                    {
                        if (res.Result < 1) continue;
                        foreach (string column in res.Columns) m.listResults.Columns.Add(new ColumnHeader() { Text = column });
                        foreach (object[] obj in res.Rows)
                        {
                            if (obj.Length < 1) continue;
                            ListViewItem i = new ListViewItem(obj[0].ToString());
                            bool temp = true;
                            foreach (object o in obj)
                                if (temp) temp = false;
                                else
                                {
                                    string tobj = o.ToString();
                                    if (tobj.Length > 100) tobj = tobj.Substring(0, 100) + "...";
                                    i.SubItems.Add(tobj);
                                }
                            m.listResults.Items.Add(i);
                        }
                        break;
                    }
                    foreach (ColumnHeader h in m.listResults.Columns) h.Width = -1;
                    m.listResults.EndUpdate();
                }

                string TO_S_OR_NOT_TO_S/*, that is the question*/ = (queries > 1 ? " queries" : " query");
                if (affected > 0) MessageBox.Show(queries + TO_S_OR_NOT_TO_S + " executed successfully, " + affected + " rows affected", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if(queries > 0) MessageBox.Show(queries + TO_S_OR_NOT_TO_S + " executed successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            queryBox.Clear();
        }
    }
}
