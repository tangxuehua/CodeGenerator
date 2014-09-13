
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
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
	public class GetEntityListForm : Form
	{
		private TabControl fileTabControl;
        private ToolBarEx[] toolBars = null;
        private List<Entity> entityList = new List<Entity>();
        private Button cancelButton;
        private Button okButton;
        private static GetEntityListForm instance = null;
        private Point location = Point.Empty;
        private string contextMenuPath = @"/CodeGenerator/GetEntityListForm/TabControl/ContextMenu";


        public event EventHandler OKButtonClicked;

        public static GetEntityListForm Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GetEntityListForm();
                }
                return instance;
            }
        }

        public List<Entity> EntityList
        {
            get
            {
                return this.entityList;
            }
            set
            {
                this.entityList = value;
            }
        }
        public TabControl FileTabControl
        {
            get
            {
                return this.fileTabControl;
            }
        }

        private void InitializeComponent()
        {
            this.fileTabControl = new System.Windows.Forms.TabControl();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileTabControl
            // 
            this.fileTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTabControl.Location = new System.Drawing.Point(0, 0);
            this.fileTabControl.Name = "fileTabControl";
            this.fileTabControl.SelectedIndex = 0;
            this.fileTabControl.ShowToolTips = true;
            this.fileTabControl.Size = new System.Drawing.Size(812, 564);
            this.fileTabControl.TabIndex = 9;
            this.fileTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShowContextMenu);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(437, 578);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 103;
            this.cancelButton.Text = "取 消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(297, 578);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 102;
            this.okButton.Text = "确 定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // GetEntityListForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(812, 614);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fileTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GetEntityListForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取Entity信息";
            this.VisibleChanged += new System.EventHandler(this.GetEntityOrCategoryListForm_VisibleChanged);
            this.ResumeLayout(false);

        }
        private void InitializeWorkspace()
        {
            CreateToolBars(this, null);
        }
        private void CreateToolBars(object sender, EventArgs e)
        {
            if (this.toolBars == null)
            {
                GetEntityListFormToolBarService toolBarService = (GetEntityListFormToolBarService)ServiceManager.Services.GetService(typeof(GetEntityListFormToolBarService));
                ToolBarEx[] toolBars = toolBarService.CreateToolbars();
                this.toolBars = toolBars;
                this.Controls.AddRange(toolBars);
            }
        }

        private GetEntityListForm()
		{
			InitializeComponent();
            InitializeWorkspace();
		}

        public void CreateEntity()
        {
            TabPage page = new TabPage("Entity" + (this.fileTabControl.TabPages.Count + 1).ToString());

            GetEntityInfoControl control = new GetEntityInfoControl();
            control.Dock = DockStyle.Fill;
            control.ParentTabControl = this.fileTabControl;

            page.Controls.Add(control);

            this.fileTabControl.TabPages.Add(page);
            this.fileTabControl.SelectedIndex = this.fileTabControl.TabPages.Count - 1;

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.EntityList.Clear();

            foreach (TabPage page in this.fileTabControl.TabPages)
            {
                Entity entity = (page.Controls[0] as GetEntityInfoControl).GetData();
                if (entity == null)
                {
                    return;
                }
                this.EntityList.Add(entity);
            }

            if (this.OKButtonClicked != null)
            {
                this.OKButtonClicked(this, EventArgs.Empty);
            }

        }

        private void GetEntityOrCategoryListForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.fileTabControl.TabPages.Clear();
                this.Location = this.location;
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
	}
}
