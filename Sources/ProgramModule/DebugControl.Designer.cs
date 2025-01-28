namespace AutoSplitterCore
{
    partial class DebugControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugControl));
            this.toSplitControl = new ReaLTaiizor.Controls.DungeonComboBox();
            this.GameToSplitLabel = new System.Windows.Forms.Label();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.btnSplitter = new System.Windows.Forms.Button();
            this.groupBoxUpload = new ReaLTaiizor.Controls.GroupBox();
            this.labelCloudVer = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSplitCf = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCfID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Running = new System.Windows.Forms.Label();
            this.NotRunning = new System.Windows.Forms.Label();
            this.btnRefreshGame = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSceneName = new System.Windows.Forms.TextBox();
            this.comboBoxIGTConversion = new System.Windows.Forms.ComboBox();
            this.textBoxIGT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.groupBoxUpload.SuspendLayout();
            this.SuspendLayout();
            // 
            // toSplitControl
            // 
            this.toSplitControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.toSplitControl.ColorA = System.Drawing.Color.Transparent;
            this.toSplitControl.ColorB = System.Drawing.Color.DarkTurquoise;
            this.toSplitControl.ColorC = System.Drawing.Color.WhiteSmoke;
            this.toSplitControl.ColorD = System.Drawing.Color.PaleTurquoise;
            this.toSplitControl.ColorE = System.Drawing.Color.WhiteSmoke;
            this.toSplitControl.ColorF = System.Drawing.Color.LightSeaGreen;
            this.toSplitControl.ColorG = System.Drawing.Color.DarkGreen;
            this.toSplitControl.ColorH = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(222)))), ((int)(((byte)(220)))));
            this.toSplitControl.ColorI = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.toSplitControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toSplitControl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.toSplitControl.DropDownHeight = 100;
            this.toSplitControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toSplitControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toSplitControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(97)))));
            this.toSplitControl.FormattingEnabled = true;
            this.toSplitControl.HoverSelectionColor = System.Drawing.Color.Empty;
            this.toSplitControl.IntegralHeight = false;
            this.toSplitControl.ItemHeight = 20;
            this.toSplitControl.Items.AddRange(new object[] {
            "Kill a Boss",
            "Kill a MiniBoss",
            "Is Activated a Idol",
            "Trigger a Position",
            "Mortal Journey",
            "Attribute",
            "Custom Flags"});
            this.toSplitControl.Location = new System.Drawing.Point(153, 46);
            this.toSplitControl.Name = "toSplitControl";
            this.toSplitControl.Size = new System.Drawing.Size(439, 26);
            this.toSplitControl.StartIndex = -1;
            this.toSplitControl.TabIndex = 13;
            // 
            // GameToSplitLabel
            // 
            this.GameToSplitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameToSplitLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.GameToSplitLabel.Location = new System.Drawing.Point(50, 46);
            this.GameToSplitLabel.Name = "GameToSplitLabel";
            this.GameToSplitLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GameToSplitLabel.Size = new System.Drawing.Size(97, 25);
            this.GameToSplitLabel.TabIndex = 51;
            this.GameToSplitLabel.Text = "Game to Split:";
            this.GameToSplitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.BackColor = System.Drawing.Color.MediumPurple;
            this.btnSaveConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConfig.Location = new System.Drawing.Point(630, 39);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 40);
            this.btnSaveConfig.TabIndex = 55;
            this.btnSaveConfig.Text = "Save Config";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            // 
            // btnSplitter
            // 
            this.btnSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplitter.BackColor = System.Drawing.Color.Teal;
            this.btnSplitter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplitter.Location = new System.Drawing.Point(711, 38);
            this.btnSplitter.Name = "btnSplitter";
            this.btnSplitter.Size = new System.Drawing.Size(75, 40);
            this.btnSplitter.TabIndex = 54;
            this.btnSplitter.Text = "Config";
            this.btnSplitter.UseVisualStyleBackColor = false;
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxUpload.BackGColor = System.Drawing.SystemColors.Highlight;
            this.groupBoxUpload.BaseColor = System.Drawing.Color.Transparent;
            this.groupBoxUpload.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            this.groupBoxUpload.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            this.groupBoxUpload.Controls.Add(this.labelCloudVer);
            this.groupBoxUpload.Controls.Add(this.label8);
            this.groupBoxUpload.Controls.Add(this.LabelVersion);
            this.groupBoxUpload.Controls.Add(this.label7);
            this.groupBoxUpload.Controls.Add(this.label6);
            this.groupBoxUpload.Controls.Add(this.btnSplitCf);
            this.groupBoxUpload.Controls.Add(this.label5);
            this.groupBoxUpload.Controls.Add(this.textBoxCfID);
            this.groupBoxUpload.Controls.Add(this.label4);
            this.groupBoxUpload.Controls.Add(this.Running);
            this.groupBoxUpload.Controls.Add(this.NotRunning);
            this.groupBoxUpload.Controls.Add(this.btnRefreshGame);
            this.groupBoxUpload.Controls.Add(this.label3);
            this.groupBoxUpload.Controls.Add(this.textBoxSceneName);
            this.groupBoxUpload.Controls.Add(this.comboBoxIGTConversion);
            this.groupBoxUpload.Controls.Add(this.textBoxIGT);
            this.groupBoxUpload.Controls.Add(this.label2);
            this.groupBoxUpload.Controls.Add(this.label11);
            this.groupBoxUpload.Controls.Add(this.label9);
            this.groupBoxUpload.Controls.Add(this.textBoxZ);
            this.groupBoxUpload.Controls.Add(this.textBoxY);
            this.groupBoxUpload.Controls.Add(this.textBoxX);
            this.groupBoxUpload.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBoxUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.groupBoxUpload.HeaderColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBoxUpload.Location = new System.Drawing.Point(19, 99);
            this.groupBoxUpload.MinimumSize = new System.Drawing.Size(136, 50);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.groupBoxUpload.Size = new System.Drawing.Size(760, 297);
            this.groupBoxUpload.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.groupBoxUpload.TabIndex = 56;
            this.groupBoxUpload.Text = "Debug Information";
            // 
            // labelCloudVer
            // 
            this.labelCloudVer.AutoSize = true;
            this.labelCloudVer.Location = new System.Drawing.Point(571, 202);
            this.labelCloudVer.Name = "labelCloudVer";
            this.labelCloudVer.Size = new System.Drawing.Size(25, 14);
            this.labelCloudVer.TabIndex = 92;
            this.labelCloudVer.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(519, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(165, 14);
            this.label8.TabIndex = 91;
            this.label8.Text = "Version AutoSplitterCore Git:";
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(571, 160);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(25, 14);
            this.LabelVersion.TabIndex = 90;
            this.LabelVersion.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(529, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(166, 14);
            this.label7.TabIndex = 89;
            this.label7.Text = "Version AutoSplitterCore:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(304, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 28);
            this.label6.TabIndex = 88;
            this.label6.Text = "Check DTFiles for Ids\r\n   Only SoulsGames\r\n";
            // 
            // btnSplitCf
            // 
            this.btnSplitCf.BackColor = System.Drawing.Color.Red;
            this.btnSplitCf.Location = new System.Drawing.Point(413, 178);
            this.btnSplitCf.Name = "btnSplitCf";
            this.btnSplitCf.Size = new System.Drawing.Size(30, 32);
            this.btnSplitCf.TabIndex = 78;
            this.btnSplitCf.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 14);
            this.label5.TabIndex = 87;
            this.label5.Text = "CheckFlag ID:";
            // 
            // textBoxCfID
            // 
            this.textBoxCfID.Location = new System.Drawing.Point(307, 185);
            this.textBoxCfID.Name = "textBoxCfID";
            this.textBoxCfID.Size = new System.Drawing.Size(100, 22);
            this.textBoxCfID.TabIndex = 86;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 14);
            this.label4.TabIndex = 85;
            this.label4.Text = "Status Game:";
            // 
            // Running
            // 
            this.Running.AutoSize = true;
            this.Running.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.Running.Location = new System.Drawing.Point(518, 85);
            this.Running.Name = "Running";
            this.Running.Size = new System.Drawing.Size(51, 14);
            this.Running.TabIndex = 84;
            this.Running.Text = "Running";
            // 
            // NotRunning
            // 
            this.NotRunning.AutoSize = true;
            this.NotRunning.ForeColor = System.Drawing.Color.Red;
            this.NotRunning.Location = new System.Drawing.Point(509, 85);
            this.NotRunning.Name = "NotRunning";
            this.NotRunning.Size = new System.Drawing.Size(75, 14);
            this.NotRunning.TabIndex = 83;
            this.NotRunning.Text = "Not Running";
            // 
            // btnRefreshGame
            // 
            this.btnRefreshGame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnRefreshGame.Location = new System.Drawing.Point(596, 59);
            this.btnRefreshGame.Name = "btnRefreshGame";
            this.btnRefreshGame.Size = new System.Drawing.Size(72, 43);
            this.btnRefreshGame.TabIndex = 82;
            this.btnRefreshGame.Text = "Refresh Game";
            this.btnRefreshGame.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 81;
            this.label3.Text = "Scene Name";
            // 
            // textBoxSceneName
            // 
            this.textBoxSceneName.Location = new System.Drawing.Point(123, 116);
            this.textBoxSceneName.Name = "textBoxSceneName";
            this.textBoxSceneName.Size = new System.Drawing.Size(123, 22);
            this.textBoxSceneName.TabIndex = 80;
            // 
            // comboBoxIGTConversion
            // 
            this.comboBoxIGTConversion.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxIGTConversion.FormattingEnabled = true;
            this.comboBoxIGTConversion.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.comboBoxIGTConversion.Items.AddRange(new object[] {
            "ms",
            "s",
            "m"});
            this.comboBoxIGTConversion.Location = new System.Drawing.Point(363, 57);
            this.comboBoxIGTConversion.Name = "comboBoxIGTConversion";
            this.comboBoxIGTConversion.Size = new System.Drawing.Size(57, 22);
            this.comboBoxIGTConversion.TabIndex = 79;
            // 
            // textBoxIGT
            // 
            this.textBoxIGT.Location = new System.Drawing.Point(321, 81);
            this.textBoxIGT.Name = "textBoxIGT";
            this.textBoxIGT.Size = new System.Drawing.Size(77, 22);
            this.textBoxIGT.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 76;
            this.label2.Text = "InGameTime:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 14);
            this.label11.TabIndex = 75;
            this.label11.Text = "Coordinates:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(83, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 14);
            this.label9.TabIndex = 74;
            this.label9.Text = "X                 Y                 Z";
            // 
            // textBoxZ
            // 
            this.textBoxZ.Location = new System.Drawing.Point(178, 81);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(52, 22);
            this.textBoxZ.TabIndex = 73;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(120, 81);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(52, 22);
            this.textBoxY.TabIndex = 72;
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(62, 81);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(52, 22);
            this.textBoxX.TabIndex = 71;
            // 
            // DebugControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBoxUpload);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnSplitter);
            this.Controls.Add(this.GameToSplitLabel);
            this.Controls.Add(this.toSplitControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.MaximizeBox = false;
            this.Name = "DebugControl";
            this.Text = "Debug";
            this.Load += new System.EventHandler(this.DebugControl_Load);
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.DungeonComboBox toSplitControl;
        private System.Windows.Forms.Label GameToSplitLabel;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.Button btnSplitter;
        private ReaLTaiizor.Controls.GroupBox groupBoxUpload;
        private System.Windows.Forms.Label labelCloudVer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSplitCf;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCfID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Running;
        private System.Windows.Forms.Label NotRunning;
        private System.Windows.Forms.Button btnRefreshGame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSceneName;
        private System.Windows.Forms.ComboBox comboBoxIGTConversion;
        private System.Windows.Forms.TextBox textBoxIGT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.TextBox textBoxX;
    }
}