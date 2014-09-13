using System;

using NetFocus.Components.GuiInterface.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public abstract class AbstractMenuCommand : AbstractCommand, IMenuCommand
	{
		bool isEnabled = true;
		
		public virtual bool Enabled 
		{
			get 
			{
				return isEnabled;
			}
			set 
			{
				isEnabled = value;
			}
		}
	}
}
