
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

using NetFocus.Components.TextEditor.Document;
using NetFocus.Components.TextEditor;
using NetFocus.UtilityTool.CodeGenerator.AddIns.Codons;
using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Services;
using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;
using NetFocus.UtilityTool.CodeGenerator.Services;



namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public class SetDefaultFilePathCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			FolderBrowserDialog d = new FolderBrowserDialog();

			if(d.ShowDialog() == DialogResult.OK)
			{
				WorkbenchSingleton.Workbench.DefaultFilePath = d.SelectedPath + @"\";
			}
		}
	}
	public class SetProcedureNameCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			ProcedureSettingForm f = new ProcedureSettingForm();
			f.StartPosition = FormStartPosition.CenterScreen;
			f.ProcedurePrefix = WorkbenchSingleton.Workbench.ProcedurePrefix;
			f.UniqueSelectProcedureSuffix = WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix;
			f.CollectionSelectProcedureSuffix = WorkbenchSingleton.Workbench.CollectionSelectProcedureSuffix;
			f.UpdateProcedureSuffix = WorkbenchSingleton.Workbench.UpdateProcedureSuffix;
			f.DeleteProcedureSuffix = WorkbenchSingleton.Workbench.DeleteProcedureSuffix;
			f.InsertProcedureSuffix = WorkbenchSingleton.Workbench.InsertProcedureSuffix;
			if(f.ShowDialog() == DialogResult.OK)
			{
				WorkbenchSingleton.Workbench.ProcedurePrefix = f.ProcedurePrefix;
				WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix = f.UniqueSelectProcedureSuffix;
				WorkbenchSingleton.Workbench.CollectionSelectProcedureSuffix = f.CollectionSelectProcedureSuffix;
				WorkbenchSingleton.Workbench.UpdateProcedureSuffix = f.UpdateProcedureSuffix;
				WorkbenchSingleton.Workbench.DeleteProcedureSuffix = f.DeleteProcedureSuffix;
				WorkbenchSingleton.Workbench.InsertProcedureSuffix = f.InsertProcedureSuffix;
			}
		}
	}

}
