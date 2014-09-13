using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class SqlConnectionForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label serverNameLabel;

        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label databaseLabel;
		private System.Windows.Forms.TextBox serverTextBox;
		private System.Windows.Forms.TextBox databaseTextBox;
		private System.Windows.Forms.Button cancelButton;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SqlConnectionForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SqlConnectionForm));
			this.serverNameLabel = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.userNameLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.userNameTextBox = new System.Windows.Forms.TextBox();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.databaseLabel = new System.Windows.Forms.Label();
			this.serverTextBox = new System.Windows.Forms.TextBox();
			this.databaseTextBox = new System.Windows.Forms.TextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// serverNameLabel
			// 
			this.serverNameLabel.Location = new System.Drawing.Point(24, 24);
			this.serverNameLabel.Name = "serverNameLabel";
			this.serverNameLabel.Size = new System.Drawing.Size(48, 23);
			this.serverNameLabel.TabIndex = 0;
			this.serverNameLabel.Text = "服务器";
			this.serverNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(64, 184);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(48, 23);
			this.OkButton.TabIndex = 8;
			this.OkButton.Text = "确 定";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// userNameLabel
			// 
			this.userNameLabel.Location = new System.Drawing.Point(24, 104);
			this.userNameLabel.Name = "userNameLabel";
			this.userNameLabel.Size = new System.Drawing.Size(48, 23);
			this.userNameLabel.TabIndex = 7;
			this.userNameLabel.Text = "用户名";
			this.userNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// passwordLabel
			// 
			this.passwordLabel.Location = new System.Drawing.Point(24, 144);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(48, 23);
			this.passwordLabel.TabIndex = 8;
			this.passwordLabel.Text = "密码";
			this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// userNameTextBox
			// 
			this.userNameTextBox.Location = new System.Drawing.Point(80, 104);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new System.Drawing.Size(168, 21);
			this.userNameTextBox.TabIndex = 2;
			this.userNameTextBox.Text = "";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(80, 144);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '*';
			this.passwordTextBox.Size = new System.Drawing.Size(168, 21);
			this.passwordTextBox.TabIndex = 3;
			this.passwordTextBox.Text = "";
			// 
			// databaseLabel
			// 
			this.databaseLabel.Location = new System.Drawing.Point(24, 64);
			this.databaseLabel.Name = "databaseLabel";
			this.databaseLabel.Size = new System.Drawing.Size(48, 23);
			this.databaseLabel.TabIndex = 11;
			this.databaseLabel.Text = "数据库";
			this.databaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// serverTextBox
			// 
			this.serverTextBox.Location = new System.Drawing.Point(80, 24);
			this.serverTextBox.Name = "serverTextBox";
			this.serverTextBox.Size = new System.Drawing.Size(168, 21);
			this.serverTextBox.TabIndex = 0;
			this.serverTextBox.Text = "";
			// 
			// databaseTextBox
			// 
			this.databaseTextBox.Location = new System.Drawing.Point(80, 64);
			this.databaseTextBox.Name = "databaseTextBox";
			this.databaseTextBox.Size = new System.Drawing.Size(168, 21);
			this.databaseTextBox.TabIndex = 1;
			this.databaseTextBox.Text = "";
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(160, 184);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(48, 24);
			this.cancelButton.TabIndex = 16;
			this.cancelButton.Text = "取 消";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// SqlConnectionForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(270, 228);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.databaseTextBox);
			this.Controls.Add(this.serverTextBox);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.userNameTextBox);
			this.Controls.Add(this.databaseLabel);
			this.Controls.Add(this.passwordLabel);
			this.Controls.Add(this.userNameLabel);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.serverNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SqlConnectionForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "服务器链接设置";
			this.ResumeLayout(false);

		}
		#endregion

        private void OkButton_Click(object sender, System.EventArgs e)
        {
			if(this.userNameTextBox.Text.Trim().Length == 0)
			{
				MessageBox.Show("用户名不能为空！","异常信息",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
				return;
			}

			SqlConnection conn = new SqlConnection();
			try
			{
				this.Cursor = Cursors.WaitCursor;			

				conn.ConnectionString = "server=" + this.serverTextBox.Text.Trim() + ";database=" + this.databaseTextBox.Text.Trim() + ";user Id=" + this.userNameTextBox.Text.Trim() + ";password=" + this.passwordTextBox.Text.Trim();
				conn.Open();

				WorkbenchSingleton.Workbench.ServerName = this.serverTextBox.Text.Trim();
				WorkbenchSingleton.Workbench.DatabaseName = this.databaseTextBox.Text.Trim();
				WorkbenchSingleton.Workbench.UserName = this.userNameTextBox.Text.Trim();
				WorkbenchSingleton.Workbench.Password = this.passwordTextBox.Text.Trim();

				this.DialogResult = DialogResult.OK;

				this.Close();

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message,"提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}
			finally
			{
				this.Cursor = Cursors.Default;
				conn.Close();
				conn.Dispose();
			}

        }

		
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
