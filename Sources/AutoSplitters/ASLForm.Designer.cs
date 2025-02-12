namespace AutoSplitterCore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ASLForm));
            this.poisonTabControl = new ReaLTaiizor.Controls.PoisonTabControl();
            this.tabPageASLConfig = new ReaLTaiizor.Controls.PoisonTabPage();
            this.lostBorderPanelASLConfig = new ReaLTaiizor.Controls.LostBorderPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.metroCheckBoxIGT = new ReaLTaiizor.Controls.MetroCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageASLDownload = new ReaLTaiizor.Controls.PoisonTabPage();
            this.poisonTabControl.SuspendLayout();
            this.tabPageASLConfig.SuspendLayout();
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
            this.tabPageASLConfig.Controls.Add(this.lostBorderPanelASLConfig);
            this.tabPageASLConfig.Controls.Add(this.label2);
            this.tabPageASLConfig.Controls.Add(this.metroCheckBoxIGT);
            this.tabPageASLConfig.Controls.Add(this.label1);
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
            // lostBorderPanelASLConfig
            // 
            this.lostBorderPanelASLConfig.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lostBorderPanelASLConfig.BorderColor = System.Drawing.Color.Transparent;
            this.lostBorderPanelASLConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lostBorderPanelASLConfig.ForeColor = System.Drawing.Color.Black;
            this.lostBorderPanelASLConfig.Location = new System.Drawing.Point(3, 3);
            this.lostBorderPanelASLConfig.Name = "lostBorderPanelASLConfig";
            this.lostBorderPanelASLConfig.Padding = new System.Windows.Forms.Padding(5);
            this.lostBorderPanelASLConfig.ShowText = false;
            this.lostBorderPanelASLConfig.Size = new System.Drawing.Size(774, 526);
            this.lostBorderPanelASLConfig.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 569);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Start override with Start+IGT\r\nNot use if GameTime is not available";
            // 
            // metroCheckBoxIGT
            // 
            this.metroCheckBoxIGT.BackColor = System.Drawing.Color.Transparent;
            this.metroCheckBoxIGT.BackgroundColor = System.Drawing.Color.White;
            this.metroCheckBoxIGT.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.metroCheckBoxIGT.Checked = false;
            this.metroCheckBoxIGT.CheckSignColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.metroCheckBoxIGT.CheckState = ReaLTaiizor.Enum.Metro.CheckState.Unchecked;
            this.metroCheckBoxIGT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroCheckBoxIGT.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            this.metroCheckBoxIGT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.metroCheckBoxIGT.IsDerivedStyle = true;
            this.metroCheckBoxIGT.Location = new System.Drawing.Point(437, 549);
            this.metroCheckBoxIGT.Name = "metroCheckBoxIGT";
            this.metroCheckBoxIGT.SignStyle = ReaLTaiizor.Enum.Metro.SignStyle.Sign;
            this.metroCheckBoxIGT.Size = new System.Drawing.Size(224, 16);
            this.metroCheckBoxIGT.Style = ReaLTaiizor.Enum.Metro.Style.Light;
            this.metroCheckBoxIGT.StyleManager = null;
            this.metroCheckBoxIGT.TabIndex = 2;
            this.metroCheckBoxIGT.Text = "Use Game Time (When is available)";
            this.metroCheckBoxIGT.ThemeAuthor = "Taiizor";
            this.metroCheckBoxIGT.ThemeName = "MetroLight";
            this.metroCheckBoxIGT.CheckedChanged += new ReaLTaiizor.Controls.MetroCheckBox.CheckedChangedEventHandler(this.metroCheckBoxIGT_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 549);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
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
            this.tabPageASLConfig.ResumeLayout(false);
            this.tabPageASLConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.PoisonTabControl poisonTabControl;
        private ReaLTaiizor.Controls.PoisonTabPage tabPageASLConfig;
        private ReaLTaiizor.Controls.PoisonTabPage tabPageASLDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ReaLTaiizor.Controls.MetroCheckBox metroCheckBoxIGT;
        private ReaLTaiizor.Controls.LostBorderPanel lostBorderPanelASLConfig;
    }
}