
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;
using System.Configuration;

using NetFocus.Components.TextEditor.Document;
using NetFocus.Components.TextEditor;
using NetFocus.UtilityTool.CodeGenerator.AddIns.Codons;
using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Services;
using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Services;
using NetFocus.UtilityTool.CodeGenerator.Commands.Components;


namespace NetFocus.UtilityTool.CodeGenerator.Gui
{

	/// <summary>
	/// A test workbench only with menu items
	/// </summary>
	public class DefaultWorkbench : Form
	{
		#region system auto generated codes

		private System.Windows.Forms.Splitter verticalSplitter;
		private System.Windows.Forms.Panel leftPanel;
		private System.Windows.Forms.Panel bottomPanel;
		public System.Windows.Forms.DataGrid SelectedProcedureDataGrid;
		private System.Windows.Forms.Splitter bottomSplitter;
		private System.Windows.Forms.DataGrid sourceProcedureDataGrid;
		private System.Windows.Forms.Splitter topSplitter;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.DataGrid tableDataGrid;
		private System.Windows.Forms.TabControl fileTabControl;

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefaultWorkbench));
            this.verticalSplitter = new System.Windows.Forms.Splitter();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.SelectedProcedureDataGrid = new System.Windows.Forms.DataGrid();
            this.bottomSplitter = new System.Windows.Forms.Splitter();
            this.sourceProcedureDataGrid = new System.Windows.Forms.DataGrid();
            this.topSplitter = new System.Windows.Forms.Splitter();
            this.topPanel = new System.Windows.Forms.Panel();
            this.tableDataGrid = new System.Windows.Forms.DataGrid();
            this.fileTabControl = new System.Windows.Forms.TabControl();
            this.leftPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedProcedureDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceProcedureDataGrid)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // verticalSplitter
            // 
            this.verticalSplitter.BackColor = System.Drawing.SystemColors.Control;
            this.verticalSplitter.Location = new System.Drawing.Point(270, 0);
            this.verticalSplitter.Name = "verticalSplitter";
            this.verticalSplitter.Size = new System.Drawing.Size(5, 502);
            this.verticalSplitter.TabIndex = 8;
            this.verticalSplitter.TabStop = false;
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.bottomPanel);
            this.leftPanel.Controls.Add(this.topSplitter);
            this.leftPanel.Controls.Add(this.topPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(270, 502);
            this.leftPanel.TabIndex = 7;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.SelectedProcedureDataGrid);
            this.bottomPanel.Controls.Add(this.bottomSplitter);
            this.bottomPanel.Controls.Add(this.sourceProcedureDataGrid);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 285);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(270, 217);
            this.bottomPanel.TabIndex = 6;
            // 
            // SelectedProcedureDataGrid
            // 
            this.SelectedProcedureDataGrid.AlternatingBackColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.BackColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelectedProcedureDataGrid.CaptionBackColor = System.Drawing.SystemColors.Control;
            this.SelectedProcedureDataGrid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.SelectedProcedureDataGrid.CaptionForeColor = System.Drawing.Color.Black;
            this.SelectedProcedureDataGrid.CaptionText = "选中的存储过程";
            this.SelectedProcedureDataGrid.ColumnHeadersVisible = false;
            this.SelectedProcedureDataGrid.DataMember = "";
            this.SelectedProcedureDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedProcedureDataGrid.FlatMode = true;
            this.SelectedProcedureDataGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this.SelectedProcedureDataGrid.ForeColor = System.Drawing.Color.Black;
            this.SelectedProcedureDataGrid.GridLineColor = System.Drawing.Color.Silver;
            this.SelectedProcedureDataGrid.HeaderBackColor = System.Drawing.Color.Black;
            this.SelectedProcedureDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8F);
            this.SelectedProcedureDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.LinkColor = System.Drawing.Color.Purple;
            this.SelectedProcedureDataGrid.Location = new System.Drawing.Point(0, 237);
            this.SelectedProcedureDataGrid.Name = "SelectedProcedureDataGrid";
            this.SelectedProcedureDataGrid.ParentRowsBackColor = System.Drawing.Color.Gray;
            this.SelectedProcedureDataGrid.ParentRowsForeColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.SelectionBackColor = System.Drawing.Color.Maroon;
            this.SelectedProcedureDataGrid.SelectionForeColor = System.Drawing.Color.White;
            this.SelectedProcedureDataGrid.Size = new System.Drawing.Size(270, 0);
            this.SelectedProcedureDataGrid.TabIndex = 6;
            this.SelectedProcedureDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.SelectedProcedureDataGrid_Paint);
            // 
            // bottomSplitter
            // 
            this.bottomSplitter.BackColor = System.Drawing.SystemColors.Control;
            this.bottomSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.bottomSplitter.Location = new System.Drawing.Point(0, 232);
            this.bottomSplitter.Name = "bottomSplitter";
            this.bottomSplitter.Size = new System.Drawing.Size(270, 5);
            this.bottomSplitter.TabIndex = 5;
            this.bottomSplitter.TabStop = false;
            // 
            // sourceProcedureDataGrid
            // 
            this.sourceProcedureDataGrid.AlternatingBackColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.BackColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.CaptionBackColor = System.Drawing.SystemColors.Control;
            this.sourceProcedureDataGrid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.sourceProcedureDataGrid.CaptionForeColor = System.Drawing.Color.Black;
            this.sourceProcedureDataGrid.CaptionText = "存储过程";
            this.sourceProcedureDataGrid.ColumnHeadersVisible = false;
            this.sourceProcedureDataGrid.DataMember = "";
            this.sourceProcedureDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.sourceProcedureDataGrid.FlatMode = true;
            this.sourceProcedureDataGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this.sourceProcedureDataGrid.ForeColor = System.Drawing.Color.Black;
            this.sourceProcedureDataGrid.GridLineColor = System.Drawing.Color.Silver;
            this.sourceProcedureDataGrid.HeaderBackColor = System.Drawing.Color.Black;
            this.sourceProcedureDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8F);
            this.sourceProcedureDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.LinkColor = System.Drawing.Color.Purple;
            this.sourceProcedureDataGrid.Location = new System.Drawing.Point(0, 0);
            this.sourceProcedureDataGrid.Name = "sourceProcedureDataGrid";
            this.sourceProcedureDataGrid.ParentRowsBackColor = System.Drawing.Color.Gray;
            this.sourceProcedureDataGrid.ParentRowsForeColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.SelectionBackColor = System.Drawing.Color.Maroon;
            this.sourceProcedureDataGrid.SelectionForeColor = System.Drawing.Color.White;
            this.sourceProcedureDataGrid.Size = new System.Drawing.Size(270, 232);
            this.sourceProcedureDataGrid.TabIndex = 4;
            this.sourceProcedureDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.sourceProcedureDataGrid_Paint);
            this.sourceProcedureDataGrid.Resize += new System.EventHandler(this.sourceProcedureDataGrid_Resize);
            // 
            // topSplitter
            // 
            this.topSplitter.BackColor = System.Drawing.SystemColors.Control;
            this.topSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.topSplitter.Location = new System.Drawing.Point(0, 280);
            this.topSplitter.Name = "topSplitter";
            this.topSplitter.Size = new System.Drawing.Size(270, 5);
            this.topSplitter.TabIndex = 5;
            this.topSplitter.TabStop = false;
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.tableDataGrid);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(270, 280);
            this.topPanel.TabIndex = 4;
            // 
            // tableDataGrid
            // 
            this.tableDataGrid.AlternatingBackColor = System.Drawing.Color.Gainsboro;
            this.tableDataGrid.BackColor = System.Drawing.Color.White;
            this.tableDataGrid.BackgroundColor = System.Drawing.Color.White;
            this.tableDataGrid.CaptionBackColor = System.Drawing.SystemColors.Control;
            this.tableDataGrid.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.tableDataGrid.CaptionForeColor = System.Drawing.Color.Black;
            this.tableDataGrid.CaptionText = "表  格";
            this.tableDataGrid.ColumnHeadersVisible = false;
            this.tableDataGrid.DataMember = "";
            this.tableDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataGrid.FlatMode = true;
            this.tableDataGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this.tableDataGrid.ForeColor = System.Drawing.Color.Black;
            this.tableDataGrid.GridLineColor = System.Drawing.Color.Silver;
            this.tableDataGrid.HeaderBackColor = System.Drawing.Color.Black;
            this.tableDataGrid.HeaderFont = new System.Drawing.Font("Tahoma", 8F);
            this.tableDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.tableDataGrid.LinkColor = System.Drawing.Color.Purple;
            this.tableDataGrid.Location = new System.Drawing.Point(0, 0);
            this.tableDataGrid.Name = "tableDataGrid";
            this.tableDataGrid.ParentRowsBackColor = System.Drawing.Color.Gray;
            this.tableDataGrid.ParentRowsForeColor = System.Drawing.Color.White;
            this.tableDataGrid.SelectionBackColor = System.Drawing.Color.Maroon;
            this.tableDataGrid.SelectionForeColor = System.Drawing.Color.White;
            this.tableDataGrid.Size = new System.Drawing.Size(270, 280);
            this.tableDataGrid.TabIndex = 7;
            this.tableDataGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.tableDataGrid_Paint);
            this.tableDataGrid.Resize += new System.EventHandler(this.tableDataGrid_Resize);
            // 
            // fileTabControl
            // 
            this.fileTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTabControl.Location = new System.Drawing.Point(275, 0);
            this.fileTabControl.Name = "fileTabControl";
            this.fileTabControl.SelectedIndex = 0;
            this.fileTabControl.ShowToolTips = true;
            this.fileTabControl.Size = new System.Drawing.Size(405, 502);
            this.fileTabControl.TabIndex = 9;
            this.fileTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShowContextMenu);
            // 
            // DefaultWorkbench
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(680, 502);
            this.Controls.Add(this.fileTabControl);
            this.Controls.Add(this.verticalSplitter);
            this.Controls.Add(this.leftPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DefaultWorkbench";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.leftPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedProcedureDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceProcedureDataGrid)).EndInit();
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tableDataGrid)).EndInit();
            this.ResumeLayout(false);

		}


		#endregion

		#region private members

		private MenuControl menuControl = new MenuControl();
		private ToolBarEx[] toolBars = null;
		private string mainMenuPath    = @"/CodeGenerator/Workbench/MainMenu";
        private string contextMenuPath = @"/CodeGenerator/Workbench/ContextMenu";
		private string databaseName = "";
		private string serverName = "";
		private string userName = "";
		private string password = "";
		private DataSet ds1 = new DataSet();
		private DataSet ds2 = new DataSet();
		private DataSet ds3 = new DataSet();
		private SqlConnection conn = new SqlConnection();
		private DataTable tempTable = new DataTable();
		private DataTable tempTable1 = new DataTable();
		private string currentProcedureMappingFileName = "";
		private string currentStandardCSharpFileName = "";
		private string currentSQLFileName = "";
        private string defaultcsdataPath = Application.StartupPath + @"\csdata\";
        private string defaultentitytemplatesPath = Application.StartupPath + @"\entitytemplates\";
		private string defaultFilePath = Application.StartupPath + @"\files\";
        private string templateFilePath = Application.StartupPath + @"\templates\";
		private string procedurePrefix = "sp_";
		private string uniqueSelectProcedureSuffix = "_Retrieve";
		private string collectionSelectProcedureSuffix = "_GetList";
		private string updateProcedureSuffix = "_Update";
		private string deleteProcedureSuffix = "_Delete";
		private string insertProcedureSuffix = "_Create";
		private string currentUser = "tangxuehua";
		private string databaseOwner = "dbo";
		private int unCreateProcedures = 0;
        private string procedureCreateLogFileName = "CreateProcedureLog";
        private string deleteFileExtension = string.Empty;
        private string closeFileExtension = string.Empty;

		#endregion

		#region public properties

		public SqlConnection Conn
		{
			get
			{
				return this.conn;
			}
			set
			{
				this.conn = value;
			}
		}
		public DataGrid TableDataGrid
		{
			get
			{
				return this.tableDataGrid;
			}
			set
			{
				this.tableDataGrid = value;
			}
		}
		public DataGrid SourceProcedureDataGrid
		{
			get
			{
				return this.sourceProcedureDataGrid;
			}
			set
			{
				this.sourceProcedureDataGrid = value;
			}
		}
		public TabControl FileTabControl
		{
			get
			{
				return this.fileTabControl;
			}
		}
		public string ServerName
		{
			get
			{
				return serverName;
			}
			set
			{
				serverName = value;
			}
		}
		public string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				userName = value;
			}
		}
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}
		public String DatabaseName
		{
			get
			{
				return databaseName;
			}
			set
			{
				databaseName = value;
			}
		}

		public DataSet DataSet1
		{
			get
			{
				return this.ds1;
			}
			set
			{
				this.ds1 = value;
			}
		}
		public DataSet DataSet2
		{
			get
			{
				return this.ds2;
			}
			set
			{
				this.ds2 = value;
			}
		}
		public DataSet DataSet3
		{
			get
			{
				return this.ds3;
			}
		}

		public DataTable TempTable
		{
			get
			{
				return this.tempTable;
			}
			set
			{
				this.tempTable = value;
			}
		}
		public DataTable TempTable1
		{
			get
			{
				return this.tempTable1;
			}
			set
			{
				this.tempTable1 = value;
			}
		}
        public string DefaultcsdataPath
        {
            get
            {
                return defaultcsdataPath;
            }
            set
            {
                defaultcsdataPath = value;
            }
        }

        public string DefaultEntityTemplatesPath
        {
            get
            {
                return defaultentitytemplatesPath;
            }
            set
            {
                defaultentitytemplatesPath = value;
            }
        }

		public string DefaultFilePath
		{
			get
			{
				return this.defaultFilePath;
			}
			set
			{
				this.defaultFilePath = value;
			}
		}
        public string TemplateFilePath
        {
            get
            {
                return this.templateFilePath;
            }
            set
            {
                this.templateFilePath = value;
            }
        }
		public string CurrentCMPMappingFileName
		{
			get
			{
				return this.currentProcedureMappingFileName;
			}
			set
			{
				this.currentProcedureMappingFileName = value;
			}
		}
		public string CurrentStandardCSharpFileName
		{
			get
			{
				return this.currentStandardCSharpFileName;
			}
			set
			{
				this.currentStandardCSharpFileName = value;
			}
		}
		public string CurrentSQLFileName
		{
			get
			{
				return this.currentSQLFileName;
			}
			set
			{
				this.currentSQLFileName = value;
			}
		}

		public string ProcedurePrefix
		{
			get
			{
				return this.procedurePrefix;
			}
			set
			{
				this.procedurePrefix = value;
			}
		}
		public string UniqueSelectProcedureSuffix
		{
			get
			{
				return this.uniqueSelectProcedureSuffix;
			}
			set
			{
				this.uniqueSelectProcedureSuffix = value;
			}
		}
		public string CollectionSelectProcedureSuffix
		{
			get
			{
				return this.collectionSelectProcedureSuffix;
			}
			set
			{
				this.collectionSelectProcedureSuffix = value;
			}
		}
		public string UpdateProcedureSuffix
		{
			get
			{
				return this.updateProcedureSuffix;
			}
			set
			{
				this.updateProcedureSuffix = value;
			}
		}
		public string DeleteProcedureSuffix
		{
			get
			{
				return this.deleteProcedureSuffix;
			}
			set
			{
				this.deleteProcedureSuffix = value;
			}
		}
		public string InsertProcedureSuffix
		{
			get
			{
				return this.insertProcedureSuffix;
			}
			set
			{
				this.insertProcedureSuffix = value;
			}
		}

		public string CurrentUserName
		{
			get
			{
				return this.currentUser;
			}
			set
			{
				this.currentUser = value;
			}
		}
		public string DatabaseOwner
		{
			get
			{
				return this.databaseOwner;
			}
			set
			{
				this.databaseOwner = value;
			}
		}
		public int UnCreateProcedures
		{
			get
			{
				return this.unCreateProcedures;
			}
			set
			{
				this.unCreateProcedures = value;
			}
		}
        public string ProcedureCreateLogFileName
        {
            get
            {
                return procedureCreateLogFileName;
            }
            set
            {
                procedureCreateLogFileName = value;
            }
        }
        public string DeleteFileExtension
        {
            get
            {
                return deleteFileExtension;
            }
            set
            {
                deleteFileExtension = value;
            }
        }
        public string CloseFileExtension
        {
            get
            {
                return closeFileExtension;
            }
            set
            {
                closeFileExtension = value;
            }
        }

		#endregion

		#region private methods

		private void sourceProcedureDataGrid_Resize(object sender, System.EventArgs e)
		{
			if(this.sourceProcedureDataGrid.TableStyles.Count > 0)
			{
				this.sourceProcedureDataGrid.TableStyles[0].GridColumnStyles[0].Width = this.sourceProcedureDataGrid.Width-45;
			}
		}

		private void tableDataGrid_Resize(object sender, System.EventArgs e)
		{
			if(this.tableDataGrid.TableStyles.Count > 0)
			{
				this.tableDataGrid.TableStyles[0].GridColumnStyles[0].Width = this.tableDataGrid.Width-45;
			}
		}
		
		private void tableDataGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int row =1;  
			int y = 0;
			if(this.ds1 != null && ds1.Tables.Count > 0)
			{
				int count =this.ds1.Tables[0].Rows.Count; 

				while( row <= count)  
				{ 
					//get & draw the header text... 
 
					string text = string.Format("{0}", row);

					y = this.tableDataGrid.GetCellBounds(row - 1, 0).Y + 2;
 
					e.Graphics.DrawString(text,this.tableDataGrid.Font, new SolidBrush(Color.Black),2,y); 

					row ++;  
				}
			}
		}

		private void sourceProcedureDataGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int row =1;  
			int y = 0;
			if(this.ds2 != null && ds2.Tables.Count > 0)
			{
				int count =this.ds2.Tables[0].Rows.Count; 

				while( row <= count)  
				{ 
					//get & draw the header text... 
 
					string text = string.Format("{0}", row);

					y = this.sourceProcedureDataGrid.GetCellBounds(row - 1, 0).Y + 2;
 
					e.Graphics.DrawString(text,this.sourceProcedureDataGrid.Font, new SolidBrush(Color.Black),2,y); 

					row ++;  
				}
			}
		}

		private void SelectedProcedureDataGrid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int row =1;  
			int y = 0;
			if(this.ds3 != null && ds3.Tables.Count > 0)
			{
				int count =this.ds3.Tables[0].Rows.Count; 

				while( row <= count)  
				{ 
					//get & draw the header text... 
 
					string text = string.Format("{0}", row);

					y = this.SelectedProcedureDataGrid.GetCellBounds(row - 1, 0).Y + 2;
 
					e.Graphics.DrawString(text,this.SelectedProcedureDataGrid.Font, new SolidBrush(Color.Black),2,y); 

					row ++;  
				}
			}
		}

		private void CreateMenu(object sender, EventArgs e)
		{
			if(mainMenuPath != null)
			{
				MenuCommand[] items = (MenuCommand[])(AddInTreeSingleton.AddInTree.GetTreeNode(mainMenuPath).BuildChildItems(this)).ToArray(typeof(MenuCommand));
				menuControl.MenuCommands.Clear();
				menuControl.MenuCommands.AddRange(items);
				menuControl.Dock = DockStyle.Top;
				this.Controls.Add(menuControl);
			}
			UpdateMenus();
		}

		private void UpdateMenus()
		{
			// update menu
			foreach (object o in menuControl.MenuCommands) 
			{
				if (o is IStatusUpdate) 
				{
					((IStatusUpdate)o).UpdateStatus();
				}
			}
		}

		private void CreateToolBars(object sender, EventArgs e)
		{
			if (this.toolBars == null) 
			{
				ToolBarService toolBarService = (ToolBarService)ServiceManager.Services.GetService(typeof(ToolBarService));
				ToolBarEx[] toolBars = toolBarService.CreateToolbars();
				this.toolBars = toolBars;
				this.Controls.AddRange(toolBars);
			}
		}

		private void UpdateCurrentFileTitle(object sender, DocumentEventArgs e)
		{
			TabPage currentPage = this.fileTabControl.SelectedTab;
			if(!currentPage.Text.EndsWith("*"))
			{
				currentPage.Text = currentPage.Text + "*";
			}
		}

		private void BindToTableDataGrid()
		{
			if(ds1 != null && ds1.Tables.Count > 0)
			{
				this.tableDataGrid.DataSource = null;

				ds1.Tables[0].TableName = "tableNameTable";

				this.tableDataGrid.SetDataBinding(ds1,"tableNameTable");
				
				DataGridTableStyle ts = new DataGridTableStyle();
				ts.MappingName = this.tableDataGrid.DataMember;
				this.tableDataGrid.TableStyles.Clear();
				this.tableDataGrid.TableStyles.Add(ts);
				ts.GridColumnStyles[0].Width = this.tableDataGrid.Width - 45;
				ts.RowHeadersVisible = false;
				ts.RowHeadersVisible = true;
				ts.RowHeaderWidth = 22;
				ts.AlternatingBackColor = Color.Gainsboro;

			}
		}

		private void BindToSourceProcedureDataGrid()
		{
			if(ds2 != null && ds2.Tables.Count > 0)
			{
				ds2.Tables[0].TableName = "procedureNameTable";
				
				this.sourceProcedureDataGrid.DataBindings.Clear();

				this.sourceProcedureDataGrid.SetDataBinding(ds2,"procedureNameTable");
				
				DataGridTableStyle ts = new DataGridTableStyle();
				ts.MappingName = this.sourceProcedureDataGrid.DataMember;
				this.sourceProcedureDataGrid.TableStyles.Clear();
				this.sourceProcedureDataGrid.TableStyles.Add(ts);
				ts.GridColumnStyles[0].Width = this.sourceProcedureDataGrid.Width - 45;
				ts.RowHeadersVisible = false;
				ts.RowHeadersVisible = true;
				ts.RowHeaderWidth = 22;
				ts.AlternatingBackColor = Color.Gainsboro;

			}
		}

		private void BindToSelectedProcedureDataGrid()
		{
			if(ds3 != null && ds3.Tables.Count > 0)
			{
				ds3.Tables[0].TableName = "procedureNameTable";
				
				this.SelectedProcedureDataGrid.DataBindings.Clear();

				this.SelectedProcedureDataGrid.SetDataBinding(ds3,"procedureNameTable");
				
				DataGridTableStyle ts = new DataGridTableStyle();
				ts.MappingName = this.SelectedProcedureDataGrid.DataMember;
				this.SelectedProcedureDataGrid.TableStyles.Clear();
				this.SelectedProcedureDataGrid.TableStyles.Add(ts);
				ts.GridColumnStyles[0].Width = this.SelectedProcedureDataGrid.Width - 45;
				ts.RowHeadersVisible = false;
				ts.RowHeadersVisible = true;
				ts.RowHeaderWidth = 22;
				ts.AlternatingBackColor = Color.Gainsboro;

			}
		}

		private void DeleteAllFiles()
		{
			string[] file1 = Directory.GetFiles(defaultFilePath, "*.xml");
			string[] file2 = Directory.GetFiles(defaultFilePath, "*.cs");
			string[] file3 = Directory.GetFiles(defaultFilePath, "*.sql");
            string[] file4 = Directory.GetFiles(defaultFilePath, "*.log");
			foreach(string fileName in file1)
			{
				if(File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
			foreach(string fileName in file2)
			{
				if(File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
			foreach(string fileName in file3)
			{
				if(File.Exists(fileName))
				{
					File.Delete(fileName);
				}
			}
            foreach (string fileName in file4)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
		}

		private void ShowContextMenu(object sender, MouseEventArgs e)
		{
            if (e.Button == MouseButtons.Right)
            {
                MenuCommand[] items = (MenuCommand[])(AddInTreeSingleton.AddInTree.GetTreeNode(contextMenuPath).BuildChildItems(this)).ToArray(typeof(MenuCommand));
                PopupMenu popupMenu = new PopupMenu();
                popupMenu.MenuCommands.AddRange(items);
                popupMenu.TrackPopup(((Control)sender).PointToScreen(e.Location));
            }
		}

		public void InitializeData()
		{
			//conn.ConnectionString = @"server=tangxuehua\netsdk;database=communityserver;user id=sa;password=";
			conn.ConnectionString = "server=" + serverName + ";database=" + databaseName + ";user Id=" + userName + ";password=" + password;

			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				SqlDataAdapter apt = new SqlDataAdapter(cmd);

				cmd.CommandText = "select name from sysobjects where xtype = 'u' and status >= 0 order by name";
				apt.Fill(ds1);

			
				cmd.CommandText = "select name from sysobjects where xtype = 'p' and status >= 0 order by name";
				apt.Fill(ds2);

				BindToTableDataGrid();
				BindToSourceProcedureDataGrid();

				SyntaxModeProvider syntaxModeProvider = new SyntaxModeProvider(Path.Combine(Application.StartupPath, @"modes"));

				HighlightingManager.Manager.AddSyntaxModeProvider(syntaxModeProvider);

				tempTable.Columns.Add(new DataColumn("procedureName",typeof(string)));
				ds3.Tables.Add(tempTable);

				tempTable1.Columns.Add(new DataColumn("procedureName",typeof(string)));

				DeleteAllFiles();

			}
			catch(Exception ex)
			{
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Application.Exit();
			}

		}

        private void InitializeCSDefaultTargetInfo()
        {
            DefaultTargetInfo.Instance.Siteurls = ConfigurationManager.AppSettings["SiteUrls"] as string;
            DefaultTargetInfo.Instance.Navbar = ConfigurationManager.AppSettings["NavBar"] as string;
            DefaultTargetInfo.Instance.ControlpanelResources = ConfigurationManager.AppSettings["CPReources"] as string;
            DefaultTargetInfo.Instance.EntityType = ConfigurationManager.AppSettings["EntityType"] as string;
            DefaultTargetInfo.Instance.Urls = ConfigurationManager.AppSettings["Urls"] as string;
            DefaultTargetInfo.Instance.Skins = ConfigurationManager.AppSettings["Skins"] as string;
            DefaultTargetInfo.Instance.Pages = ConfigurationManager.AppSettings["Pages"] as string;
            DefaultTargetInfo.Instance.Codefile = ConfigurationManager.AppSettings["Codes"] as string;
            DefaultTargetInfo.Instance.RequestBuilder = ConfigurationManager.AppSettings["RequestBuilder"] as string;
            DefaultTargetInfo.Instance.Entity = ConfigurationManager.AppSettings["Entity"] as string;
            DefaultTargetInfo.Instance.EntityRequest = ConfigurationManager.AppSettings["EntityRequest"] as string;
            DefaultTargetInfo.Instance.BusinessManager = ConfigurationManager.AppSettings["BusinessManager"] as string;
        }

		#endregion

		public DefaultWorkbench()
		{
			InitializeComponent();
            this.Text = "文档生成工具0.1版";
            InitializeCSDefaultTargetInfo();
		}

		public void InitializeWorkspace()
		{
			CreateToolBars(this,null);
			CreateMenu(this,null);
		}

		public void NewFile(string fileName, string language)
		{
			TabPage page = new TabPage(fileName);
			page.Tag = this.defaultFilePath + fileName;
            page.ToolTipText = this.defaultFilePath + fileName;
			TextEditorControl textEditor = new TextEditorControl();
			textEditor.Dock = DockStyle.Fill;

			//目前就这么三种文件支持高亮度显示
			if(language == ".cs")
			{
				language = "C#";
			}
			if(language == ".xml")
			{
				language = "XML";
			}
			if(language == ".sql")
			{
				language = "SQL";
			}
            if (language == ".log")
            {
                language = "Mixed";
            }

			textEditor.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighterByName(language);
            textEditor.ActiveTextAreaControl.TextArea.MouseUp += new MouseEventHandler(ShowContextMenu);
			textEditor.Document.DocumentChanged += new DocumentEventHandler(this.UpdateCurrentFileTitle);

			page.Controls.Add(textEditor);

			this.fileTabControl.TabPages.Add(page);
			this.fileTabControl.SelectedIndex = this.fileTabControl.TabPages.Count - 1;

		}
		public void OpenFile(string fileName)
		{
			//先查看要打开的文件是否已经打开
			foreach(TabPage page1 in this.fileTabControl.TabPages)  
			{
				string file = page1.Tag.ToString().EndsWith("*") ? page1.Tag.ToString().Substring(0,page1.Tag.ToString().Length - 1) : page1.Tag.ToString();
				if(file == fileName)
				{
					MessageBox.Show("该文件已经打开！","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
					return;
				}			
			}
			TabPage page = new TabPage(Path.GetFileName(fileName));
			page.Tag = fileName;
			page.ToolTipText = fileName;
			TextEditorControl textEditor = new TextEditorControl();
			textEditor.LoadFile(fileName,true);
			textEditor.Dock = DockStyle.Fill;
            textEditor.ActiveTextAreaControl.TextArea.MouseUp += new MouseEventHandler(ShowContextMenu);
			textEditor.Document.DocumentChanged += new DocumentEventHandler(this.UpdateCurrentFileTitle);
			page.Controls.Add(textEditor);

			this.fileTabControl.TabPages.Add(page);
			this.fileTabControl.SelectedIndex = this.fileTabControl.TabPages.Count - 1;

		}

		public void BindDataGrids()
		{
			BindToTableDataGrid();
			BindToSourceProcedureDataGrid();
		}

	}
}
