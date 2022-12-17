namespace AutoSplitterCore
{
    partial class UpdateShowDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateShowDialog));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGoToDownloadPage = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.labelCloudVer = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnGoToDownloadPage);
            this.groupBox2.Controls.Add(this.label76);
            this.groupBox2.Controls.Add(this.label78);
            this.groupBox2.Controls.Add(this.labelCloudVer);
            this.groupBox2.Controls.Add(this.LabelVersion);
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 131);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ASC Update";
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnClose.Location = new System.Drawing.Point(161, 95);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 23);
            this.btnClose.TabIndex = 79;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGoToDownloadPage
            // 
            this.btnGoToDownloadPage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnGoToDownloadPage.Location = new System.Drawing.Point(18, 95);
            this.btnGoToDownloadPage.Name = "btnGoToDownloadPage";
            this.btnGoToDownloadPage.Size = new System.Drawing.Size(137, 23);
            this.btnGoToDownloadPage.TabIndex = 77;
            this.btnGoToDownloadPage.Text = "Go to download page";
            this.btnGoToDownloadPage.UseVisualStyleBackColor = true;
            this.btnGoToDownloadPage.Click += new System.EventHandler(this.btnGoToDownloadPage_Click);
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(94, 56);
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
            this.labelCloudVer.Location = new System.Drawing.Point(135, 75);
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
            // UpdateShowDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 153);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(345, 192);
            this.MinimumSize = new System.Drawing.Size(345, 192);
            this.Name = "UpdateShowDialog";
            this.Text = "ASC: New version available";
            this.Load += new System.EventHandler(this.UpdateShowDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGoToDownloadPage;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label labelCloudVer;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Button btnClose;
    }
}