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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleAuth));
            this.checkedListBoxGamesSearch = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new ReaLTaiizor.Controls.Button();
            this.btnForgetLogin = new ReaLTaiizor.Controls.Button();
            this.groupBoxUpload = new ReaLTaiizor.Controls.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkedListBoxGames = new System.Windows.Forms.CheckedListBox();
            this.btnUploadProfile = new ReaLTaiizor.Controls.Button();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TextboxDescription = new ReaLTaiizor.Controls.HopeTextBox();
            this.textBoxAuthor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCurrrentProfile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new ReaLTaiizor.Controls.GroupBox();
            this.TextBoxSummary = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBoxManagment = new ReaLTaiizor.Controls.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnInstall = new ReaLTaiizor.Controls.Button();
            groupBox1 = new ReaLTaiizor.Controls.GroupBox();
            groupBox1.SuspendLayout();
            this.groupBoxUpload.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBoxManagment.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = System.Drawing.Color.Transparent;
            groupBox1.BackGColor = System.Drawing.Color.Khaki;
            groupBox1.BaseColor = System.Drawing.Color.Transparent;
            groupBox1.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            groupBox1.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            groupBox1.Controls.Add(this.btnInstall);
            groupBox1.Controls.Add(this.checkedListBoxGamesSearch);
            groupBox1.Controls.Add(this.label7);
            groupBox1.Controls.Add(this.textBoxSearch);
            groupBox1.Controls.Add(this.listViewFiles);
            groupBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            groupBox1.HeaderColor = System.Drawing.Color.PowderBlue;
            groupBox1.Location = new System.Drawing.Point(20, 305);
            groupBox1.MinimumSize = new System.Drawing.Size(136, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            groupBox1.Size = new System.Drawing.Size(744, 474);
            groupBox1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            groupBox1.TabIndex = 1;
            groupBox1.Text = "Download";
            // 
            // checkedListBoxGamesSearch
            // 
            this.checkedListBoxGamesSearch.CheckOnClick = true;
            this.checkedListBoxGamesSearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxGamesSearch.FormattingEnabled = true;
            this.checkedListBoxGamesSearch.Location = new System.Drawing.Point(22, 104);
            this.checkedListBoxGamesSearch.Name = "checkedListBoxGamesSearch";
            this.checkedListBoxGamesSearch.Size = new System.Drawing.Size(176, 157);
            this.checkedListBoxGamesSearch.TabIndex = 5;
            this.checkedListBoxGamesSearch.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxGamesSearch_ItemCheck);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 14);
            this.label7.TabIndex = 4;
            this.label7.Text = "Search";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(22, 62);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(176, 21);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // listViewFiles
            // 
            this.listViewFiles.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(224, 39);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(498, 427);
            this.listViewFiles.TabIndex = 2;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.List;
            this.listViewFiles.SelectedIndexChanged += new System.EventHandler(this.listViewFiles_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.linkLabel1.LinkColor = System.Drawing.Color.Red;
            this.linkLabel1.Location = new System.Drawing.Point(79, 45);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 21);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "NoLogin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(21, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email>";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnLogin.EnteredColor = System.Drawing.Color.Red;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLogin.Image = null;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.InactiveColor = System.Drawing.Color.Tomato;
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
            // btnForgetLogin
            // 
            this.btnForgetLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnForgetLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnForgetLogin.Enabled = false;
            this.btnForgetLogin.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnForgetLogin.Image = null;
            this.btnForgetLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnForgetLogin.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.Location = new System.Drawing.Point(1512, 104);
            this.btnForgetLogin.Name = "btnForgetLogin";
            this.btnForgetLogin.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnForgetLogin.Size = new System.Drawing.Size(120, 42);
            this.btnForgetLogin.TabIndex = 3;
            this.btnForgetLogin.Text = "Forget Login";
            this.btnForgetLogin.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnForgetLogin.Click += new System.EventHandler(this.btnForgetLogin_Click);
            // 
            // groupBoxUpload
            // 
            this.groupBoxUpload.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxUpload.BackGColor = System.Drawing.SystemColors.Highlight;
            this.groupBoxUpload.BaseColor = System.Drawing.Color.Transparent;
            this.groupBoxUpload.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            this.groupBoxUpload.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            this.groupBoxUpload.Controls.Add(this.label6);
            this.groupBoxUpload.Controls.Add(this.checkedListBoxGames);
            this.groupBoxUpload.Controls.Add(this.btnUploadProfile);
            this.groupBoxUpload.Controls.Add(this.textBoxDate);
            this.groupBoxUpload.Controls.Add(this.label3);
            this.groupBoxUpload.Controls.Add(this.label5);
            this.groupBoxUpload.Controls.Add(this.TextboxDescription);
            this.groupBoxUpload.Controls.Add(this.textBoxAuthor);
            this.groupBoxUpload.Controls.Add(this.label4);
            this.groupBoxUpload.Controls.Add(this.textBoxCurrrentProfile);
            this.groupBoxUpload.Controls.Add(this.label2);
            this.groupBoxUpload.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBoxUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.groupBoxUpload.HeaderColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBoxUpload.Location = new System.Drawing.Point(20, 26);
            this.groupBoxUpload.MinimumSize = new System.Drawing.Size(136, 50);
            this.groupBoxUpload.Name = "groupBoxUpload";
            this.groupBoxUpload.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.groupBoxUpload.Size = new System.Drawing.Size(834, 246);
            this.groupBoxUpload.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.groupBoxUpload.TabIndex = 4;
            this.groupBoxUpload.Text = "Upload Current Profile";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(605, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 14);
            this.label6.TabIndex = 40;
            this.label6.Text = "Select Games For Profile";
            // 
            // checkedListBoxGames
            // 
            this.checkedListBoxGames.CheckOnClick = true;
            this.checkedListBoxGames.FormattingEnabled = true;
            this.checkedListBoxGames.Location = new System.Drawing.Point(605, 57);
            this.checkedListBoxGames.Name = "checkedListBoxGames";
            this.checkedListBoxGames.Size = new System.Drawing.Size(207, 157);
            this.checkedListBoxGames.TabIndex = 39;
            // 
            // btnUploadProfile
            // 
            this.btnUploadProfile.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadProfile.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadProfile.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnUploadProfile.Image = null;
            this.btnUploadProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadProfile.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.Location = new System.Drawing.Point(268, 204);
            this.btnUploadProfile.Name = "btnUploadProfile";
            this.btnUploadProfile.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnUploadProfile.Size = new System.Drawing.Size(118, 29);
            this.btnUploadProfile.TabIndex = 38;
            this.btnUploadProfile.Text = "Upload";
            this.btnUploadProfile.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnUploadProfile.Click += new System.EventHandler(this.btnUploadProfile_Click);
            // 
            // textBoxDate
            // 
            this.textBoxDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDate.Enabled = false;
            this.textBoxDate.Location = new System.Drawing.Point(428, 56);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(145, 22);
            this.textBoxDate.TabIndex = 37;
            this.textBoxDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(389, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 14);
            this.label3.TabIndex = 36;
            this.label3.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(37, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 35;
            this.label5.Text = "Description";
            // 
            // TextboxDescription
            // 
            this.TextboxDescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TextboxDescription.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(66)))));
            this.TextboxDescription.BorderColorA = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.TextboxDescription.BorderColorB = System.Drawing.Color.DarkSlateGray;
            this.TextboxDescription.Enabled = false;
            this.TextboxDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxDescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(49)))), ((int)(((byte)(51)))));
            this.TextboxDescription.Hint = "";
            this.TextboxDescription.Location = new System.Drawing.Point(113, 98);
            this.TextboxDescription.MaxLength = 32767;
            this.TextboxDescription.Multiline = true;
            this.TextboxDescription.Name = "TextboxDescription";
            this.TextboxDescription.PasswordChar = '\0';
            this.TextboxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextboxDescription.SelectedText = "";
            this.TextboxDescription.SelectionLength = 0;
            this.TextboxDescription.SelectionStart = 0;
            this.TextboxDescription.Size = new System.Drawing.Size(468, 96);
            this.TextboxDescription.TabIndex = 34;
            this.TextboxDescription.TabStop = false;
            this.TextboxDescription.UseSystemPasswordChar = false;
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAuthor.Enabled = false;
            this.textBoxAuthor.Location = new System.Drawing.Point(189, 70);
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.textBoxAuthor.ReadOnly = true;
            this.textBoxAuthor.Size = new System.Drawing.Size(194, 22);
            this.textBoxAuthor.TabIndex = 32;
            this.textBoxAuthor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(113, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 14);
            this.label4.TabIndex = 31;
            this.label4.Text = "Author";
            // 
            // textBoxCurrrentProfile
            // 
            this.textBoxCurrrentProfile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrrentProfile.Enabled = false;
            this.textBoxCurrrentProfile.Location = new System.Drawing.Point(189, 37);
            this.textBoxCurrrentProfile.Name = "textBoxCurrrentProfile";
            this.textBoxCurrrentProfile.ReadOnly = true;
            this.textBoxCurrrentProfile.Size = new System.Drawing.Size(194, 22);
            this.textBoxCurrrentProfile.TabIndex = 29;
            this.textBoxCurrrentProfile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(88, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Current Profile";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.BackGColor = System.Drawing.Color.MediumTurquoise;
            this.groupBox5.BaseColor = System.Drawing.Color.Transparent;
            this.groupBox5.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            this.groupBox5.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            this.groupBox5.Controls.Add(this.TextBoxSummary);
            this.groupBox5.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBox5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.groupBox5.HeaderColor = System.Drawing.Color.DarkOrange;
            this.groupBox5.Location = new System.Drawing.Point(782, 305);
            this.groupBox5.MinimumSize = new System.Drawing.Size(136, 50);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.groupBox5.Size = new System.Drawing.Size(669, 474);
            this.groupBox5.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.groupBox5.TabIndex = 24;
            this.groupBox5.Text = "Selected Profile Summary";
            // 
            // TextBoxSummary
            // 
            this.TextBoxSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSummary.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.TextBoxSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxSummary.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxSummary.Location = new System.Drawing.Point(14, 31);
            this.TextBoxSummary.Multiline = true;
            this.TextBoxSummary.Name = "TextBoxSummary";
            this.TextBoxSummary.ReadOnly = true;
            this.TextBoxSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxSummary.Size = new System.Drawing.Size(645, 435);
            this.TextBoxSummary.TabIndex = 3;
            this.TextBoxSummary.TabStop = false;
            // 
            // groupBoxManagment
            // 
            this.groupBoxManagment.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxManagment.BackGColor = System.Drawing.Color.Transparent;
            this.groupBoxManagment.BaseColor = System.Drawing.Color.Transparent;
            this.groupBoxManagment.BorderColorG = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(159)))), ((int)(((byte)(161)))));
            this.groupBoxManagment.BorderColorH = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(180)))), ((int)(((byte)(186)))));
            this.groupBoxManagment.Controls.Add(this.textBox1);
            this.groupBoxManagment.Controls.Add(this.groupBoxUpload);
            this.groupBoxManagment.Controls.Add(groupBox1);
            this.groupBoxManagment.Controls.Add(this.groupBox5);
            this.groupBoxManagment.Enabled = false;
            this.groupBoxManagment.Font = new System.Drawing.Font("Tahoma", 9F);
            this.groupBoxManagment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.groupBoxManagment.HeaderColor = System.Drawing.Color.ForestGreen;
            this.groupBoxManagment.Location = new System.Drawing.Point(25, 78);
            this.groupBoxManagment.MinimumSize = new System.Drawing.Size(136, 50);
            this.groupBoxManagment.Name = "groupBoxManagment";
            this.groupBoxManagment.Padding = new System.Windows.Forms.Padding(5, 28, 5, 5);
            this.groupBoxManagment.Size = new System.Drawing.Size(1469, 795);
            this.groupBoxManagment.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.groupBoxManagment.TabIndex = 26;
            this.groupBoxManagment.Text = "Cloud Profiles";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Yellow;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(930, 50);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(472, 222);
            this.textBox1.TabIndex = 25;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.Transparent;
            this.btnInstall.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstall.EnteredBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnInstall.EnteredColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnInstall.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInstall.Image = null;
            this.btnInstall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstall.InactiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(34)))), ((int)(((byte)(37)))));
            this.btnInstall.Location = new System.Drawing.Point(48, 317);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.PressedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnInstall.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.btnInstall.Size = new System.Drawing.Size(118, 47);
            this.btnInstall.TabIndex = 39;
            this.btnInstall.Text = "Install Selected";
            this.btnInstall.TextAlignment = System.Drawing.StringAlignment.Center;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // GoogleAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1651, 890);
            this.Controls.Add(this.groupBoxManagment);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnForgetLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLogin);
            this.Image = null;
            this.Name = "GoogleAuth";
            this.Text = "Google Auth";
            this.Load += new System.EventHandler(this.GoogleAuth_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.groupBoxUpload.ResumeLayout(false);
            this.groupBoxUpload.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBoxManagment.ResumeLayout(false);
            this.groupBoxManagment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewFiles;
        private ReaLTaiizor.Controls.Button btnForgetLogin;
        private ReaLTaiizor.Controls.GroupBox groupBoxUpload;
        private System.Windows.Forms.Label label5;
        private ReaLTaiizor.Controls.HopeTextBox TextboxDescription;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCurrrentProfile;
        private System.Windows.Forms.Label label2;
        private ReaLTaiizor.Controls.Button btnUploadProfile;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label label3;
        private ReaLTaiizor.Controls.GroupBox groupBox5;
        internal System.Windows.Forms.TextBox TextBoxSummary;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ReaLTaiizor.Controls.GroupBox groupBoxManagment;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox checkedListBoxGames;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.CheckedListBox checkedListBoxGamesSearch;
        private ReaLTaiizor.Controls.Button btnInstall;
    }
}