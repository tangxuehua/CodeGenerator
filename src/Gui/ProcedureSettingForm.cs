using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	/// <summary>
	/// ProcedureSettingForm 的摘要说明。
	/// </summary>
	public class ProcedureSettingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox prefixTextBox;
		private System.Windows.Forms.TextBox selectListTextBox;
		private System.Windows.Forms.TextBox selectByKeyTextBox;
		private System.Windows.Forms.TextBox updateTextBox;
		private System.Windows.Forms.TextBox insertTextBox;
		private System.Windows.Forms.TextBox deleteTextBox;


		private string procedurePrefix = "Sp_";
		private string uniqueSelectProcedureSuffix = "_Get";
		private string collectionSelectProcedureSuffix = "_GetCollection";
		private string updateProcedureSuffix = "_Update";
		private string deleteProcedureSuffix = "_Delete";
		private string insertProcedureSuffix = "_Insert";

		public string ProcedurePrefix
		{
			get
			{
				return procedurePrefix;
			}
			set
			{
				procedurePrefix = value;
			}
		}
		public string UniqueSelectProcedureSuffix
		{
			get
			{
				return uniqueSelectProcedureSuffix;
			}
			set
			{
				uniqueSelectProcedureSuffix = value;
			}
		}
		public string CollectionSelectProcedureSuffix
		{
			get
			{
				return collectionSelectProcedureSuffix;
			}
			set
			{
				collectionSelectProcedureSuffix = value;
			}
		}
		public string UpdateProcedureSuffix
		{
			get
			{
				return updateProcedureSuffix;
			}
			set
			{
				updateProcedureSuffix = value;
			}
		}
		public string DeleteProcedureSuffix
		{
			get
			{
				return deleteProcedureSuffix;
			}
			set
			{
				deleteProcedureSuffix = value;
			}
		}
		public string InsertProcedureSuffix
		{
			get
			{
				return insertProcedureSuffix;
			}
			set
			{
				insertProcedureSuffix = value;
			}
		}

		public ProcedureSettingForm()
		{
			InitializeComponent();
		}


		#region Windows 窗体设计器生成的代码

		private void InitializeComponent()
		{
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.prefixTextBox = new System.Windows.Forms.TextBox();
			this.selectListTextBox = new System.Windows.Forms.TextBox();
			this.selectByKeyTextBox = new System.Windows.Forms.TextBox();
			this.updateTextBox = new System.Windows.Forms.TextBox();
			this.insertTextBox = new System.Windows.Forms.TextBox();
			this.deleteTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(232, 224);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(56, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "取 消";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(120, 224);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(56, 23);
			this.okButton.TabIndex = 4;
			this.okButton.Text = "确 定";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(40, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "存储过程前缀";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(40, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "SELECTLIST存储过程后缀";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(40, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 23);
			this.label3.TabIndex = 9;
			this.label3.Text = "UPDATE存储过程后缀";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(40, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "SELECTBYKEY存储过程后缀";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(40, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(152, 23);
			this.label6.TabIndex = 10;
			this.label6.Text = "INSERT存储过程后缀";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(40, 184);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(152, 23);
			this.label5.TabIndex = 11;
			this.label5.Text = "DELETE存储过程后缀";
			// 
			// prefixTextBox
			// 
			this.prefixTextBox.Location = new System.Drawing.Point(208, 16);
			this.prefixTextBox.Name = "prefixTextBox";
			this.prefixTextBox.Size = new System.Drawing.Size(176, 21);
			this.prefixTextBox.TabIndex = 12;
			this.prefixTextBox.Text = "";
			// 
			// selectListTextBox
			// 
			this.selectListTextBox.Location = new System.Drawing.Point(208, 48);
			this.selectListTextBox.Name = "selectListTextBox";
			this.selectListTextBox.Size = new System.Drawing.Size(176, 21);
			this.selectListTextBox.TabIndex = 13;
			this.selectListTextBox.Text = "";
			// 
			// selectByKeyTextBox
			// 
			this.selectByKeyTextBox.Location = new System.Drawing.Point(208, 80);
			this.selectByKeyTextBox.Name = "selectByKeyTextBox";
			this.selectByKeyTextBox.Size = new System.Drawing.Size(176, 21);
			this.selectByKeyTextBox.TabIndex = 14;
			this.selectByKeyTextBox.Text = "";
			// 
			// updateTextBox
			// 
			this.updateTextBox.Location = new System.Drawing.Point(208, 112);
			this.updateTextBox.Name = "updateTextBox";
			this.updateTextBox.Size = new System.Drawing.Size(176, 21);
			this.updateTextBox.TabIndex = 15;
			this.updateTextBox.Text = "";
			// 
			// insertTextBox
			// 
			this.insertTextBox.Location = new System.Drawing.Point(208, 144);
			this.insertTextBox.Name = "insertTextBox";
			this.insertTextBox.Size = new System.Drawing.Size(176, 21);
			this.insertTextBox.TabIndex = 16;
			this.insertTextBox.Text = "";
			// 
			// deleteTextBox
			// 
			this.deleteTextBox.Location = new System.Drawing.Point(208, 176);
			this.deleteTextBox.Name = "deleteTextBox";
			this.deleteTextBox.Size = new System.Drawing.Size(176, 21);
			this.deleteTextBox.TabIndex = 17;
			this.deleteTextBox.Text = "";
			// 
			// ProcedureSettingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(424, 266);
			this.Controls.Add(this.deleteTextBox);
			this.Controls.Add(this.insertTextBox);
			this.Controls.Add(this.updateTextBox);
			this.Controls.Add(this.selectByKeyTextBox);
			this.Controls.Add(this.selectListTextBox);
			this.Controls.Add(this.prefixTextBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ProcedureSettingForm";
			this.Text = "设置存储过程的前缀和后缀";
			this.Load += new System.EventHandler(this.ProcedureSettingForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public ProcedureSettingForm(string procedurePrefix,string uniqueSelectProcedureSuffix,string collectionSelectProcedureSuffix,string updateProcedureSuffix,string deleteProcedureSuffix,string insertProcedureSuffix)
		{
			this.procedurePrefix = procedurePrefix;
			this.uniqueSelectProcedureSuffix = uniqueSelectProcedureSuffix;
			this.collectionSelectProcedureSuffix = collectionSelectProcedureSuffix;
			this.updateProcedureSuffix = updateProcedureSuffix;
			this.deleteProcedureSuffix = deleteProcedureSuffix;
			this.insertProcedureSuffix = insertProcedureSuffix;

		}

		
		private void ProcedureSettingForm_Load(object sender, System.EventArgs e)
		{
			this.prefixTextBox.Text = procedurePrefix;
			this.selectByKeyTextBox.Text = uniqueSelectProcedureSuffix;
			this.selectListTextBox.Text = collectionSelectProcedureSuffix;
			this.updateTextBox.Text = updateProcedureSuffix;
			this.deleteTextBox.Text = deleteProcedureSuffix;
			this.insertTextBox.Text = insertProcedureSuffix;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			procedurePrefix = this.prefixTextBox.Text;
			uniqueSelectProcedureSuffix = this.selectByKeyTextBox.Text;
			collectionSelectProcedureSuffix = this.selectListTextBox.Text;
			updateProcedureSuffix = this.updateTextBox.Text;
			deleteProcedureSuffix = this.deleteTextBox.Text;
			insertProcedureSuffix = this.insertTextBox.Text;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

	}
}
