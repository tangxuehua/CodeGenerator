
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
	public class UndoCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.Undo();
			}
		}
	}
	public class RedoCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.Redo();
			}
		}
	}
	public class CutCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null,null);
			}
		}
	}
	public class CopyCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null,null);
			}
		}
	}
	public class PasteCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null,null);
				textEditor.ActiveTextAreaControl.TextArea.Refresh();
			}
		}
	}
	public class DeleteCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(null,null);
			}
		}
	}
	public class SelectAllCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;
			if(page != null)
			{
				TextEditorControl textEditor = page.Controls[0] as TextEditorControl;
				textEditor.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(null,null);
			}
		}
	}



}
