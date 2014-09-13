using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NetFocus.UtilityTool.CodeGenerator.Commands.Components;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
    public partial class GetEntityListTargetInfoForm : Form
    {
        EntityTargetInfo postOrCategoryTargetInfo = null;

        public GetEntityListTargetInfoForm()
        {
            InitializeComponent();
        }

        public EntityTargetInfo EntityTargetInfo
        {
            get
            {
                return postOrCategoryTargetInfo;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            postOrCategoryTargetInfo = new EntityTargetInfo();

            postOrCategoryTargetInfo.Codefile = this.codefileTextBox.Text.Trim();
            postOrCategoryTargetInfo.ControlpanelResources = this.controlpanelResourcesTextBox.Text.Trim();
            postOrCategoryTargetInfo.Navbar = this.navbarTextBox.Text.Trim();
            postOrCategoryTargetInfo.Pages = this.pagesTextBox.Text.Trim();
            postOrCategoryTargetInfo.EntityType = this.postTypeTextBox.Text.Trim();
            postOrCategoryTargetInfo.Siteurls = this.siteurlsTextBox.Text.Trim();
            postOrCategoryTargetInfo.Skins = this.skinsTextBox.Text.Trim();
            postOrCategoryTargetInfo.Urls = this.urlsTextBox.Text.Trim();
            postOrCategoryTargetInfo.EntityRequest = this.entityRequestTextBox.Text.Trim();
            postOrCategoryTargetInfo.BusinessManager = this.businessManagerTextBox.Text.Trim();
            postOrCategoryTargetInfo.Entity = this.entityTextBox.Text.Trim();
            postOrCategoryTargetInfo.RequestBuilder = this.requestBuilderTextBox.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void siteurlsButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.siteurlsTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void navbarButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.navbarTextBox.Text = this.openFileDialog1.FileName;
            }    
        }

        private void controlpanelResourcesButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.controlpanelResourcesTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void postTypeButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.postTypeTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void urlsButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.urlsTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void skinsButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.skinsTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void codeFileButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.codefileTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void pagesButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pagesTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void GetTargetInfoForm_Load(object sender, EventArgs e)
        {
            this.codefileTextBox.Text = DefaultTargetInfo.Instance.Codefile;
            this.controlpanelResourcesTextBox.Text = DefaultTargetInfo.Instance.ControlpanelResources;
            this.navbarTextBox.Text = DefaultTargetInfo.Instance.Navbar;
            this.pagesTextBox.Text = DefaultTargetInfo.Instance.Pages;
            this.postTypeTextBox.Text = DefaultTargetInfo.Instance.EntityType;
            this.siteurlsTextBox.Text = DefaultTargetInfo.Instance.Siteurls;
            this.skinsTextBox.Text = DefaultTargetInfo.Instance.Skins;
            this.urlsTextBox.Text = DefaultTargetInfo.Instance.Urls;
            this.businessManagerTextBox.Text = DefaultTargetInfo.Instance.BusinessManager;
            this.requestBuilderTextBox.Text = DefaultTargetInfo.Instance.RequestBuilder;
            this.entityRequestTextBox.Text = DefaultTargetInfo.Instance.EntityRequest;
            this.entityTextBox.Text = DefaultTargetInfo.Instance.Entity;
        }

        private void requestBuilderButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.requestBuilderTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void businessManagerButton_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.businessManagerTextBox.Text = this.openFileDialog1.FileName;
            }
        }

        private void entityRequestButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.entityRequestTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void entityButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.entityTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

    }
}