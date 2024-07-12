namespace AutoSplitterCore
{
    partial class ProfileManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileManager));
            this.groupBoxManagement = new System.Windows.Forms.GroupBox();
            this.btnRemoveProfile = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnSetProfile = new System.Windows.Forms.PictureBox();
            this.textBoxCurrrentProfile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadProfile = new System.Windows.Forms.Button();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxSummary = new System.Windows.Forms.TextBox();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAuthor = new System.Windows.Forms.TextBox();
            this.btnSetAuthor = new System.Windows.Forms.PictureBox();
            this.groupBoxManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetProfile)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetAuthor)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxManagement
            // 
            this.groupBoxManagement.Controls.Add(this.btnSetAuthor);
            this.groupBoxManagement.Controls.Add(this.textBoxAuthor);
            this.groupBoxManagement.Controls.Add(this.label4);
            this.groupBoxManagement.Controls.Add(this.btnRemoveProfile);
            this.groupBoxManagement.Controls.Add(this.btnSaveProfile);
            this.groupBoxManagement.Controls.Add(this.btnSetProfile);
            this.groupBoxManagement.Controls.Add(this.textBoxCurrrentProfile);
            this.groupBoxManagement.Controls.Add(this.label2);
            this.groupBoxManagement.Controls.Add(this.label1);
            this.groupBoxManagement.Controls.Add(this.btnLoadProfile);
            this.groupBoxManagement.Controls.Add(this.btnBrowser);
            this.groupBoxManagement.Controls.Add(this.label3);
            this.groupBoxManagement.Controls.Add(this.textBoxSavePath);
            this.groupBoxManagement.Controls.Add(this.groupBox1);
            this.groupBoxManagement.Controls.Add(this.comboBoxProfiles);
            this.groupBoxManagement.Location = new System.Drawing.Point(12, 12);
            this.groupBoxManagement.Name = "groupBoxManagement";
            this.groupBoxManagement.Size = new System.Drawing.Size(644, 584);
            this.groupBoxManagement.TabIndex = 1;
            this.groupBoxManagement.TabStop = false;
            this.groupBoxManagement.Text = "Management";
            // 
            // btnRemoveProfile
            // 
            this.btnRemoveProfile.Location = new System.Drawing.Point(336, 98);
            this.btnRemoveProfile.Name = "btnRemoveProfile";
            this.btnRemoveProfile.Size = new System.Drawing.Size(93, 23);
            this.btnRemoveProfile.TabIndex = 19;
            this.btnRemoveProfile.Text = "Remove Profile";
            
            this.btnRemoveProfile.Click += new System.EventHandler(this.btnRemoveProfile_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Location = new System.Drawing.Point(293, 538);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(83, 35);
            this.btnSaveProfile.TabIndex = 4;
            this.btnSaveProfile.Text = "Save Current Profile";
            
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // btnSetProfile
            // 
            this.btnSetProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnSetProfile.Image")));
            this.btnSetProfile.Location = new System.Drawing.Point(538, 137);
            this.btnSetProfile.Name = "btnSetProfile";
            this.btnSetProfile.Size = new System.Drawing.Size(22, 21);
            this.btnSetProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSetProfile.TabIndex = 17;
            this.btnSetProfile.TabStop = false;
            this.btnSetProfile.Click += new System.EventHandler(this.btnSetProfile_Click);
            // 
            // textBoxCurrrentProfile
            // 
            this.textBoxCurrrentProfile.Location = new System.Drawing.Point(141, 137);
            this.textBoxCurrrentProfile.Name = "textBoxCurrrentProfile";
            this.textBoxCurrrentProfile.Size = new System.Drawing.Size(391, 20);
            this.textBoxCurrrentProfile.TabIndex = 16;
            this.textBoxCurrrentProfile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxCurrrentProfile.TextChanged += new System.EventHandler(this.textBoxCurrrentProfile_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Fuchsia;
            this.label2.Location = new System.Drawing.Point(62, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Current Profile";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(76, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Profiles";
            // 
            // btnLoadProfile
            // 
            this.btnLoadProfile.Location = new System.Drawing.Point(237, 98);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(93, 23);
            this.btnLoadProfile.TabIndex = 14;
            this.btnLoadProfile.Text = "Load Profile";
            
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(552, 29);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(83, 23);
            this.btnBrowser.TabIndex = 11;
            this.btnBrowser.Text = "Browser";
            
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Path Save";
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(35, 31);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(511, 20);
            this.textBoxSavePath.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxSummary);
            this.groupBox1.Location = new System.Drawing.Point(35, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 321);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Summary";
            // 
            // TextBoxSummary
            // 
            this.TextBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSummary.Location = new System.Drawing.Point(20, 29);
            this.TextBoxSummary.Multiline = true;
            this.TextBoxSummary.Name = "TextBoxSummary";
            this.TextBoxSummary.ReadOnly = true;
            this.TextBoxSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxSummary.Size = new System.Drawing.Size(535, 273);
            this.TextBoxSummary.TabIndex = 3;
            this.TextBoxSummary.TabStop = false;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(127, 71);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(433, 21);
            this.comboBoxProfiles.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(191, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Author";
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.Location = new System.Drawing.Point(235, 168);
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.textBoxAuthor.Size = new System.Drawing.Size(194, 20);
            this.textBoxAuthor.TabIndex = 21;
            this.textBoxAuthor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.textBoxAuthor_TextChanged);
            // 
            // btnSetAuthor
            // 
            this.btnSetAuthor.Image = ((System.Drawing.Image)(resources.GetObject("btnSetAuthor.Image")));
            this.btnSetAuthor.Location = new System.Drawing.Point(438, 168);
            this.btnSetAuthor.Name = "btnSetAuthor";
            this.btnSetAuthor.Size = new System.Drawing.Size(22, 21);
            this.btnSetAuthor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSetAuthor.TabIndex = 22;
            this.btnSetAuthor.TabStop = false;
            this.btnSetAuthor.Click += new System.EventHandler(this.btnSetAuthor_Click);
            // 
            // ProfileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 603);
            this.Controls.Add(this.groupBoxManagement);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProfileManager";
            this.Text = "Profile Manager";
            this.Load += new System.EventHandler(this.ProfileManager_Load);
            this.groupBoxManagement.ResumeLayout(false);
            this.groupBoxManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetProfile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetAuthor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxManagement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnLoadProfile;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox TextBoxSummary;
        private System.Windows.Forms.TextBox textBoxCurrrentProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnSetProfile;
        private System.Windows.Forms.Button btnRemoveProfile;
        private System.Windows.Forms.PictureBox btnSetAuthor;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.Label label4;
    }
}