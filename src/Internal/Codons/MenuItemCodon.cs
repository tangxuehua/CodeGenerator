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
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;
using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.AddIns.Codons
{
	[CodonTypeAttribute("MenuItem")]
	public class MenuItemCodon : AbstractCodon
	{
		[XmlMemberAttribute("label", IsRequired=true)]
		string label       = null;
		
		[XmlMemberAttribute("description")]
		string description = null;
		
		[XmlMemberArrayAttribute("shortcut",Separator=new char[]{ '|'})]
		string[] shortcut    = null;
		
		[XmlMemberAttribute("icon")]
		string icon        = null;
		
		[XmlMemberAttribute("link")]
		string link        = null;
		
		public string Link 
		{
			get 
			{
				return link;
			}
			set 
			{
				link = value;
			}
		}
		
		public override bool HandleConditions 
		{
			get 
			{
				return true;
			}
		}
		
		public string Label 
		{
			get 
			{
				return label;
			}
			set 
			{
				label = value;
			}
		}
		
		public string Description 
		{
			get 
			{
				return description;
			}
			set 
			{
				description = value;
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
		
		public string[] Shortcut 
		{
			get 
			{
				return shortcut;
			}
			set 
			{
				shortcut = value;
			}
		}
		
		/// <summary>
		/// Creates an item with the specified sub items. And the current
		/// Condition status for this item.
		/// </summary>
		public override object BuildItem(object owner, ArrayList subItems, ConditionCollection conditions)
		{
			MenuCommand newItem = null;

				object o = null;
				if (Class != null) 
				{
					o = AddIn.CreateObject(Class);
				}
			if (o != null) 
			{
				if (o is IMenuCommand) 
				{
					IMenuCommand menuCommand = (IMenuCommand)o;
					menuCommand.Owner = owner;

					newItem = new SdMenuCommand(conditions, owner, Label, menuCommand);
			
				} 
			}

			if (newItem == null) 
			{
				MenuCommand menuCommand = new SdMenuCommand(conditions, owner, Label);
				if (subItems != null && subItems.Count > 0) 
				{
					foreach (object item in subItems) 
					{
						if (item != null && item is MenuCommand) 
						{
							menuCommand.MenuCommands.Add((MenuCommand)item);
						}
					}
				}
				newItem = menuCommand;
			}
		
			
			if (Shortcut != null && newItem is SdMenuCommand) 
			{
				try 
				{
					foreach (string key in this.shortcut) 
					{
						((SdMenuCommand)newItem).Shortcut |= (System.Windows.Forms.Shortcut)Enum.Parse(typeof(System.Windows.Forms.Shortcut), key);
					}
				} 
				catch (Exception) 
				{
					((SdMenuCommand)newItem).Shortcut = System.Windows.Forms.Shortcut.None;
				}
			}
			newItem.Enabled = true; //action != ConditionFailedAction.Disable;
			return newItem;
		}
	}
}
