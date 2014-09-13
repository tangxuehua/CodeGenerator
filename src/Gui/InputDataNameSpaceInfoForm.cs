using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class InputDataNameSpaceInfoForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label namespaceLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox namespaceTextBox;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public InputDataNameSpaceInfoForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDataNameSpaceInfoForm));
            this.namespaceLabel = new System.Windows.Forms.Label();
            this.namespaceTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // namespaceLabel
            // 
            this.namespaceLabel.Location = new System.Drawing.Point(32, 24);
            this.namespaceLabel.Name = "namespaceLabel";
            this.namespaceLabel.Size = new System.Drawing.Size(64, 23);
            this.namespaceLabel.TabIndex = 0;
            this.namespaceLabel.Text = "命名空间";
            this.namespaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // namespaceTextBox
            // 
            this.namespaceTextBox.Location = new System.Drawing.Point(112, 24);
            this.namespaceTextBox.Name = "namespaceTextBox";
            this.namespaceTextBox.Size = new System.Drawing.Size(248, 21);
            this.namespaceTextBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(120, 72);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(56, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "确 定";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(224, 72);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(56, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "取 消";
            this.cancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // InputClassInfoForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(390, 116);
            this.Controls.Add(this.namespaceTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.namespaceLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InputClassInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输入数据层的命名空间";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void okButton_Click(object sender, System.EventArgs e)
		{
			if(this.namespaceTextBox.Text.Trim().Length == 0)
			{
				MessageBox.Show("命名空间不能为空!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}

			this.Text = this.namespaceTextBox.Text.Trim();

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		
		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


	}
}
