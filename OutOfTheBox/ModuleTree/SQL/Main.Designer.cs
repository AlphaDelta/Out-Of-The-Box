namespace OutOfTheBox.ModuleTree.SQL
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lSchemata = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabRows = new System.Windows.Forms.TabPage();
            this.listRows = new OutOfTheBox.Common.ListViewExtended();
            this.tabStructure = new System.Windows.Forms.TabPage();
            this.tabResults = new System.Windows.Forms.TabPage();
            this.listResults = new OutOfTheBox.Common.ListViewExtended();
            this.linkClose = new System.Windows.Forms.LinkLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileDump = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileDumpDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileDumpTable = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditDrop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuActionQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.treeSchemata = new OutOfTheBox.Common.TreeViewExtended();
            this.tabs.SuspendLayout();
            this.tabRows.SuspendLayout();
            this.tabResults.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lSchemata
            // 
            this.lSchemata.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lSchemata.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lSchemata.Location = new System.Drawing.Point(0, 25);
            this.lSchemata.Name = "lSchemata";
            this.lSchemata.Size = new System.Drawing.Size(185, 23);
            this.lSchemata.TabIndex = 2;
            this.lSchemata.Text = "Populating...";
            this.lSchemata.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabRows);
            this.tabs.Controls.Add(this.tabStructure);
            this.tabs.Controls.Add(this.tabResults);
            this.tabs.Location = new System.Drawing.Point(192, 24);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(689, 427);
            this.tabs.TabIndex = 3;
            // 
            // tabRows
            // 
            this.tabRows.Controls.Add(this.listRows);
            this.tabRows.Location = new System.Drawing.Point(4, 22);
            this.tabRows.Name = "tabRows";
            this.tabRows.Size = new System.Drawing.Size(681, 401);
            this.tabRows.TabIndex = 2;
            this.tabRows.Text = "Rows";
            this.tabRows.UseVisualStyleBackColor = true;
            // 
            // listRows
            // 
            this.listRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listRows.FullRowSelect = true;
            this.listRows.GridLines = true;
            this.listRows.Location = new System.Drawing.Point(3, 3);
            this.listRows.Name = "listRows";
            this.listRows.OwnerDraw = true;
            this.listRows.SingleColumn = false;
            this.listRows.Size = new System.Drawing.Size(675, 395);
            this.listRows.TabIndex = 0;
            this.listRows.UseCompatibleStateImageBehavior = false;
            this.listRows.View = System.Windows.Forms.View.Details;
            // 
            // tabStructure
            // 
            this.tabStructure.Location = new System.Drawing.Point(4, 22);
            this.tabStructure.Name = "tabStructure";
            this.tabStructure.Padding = new System.Windows.Forms.Padding(3);
            this.tabStructure.Size = new System.Drawing.Size(681, 401);
            this.tabStructure.TabIndex = 1;
            this.tabStructure.Text = "Structure";
            this.tabStructure.UseVisualStyleBackColor = true;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.listResults);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Size = new System.Drawing.Size(681, 401);
            this.tabResults.TabIndex = 3;
            this.tabResults.Text = "Results";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // listResults
            // 
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.FullRowSelect = true;
            this.listResults.GridLines = true;
            this.listResults.Location = new System.Drawing.Point(3, 3);
            this.listResults.Name = "listResults";
            this.listResults.OwnerDraw = true;
            this.listResults.SingleColumn = false;
            this.listResults.Size = new System.Drawing.Size(675, 395);
            this.listResults.TabIndex = 1;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.View = System.Windows.Forms.View.Details;
            // 
            // linkClose
            // 
            this.linkClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkClose.AutoSize = true;
            this.linkClose.BackColor = System.Drawing.Color.Transparent;
            this.linkClose.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkClose.Location = new System.Drawing.Point(861, 28);
            this.linkClose.Name = "linkClose";
            this.linkClose.Size = new System.Drawing.Size(14, 13);
            this.linkClose.TabIndex = 2;
            this.linkClose.TabStop = true;
            this.linkClose.Text = "X";
            this.linkClose.Visible = false;
            this.linkClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.editToolStripMenuItem,
            this.menuAction});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(878, 24);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileDump});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuFileDump
            // 
            this.menuFileDump.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileDumpDatabase,
            this.menuFileDumpTable});
            this.menuFileDump.Image = global::OutOfTheBox.Properties.Resources.SQL_dump;
            this.menuFileDump.Name = "menuFileDump";
            this.menuFileDump.Size = new System.Drawing.Size(107, 22);
            this.menuFileDump.Text = "Dump";
            // 
            // menuFileDumpDatabase
            // 
            this.menuFileDumpDatabase.Enabled = false;
            this.menuFileDumpDatabase.Image = global::OutOfTheBox.Properties.Resources.SQL_folder_database;
            this.menuFileDumpDatabase.Name = "menuFileDumpDatabase";
            this.menuFileDumpDatabase.Size = new System.Drawing.Size(131, 22);
            this.menuFileDumpDatabase.Text = "Database...";
            // 
            // menuFileDumpTable
            // 
            this.menuFileDumpTable.Enabled = false;
            this.menuFileDumpTable.Image = global::OutOfTheBox.Properties.Resources.SQL_folder_table;
            this.menuFileDumpTable.Name = "menuFileDumpTable";
            this.menuFileDumpTable.Size = new System.Drawing.Size(131, 22);
            this.menuFileDumpTable.Text = "Table...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditInsert,
            this.menuEditEdit,
            this.menuEditRemove,
            this.toolStripSeparator2,
            this.menuEditDrop,
            this.menuEditEmpty});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuEditInsert
            // 
            this.menuEditInsert.Enabled = false;
            this.menuEditInsert.Image = global::OutOfTheBox.Properties.Resources.SQL_insert;
            this.menuEditInsert.Name = "menuEditInsert";
            this.menuEditInsert.Size = new System.Drawing.Size(151, 22);
            this.menuEditInsert.Text = "Insert new row";
            // 
            // menuEditEdit
            // 
            this.menuEditEdit.Enabled = false;
            this.menuEditEdit.Image = global::OutOfTheBox.Properties.Resources.SQL_edit;
            this.menuEditEdit.Name = "menuEditEdit";
            this.menuEditEdit.Size = new System.Drawing.Size(151, 22);
            this.menuEditEdit.Text = "Edit";
            // 
            // menuEditRemove
            // 
            this.menuEditRemove.Enabled = false;
            this.menuEditRemove.Image = global::OutOfTheBox.Properties.Resources.SQL_remove;
            this.menuEditRemove.Name = "menuEditRemove";
            this.menuEditRemove.Size = new System.Drawing.Size(151, 22);
            this.menuEditRemove.Text = "Remove";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // menuEditDrop
            // 
            this.menuEditDrop.Enabled = false;
            this.menuEditDrop.Image = global::OutOfTheBox.Properties.Resources.SQL_drop;
            this.menuEditDrop.Name = "menuEditDrop";
            this.menuEditDrop.Size = new System.Drawing.Size(151, 22);
            this.menuEditDrop.Text = "Drop";
            // 
            // menuEditEmpty
            // 
            this.menuEditEmpty.Enabled = false;
            this.menuEditEmpty.Image = global::OutOfTheBox.Properties.Resources.SQL_empty;
            this.menuEditEmpty.Name = "menuEditEmpty";
            this.menuEditEmpty.Size = new System.Drawing.Size(151, 22);
            this.menuEditEmpty.Text = "Empty";
            // 
            // menuAction
            // 
            this.menuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuActionQuery});
            this.menuAction.Name = "menuAction";
            this.menuAction.Size = new System.Drawing.Size(54, 20);
            this.menuAction.Text = "Action";
            // 
            // menuActionQuery
            // 
            this.menuActionQuery.Image = global::OutOfTheBox.Properties.Resources.SQL_query;
            this.menuActionQuery.Name = "menuActionQuery";
            this.menuActionQuery.Size = new System.Drawing.Size(106, 22);
            this.menuActionQuery.Text = "Query";
            this.menuActionQuery.Click += new System.EventHandler(this.menuActionQuery_Click);
            // 
            // treeSchemata
            // 
            this.treeSchemata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeSchemata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeSchemata.HideSelection = false;
            this.treeSchemata.HotTracking = true;
            this.treeSchemata.Location = new System.Drawing.Point(-1, 24);
            this.treeSchemata.Name = "treeSchemata";
            this.treeSchemata.Size = new System.Drawing.Size(187, 427);
            this.treeSchemata.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 449);
            this.Controls.Add(this.linkClose);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.lSchemata);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.treeSchemata);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Main";
            this.tabs.ResumeLayout(false);
            this.tabRows.ResumeLayout(false);
            this.tabResults.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.MenuStrip menuStrip;
        public Common.TreeViewExtended treeSchemata;
        public System.Windows.Forms.Label lSchemata;
        public System.Windows.Forms.TabControl tabs;
        public System.Windows.Forms.TabPage tabStructure;
        public System.Windows.Forms.TabPage tabRows;
        public Common.ListViewExtended listRows;
        public System.Windows.Forms.ToolStripMenuItem menuFile;
        public System.Windows.Forms.ToolStripMenuItem menuFileDump;
        public System.Windows.Forms.ToolStripMenuItem menuFileDumpDatabase;
        public System.Windows.Forms.ToolStripMenuItem menuFileDumpTable;
        public System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem menuEditInsert;
        public System.Windows.Forms.ToolStripMenuItem menuEditEdit;
        public System.Windows.Forms.ToolStripMenuItem menuEditRemove;
        public System.Windows.Forms.ToolStripMenuItem menuEditDrop;
        public System.Windows.Forms.ToolStripMenuItem menuEditEmpty;
        public System.Windows.Forms.ToolStripMenuItem menuAction;
        public System.Windows.Forms.ToolStripMenuItem menuActionQuery;
        public System.Windows.Forms.TabPage tabResults;
        public Common.ListViewExtended listResults;
        private System.Windows.Forms.LinkLabel linkClose;
    }
}