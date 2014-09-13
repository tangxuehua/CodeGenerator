using System;

using NetFocus.Components.AddIns;
using NetFocus.Components.AddIns.Exceptions;
using NetFocus.Components.GuiInterface.Services;
using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.UtilityTool.CodeGenerator.AddIns.Codons;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;
using NetFocus.UtilityTool.CodeGenerator.Commands;

namespace NetFocus.UtilityTool.CodeGenerator.Services
{
	public class GetEntityListFormToolBarService : AbstractService
	{
        readonly static string toolBarPath = "/CodeGenerator/Workbench/GetEntityListForm/ToolBars";
		
		IAddInTreeNode node;

        public GetEntityListFormToolBarService()
		{
			try 
			{
				this.node = AddInTreeSingleton.AddInTree.GetTreeNode(toolBarPath);
			} 
			catch (TreePathNotFoundException) 
			{
				this.node = null;
			}
		}
		
		public ToolBarEx[] CreateToolbars()
		{
			if (node == null) 
			{
				return new ToolBarEx[] {};
			}
			ToolBarCodon[] codons = (ToolBarCodon[])(node.BuildChildItems(this)).ToArray(typeof(ToolBarCodon));
			
			ToolBarEx[] toolBars = new ToolBarEx[codons.Length];
			
			for (int i = 0; i < codons.Length; ++i) 
			{
				toolBars[i] = CreateToolBarFromCodon(WorkbenchSingleton.Workbench, codons[i]);
			}

			return toolBars;
		}
		
		public ToolBarEx CreateToolBarFromCodon(object owner, ToolBarCodon codon)
		{
			ToolBarEx bar = new ToolBarEx(BarType.ToolBar);
			
			foreach (ToolBarItemCodon childCodon in codon.SubItems) 
			{
				ToolBarItem item = new ToolBarItem();
				item.Text = childCodon.ToolTip;
				if(childCodon.Class != null) 
				{
					object o = childCodon.AddIn.CreateObject(childCodon.Class);
					if(o is IMenuCommand)
					{
						item.Click +=new EventHandler(new ToolBarItemWrapper((IMenuCommand)o).Execute);
					}
					bar.Items.Add(item);
				}
			}
			return bar;
		}

		public class ToolBarItemWrapper
		{
			IMenuCommand menuCommand = null;

			public ToolBarItemWrapper(IMenuCommand menuCommand)
			{
				this.menuCommand = menuCommand;
			}
			public void Execute(object sender,EventArgs e)
			{
				menuCommand.Run();
			}
		}
		
	}
}
