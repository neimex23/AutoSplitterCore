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
            this.groupBoxUpdate = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGoToDownloadPage = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.labelCloudVer = new System.Windows.Forms.Label();
            this.LabelVersion = new System.Windows.Forms.Label();
            this.groupBoxInstallerSelect = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPortable = new System.Windows.Forms.Button();
            this.btnInstaller = new System.Windows.Forms.Button();
            this.groupBoxUpdating = new System.Windows.Forms.GroupBox();
            this.progressBarUpdating = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxUpdate.SuspendLayout();
            this.groupBoxInstallerSelect.SuspendLayout();
            this.groupBoxUpdating.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxUpdate
            // 
            this.groupBoxUpdate.Controls.Add(this.btnUpdate);
            this.groupBoxUpdate.Controls.Add(this.btnClose);
            this.groupBoxUpdate.Controls.Add(this.btnGoToDownloadPage);
            this.groupBoxUpdate.Controls.Add(this.label76);
            this.groupBoxUpdate.Controls.Add(this.label78);
            this.groupBoxUpdate.Controls.Add(this.labelCloudVer);
            this.groupBoxUpdate.Controls.Add(this.LabelVersion);
            this.groupBoxUpdate.Location = new System.Drawing.Point(12, 13);
            this.groupBoxUpdate.Name = "groupBoxUpdate";
            this.groupBoxUpdate.Size = new System.Drawing.Size(305, 164);
            this.groupBoxUpdate.TabIndex = 5;
            this.groupBoxUpdate.TabStop = false;
            this.groupBoxUpdate.Text = "ASC Update";
            // 
            // btnUpdate
            // 
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnUpdate.Location = new System.Drawing.Point(85, 97);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(137, 23);
            this.btnUpdate.TabIndex = 80;
            this.btnUpdate.Text = "Update";
            
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnClose.Location = new System.Drawing.Point(162, 126);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(137, 23);
            this.btnClose.TabIndex = 79;
            this.btnClose.Text = "Close";
            
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnGoToDownloadPage
            // 
            this.btnGoToDownloadPage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnGoToDownloadPage.Location = new System.Drawing.Point(18, 126);
            this.btnGoToDownloadPage.Name = "btnGoToDownloadPage";
            this.btnGoToDownloadPage.Size = new System.Drawing.Size(137, 23);
            this.btnGoToDownloadPage.TabIndex = 77;
            this.btnGoToDownloadPage.Text = "Go to download page";
            
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
            // groupBoxInstallerSelect
            // 
            this.groupBoxInstallerSelect.Controls.Add(this.label1);
            this.groupBoxInstallerSelect.Controls.Add(this.btnPortable);
            this.groupBoxInstallerSelect.Controls.Add(this.btnInstaller);
            this.groupBoxInstallerSelect.Location = new System.Drawing.Point(12, 14);
            this.groupBoxInstallerSelect.Name = "groupBoxInstallerSelect";
            this.groupBoxInstallerSelect.Size = new System.Drawing.Size(305, 163);
            this.groupBoxInstallerSelect.TabIndex = 81;
            this.groupBoxInstallerSelect.TabStop = false;
            this.groupBoxInstallerSelect.Text = "Installer Select";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Select Installation Method";
            // 
            // btnPortable
            // 
            this.btnPortable.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnPortable.Location = new System.Drawing.Point(161, 84);
            this.btnPortable.Name = "btnPortable";
            this.btnPortable.Size = new System.Drawing.Size(137, 23);
            this.btnPortable.TabIndex = 82;
            this.btnPortable.Text = "Portable";
            
            this.btnPortable.Click += new System.EventHandler(this.btnPortable_Click);
            // 
            // btnInstaller
            // 
            this.btnInstaller.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnInstaller.Location = new System.Drawing.Point(18, 84);
            this.btnInstaller.Name = "btnInstaller";
            this.btnInstaller.Size = new System.Drawing.Size(137, 23);
            this.btnInstaller.TabIndex = 81;
            this.btnInstaller.Text = "Installer";
            
            this.btnInstaller.Click += new System.EventHandler(this.btnInstaller_Click);
            // 
            // groupBoxUpdating
            // 
            this.groupBoxUpdating.Controls.Add(this.progressBarUpdating);
            this.groupBoxUpdating.Controls.Add(this.label2);
            this.groupBoxUpdating.Location = new System.Drawing.Point(12, 11);
            this.groupBoxUpdating.Name = "groupBoxUpdating";
            this.groupBoxUpdating.Size = new System.Drawing.Size(306, 166);
            this.groupBoxUpdating.TabIndex = 82;
            this.groupBoxUpdating.TabStop = false;
            // 
            // progressBarUpdating
            // 
            this.progressBarUpdating.Location = new System.Drawing.Point(54, 77);
            this.progressBarUpdating.Name = "progressBarUpdating";
            this.progressBarUpdating.Size = new System.Drawing.Size(197, 23);
            this.progressBarUpdating.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Updating...";
            // 
            // UpdateShowDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 185);
            this.Controls.Add(this.groupBoxUpdate);
            this.Controls.Add(this.groupBoxInstallerSelect);
            this.Controls.Add(this.groupBoxUpdating);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(345, 224);
            this.Name = "UpdateShowDialog";
            this.Text = "ASC: New version available";
            this.Load += new System.EventHandler(this.UpdateShowDialog_Load);
            this.groupBoxUpdate.ResumeLayout(false);
            this.groupBoxUpdate.PerformLayout();
            this.groupBoxInstallerSelect.ResumeLayout(false);
            this.groupBoxInstallerSelect.PerformLayout();
            this.groupBoxUpdating.ResumeLayout(false);
            this.groupBoxUpdating.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxUpdate;
        private System.Windows.Forms.Button btnGoToDownloadPage;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label labelCloudVer;
        private System.Windows.Forms.Label LabelVersion;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox groupBoxInstallerSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPortable;
        private System.Windows.Forms.Button btnInstaller;
        private System.Windows.Forms.GroupBox groupBoxUpdating;
        private System.Windows.Forms.ProgressBar progressBarUpdating;
        private System.Windows.Forms.Label label2;
    }
}