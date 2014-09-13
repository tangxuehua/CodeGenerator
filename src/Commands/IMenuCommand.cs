using System;
using NetFocus.Components.GuiInterface.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
	public interface IMenuCommand : ICommand
	{
		bool Enabled 
		{
			get;
			set;
		}
	}
}
