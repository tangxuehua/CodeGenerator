using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;



namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public class MaximizeCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			WorkbenchSingleton.Workbench.WindowState = FormWindowState.Maximized;
		}
	}
	public class NormalCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			WorkbenchSingleton.Workbench.WindowState = FormWindowState.Normal;
		}
	}
	public class MinimizeCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			WorkbenchSingleton.Workbench.WindowState = FormWindowState.Minimized;
		}
	}
}
