using System;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using NetFocus.UtilityTool.CodeGenerator.Commands.Components;
using NetFocus.Components.GuiInterface.Commands;
using NetFocus.Components.AddIns.Codons;
using NetFocus.Components.AddIns.Attributes;
using NetFocus.Components.AddIns.Conditions;


using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.UtilityTool.CodeGenerator.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.AddIns.Codons
{
	[CodonTypeAttribute("ToolBarItem")]
	public class ToolBarItemCodon : AbstractCodon
	{
		[XmlMemberAttribute("icon")]
		string icon        = null;
		
		[XmlMemberAttributeAttribute("tooltip")]
		string toolTip     = null;
		
		public override bool HandleConditions 
		{
			get 
			{
				return true;
			}
		}
		
		public string Icon 
		{
			get 
			{
				return icon;
			}
			set 
			{
				icon = value;
			}
		}

		public string ToolTip 
		{
			get 
			{
				return toolTip;
			}
			set 
			{
				toolTip = value;
			}
		}
		
		/// <summary>
		/// Creates an item with the specified sub items. And the current
		/// Condition status for this item.
		/// </summary>
		public override object BuildItem(object owner, ArrayList subItems, ConditionCollection conditions)
		{
			return this;
		}
	}
}
