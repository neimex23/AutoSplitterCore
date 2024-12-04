namespace AutoSplitterCore
{
    partial class GoogleAuth
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
            ReaLTaiizor.Controls.GroupBox groupBox1;
            this.btnLogin = new ReaLTaiizor.Controls.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewFiles = new ReaLTaiizor.Controls.MaterialListView();
            groupBox1 = new ReaLTaiizor.Controls.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnLogin.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLogin.Image = null;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnLogin.Location = new System.Drawing.Point(1512, 55);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnLogin.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnLogin.Size = new System.Drawing.Size(120, 40);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox1
            // 
            groupBox1.BackColor = System.Drawing.Color.Transparent;
            groupBox1.BackGColor = System.Drawing.Color.Khaki;
            groupBox1.BaseColor = System.Drawing.Color.Transparent;
            groupBox1.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            groupBox1.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            groupBox1.Controls.Add(this.listViewFiles);
            groupBox1.Controls.Add(this.linkLabel1);
            groupBox1.Controls.Add(this.label1);
            groupBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            groupBox1.HeaderColor = System.Drawing.Color.PowderBlue;
            groupBox1.Location = new System.Drawing.Point(25, 55);
            groupBox1.MinimumSize = new System.Drawing.Size(136, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            groupBox1.Size = new System.Drawing.Size(879, 890);
            groupBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            groupBox1.TabIndex = 1;
            groupBox1.Text = "Profile View";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(57, 28);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(59, 14);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email>";
            // 
            // listViewFiles
            // 
            this.listViewFiles.AutoSizeTable = false;
            this.listViewFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.listViewFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewFiles.Depth = 0;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(11, 56);
            this.listViewFiles.MinimumSize = new System.Drawing.Size(200, 100);
            this.listViewFiles.MouseLocation = new System.Drawing.Point(-1, -1);
            this.listViewFiles.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.OwnerDraw = true;
            this.listViewFiles.Size = new System.Drawing.Size(672, 817);
            this.listViewFiles.TabIndex = 2;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // GoogleAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1651, 1014);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(groupBox1);
            this.Image = null;
            this.Name = "GoogleAuth";
            this.Text = "Google Auth";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private ReaLTaiizor.Controls.MaterialListView listViewFiles;
    }
}