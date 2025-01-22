namespace AutoSplitterCore
{
    partial class Report
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
            this.labelEmail = new System.Windows.Forms.Label();
            this.aloneComboBoxRason = new ReaLTaiizor.Controls.AloneComboBox();
            this.textBoxDetail = new System.Windows.Forms.TextBox();
            this.btnSend = new ReaLTaiizor.Controls.Button();
            this.SuspendLayout();
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.ForeColor = System.Drawing.Color.Red;
            this.labelEmail.Location = new System.Drawing.Point(15, 86);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(32, 13);
            this.labelEmail.TabIndex = 0;
            this.labelEmail.Text = "Email";
            // 
            // aloneComboBoxRason
            // 
            this.aloneComboBoxRason.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aloneComboBoxRason.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.aloneComboBoxRason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aloneComboBoxRason.EnabledCalc = true;
            this.aloneComboBoxRason.FormattingEnabled = true;
            this.aloneComboBoxRason.ItemHeight = 20;
            this.aloneComboBoxRason.Items.AddRange(new object[] {
            "Error to Upload",
            "Language Ofensive, discrimination, other",
            "Trash Profile",
            "Other"});
            this.aloneComboBoxRason.Location = new System.Drawing.Point(106, 101);
            this.aloneComboBoxRason.Name = "aloneComboBoxRason";
            this.aloneComboBoxRason.Size = new System.Drawing.Size(273, 26);
            this.aloneComboBoxRason.TabIndex = 2;
            // 
            // textBoxDetail
            // 
            this.textBoxDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDetail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDetail.Location = new System.Drawing.Point(32, 136);
            this.textBoxDetail.Multiline = true;
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Size = new System.Drawing.Size(410, 175);
            this.textBoxDetail.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnSend.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Image = null;
            this.btnSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSend.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnSend.Location = new System.Drawing.Point(198, 316);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnSend.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnSend.Size = new System.Drawing.Size(81, 24);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 345);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBoxDetail);
            this.Controls.Add(this.aloneComboBoxRason);
            this.Controls.Add(this.labelEmail);
            this.FormStyle = ReaLTaiizor.Enum.Material.FormStyles.ActionBar_48;
            this.MaximizeBox = false;
            this.Name = "Report";
            this.Padding = new System.Windows.Forms.Padding(3, 72, 3, 3);
            this.Text = "Report";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEmail;
        private ReaLTaiizor.Controls.AloneComboBox aloneComboBoxRason;
        private System.Windows.Forms.TextBox textBoxDetail;
        private ReaLTaiizor.Controls.Button btnSend;
    }
}