namespace AutoSplitterCore
{
    partial class UpdateShowDialogSouls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateShowDialogSouls));
            this.groupBoxUpdate = new System.Windows.Forms.GroupBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGoToDownloadPage = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.labelCloudVer = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.TextBoxWarning = new System.Windows.Forms.TextBox();
            this.groupBoxUpdating = new System.Windows.Forms.GroupBox();
            this.progressBarUpdating = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxUpdate.SuspendLayout();
            this.groupBoxUpdating.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUpdate
            // 
            this.groupBoxUpdate.Controls.Add(this.btnDownload);
            this.groupBoxUpdate.Controls.Add(this.btnClose);
            this.groupBoxUpdate.Controls.Add(this.btnGoToDownloadPage);
            this.groupBoxUpdate.Controls.Add(this.label76);
            this.groupBoxUpdate.Controls.Add(this.label78);
            this.groupBoxUpdate.Controls.Add(this.labelCloudVer);
            this.groupBoxUpdate.Controls.Add(this.LabelVersion);
            this.groupBoxUpdate.Location = new System.Drawing.Point(221, 231);
            this.groupBoxUpdate.Name = "groupBoxUpdate";
            this.groupBoxUpdate.Size = new System.Drawing.Size(305, 144);
            this.groupBoxUpdate.TabIndex = 6;
            this.groupBoxUpdate.TabStop = false;
            this.groupBoxUpdate.Text = "SoulsMemory Update";
            // 
            // btnDownload
            // 
            this.btnDownload.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnDownload.Location = new System.Drawing.Point(18, 95);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(137, 23);
            this.btnDownload.TabIndex = 80;
            this.btnDownload.Text = "Download";
            
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnClose.Location = new System.Drawing.Point(161, 95);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 23);
            this.btnClose.TabIndex = 79;
            this.btnClose.Text = "Close";
            
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGoToDownloadPage
            // 
            this.btnGoToDownloadPage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnGoToDownloadPage.Location = new System.Drawing.Point(76, 121);
            this.btnGoToDownloadPage.Name = "btnGoToDownloadPage";
            this.btnGoToDownloadPage.Size = new System.Drawing.Size(137, 23);
            this.btnGoToDownloadPage.TabIndex = 77;
            this.btnGoToDownloadPage.Text = "Go to download page";
            
            this.btnGoToDownloadPage.Click += new System.EventHandler(this.btnGoToDownloadPage_Click);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(96, 56);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(139, 13);
            this.label76.TabIndex = 76;
            this.label76.Text = "Latest available version:      ";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(114, 16);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(99, 13);
            this.label78.TabIndex = 75;
            this.label78.Text = "Current version:      ";
            // 
            // labelCloudVer
            // 
            this.labelCloudVer.AutoSize = true;
            this.labelCloudVer.Location = new System.Drawing.Point(135, 73);
            this.labelCloudVer.Name = "labelCloudVer";
            this.labelCloudVer.Size = new System.Drawing.Size(40, 13);
            this.labelCloudVer.TabIndex = 74;
            this.labelCloudVer.Text = "0.0.0.0";
            // 
            // LabelVersion
            // 
            this.LabelVersion.AutoSize = true;
            this.LabelVersion.Location = new System.Drawing.Point(135, 33);
            this.LabelVersion.Name = "LabelVersion";
            this.LabelVersion.Size = new System.Drawing.Size(40, 13);
            this.LabelVersion.TabIndex = 72;
            this.LabelVersion.Text = "0.0.0.0";
            // 
            // TextBoxWarning
            // 
            this.TextBoxWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxWarning.Location = new System.Drawing.Point(12, 12);
            this.TextBoxWarning.Multiline = true;
            this.TextBoxWarning.Name = "TextBoxWarning";
            this.TextBoxWarning.ReadOnly = true;
            this.TextBoxWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxWarning.Size = new System.Drawing.Size(675, 199);
            this.TextBoxWarning.TabIndex = 8;
            this.TextBoxWarning.TabStop = false;
            this.TextBoxWarning.Text = resources.GetString("TextBoxWarning.Text");
            // 
            // groupBoxUpdating
            // 
            this.groupBoxUpdating.Controls.Add(this.progressBarUpdating);
            this.groupBoxUpdating.Controls.Add(this.label1);
            this.groupBoxUpdating.Location = new System.Drawing.Point(158, 231);
            this.groupBoxUpdating.Name = "groupBoxUpdating";
            this.groupBoxUpdating.Size = new System.Drawing.Size(399, 144);
            this.groupBoxUpdating.TabIndex = 9;
            this.groupBoxUpdating.TabStop = false;
            // 
            // progressBarUpdating
            // 
            this.progressBarUpdating.Location = new System.Drawing.Point(51, 75);
            this.progressBarUpdating.Name = "progressBarUpdating";
            this.progressBarUpdating.Size = new System.Drawing.Size(300, 23);
            this.progressBarUpdating.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Updating...";
            // 
            // UpdateShowDialogSouls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 406);
            this.Controls.Add(this.groupBoxUpdating);
            this.Controls.Add(this.TextBoxWarning);
            this.Controls.Add(this.groupBoxUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(715, 445);
            this.MinimumSize = new System.Drawing.Size(715, 445);
            this.Name = "UpdateShowDialogSouls";
            this.Text = "SoulMemory New Update";
            this.Load += new System.EventHandler(this.UpdateShowDialogSouls_Load);
            this.groupBoxUpdate.ResumeLayout(false);
            this.groupBoxUpdate.PerformLayout();
            this.groupBoxUpdating.ResumeLayout(false);
            this.groupBoxUpdating.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUpdate;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGoToDownloadPage;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label labelCloudVer;
        private System.Windows.Forms.Label LabelVersion;
        internal System.Windows.Forms.TextBox TextBoxWarning;
        private System.Windows.Forms.GroupBox groupBoxUpdating;
        private System.Windows.Forms.ProgressBar progressBarUpdating;
        private System.Windows.Forms.Label label1;
    }
}