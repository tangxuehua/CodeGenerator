
using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;

using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.Components.AddIns.Conditions;
using NetFocus.UtilityTool.CodeGenerator.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.Gui.Components
{
	public class SdMenuCommand : MenuCommand, IStatusUpdate
	{
		object caller;
		ConditionCollection conditionCollection;
		string description   = String.Empty;
		string localizedText = String.Empty;
		IMenuCommand menuCommand = null;
		
		public IMenuCommand Command 
		{
			get 
			{
				return menuCommand;
			}
			set 
			{
				menuCommand = value;
				UpdateStatus();
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
		
		public SdMenuCommand(ConditionCollection conditionCollection, object caller, string label) : base(label)
		{
			this.caller              = caller;
			this.conditionCollection = conditionCollection;
			this.localizedText       = label;
			UpdateStatus();
			
		}
		
		public SdMenuCommand(ConditionCollection conditionCollection, object caller, string label, IMenuCommand menuCommand) : base(label)
		{
			this.caller = caller;
			this.conditionCollection = conditionCollection;
			this.localizedText       = label;
			this.menuCommand = menuCommand;
			UpdateStatus();
		}
		
		public SdMenuCommand(ConditionCollection conditionCollection, object caller, string label, EventHandler handler) : base(label, handler)
		{
			this.caller = caller;
			this.conditionCollection = conditionCollection;
			this.localizedText       = label;
			UpdateStatus();
		}
		
		public SdMenuCommand(object caller, string label, EventHandler handler) : base(label, handler)
		{
			this.caller = caller;
			this.localizedText       = label;
			UpdateStatus();
		}
		
		public override void OnClick(System.EventArgs e)
		{
			base.OnClick(e);
			if (menuCommand != null) 
			{
				menuCommand.Run();
			}
		}
		
		public override bool Visible 
		{
			get 
			{
				bool isVisible = base.Visible;
				if (conditionCollection != null) 
				{
					ConditionFailedAction failedAction = conditionCollection.GetCurrentConditionFailedAction(caller);
					isVisible &= failedAction != ConditionFailedAction.Exclude;
				}
				return isVisible;
			}
			set 
			{
				base.Visible = value;
			}
		}
		
		public override bool Enabled 
		{
			get 
			{
				bool isEnabled = true; //base.Enabled;
				if (conditionCollection != null) 
				{
					ConditionFailedAction failedAction = conditionCollection.GetCurrentConditionFailedAction(caller);
					isEnabled &= failedAction != ConditionFailedAction.Disable;
				}
				if (menuCommand != null) 
				{
					isEnabled &= menuCommand.Enabled;
				}
				return isEnabled;
			}
			set 
			{
				base.Enabled = value;
			}
		}
		
		
		public virtual void UpdateStatus()
		{
			if (conditionCollection != null) 
			{
				ConditionFailedAction failedAction = conditionCollection.GetCurrentConditionFailedAction(caller);
				bool isVisible = failedAction != ConditionFailedAction.Exclude;
				if (base.Visible != isVisible) 
				{
					base.Visible = isVisible;
				}
				bool isEnabled = failedAction != ConditionFailedAction.Disable;
				if (base.Enabled != isEnabled) 
				{
					base.Enabled = isEnabled;
				}
			}
			if (menuCommand != null) 
			{
				bool isEnabled = Enabled & menuCommand.Enabled;
				if (base.Enabled != isEnabled) 
				{
					base.Enabled = isEnabled;
				}
			}
			Text = localizedText;
		}

	}
}
