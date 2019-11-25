namespace TextSearch
{
    partial class Search
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fileNameTxtBox = new System.Windows.Forms.TextBox();
            this.searchWordTxtBox = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.searchBtn = new System.Windows.Forms.Button();
            this.textLine = new System.Windows.Forms.ListView();
            this.lineNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.progressLbl = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Word";
            // 
            // fileNameTxtBox
            // 
            this.fileNameTxtBox.Location = new System.Drawing.Point(96, 13);
            this.fileNameTxtBox.Name = "fileNameTxtBox";
            this.fileNameTxtBox.Size = new System.Drawing.Size(454, 20);
            this.fileNameTxtBox.TabIndex = 2;
            // 
            // searchWordTxtBox
            // 
            this.searchWordTxtBox.Location = new System.Drawing.Point(97, 39);
            this.searchWordTxtBox.Name = "searchWordTxtBox";
            this.searchWordTxtBox.Size = new System.Drawing.Size(453, 20);
            this.searchWordTxtBox.TabIndex = 3;
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(556, 10);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 4;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(556, 36);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 5;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // textLine
            // 
            this.textLine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lineNo,
            this.text});
            this.textLine.FullRowSelect = true;
            this.textLine.GridLines = true;
            this.textLine.Location = new System.Drawing.Point(13, 76);
            this.textLine.Name = "textLine";
            this.textLine.Size = new System.Drawing.Size(618, 331);
            this.textLine.TabIndex = 6;
            this.textLine.UseCompatibleStateImageBehavior = false;
            this.textLine.View = System.Windows.Forms.View.Details;
            // 
            // lineNo
            // 
            this.lineNo.Text = "Line No.";
            this.lineNo.Width = 88;
            // 
            // text
            // 
            this.text.Text = "Text";
            this.text.Width = 525;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 413);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(619, 23);
            this.progressBar.TabIndex = 7;
            // 
            // progressLbl
            // 
            this.progressLbl.AutoSize = true;
            this.progressLbl.Location = new System.Drawing.Point(12, 443);
            this.progressLbl.Name = "progressLbl";
            this.progressLbl.Size = new System.Drawing.Size(0, 13);
            this.progressLbl.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(643, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 488);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressLbl);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textLine);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.browseBtn);
            this.Controls.Add(this.searchWordTxtBox);
            this.Controls.Add(this.fileNameTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Search";
            this.Text = "Text Search";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileNameTxtBox;
        private System.Windows.Forms.TextBox searchWordTxtBox;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.ListView textLine;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label progressLbl;
        private System.Windows.Forms.ColumnHeader lineNo;
        private System.Windows.Forms.ColumnHeader text;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

