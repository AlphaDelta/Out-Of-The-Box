namespace OutOfTheBox.Modules
{
    partial class SQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQL));
            this.imgConnect = new System.Windows.Forms.PictureBox();
            this.pDBType = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.lUser = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rMySQLi = new System.Windows.Forms.RadioButton();
            this.rMySQL_PHP = new System.Windows.Forms.RadioButton();
            this.rMSSQL = new System.Windows.Forms.RadioButton();
            this.rMySQL = new System.Windows.Forms.RadioButton();
            this.btnConnect = new System.Windows.Forms.Button();
            this.box = new System.Windows.Forms.Label();
            this.pConnecting = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lConnecting = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imgConnect)).BeginInit();
            this.pDBType.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pConnecting.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgConnect
            // 
            this.imgConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgConnect.Image = ((System.Drawing.Image)(resources.GetObject("imgConnect.Image")));
            this.imgConnect.Location = new System.Drawing.Point(-1, -1);
            this.imgConnect.Name = "imgConnect";
            this.imgConnect.Size = new System.Drawing.Size(402, 130);
            this.imgConnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgConnect.TabIndex = 0;
            this.imgConnect.TabStop = false;
            // 
            // pDBType
            // 
            this.pDBType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pDBType.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pDBType.Controls.Add(this.groupBox2);
            this.pDBType.Controls.Add(this.groupBox1);
            this.pDBType.Controls.Add(this.btnConnect);
            this.pDBType.Controls.Add(this.box);
            this.pDBType.Location = new System.Drawing.Point(1, 12);
            this.pDBType.Name = "pDBType";
            this.pDBType.Size = new System.Drawing.Size(285, 90);
            this.pDBType.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lPassword);
            this.groupBox2.Controls.Add(this.lUser);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.lHost);
            this.groupBox2.Controls.Add(this.txtHost);
            this.groupBox2.Location = new System.Drawing.Point(156, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 40);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information";
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(8, 71);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(56, 13);
            this.lPassword.TabIndex = 4;
            this.lPassword.Text = "Password:";
            // 
            // lUser
            // 
            this.lUser.AutoSize = true;
            this.lUser.Location = new System.Drawing.Point(6, 44);
            this.lUser.Name = "lUser";
            this.lUser.Size = new System.Drawing.Size(58, 13);
            this.lUser.TabIndex = 3;
            this.lUser.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(70, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(41, 20);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUser.Location = new System.Drawing.Point(70, 42);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(41, 20);
            this.txtUser.TabIndex = 5;
            // 
            // lHost
            // 
            this.lHost.AutoSize = true;
            this.lHost.Location = new System.Drawing.Point(32, 19);
            this.lHost.Name = "lHost";
            this.lHost.Size = new System.Drawing.Size(32, 13);
            this.lHost.TabIndex = 1;
            this.lHost.Text = "Host:";
            // 
            // txtHost
            // 
            this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHost.Location = new System.Drawing.Point(70, 16);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(41, 20);
            this.txtHost.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.rMySQLi);
            this.groupBox1.Controls.Add(this.rMySQL_PHP);
            this.groupBox1.Controls.Add(this.rMSSQL);
            this.groupBox1.Controls.Add(this.rMySQL);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 40);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database system";
            // 
            // rMySQLi
            // 
            this.rMySQLi.AutoSize = true;
            this.rMySQLi.Location = new System.Drawing.Point(10, 65);
            this.rMySQLi.Name = "rMySQLi";
            this.rMySQLi.Size = new System.Drawing.Size(100, 17);
            this.rMySQLi.TabIndex = 2;
            this.rMySQLi.TabStop = true;
            this.rMySQLi.Text = "MySQL (Vector)";
            this.rMySQLi.UseVisualStyleBackColor = true;
            // 
            // rMySQL_PHP
            // 
            this.rMySQL_PHP.AutoSize = true;
            this.rMySQL_PHP.Location = new System.Drawing.Point(10, 42);
            this.rMySQL_PHP.Name = "rMySQL_PHP";
            this.rMySQL_PHP.Size = new System.Drawing.Size(97, 17);
            this.rMySQL_PHP.TabIndex = 1;
            this.rMySQL_PHP.Text = "MySQL (PASS)";
            this.rMySQL_PHP.UseVisualStyleBackColor = true;
            // 
            // rMSSQL
            // 
            this.rMSSQL.AutoSize = true;
            this.rMSSQL.Enabled = false;
            this.rMSSQL.Location = new System.Drawing.Point(10, 88);
            this.rMSSQL.Name = "rMSSQL";
            this.rMSSQL.Size = new System.Drawing.Size(62, 17);
            this.rMSSQL.TabIndex = 3;
            this.rMSSQL.Text = "MSSQL";
            this.rMSSQL.UseVisualStyleBackColor = true;
            // 
            // rMySQL
            // 
            this.rMySQL.AutoSize = true;
            this.rMySQL.Checked = true;
            this.rMySQL.Location = new System.Drawing.Point(10, 19);
            this.rMySQL.Name = "rMySQL";
            this.rMySQL.Size = new System.Drawing.Size(60, 17);
            this.rMySQL.TabIndex = 0;
            this.rMySQL.TabStop = true;
            this.rMySQL.Text = "MySQL";
            this.rMySQL.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Enabled = false;
            this.btnConnect.Location = new System.Drawing.Point(198, 57);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // box
            // 
            this.box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.box.BackColor = System.Drawing.SystemColors.Menu;
            this.box.Location = new System.Drawing.Point(2, 49);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(283, 41);
            this.box.TabIndex = 10;
            // 
            // pConnecting
            // 
            this.pConnecting.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pConnecting.Controls.Add(this.progressBar1);
            this.pConnecting.Controls.Add(this.lConnecting);
            this.pConnecting.Location = new System.Drawing.Point(0, 129);
            this.pConnecting.Name = "pConnecting";
            this.pConnecting.Size = new System.Drawing.Size(400, 203);
            this.pConnecting.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(81, 86);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(239, 17);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // lConnecting
            // 
            this.lConnecting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lConnecting.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lConnecting.Location = new System.Drawing.Point(12, 61);
            this.lConnecting.Name = "lConnecting";
            this.lConnecting.Size = new System.Drawing.Size(376, 23);
            this.lConnecting.TabIndex = 0;
            this.lConnecting.Text = "Connecting to null database";
            this.lConnecting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 332);
            this.Controls.Add(this.pConnecting);
            this.Controls.Add(this.pDBType);
            this.Controls.Add(this.imgConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SQL";
            this.Text = "SQL Connect";
            ((System.ComponentModel.ISupportInitialize)(this.imgConnect)).EndInit();
            this.pDBType.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pConnecting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgConnect;
        private System.Windows.Forms.Panel pDBType;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label box;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rMySQL;
        private System.Windows.Forms.RadioButton rMSSQL;
        private System.Windows.Forms.RadioButton rMySQLi;
        private System.Windows.Forms.RadioButton rMySQL_PHP;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel pConnecting;
        private System.Windows.Forms.Label lConnecting;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}