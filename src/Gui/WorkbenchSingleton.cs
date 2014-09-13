using System;
using System.CodeDom.Compiler;
using System.Windows.Forms;


namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
	public class WorkbenchSingleton
	{
		
		static DefaultWorkbench workbench    = null;

		static void CreateWorkspace()
		{
			DefaultWorkbench w = new DefaultWorkbench();
			workbench = w;				
			w.InitializeWorkspace();//初始化菜单,工具栏,状态栏之类的东西.

		}
		

		public static DefaultWorkbench Workbench 
		{
			get {
				if (workbench == null) {  //惰性初始化
					CreateWorkspace();
				}
				return workbench;
			}
		}
		
	}
}
