
using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Commands;

using NetFocus.UtilityTool.CodeGenerator.Services;


namespace NetFocus.UtilityTool.CodeGenerator
{
    public class MainClass
    {

        [STAThread()]
        public static void Main(string[] args)
        {
            bool ignoreDefaultPath = false;
            string[] addInDirs = AddInSettingsHandler.GetAddInDirectories(out ignoreDefaultPath);
            AddInTreeSingleton.SetAddInDirectories(addInDirs);

            ArrayList commands = null;
            try
            {
                ServiceManager.Services.InitializeServicesSubsystem(@"/CodeGenerator/Services");

                commands = AddInTreeSingleton.AddInTree.GetTreeNode(@"/CodeGenerator/Autostart").BuildChildItems(null);
                for (int i = 0; i < commands.Count - 1; ++i)
                {
                    ((ICommand)commands[i]).Run();
                }
            }
            catch (XmlException e)
            {
                MessageBox.Show("Could not load XML :" + Environment.NewLine + e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                if (commands.Count > 0)
                {
                    ((ICommand)commands[commands.Count - 1]).Run();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

    }
}


