using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;



namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public class AboutCommand : AbstractMenuCommand
	{
		public override void Run()
		{
			AboutForm f = new AboutForm();
			f.Show();
		}
	}



}
