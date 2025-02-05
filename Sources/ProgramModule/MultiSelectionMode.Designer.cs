namespace AutoSplitterCore
{
    partial class MultiSelectionMode
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
            this.btn_AddMulti = new ReaLTaiizor.Controls.Button();
            this.panelItems = new System.Windows.Forms.FlowLayoutPanel();
            this.searchPanel = new ReaLTaiizor.Controls.LostBorderPanel();
            this.btnSearch = new ReaLTaiizor.Controls.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_AddMulti
            // 
            this.btn_AddMulti.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddMulti.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddMulti.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AddMulti.Image = null;
            this.btn_AddMulti.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_AddMulti.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.Location = new System.Drawing.Point(284, 480);
            this.btn_AddMulti.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AddMulti.Name = "btn_AddMulti";
            this.btn_AddMulti.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btn_AddMulti.Size = new System.Drawing.Size(120, 31);
            this.btn_AddMulti.TabIndex = 2;
            this.btn_AddMulti.Text = "Add";
            this.btn_AddMulti.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btn_AddMulti.Click += new System.EventHandler(this.btn_AddMulti_Click);
            // 
            // panelItems
            // 
            this.panelItems.BackColor = System.Drawing.Color.Khaki;
            this.panelItems.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelItems.Location = new System.Drawing.Point(6, 67);
            this.panelItems.Name = "panelItems";
            this.panelItems.Size = new System.Drawing.Size(670, 373);
            this.panelItems.TabIndex = 1;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.searchPanel.BorderColor = System.Drawing.Color.DodgerBlue;
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.searchPanel.ForeColor = System.Drawing.Color.White;
            this.searchPanel.Location = new System.Drawing.Point(6, 441);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(5);
            this.searchPanel.ShowText = true;
            this.searchPanel.Size = new System.Drawing.Size(670, 33);
            this.searchPanel.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnSearch.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = null;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.InactiveColor = System.Drawing.Color.DarkSlateGray;
            this.btnSearch.Location = new System.Drawing.Point(547, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnSearch.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnSearch.Size = new System.Drawing.Size(120, 31);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(3, 2);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(545, 29);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
            // 
            // MultiSelectionMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(682, 521);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.panelItems);
            this.Controls.Add(this.btn_AddMulti);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(682, 521);
            this.MinimumSize = new System.Drawing.Size(682, 521);
            this.Name = "MultiSelectionMode";
            this.Text = "MultiSelecctionMode";
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ReaLTaiizor.Controls.Button btn_AddMulti;
        private System.Windows.Forms.FlowLayoutPanel panelItems;
        private ReaLTaiizor.Controls.LostBorderPanel searchPanel;
        private System.Windows.Forms.TextBox searchTextBox;
        private ReaLTaiizor.Controls.Button btnSearch;
    }
}