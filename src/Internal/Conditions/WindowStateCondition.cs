using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

using NetFocus.Components.AddIns.Codons;
using NetFocus.Components.AddIns.Attributes;
using NetFocus.Components.AddIns.Conditions;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;

namespace NetFocus.UtilityTool.CodeGenerator.AddIns.Codons
{
	public enum WindowState 
	{
		Min     = 1,   //0001
		Normal  = 2,   //0010
		Max     = 4    //0100
	}

	[ConditionAttribute()]
	public class WindowStateCondition : AbstractCondition
	{
		[XmlMemberAttribute("windowstate", IsRequired = true)]
		WindowState windowState = WindowState.Normal;

		public override bool IsValid(object owner)
		{
			bool isWindowStateOk = false;

			if ((windowState & WindowState.Min) > 0) 
			{
				isWindowStateOk |= WorkbenchSingleton.Workbench.WindowState == FormWindowState.Minimized;
			} 
			if ((windowState & WindowState.Normal) > 0) 
			{
				isWindowStateOk |= WorkbenchSingleton.Workbench.WindowState == FormWindowState.Normal;
			}
			if ((windowState & WindowState.Max) > 0) 
			{
				isWindowStateOk |= WorkbenchSingleton.Workbench.WindowState == FormWindowState.Maximized;
			}

			return isWindowStateOk;

		}
		
		
	}
}