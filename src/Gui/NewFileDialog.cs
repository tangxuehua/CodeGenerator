using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text;


namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class NewFileDialog : Form
	{
		Container components = new Container();
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox fileTextBox;
		private System.Windows.Forms.Button cancelbutton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button openbutton;
		
		public NewFileDialog()
		{
			InitializeComponent();
		}
		

		void OpenEvent(object sender, EventArgs e)
		{
			if(fileTextBox.Text.Trim() == string.Empty)
			{
                MessageBox.Show("请输入文件名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if(fileTextBox.Text.Trim().LastIndexOf(".") < 0)
			{
                MessageBox.Show("请输入文件后缀名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if(fileTextBox.Text.Trim().LastIndexOf(".") == fileTextBox.Text.Trim().Length - 1)
			{
                MessageBox.Show("请输入文件后缀名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			this.Tag = fileTextBox.Text.Trim();
			this.Text = fileTextBox.Text.Trim().Substring(fileTextBox.Text.Trim().LastIndexOf("."));

			DialogResult = DialogResult.OK;

			this.Close();

		}
		void CancelEvent(object sender, EventArgs e)
		{
			this.Close();

		}
		

		void InitializeComponent()
		{
			this.cancelbutton = new System.Windows.Forms.Button();
			this.openbutton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.fileTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cancelbutton
			// 
			this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelbutton.Location = new System.Drawing.Point(296, 64);
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.Size = new System.Drawing.Size(72, 25);
			this.cancelbutton.TabIndex = 2;
			this.cancelbutton.Text = "取 消";
			this.cancelbutton.Click += new System.EventHandler(this.CancelEvent);
			// 
			// openbutton
			// 
			this.openbutton.Location = new System.Drawing.Point(192, 64);
			this.openbutton.Name = "openbutton";
			this.openbutton.Size = new System.Drawing.Size(72, 25);
			this.openbutton.TabIndex = 1;
			this.openbutton.Text = "确 定";
			this.openbutton.Click += new System.EventHandler(this.OpenEvent);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "输入文件名";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fileTextBox
			// 
			this.fileTextBox.Location = new System.Drawing.Point(136, 16);
			this.fileTextBox.Name = "fileTextBox";
			this.fileTextBox.Size = new System.Drawing.Size(232, 21);
			this.fileTextBox.TabIndex = 0;
			this.fileTextBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 32);
			this.label2.TabIndex = 4;
			this.label2.Text = "目前c#、sql、xml三种文件支持高亮度显示";
			// 
			// NewFileDialog
			// 
			this.AcceptButton = this.openbutton;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.cancelbutton;
			this.ClientSize = new System.Drawing.Size(378, 112);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.fileTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.openbutton);
			this.Controls.Add(this.cancelbutton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewFileDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新建文件";
			this.Load += new System.EventHandler(this.NewFileDialog_Load);
			this.ResumeLayout(false);

		}

		private void NewFileDialog_Load(object sender, System.EventArgs e)
		{
			this.fileTextBox.Focus();
		}
		


		



	}
}
