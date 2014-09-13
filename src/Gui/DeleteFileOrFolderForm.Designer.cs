namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
    partial class DeleteFileOrFolderForm
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
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.suffixTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.folderNameTextBox = new System.Windows.Forms.TextBox();
            this.fileInclusiveCheckBox = new System.Windows.Forms.CheckBox();
            this.folderInclusiveCheckBox = new System.Windows.Forms.CheckBox();
            this.ignoreFolderHiddenCheckBox = new System.Windows.Forms.CheckBox();
            this.ignoreFileHiddenCheckBox = new System.Windows.Forms.CheckBox();
            this.selectPathButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(106, 20);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(253, 21);
            this.pathTextBox.TabIndex = 0;
            // 
            // suffixTextBox
            // 
            this.suffixTextBox.Location = new System.Drawing.Point(106, 61);
            this.suffixTextBox.Name = "suffixTextBox";
            this.suffixTextBox.Size = new System.Drawing.Size(167, 21);
            this.suffixTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "根目录";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "文件后缀";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(325, 139);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 100;
            this.okButton.Text = "确 定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "文件夹名";
            // 
            // folderNameTextBox
            // 
            this.folderNameTextBox.Location = new System.Drawing.Point(106, 101);
            this.folderNameTextBox.Name = "folderNameTextBox";
            this.folderNameTextBox.Size = new System.Drawing.Size(167, 21);
            this.folderNameTextBox.TabIndex = 5;
            // 
            // fileInclusiveCheckBox
            // 
            this.fileInclusiveCheckBox.AutoSize = true;
            this.fileInclusiveCheckBox.Checked = true;
            this.fileInclusiveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fileInclusiveCheckBox.Location = new System.Drawing.Point(374, 63);
            this.fileInclusiveCheckBox.Name = "fileInclusiveCheckBox";
            this.fileInclusiveCheckBox.Size = new System.Drawing.Size(48, 16);
            this.fileInclusiveCheckBox.TabIndex = 4;
            this.fileInclusiveCheckBox.Text = "递归";
            this.fileInclusiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // folderInclusiveCheckBox
            // 
            this.folderInclusiveCheckBox.AutoSize = true;
            this.folderInclusiveCheckBox.Checked = true;
            this.folderInclusiveCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.folderInclusiveCheckBox.Location = new System.Drawing.Point(374, 103);
            this.folderInclusiveCheckBox.Name = "folderInclusiveCheckBox";
            this.folderInclusiveCheckBox.Size = new System.Drawing.Size(48, 16);
            this.folderInclusiveCheckBox.TabIndex = 7;
            this.folderInclusiveCheckBox.Text = "递归";
            this.folderInclusiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // ignoreFolderHiddenCheckBox
            // 
            this.ignoreFolderHiddenCheckBox.AutoSize = true;
            this.ignoreFolderHiddenCheckBox.Checked = true;
            this.ignoreFolderHiddenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreFolderHiddenCheckBox.Location = new System.Drawing.Point(296, 103);
            this.ignoreFolderHiddenCheckBox.Name = "ignoreFolderHiddenCheckBox";
            this.ignoreFolderHiddenCheckBox.Size = new System.Drawing.Size(72, 16);
            this.ignoreFolderHiddenCheckBox.TabIndex = 6;
            this.ignoreFolderHiddenCheckBox.Text = "搜索隐藏";
            this.ignoreFolderHiddenCheckBox.UseVisualStyleBackColor = true;
            // 
            // ignoreFileHiddenCheckBox
            // 
            this.ignoreFileHiddenCheckBox.AutoSize = true;
            this.ignoreFileHiddenCheckBox.Checked = true;
            this.ignoreFileHiddenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreFileHiddenCheckBox.Location = new System.Drawing.Point(296, 63);
            this.ignoreFileHiddenCheckBox.Name = "ignoreFileHiddenCheckBox";
            this.ignoreFileHiddenCheckBox.Size = new System.Drawing.Size(72, 16);
            this.ignoreFileHiddenCheckBox.TabIndex = 3;
            this.ignoreFileHiddenCheckBox.Text = "搜索隐藏";
            this.ignoreFileHiddenCheckBox.UseVisualStyleBackColor = true;
            // 
            // selectPathButton
            // 
            this.selectPathButton.Location = new System.Drawing.Point(376, 19);
            this.selectPathButton.Name = "selectPathButton";
            this.selectPathButton.Size = new System.Drawing.Size(41, 23);
            this.selectPathButton.TabIndex = 1;
            this.selectPathButton.Text = "...";
            this.selectPathButton.UseVisualStyleBackColor = true;
            this.selectPathButton.Click += new System.EventHandler(this.selectPathButton_Click);
            // 
            // DeleteFileOrFolderForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 181);
            this.Controls.Add(this.selectPathButton);
            this.Controls.Add(this.ignoreFolderHiddenCheckBox);
            this.Controls.Add(this.ignoreFileHiddenCheckBox);
            this.Controls.Add(this.folderInclusiveCheckBox);
            this.Controls.Add(this.fileInclusiveCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.folderNameTextBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.suffixTextBox);
            this.Controls.Add(this.pathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "DeleteFileOrFolderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "删除文件或文件夹";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.TextBox suffixTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox folderNameTextBox;
        private System.Windows.Forms.CheckBox fileInclusiveCheckBox;
        private System.Windows.Forms.CheckBox folderInclusiveCheckBox;
        private System.Windows.Forms.CheckBox ignoreFolderHiddenCheckBox;
        private System.Windows.Forms.CheckBox ignoreFileHiddenCheckBox;
        private System.Windows.Forms.Button selectPathButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }


}