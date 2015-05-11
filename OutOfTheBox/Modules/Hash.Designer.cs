namespace OutOfTheBox.Modules
{
    partial class Hash
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
            this.imgBox = new System.Windows.Forms.PictureBox();
            this.lDrop = new System.Windows.Forms.Label();
            this.pMain = new System.Windows.Forms.Panel();
            this.txtData = new System.Windows.Forms.TextBox();
            this.lData = new System.Windows.Forms.Label();
            this.lHex = new System.Windows.Forms.Label();
            this.txtMD5 = new System.Windows.Forms.TextBox();
            this.lMD5 = new System.Windows.Forms.Label();
            this.txtSHA1 = new System.Windows.Forms.TextBox();
            this.lSHA1 = new System.Windows.Forms.Label();
            this.txtSHA256 = new System.Windows.Forms.TextBox();
            this.lSHA256 = new System.Windows.Forms.Label();
            this.txtSHA512 = new System.Windows.Forms.TextBox();
            this.lSHA512 = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).BeginInit();
            this.pMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgBox
            // 
            this.imgBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgBox.Image = global::OutOfTheBox.Properties.Resources.box;
            this.imgBox.Location = new System.Drawing.Point(142, 82);
            this.imgBox.Name = "imgBox";
            this.imgBox.Size = new System.Drawing.Size(128, 128);
            this.imgBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgBox.TabIndex = 0;
            this.imgBox.TabStop = false;
            // 
            // lDrop
            // 
            this.lDrop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lDrop.AutoSize = true;
            this.lDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDrop.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lDrop.Location = new System.Drawing.Point(138, 218);
            this.lDrop.Name = "lDrop";
            this.lDrop.Size = new System.Drawing.Size(150, 16);
            this.lDrop.TabIndex = 1;
            this.lDrop.Text = "Drop or paste data here";
            // 
            // pMain
            // 
            this.pMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pMain.Controls.Add(this.txtHex);
            this.pMain.Controls.Add(this.lSHA512);
            this.pMain.Controls.Add(this.lSHA256);
            this.pMain.Controls.Add(this.lSHA1);
            this.pMain.Controls.Add(this.lMD5);
            this.pMain.Controls.Add(this.txtSHA512);
            this.pMain.Controls.Add(this.txtSHA256);
            this.pMain.Controls.Add(this.txtSHA1);
            this.pMain.Controls.Add(this.txtMD5);
            this.pMain.Controls.Add(this.lHex);
            this.pMain.Controls.Add(this.lData);
            this.pMain.Controls.Add(this.txtData);
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(426, 332);
            this.pMain.TabIndex = 2;
            // 
            // txtData
            // 
            this.txtData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtData.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtData.Location = new System.Drawing.Point(68, 12);
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.Size = new System.Drawing.Size(346, 20);
            this.txtData.TabIndex = 0;
            // 
            // lData
            // 
            this.lData.AutoSize = true;
            this.lData.Location = new System.Drawing.Point(29, 15);
            this.lData.Name = "lData";
            this.lData.Size = new System.Drawing.Size(33, 13);
            this.lData.TabIndex = 1;
            this.lData.Text = "Data:";
            // 
            // lHex
            // 
            this.lHex.AutoSize = true;
            this.lHex.Location = new System.Drawing.Point(33, 41);
            this.lHex.Name = "lHex";
            this.lHex.Size = new System.Drawing.Size(29, 13);
            this.lHex.TabIndex = 3;
            this.lHex.Text = "Hex:";
            // 
            // txtMD5
            // 
            this.txtMD5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMD5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMD5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMD5.Location = new System.Drawing.Point(68, 213);
            this.txtMD5.Name = "txtMD5";
            this.txtMD5.ReadOnly = true;
            this.txtMD5.Size = new System.Drawing.Size(346, 20);
            this.txtMD5.TabIndex = 4;
            // 
            // lMD5
            // 
            this.lMD5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lMD5.AutoSize = true;
            this.lMD5.Location = new System.Drawing.Point(29, 216);
            this.lMD5.Name = "lMD5";
            this.lMD5.Size = new System.Drawing.Size(33, 13);
            this.lMD5.TabIndex = 5;
            this.lMD5.Text = "MD5:";
            // 
            // txtSHA1
            // 
            this.txtSHA1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSHA1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSHA1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSHA1.Location = new System.Drawing.Point(68, 239);
            this.txtSHA1.Name = "txtSHA1";
            this.txtSHA1.ReadOnly = true;
            this.txtSHA1.Size = new System.Drawing.Size(346, 20);
            this.txtSHA1.TabIndex = 4;
            // 
            // lSHA1
            // 
            this.lSHA1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lSHA1.AutoSize = true;
            this.lSHA1.Location = new System.Drawing.Point(24, 242);
            this.lSHA1.Name = "lSHA1";
            this.lSHA1.Size = new System.Drawing.Size(38, 13);
            this.lSHA1.TabIndex = 5;
            this.lSHA1.Text = "SHA1:";
            // 
            // txtSHA256
            // 
            this.txtSHA256.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSHA256.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSHA256.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSHA256.Location = new System.Drawing.Point(68, 265);
            this.txtSHA256.Name = "txtSHA256";
            this.txtSHA256.ReadOnly = true;
            this.txtSHA256.Size = new System.Drawing.Size(346, 20);
            this.txtSHA256.TabIndex = 4;
            // 
            // lSHA256
            // 
            this.lSHA256.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lSHA256.AutoSize = true;
            this.lSHA256.Location = new System.Drawing.Point(12, 268);
            this.lSHA256.Name = "lSHA256";
            this.lSHA256.Size = new System.Drawing.Size(50, 13);
            this.lSHA256.TabIndex = 5;
            this.lSHA256.Text = "SHA256:";
            // 
            // txtSHA512
            // 
            this.txtSHA512.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSHA512.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSHA512.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSHA512.Location = new System.Drawing.Point(68, 291);
            this.txtSHA512.Name = "txtSHA512";
            this.txtSHA512.ReadOnly = true;
            this.txtSHA512.Size = new System.Drawing.Size(346, 20);
            this.txtSHA512.TabIndex = 4;
            // 
            // lSHA512
            // 
            this.lSHA512.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lSHA512.AutoSize = true;
            this.lSHA512.Location = new System.Drawing.Point(12, 294);
            this.lSHA512.Name = "lSHA512";
            this.lSHA512.Size = new System.Drawing.Size(50, 13);
            this.lSHA512.TabIndex = 5;
            this.lSHA512.Text = "SHA512:";
            // 
            // txtHex
            // 
            this.txtHex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHex.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHex.Location = new System.Drawing.Point(68, 38);
            this.txtHex.Multiline = true;
            this.txtHex.Name = "txtHex";
            this.txtHex.ReadOnly = true;
            this.txtHex.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHex.Size = new System.Drawing.Size(346, 163);
            this.txtHex.TabIndex = 6;
            this.txtHex.WordWrap = false;
            // 
            // Hash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(426, 332);
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.lDrop);
            this.Controls.Add(this.imgBox);
            this.Name = "Hash";
            this.Text = "Hash";
            ((System.ComponentModel.ISupportInitialize)(this.imgBox)).EndInit();
            this.pMain.ResumeLayout(false);
            this.pMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgBox;
        private System.Windows.Forms.Label lDrop;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Label lData;
        private System.Windows.Forms.Label lHex;
        private System.Windows.Forms.TextBox txtMD5;
        private System.Windows.Forms.Label lMD5;
        private System.Windows.Forms.Label lSHA1;
        private System.Windows.Forms.TextBox txtSHA1;
        private System.Windows.Forms.Label lSHA256;
        private System.Windows.Forms.TextBox txtSHA256;
        private System.Windows.Forms.Label lSHA512;
        private System.Windows.Forms.TextBox txtSHA512;
        private System.Windows.Forms.TextBox txtHex;
    }
}