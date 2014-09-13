using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NetFocus.UtilityTool.CodeGenerator.Commands.Components;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
    public partial class DeleteFileOrFolderForm : Form
    {
        public DeleteFileOrFolderForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            //初始化数据
            string path = this.pathTextBox.Text.Trim();
            string suffixList = this.suffixTextBox.Text.Trim();
            string folderNames = this.folderNameTextBox.Text.Trim();

            //数据校验
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请输入完整的路径信息！", "提示");
                this.pathTextBox.Focus();
                return;
            }

            if (!Directory.Exists(path))
            {
                MessageBox.Show("您输入的路径不存在！", "提示");
                this.pathTextBox.Focus();
                this.pathTextBox.SelectAll();
                return;
            }

            //删除文件夹    
            if (folderNames.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
            {
                int deletedFolderCount = 0;
                try
                {
                    DeleteFolders(path, folderNames.Split(new char[] { ';' }), this.folderInclusiveCheckBox.Checked, !this.ignoreFolderHiddenCheckBox.Checked, ref deletedFolderCount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show(string.Format("文件夹删除成功，共删除了{0}个文件夹！", deletedFolderCount.ToString()), "提示");
            }
            //删除文件
            if (suffixList.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length > 0)
            {
                int deletedFileCount = 0;
                try
                {
                    DeleteFiles(path, suffixList.Split(new char[] { ';' }), this.fileInclusiveCheckBox.Checked, !this.ignoreFileHiddenCheckBox.Checked, ref deletedFileCount);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show(string.Format("文件删除成功，共删除了{0}个文件！", deletedFileCount.ToString()), "提示");
            }

        }

        /// <summary>
        /// 删除指定目录下的指定后缀名的文件
        /// </summary>
        /// <param name="directory">要删除的文件所在的目录，是绝对目录，如d:\temp</param>
        /// <param name="masks">要删除的文件的后缀名的一个数组，比如masks中包含了.cs,.vb,.c这三个元素</param>
        /// <param name="searchSubdirectories">表示是否需要递归删除，即是否也要删除子目录中相应的文件</param>
        /// <param name="ignoreHidden">表示是否忽略隐藏文件</param>
        /// /// <param name="deletedFileCount">表示总共删除的文件数</param>
        public void DeleteFiles(string directory, string[] masks, bool searchSubdirectories, bool ignoreHidden, ref int deletedFileCount)
        {
            //先删除当前目录下指定后缀名的所有文件
            foreach (string file in Directory.GetFiles(directory, "*.*"))
            {
                if (!(ignoreHidden && (File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden))
                {
                    foreach (string mask in masks)
                    {
                        if (Path.GetExtension(file) == mask)
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                            deletedFileCount++;
                        }
                    }
                }
            }

            //如果需要对子目录进行处理，则对子目录也进行递归操作
            if (searchSubdirectories)
            {
                string[] childDirectories = Directory.GetDirectories(directory);
                foreach (string dir in childDirectories)
                {
                    if (!(ignoreHidden && (File.GetAttributes(dir) & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        DeleteFiles(dir, masks, searchSubdirectories, ignoreHidden, ref deletedFileCount);
                    }
                }
            }
        }

        /// <summary>
        /// 删除指定目录下的指定名称的文件夹
        /// </summary>
        /// <param name="directory">要删除的文件夹所在的目录，是绝对目录，如d:\temp</param>
        /// <param name="masks">要删除的文件夹名的一个数组，比如bin,obj</param>
        /// <param name="searchSubdirectories">表示是否需要递归删除，即是否也要删除子目录中相应的文件夹</param>
        /// <param name="ignoreHidden">表示是否忽略隐藏文件夹</param>
        /// /// <param name="deletedFileCount">表示总共删除的文件夹数</param>
        public void DeleteFolders(string directory, string[] folderNames, bool searchSubdirectories, bool ignoreHidden, ref int deletedFolderCount)
        {
            foreach (string folderName in folderNames)
            {
                foreach (string path in Directory.GetDirectories(directory, folderName, searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                {
                    if (!(ignoreHidden && (File.GetAttributes(path) & FileAttributes.Hidden) == FileAttributes.Hidden))
                    {
                        Directory.Delete(path, true);
                        deletedFolderCount++;
                    }
                }
            }
        }

        private void selectPathButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pathTextBox.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

    }


}