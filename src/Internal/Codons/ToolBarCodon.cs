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


using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.AddIns.Codons
{
	[CodonTypeAttribute("ToolBar")]
	public class ToolBarCodon : AbstractCodon
	{
		ArrayList subItems = null;
		ConditionCollection conditions;
		bool      enabled  = true;
		
		public override bool HandleConditions 
		{
			get 
			{
				return true;
			}
		}
		public ArrayList SubItems 
		{
			get 
			{
				return subItems;
			}
			set 
			{
				subItems = value;
			}
		}
		
		public bool Enabled 
		{
			get 
			{
				return enabled;
			}
			set 
			{
				enabled = value;
			}
		}
		
		/// <summary>
		/// Creates an item with the specified sub items. And the current
		/// Condition status for this item.
		/// </summary>
		public override object BuildItem(object owner, ArrayList subItems, ConditionCollection conditions)
		{
			this.subItems = subItems;
			enabled       = false; //action != ConditionFailedAction.Disable;
			this.conditions = conditions;
			return this;
		}
	}
}
