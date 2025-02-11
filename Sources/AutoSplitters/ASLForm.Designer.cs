namespace AutoSplitterCore.Sources.AutoSplitters
{
    partial class ASLForm
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
            this.poisonTabControl = new ReaLTaiizor.Controls.PoisonTabControl();
            this.tabPageASLConfig = new ReaLTaiizor.Controls.PoisonTabPage();
            this.tabPageASLDownload = new ReaLTaiizor.Controls.PoisonTabPage();
            this.poisonTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // poisonTabControl
            // 
            this.poisonTabControl.Controls.Add(this.tabPageASLConfig);
            this.poisonTabControl.Controls.Add(this.tabPageASLDownload);
            this.poisonTabControl.Location = new System.Drawing.Point(6, 67);
            this.poisonTabControl.Name = "poisonTabControl";
            this.poisonTabControl.SelectedIndex = 0;
            this.poisonTabControl.Size = new System.Drawing.Size(788, 683);
            this.poisonTabControl.TabIndex = 0;
            this.poisonTabControl.UseSelectable = true;
            // 
            // tabPageASLConfig
            // 
            this.tabPageASLConfig.HorizontalScrollbarBarColor = true;
            this.tabPageASLConfig.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageASLConfig.HorizontalScrollbarSize = 10;
            this.tabPageASLConfig.Location = new System.Drawing.Point(4, 38);
            this.tabPageASLConfig.Name = "tabPageASLConfig";
            this.tabPageASLConfig.Size = new System.Drawing.Size(780, 641);
            this.tabPageASLConfig.TabIndex = 0;
            this.tabPageASLConfig.Text = "ASL Configuration";
            this.tabPageASLConfig.VerticalScrollbarBarColor = true;
            this.tabPageASLConfig.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageASLConfig.VerticalScrollbarSize = 10;
            // 
            // tabPageASLDownload
            // 
            this.tabPageASLDownload.HorizontalScrollbarBarColor = true;
            this.tabPageASLDownload.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageASLDownload.HorizontalScrollbarSize = 10;
            this.tabPageASLDownload.Location = new System.Drawing.Point(4, 38);
            this.tabPageASLDownload.Name = "tabPageASLDownload";
            this.tabPageASLDownload.Size = new System.Drawing.Size(780, 641);
            this.tabPageASLDownload.TabIndex = 1;
            this.tabPageASLDownload.Text = "Download ASL Files";
            this.tabPageASLDownload.VerticalScrollbarBarColor = true;
            this.tabPageASLDownload.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageASLDownload.VerticalScrollbarSize = 10;
            // 
            // ASLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 756);
            this.Controls.Add(this.poisonTabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ASLForm";
            this.Text = "ASL";
            this.Load += new System.EventHandler(this.ASLForm_Load);
            this.poisonTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.PoisonTabControl poisonTabControl;
        private ReaLTaiizor.Controls.PoisonTabPage tabPageASLConfig;
        private ReaLTaiizor.Controls.PoisonTabPage tabPageASLDownload;
    }
}