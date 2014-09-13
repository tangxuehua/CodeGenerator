using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;



namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public class StartWorkbenchCommand : AbstractMenuCommand
	{
		public override void Run()
		{
		    Application.Run(WorkbenchSingleton.Workbench);
		}
	}
}
