namespace AutoSplitterCore
{
    partial class Debug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
            this.comboBoxGame = new System.Windows.Forms.ComboBox();
            this.GameToSplitLabel = new System.Windows.Forms.Label();
            this.btnSplitter = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.groupBoxDebug = new System.Windows.Forms.GroupBox();
            this.labelCloudVer = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Running = new System.Windows.Forms.Label();
            this.NotRunning = new System.Windows.Forms.Label();
            this.btnRefreshGame = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSceneName = new System.Windows.Forms.TextBox();
            this.textBoxIGT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxZ = new System.Windows.Forms.TextBox();
            this.textBoxY = new System.Windows.Forms.TextBox();
            this.textBoxX = new System.Windows.Forms.TextBox();
            this.checkBoxPracticeMode = new System.Windows.Forms.CheckBox();
            this.LabelLog = new System.Windows.Forms.Label();
            this.btnResetFlags = new System.Windows.Forms.Button();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.groupBoxDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxGame
            // 
            this.comboBoxGame.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxGame.FormattingEnabled = true;
            this.comboBoxGame.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.comboBoxGame.Location = new System.Drawing.Point(203, 24);
            this.comboBoxGame.Name = "comboBoxGame";
            this.comboBoxGame.Size = new System.Drawing.Size(490, 21);
            this.comboBoxGame.TabIndex = 51;
            this.comboBoxGame.SelectedIndexChanged += new System.EventHandler(this.comboBoxGame_SelectedIndexChanged);
            // 
            // GameToSplitLabel
            // 
            this.GameToSplitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameToSplitLabel.Location = new System.Drawing.Point(65, 24);
            this.GameToSplitLabel.Name = "GameToSplitLabel";
            this.GameToSplitLabel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.GameToSplitLabel.Size = new System.Drawing.Size(97, 25);
            this.GameToSplitLabel.TabIndex = 50;
            this.GameToSplitLabel.Text = "Game to Split:";
            this.GameToSplitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSplitter
            // 
            this.btnSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplitter.BackColor = System.Drawing.Color.Teal;
            this.btnSplitter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSplitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSplitter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSplitter.Location = new System.Drawing.Point(831, 12);
            this.btnSplitter.Name = "btnSplitter";
            this.btnSplitter.Size = new System.Drawing.Size(75, 40);
            this.btnSplitter.TabIndex = 52;
            this.btnSplitter.Text = "Config";
            this.btnSplitter.UseVisualStyleBackColor = false;
            this.btnSplitter.Click += new System.EventHandler(this.btnSplitter_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.BackColor = System.Drawing.Color.MediumPurple;
            this.btnSaveConfig.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConfig.Location = new System.Drawing.Point(750, 13);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(75, 40);
            this.btnSaveConfig.TabIndex = 53;
            this.btnSaveConfig.Text = "Save Config";
            this.btnSaveConfig.UseVisualStyleBackColor = false;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // groupBoxDebug
            // 
            this.groupBoxDebug.Controls.Add(this.labelCloudVer);
            this.groupBoxDebug.Controls.Add(this.label8);
            this.groupBoxDebug.Controls.Add(this.LabelVersion);
            this.groupBoxDebug.Controls.Add(this.label7);
            this.groupBoxDebug.Controls.Add(this.label4);
            this.groupBoxDebug.Controls.Add(this.Running);
            this.groupBoxDebug.Controls.Add(this.NotRunning);
            this.groupBoxDebug.Controls.Add(this.btnRefreshGame);
            this.groupBoxDebug.Controls.Add(this.label3);
            this.groupBoxDebug.Controls.Add(this.textBoxSceneName);
            this.groupBoxDebug.Controls.Add(this.textBoxIGT);
            this.groupBoxDebug.Controls.Add(this.label2);
            this.groupBoxDebug.Controls.Add(this.label11);
            this.groupBoxDebug.Controls.Add(this.label9);
            this.groupBoxDebug.Controls.Add(this.textBoxZ);
            this.groupBoxDebug.Controls.Add(this.textBoxY);
            this.groupBoxDebug.Controls.Add(this.textBoxX);
            this.groupBoxDebug.Location = new System.Drawing.Point(127, 85);
            this.groupBoxDebug.Name = "groupBoxDebug";
            this.groupBoxDebug.Size = new System.Drawing.Size(633, 237);
            this.groupBoxDebug.TabIndex = 56;
            this.groupBoxDebug.TabStop = false;
            this.groupBoxDebug.Text = "Debug Information";
            // 
            // labelCloudVer
            // 
            this.labelCloudVer.AutoSize = true;
            this.labelCloudVer.Location = new System.Drawing.Point(319, 209);
            this.labelCloudVer.Name = "labelCloudVer";
            this.labelCloudVer.Size = new System.Drawing.Size(25, 13);
            this.labelCloudVer.TabIndex = 70;
            this.labelCloudVer.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(267, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Version AutoSplitterCore Git:";
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(319, 167);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(25, 13);
            this.LabelVersion.TabIndex = 68;
            this.LabelVersion.Text = "???";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(276, 148);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "Version AutoSplitterCore:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Status Game:";
            // 
            // Running
            // 
            this.Running.AutoSize = true;
            this.Running.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.Running.Location = new System.Drawing.Point(476, 68);
            this.Running.Name = "Running";
            this.Running.Size = new System.Drawing.Size(47, 13);
            this.Running.TabIndex = 62;
            this.Running.Text = "Running";
            // 
            // NotRunning
            // 
            this.NotRunning.AutoSize = true;
            this.NotRunning.ForeColor = System.Drawing.Color.Red;
            this.NotRunning.Location = new System.Drawing.Point(467, 68);
            this.NotRunning.Name = "NotRunning";
            this.NotRunning.Size = new System.Drawing.Size(67, 13);
            this.NotRunning.TabIndex = 61;
            this.NotRunning.Text = "Not Running";
            // 
            // btnRefreshGame
            // 
            this.btnRefreshGame.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnRefreshGame.Location = new System.Drawing.Point(554, 42);
            this.btnRefreshGame.Name = "btnRefreshGame";
            this.btnRefreshGame.Size = new System.Drawing.Size(72, 43);
            this.btnRefreshGame.TabIndex = 60;
            this.btnRefreshGame.Text = "Refresh Game";
            this.btnRefreshGame.UseVisualStyleBackColor = false;
            this.btnRefreshGame.Click += new System.EventHandler(this.btnRefreshGame_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Scene Name";
            // 
            // textBoxSceneName
            // 
            this.textBoxSceneName.Location = new System.Drawing.Point(81, 99);
            this.textBoxSceneName.Name = "textBoxSceneName";
            this.textBoxSceneName.Size = new System.Drawing.Size(123, 20);
            this.textBoxSceneName.TabIndex = 58;
            // 
            // textBoxIGT
            // 
            this.textBoxIGT.Location = new System.Drawing.Point(279, 64);
            this.textBoxIGT.Name = "textBoxIGT";
            this.textBoxIGT.Size = new System.Drawing.Size(96, 20);
            this.textBoxIGT.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "InGameTime:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(73, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Coordinates:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "X                 Y                 Z";
            // 
            // textBoxZ
            // 
            this.textBoxZ.Location = new System.Drawing.Point(136, 64);
            this.textBoxZ.Name = "textBoxZ";
            this.textBoxZ.Size = new System.Drawing.Size(52, 20);
            this.textBoxZ.TabIndex = 13;
            // 
            // textBoxY
            // 
            this.textBoxY.Location = new System.Drawing.Point(78, 64);
            this.textBoxY.Name = "textBoxY";
            this.textBoxY.Size = new System.Drawing.Size(52, 20);
            this.textBoxY.TabIndex = 12;
            // 
            // textBoxX
            // 
            this.textBoxX.Location = new System.Drawing.Point(20, 64);
            this.textBoxX.Name = "textBoxX";
            this.textBoxX.Size = new System.Drawing.Size(52, 20);
            this.textBoxX.TabIndex = 11;
            // 
            // checkBoxPracticeMode
            // 
            this.checkBoxPracticeMode.AutoSize = true;
            this.checkBoxPracticeMode.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.checkBoxPracticeMode.Location = new System.Drawing.Point(113, 51);
            this.checkBoxPracticeMode.Name = "checkBoxPracticeMode";
            this.checkBoxPracticeMode.Size = new System.Drawing.Size(95, 17);
            this.checkBoxPracticeMode.TabIndex = 67;
            this.checkBoxPracticeMode.Text = "Practice Mode";
            this.checkBoxPracticeMode.UseVisualStyleBackColor = true;
            this.checkBoxPracticeMode.CheckedChanged += new System.EventHandler(this.checkBoxPracticeMode_CheckedChanged);
            // 
            // LabelLog
            // 
            this.LabelLog.AutoSize = true;
            this.LabelLog.Location = new System.Drawing.Point(42, 323);
            this.LabelLog.Name = "LabelLog";
            this.LabelLog.Size = new System.Drawing.Size(28, 13);
            this.LabelLog.TabIndex = 71;
            this.LabelLog.Text = "Log:";
            // 
            // btnResetFlags
            // 
            this.btnResetFlags.Location = new System.Drawing.Point(808, 284);
            this.btnResetFlags.Name = "btnResetFlags";
            this.btnResetFlags.Size = new System.Drawing.Size(75, 23);
            this.btnResetFlags.TabIndex = 72;
            this.btnResetFlags.Text = "Reset Flags";
            this.btnResetFlags.Click += new System.EventHandler(this.btnResetFlags_Click);
            // 
            // listViewLog
            // 
            this.listViewLog.HideSelection = false;
            this.listViewLog.Location = new System.Drawing.Point(28, 341);
            this.listViewLog.MultiSelect = false;
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(878, 338);
            this.listViewLog.TabIndex = 73;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(808, 312);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 74;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 691);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.listViewLog);
            this.Controls.Add(this.btnResetFlags);
            this.Controls.Add(this.LabelLog);
            this.Controls.Add(this.checkBoxPracticeMode);
            this.Controls.Add(this.groupBoxDebug);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnSplitter);
            this.Controls.Add(this.comboBoxGame);
            this.Controls.Add(this.GameToSplitLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(934, 730);
            this.MinimumSize = new System.Drawing.Size(934, 730);
            this.Name = "Debug";
            this.Text = "AutoSplitterCore Debug";
            this.Load += new System.EventHandler(this.Debug_Load);
            this.groupBoxDebug.ResumeLayout(false);
            this.groupBoxDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxGame;
        private System.Windows.Forms.Label GameToSplitLabel;
        private System.Windows.Forms.Button btnSplitter;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.GroupBox groupBoxDebug;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxZ;
        private System.Windows.Forms.TextBox textBoxY;
        private System.Windows.Forms.TextBox textBoxX;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRefreshGame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSceneName;
        private System.Windows.Forms.TextBox textBoxIGT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Running;
        private System.Windows.Forms.Label NotRunning;
        private System.Windows.Forms.CheckBox checkBoxPracticeMode;
        private System.Windows.Forms.Label labelCloudVer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LabelLog;
        private System.Windows.Forms.Button btnResetFlags;
        private System.Windows.Forms.ListView listViewLog;
        private System.Windows.Forms.Button btnClearLog;
    }
}