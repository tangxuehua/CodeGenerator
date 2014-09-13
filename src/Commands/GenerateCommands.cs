
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;

using NetFocus.Components.TextEditor.Document;
using NetFocus.Components.TextEditor;
using NetFocus.UtilityTool.CodeGenerator.AddIns.Codons;
using NetFocus.Components.AddIns;
using NetFocus.Components.GuiInterface.Services;
using NetFocus.Components.UtilityLibrary.CommandBars;
using NetFocus.Components.UtilityLibrary.Menus;
using NetFocus.UtilityTool.CodeGenerator.Gui.Components;
using NetFocus.UtilityTool.CodeGenerator.Gui;
using NetFocus.UtilityTool.CodeGenerator.Services;


using NetFocus.UtilityTool.CodeGenerator.Commands.Components;
using System.Reflection;
using NetFocus.Web.Core;

namespace NetFocus.UtilityTool.CodeGenerator.Commands
{
    #region Commands

    public class SetDataBaseConnectionCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            SqlConnectionForm connectionForm = new SqlConnectionForm();

            if (connectionForm.ShowDialog() == DialogResult.OK)
            {
                WorkbenchSingleton.Workbench.InitializeData();
            }
        }
    }

    public class GeneratePublicPropertyCommand : AbstractMenuCommand
    {
        private void GeneratePublicProperty(StringBuilder sb, string privateMemberInfo)
        {
            string[] s = privateMemberInfo.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length <= 2)
            {
                return;
            }
            string type = s[1];
            string fieldName = s[2];

            sb.Append("public " + type + " " + fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1));
            sb.Append(Environment.NewLine);
            sb.Append("{");
            sb.Append(Environment.NewLine);
            sb.Append("    get");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("        return " + fieldName + ";");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("    set");
            sb.Append(Environment.NewLine);
            sb.Append("    {");
            sb.Append(Environment.NewLine);
            sb.Append("        " + fieldName + " = value;");
            sb.Append(Environment.NewLine);
            sb.Append("    }");
            sb.Append(Environment.NewLine);
            sb.Append("}");
            sb.Append(Environment.NewLine);


        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.FileTabControl.SelectedTab == null)
            {
                return;
            }

            new SaveFileCommand().Run();

            string privateString = ((TextEditorControl)WorkbenchSingleton.Workbench.FileTabControl.SelectedTab.Controls[0]).Document.TextContent;
            string publicString = string.Empty;
            StringBuilder sb = new StringBuilder();

            privateString = privateString.Replace("\r\n", "\n");
            string[] s = privateString.Split(new char[] { '\n' });

            int i = 0;
            while (i < s.Length)
            {
                if (s[i] != string.Empty)
                {
                    GeneratePublicProperty(sb, s[i]);
                }
                i++;
            }

            publicString = sb.ToString();

            ((TextEditorControl)WorkbenchSingleton.Workbench.FileTabControl.SelectedTab.Controls[0]).Document.TextContent = publicString;

        }

    }

    public class GenerateEntityClassCommand : AbstractMenuCommand
    {
        private void WritePublicAttribute(StreamWriter fileStream, string fieldName, string type)
        {
            fileStream.WriteLine("        public " + type + " " + fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1));
            fileStream.WriteLine("        {");
            fileStream.WriteLine("            get");
            fileStream.WriteLine("            {");
            fileStream.WriteLine("                return " + fieldName + ";");
            fileStream.WriteLine("            }");
            fileStream.WriteLine("            set");
            fileStream.WriteLine("            {");
            fileStream.WriteLine("                " + fieldName + " = value;");
            fileStream.WriteLine("            }");
            fileStream.WriteLine("        }");


        }
        private void WriteAssignPublicPropertyValueString(StreamWriter fileStream, string fieldName, string type, string indent)
        {
            fileStream.WriteLine(indent + "if (row[\"" + fieldName + "\"] != DBNull.Value)");
            fileStream.WriteLine(indent + "{");
            fileStream.WriteLine(indent + "    " + fieldName + " = (" + type + ")row[\"" + fieldName + "\"];");
            fileStream.WriteLine(indent + "}");

        }
        private void WriteConstructorWithDataRowParameter(StreamWriter fileStream, string className, XmlNodeList fieldNodeList)
        {
            fileStream.WriteLine("        public " + className + "(DataRow row)");
            fileStream.WriteLine("        {");

            fileStream.WriteLine("            if (row == null)");
            fileStream.WriteLine("            {");
            fileStream.WriteLine("                return;");
            fileStream.WriteLine("            }");

            foreach (XmlNode node in fieldNodeList)
            {
                string fieldName = node.SelectSingleNode("Name").InnerText;
                fieldName = fieldName.Substring(0, 1).ToLower() + fieldName.Substring(1);
                string type = node.SelectSingleNode("Type").InnerText;

                WriteAssignPublicPropertyValueString(fileStream, fieldName, type, "            ");

            }

            fileStream.WriteLine("        }");

        }
        private void CreateTableMappingNode(XmlDocument doc, string namespaceStr, string className, string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = WorkbenchSingleton.Workbench.Conn;
            cmd.CommandText = "select top 1 * from [" + tableName + "]";
            SqlDataAdapter apt = new SqlDataAdapter(cmd);
            WorkbenchSingleton.Workbench.Conn.Close();
            WorkbenchSingleton.Workbench.Conn.Open();
            DataSet ds = new DataSet();
            apt.Fill(ds);

            if (ds.Tables.Count > 0)
            {
                XmlElement classNode = doc.CreateElement("Class");

                XmlAttribute namespaceAttribute = doc.CreateAttribute("namespace");
                namespaceAttribute.Value = namespaceStr;

                XmlAttribute nameAttribute = doc.CreateAttribute("name");
                nameAttribute.Value = className;

                classNode.Attributes.Append(namespaceAttribute);
                classNode.Attributes.Append(nameAttribute);

                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    XmlNode fieldNode = doc.CreateElement("Field");

                    XmlNode nameNode = doc.CreateElement("Name");
                    nameNode.InnerText = col.ColumnName;

                    XmlNode typeNode = doc.CreateElement("Type");
                    typeNode.InnerText = col.DataType.Name;

                    fieldNode.AppendChild(nameNode);
                    fieldNode.AppendChild(typeNode);

                    classNode.AppendChild(fieldNode);

                }

                doc.AppendChild(classNode);

            }

            WorkbenchSingleton.Workbench.Conn.Close();

        }
        private void CreatePersistantClassFile(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("using System;");
            fileStream.WriteLine("using System.Data;");
            fileStream.WriteLine("using NetFocus.Components.CMPServices;");
            fileStream.WriteLine();
            fileStream.WriteLine("namespace " + doc.DocumentElement.Attributes["namespace"].Value.ToString());
            fileStream.WriteLine("{");
            fileStream.WriteLine("    public class " + doc.DocumentElement.Attributes["name"].Value.ToString() + " : PersistableObject");
            fileStream.WriteLine("    {");

            XmlNodeList fieldNodeList = doc.DocumentElement.ChildNodes;

            foreach (XmlNode node in fieldNodeList)
            {
                string fieldName = node.SelectSingleNode("Name").InnerText.Substring(0, 1).ToLower() + node.SelectSingleNode("Name").InnerText.Substring(1);
                string type = node.SelectSingleNode("Type").InnerText;
                if (type == "String")
                {
                    fileStream.WriteLine("        private " + type + " " + fieldName + " = String.Empty;");
                }
                else if (type == "DateTime")
                {
                    fileStream.WriteLine("        private " + type + " " + fieldName + " = new DateTime(1900,1,1);");
                }
                else if (type == "Int32")
                {
                    fileStream.WriteLine("        private " + type + " " + fieldName + " = 0;");
                }
                else
                {
                    fileStream.WriteLine("        private " + type + " " + fieldName + ";");
                }
            }
            fileStream.WriteLine("");
            fileStream.WriteLine("        public " + doc.DocumentElement.Attributes["name"].Value.ToString() + "()");
            fileStream.WriteLine("        { }");

            fileStream.WriteLine("");

            WriteConstructorWithDataRowParameter(fileStream, doc.DocumentElement.Attributes["name"].Value.ToString(), fieldNodeList);

            foreach (XmlNode node in fieldNodeList)
            {
                string fieldName = node.SelectSingleNode("Name").InnerText.Substring(0, 1).ToLower() + node.SelectSingleNode("Name").InnerText.Substring(1);
                string type = node.SelectSingleNode("Type").InnerText;

                fileStream.WriteLine();

                WritePublicAttribute(fileStream, fieldName, type);

            }

            fileStream.WriteLine();
            fileStream.WriteLine("    }");
            fileStream.WriteLine("}");

        }

        public override void Run()
        {
            InputDataNameSpaceInfoForm f = new InputDataNameSpaceInfoForm();

            if (WorkbenchSingleton.Workbench.DataSet1.Tables.Count == 0)
            {
                return;
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                for (int j = 0; j < WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows.Count; j++)
                {
                    if (WorkbenchSingleton.Workbench.TableDataGrid.IsSelected(j) == true)
                    {
                        string tableName = WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows[j][0].ToString();
                        if (File.Exists(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".cs"))
                        {
                            bool isOpen = false;
                            foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                            {
                                if (page.Tag.ToString() == WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".cs")
                                {
                                    //						MessageBox.Show("该表所对应的持久性对象类文件已经打开！","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                    isOpen = true;
                                    break;
                                }
                            }
                            //				if(MessageBox.Show("该表所对应的持久性对象类文件已经生成，是否要打开该文件？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                            //				{
                            if (isOpen == false)
                            {
                                WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".cs");
                            }
                            //				}
                            continue;
                        }


                        string className = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);
                        string namespaceStr = f.Text;
                        WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName = WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".cs";
                        XmlDocument doc = new XmlDocument();
                        try
                        {
                            CreateTableMappingNode(doc, namespaceStr, className, tableName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        StreamWriter fileStream = new StreamWriter(WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName);
                        try
                        {
                            CreatePersistantClassFile(fileStream, doc);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        finally
                        {
                            fileStream.Close();
                        }

                        WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName);


                    }
                }
            }

        }
    }

    public class GenerateCreateTableScriptCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            Assembly assembly = typeof(WorkbenchSingleton).Assembly;

            StringBuilder sb = new StringBuilder();

            string tableFormat = @"CREATE TABLE [tb_{0}s](
    [EntityId] [int] IDENTITY(1,1) NOT NULL,";
 string tableFormat2 = @"CONSTRAINT [PK_tb_{0}s] PRIMARY KEY CLUSTERED 
(
    [EntityId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO";
            string fieldFormat1 = "    [{0}] [{1}]";
            string fieldFormat2 = "    [{0}] [varchar](128)";

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(NetFocus.Web.Core.Entity).IsAssignableFrom(type))
                {
                    string str1 = string.Format(tableFormat, type.Name);
                    string str2 = string.Format(tableFormat2, type.Name);
                    StringBuilder sb2 = new StringBuilder();

                    if (typeof(NetFocus.Web.Core.AttachmentEntity).IsAssignableFrom(type))
                    {
                        sb2.AppendLine();
                        sb2.Append(string.Format(fieldFormat2 + ",", "AttachmentFileName"));
                        sb2.AppendLine();
                        sb2.Append(string.Format(fieldFormat1 + ",", "AttachmentContent", "image"));
                    }
                    PropertyInfo[] properties = type.GetProperties();
                    List<PropertyInfo> propertyList = new List<PropertyInfo>();
                    propertyList.AddRange(properties);
                    foreach (PropertyInfo propertyInfo in propertyList)
                    {
                        Type propertyType = propertyInfo.PropertyType;
                        if (typeof(Property).IsAssignableFrom(propertyType) && propertyInfo.DeclaringType == type)
                        {
                            string propertyName = propertyInfo.Name;
                            Property property = Activator.CreateInstance(propertyType) as Property;
                            string realPropertyTypeName = null;
                            if (property.ObjectValue == null)
                            {
                                realPropertyTypeName = "string";
                            }
                            else
                            {
                                realPropertyTypeName = property.ObjectValue.GetType().Name.ToLower();
                            }
                            if (realPropertyTypeName == "int32")
                            {
                                realPropertyTypeName = "int";
                            }
                            switch (realPropertyTypeName)
                            {
                                case "int":
                                case "float":
                                case "long":
                                case "datetime":
                                case "guid":
                                    sb2.AppendLine();
                                    sb2.Append(string.Format(fieldFormat1, propertyName, realPropertyTypeName));
                                    if (propertyList.IndexOf(propertyInfo) < propertyList.Count - 1)
                                    {
                                        sb2.Append(",");
                                    }
                                    break;
                                case "string":
                                    sb2.AppendLine();
                                    sb2.Append(string.Format(fieldFormat2, propertyName));
                                    if (propertyList.IndexOf(propertyInfo) < propertyList.Count - 1)
                                    {
                                        sb2.Append(",");
                                    }
                                    break;
                            }
                        }
                    }

                    sb.Append(str1 + sb2.ToString().Substring(0, sb2.ToString().Length - 1) + Environment.NewLine + str2);
                    sb.AppendLine();
                }
            }

            string file = WorkbenchSingleton.Workbench.DefaultFilePath + Guid.NewGuid().ToString() + ".sql";
            StreamWriter writer = new StreamWriter(file);
            writer.Write(sb.ToString());
            writer.Close();
            writer.Dispose();

            WorkbenchSingleton.Workbench.OpenFile(file);

        }
    }

    public class GenerateTableMappingsCommand : AbstractMenuCommand
    {
        private string GetClassMember(string parameterName)
        {
            string returnValue = parameterName.Substring(1);
            string firstChar = returnValue.Substring(0, 1).ToUpper();
            returnValue = firstChar + returnValue.Substring(1);

            return returnValue;
        }
        private void CreateTableMappingNode(XmlDocument doc, string className, string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = WorkbenchSingleton.Workbench.Conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = string.Format("sp_{0}_Create", tableName);
            WorkbenchSingleton.Workbench.Conn.Close();
            WorkbenchSingleton.Workbench.Conn.Open();

            SqlCommandBuilder.DeriveParameters(cmd);

            XmlElement tableMappingNode = doc.CreateElement("tableMapping");

            XmlAttribute entityTypeAttribute = doc.CreateAttribute("entityType");
            entityTypeAttribute.Value = string.Format("NetFocus.Web.Applications.Yuanlin.{0}, System.Business.Modules.Yuanlin", className);
            XmlAttribute tableNameAttribute = doc.CreateAttribute("tableName");
            tableNameAttribute.Value = tableName;
            XmlAttribute createCommandAttribute = doc.CreateAttribute("createCommand");
            createCommandAttribute.Value = tableName.Substring(3) + "_Create";
            XmlAttribute retrieveCommandttribute = doc.CreateAttribute("retrieveCommand");
            retrieveCommandttribute.Value = tableName.Substring(3) + "_Retrieve";
            XmlAttribute updateCommandAttribute = doc.CreateAttribute("updateCommand");
            updateCommandAttribute.Value = tableName.Substring(3) + "_Update";
            XmlAttribute deleteCommandAttribute = doc.CreateAttribute("deleteCommand");
            deleteCommandAttribute.Value = tableName.Substring(3) + "_Delete";

            tableMappingNode.Attributes.Append(entityTypeAttribute);
            tableMappingNode.Attributes.Append(tableNameAttribute);
            tableMappingNode.Attributes.Append(createCommandAttribute);
            tableMappingNode.Attributes.Append(retrieveCommandttribute);
            tableMappingNode.Attributes.Append(updateCommandAttribute);
            tableMappingNode.Attributes.Append(deleteCommandAttribute);

            foreach (SqlParameter parameter in cmd.Parameters)
            {
                if (parameter.ParameterName == "@RETURN_VALUE")
                {
                    continue;
                }

                XmlNode fieldMappingNode = doc.CreateElement("fieldMapping");

                XmlAttribute propertyNameAttribute = doc.CreateAttribute("propertyName");
                propertyNameAttribute.Value = GetClassMember(parameter.ParameterName);
                fieldMappingNode.Attributes.Append(propertyNameAttribute);

                XmlAttribute fieldNameAttribute = doc.CreateAttribute("fieldName");
                fieldNameAttribute.Value = GetClassMember(parameter.ParameterName);
                fieldMappingNode.Attributes.Append(fieldNameAttribute);

                XmlAttribute dbTypeHintAttribute = doc.CreateAttribute("dbTypeHint");
                string temp = parameter.SqlDbType.ToString().ToLower();
                int size = 4;

                if (temp == "varchar")
                {
                    dbTypeHintAttribute.Value = string.Format("varchar({0})", parameter.Size.ToString());
                    size = parameter.Size;
                }
                else if (temp == "nvarchar")
                {
                    if (parameter.Size == 1073741823)
                    {
                        dbTypeHintAttribute.Value = "ntext";
                        size = 16;
                    }
                    else
                    {
                        dbTypeHintAttribute.Value = string.Format("nvarchar({0})", parameter.Size.ToString());
                        size = parameter.Size;
                    }
                }
                else
                {
                    dbTypeHintAttribute.Value = temp;
                    if (parameter.SqlDbType == SqlDbType.BigInt)
                    {
                        size = 8;
                    }
                    else if (parameter.SqlDbType == SqlDbType.Image)
                    {
                        size = 16;
                    }
                    if (parameter.SqlDbType == SqlDbType.Int)
                    {
                        size = 4;
                    }
                    if (parameter.SqlDbType == SqlDbType.DateTime)
                    {
                        size = 8;
                    }
                    if (parameter.SqlDbType == SqlDbType.UniqueIdentifier)
                    {
                        size = 16;
                    }
                }
                fieldMappingNode.Attributes.Append(dbTypeHintAttribute);

                XmlAttribute fieldDBTypeEnumAttribute = doc.CreateAttribute("fieldDBTypeEnum");
                if (dbTypeHintAttribute.Value == "ntext")
                {
                    fieldDBTypeEnumAttribute.Value = "NText";
                }
                else
                {
                    fieldDBTypeEnumAttribute.Value = parameter.SqlDbType.ToString();
                }
                fieldMappingNode.Attributes.Append(fieldDBTypeEnumAttribute);

                XmlAttribute fieldSizeAttribute = doc.CreateAttribute("fieldSize");
                fieldSizeAttribute.Value = size.ToString();
                fieldMappingNode.Attributes.Append(fieldSizeAttribute);

                tableMappingNode.AppendChild(fieldMappingNode);

            }

            doc.DocumentElement.AppendChild(tableMappingNode);

            WorkbenchSingleton.Workbench.Conn.Close();

        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.DataSet1.Tables.Count == 0)
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            XmlNode tableMappingsNode = doc.CreateElement("tableMappings");
            doc.AppendChild(tableMappingsNode);

            for (int j = 0; j < WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows.Count; j++)
            {
                if (WorkbenchSingleton.Workbench.TableDataGrid.IsSelected(j) == true)
                {
                    string tableName = WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows[j][0].ToString();
                    string className = tableName.Substring(3, tableName.Length - 4);
                    try
                    {
                        CreateTableMappingNode(doc, className, tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            string file = WorkbenchSingleton.Workbench.DefaultFilePath + Guid.NewGuid() + ".xml";
            doc.Save(file);
            WorkbenchSingleton.Workbench.OpenFile(file);
        }
    }

    public class GenerateCMPMappingFileCommand : AbstractMenuCommand
    {
        private string GetClassMember(string parameterName)
        {
            string returnValue = parameterName.Substring(1);
            string firstChar = returnValue.Substring(0, 1).ToUpper();
            returnValue = firstChar + returnValue.Substring(1);

            return returnValue;
        }
        private bool IsSelected(DataRow row)
        {
            foreach (DataRow r in WorkbenchSingleton.Workbench.DataSet3.Tables[0].Rows)
            {
                if (row[0] == r[0])
                {
                    return true;
                }
            }
            return false;
        }
        private void RemoveSelectedProcedures()
        {
            DataRow row = null;

            WorkbenchSingleton.Workbench.TempTable1.Rows.Clear();

            foreach (DataRow r in WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows)
            {
                if (!IsSelected(r))
                {
                    row = WorkbenchSingleton.Workbench.TempTable1.NewRow();
                    row[0] = r[0];
                    WorkbenchSingleton.Workbench.TempTable1.Rows.Add(row);
                }
            }

            WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Clear();
            foreach (DataRow r1 in WorkbenchSingleton.Workbench.TempTable1.Rows)
            {
                DataRow newRow = WorkbenchSingleton.Workbench.DataSet2.Tables[0].NewRow();
                newRow[0] = r1[0];
                WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Add(newRow);
            }

        }
        private void CreateCommandMappingNode(XmlDocument doc, string commandName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = WorkbenchSingleton.Workbench.Conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = commandName;
            WorkbenchSingleton.Workbench.Conn.Close();
            WorkbenchSingleton.Workbench.Conn.Open();

            SqlCommandBuilder.DeriveParameters(cmd);


            XmlElement commandElement = doc.CreateElement("command");

            XmlAttribute commandNameAttribute = doc.CreateAttribute("commandId");
            commandNameAttribute.Value = commandName.Substring(6);
            commandElement.Attributes.Append(commandNameAttribute);

            commandNameAttribute = doc.CreateAttribute("commandName");
            commandNameAttribute.Value = commandName;
            commandElement.Attributes.Append(commandNameAttribute);

            XmlElement parametersElement = doc.CreateElement("parameters");

            foreach (SqlParameter parameter in cmd.Parameters)
            {
                if (parameter.ParameterName == "@RETURN_VALUE")
                {
                    continue;
                }

                XmlElement parameterElement = doc.CreateElement("parameter");

                XmlAttribute classMemberAttribute = doc.CreateAttribute("classMember");
                classMemberAttribute.Value = GetClassMember(parameter.ParameterName);
                parameterElement.Attributes.Append(classMemberAttribute);

                XmlAttribute parameterNameAttribute = doc.CreateAttribute("parameterName");
                parameterNameAttribute.Value = parameter.ParameterName;
                parameterElement.Attributes.Append(parameterNameAttribute);

                XmlAttribute dbTypeHintAttribute = doc.CreateAttribute("dbTypeHint");
                dbTypeHintAttribute.Value = parameter.SqlDbType.ToString();
                parameterElement.Attributes.Append(dbTypeHintAttribute);


                XmlAttribute paramDirectionAttribute = doc.CreateAttribute("paramDirection");
                if (parameter.Direction == ParameterDirection.InputOutput)
                {
                    paramDirectionAttribute.Value = ParameterDirection.Output.ToString();
                }
                else
                {
                    paramDirectionAttribute.Value = parameter.Direction.ToString();
                }
                parameterElement.Attributes.Append(paramDirectionAttribute);

                parametersElement.AppendChild(parameterElement);
            }

            commandElement.AppendChild(parametersElement);

            if (commandName.EndsWith("Retrieve"))
            {
                string tableName = commandName.Substring(3, commandName.Length - 12);
                string className = tableName.Substring(3, tableName.Length - 4);

                cmd = new SqlCommand();
                cmd.Connection = WorkbenchSingleton.Workbench.Conn;
                cmd.CommandText = "select top 1 * from [" + tableName + "]";
                SqlDataAdapter apt = new SqlDataAdapter(cmd);
                WorkbenchSingleton.Workbench.Conn.Close();
                WorkbenchSingleton.Workbench.Conn.Open();
                DataSet ds = new DataSet();
                apt.Fill(ds);

                WorkbenchSingleton.Workbench.Conn.Close();

                if (ds.Tables.Count > 0)
                {
                    XmlElement returnEntityCollectionNode = doc.CreateElement("returnEntityCollection");
                    XmlElement returnEntityNode = doc.CreateElement("returnEntity");

                    XmlAttribute entityTypeAttribute = doc.CreateAttribute("entityType");
                    entityTypeAttribute.Value = "NetFocus.Web.Applications.Yuanlin." + className;
                    returnEntityNode.Attributes.Append(entityTypeAttribute);

                    XmlAttribute entityReturnModeAttribute = doc.CreateAttribute("entityReturnMode");
                    entityReturnModeAttribute.Value = "Single";
                    returnEntityNode.Attributes.Append(entityReturnModeAttribute);

                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        XmlNode fieldMappingNode = doc.CreateElement("fieldMapping");

                        XmlAttribute propertyNameAttribute = doc.CreateAttribute("propertyName");
                        propertyNameAttribute.Value = col.ColumnName;
                        fieldMappingNode.Attributes.Append(propertyNameAttribute);

                        XmlAttribute fieldNameAttribute = doc.CreateAttribute("fieldName");
                        fieldNameAttribute.Value = col.ColumnName;
                        fieldMappingNode.Attributes.Append(fieldNameAttribute);

                        returnEntityNode.AppendChild(fieldMappingNode);
                    }
                    returnEntityCollectionNode.AppendChild(returnEntityNode);
                    commandElement.AppendChild(returnEntityCollectionNode);
                }
            }

            doc.DocumentElement.AppendChild(commandElement);

        }
        private void CreateContainerMappingNode(XmlDocument doc, string containerMappingNodeId)
        {
            XmlElement containerElement = doc.CreateElement("ContainerMapping");

            XmlAttribute idAttribute = doc.CreateAttribute("Id");
            idAttribute.Value = containerMappingNodeId;
            containerElement.Attributes.Append(idAttribute);

            doc.AppendChild(containerElement);

            //对选中的存储过程进行处理
            for (int j = 0; j < WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count; j++)
            {
                if (WorkbenchSingleton.Workbench.SourceProcedureDataGrid.IsSelected(j) == true)
                {
                    CreateCommandMappingNode(doc, WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows[j][0].ToString());
                    DataRow row = WorkbenchSingleton.Workbench.TempTable.NewRow();
                    row[0] = WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows[j][0].ToString();
                    WorkbenchSingleton.Workbench.TempTable.Rows.Add(row);
                }
            }



            doc.Save(WorkbenchSingleton.Workbench.CurrentCMPMappingFileName);

            RemoveSelectedProcedures();

        }
        private void BindToSelectedProcedureDataGrid()
        {
            if (WorkbenchSingleton.Workbench.DataSet3 != null && WorkbenchSingleton.Workbench.DataSet3.Tables.Count > 0)
            {
                WorkbenchSingleton.Workbench.DataSet3.Tables[0].TableName = "procedureNameTable";

                WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.DataBindings.Clear();

                WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.SetDataBinding(WorkbenchSingleton.Workbench.DataSet3, "procedureNameTable");

                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.DataMember;
                WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.TableStyles.Clear();
                WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.TableStyles.Add(ts);
                ts.GridColumnStyles[0].Width = WorkbenchSingleton.Workbench.SelectedProcedureDataGrid.Width - 45;
                ts.RowHeadersVisible = false;
                ts.RowHeadersVisible = true;
                ts.RowHeaderWidth = 22;
                ts.AlternatingBackColor = Color.Gainsboro;

            }
        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.DataSet2.Tables.Count == 0)
            {
                return;
            }
            int j = 0;
            for (j = 0; j < WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count; j++)
            {
                if (WorkbenchSingleton.Workbench.SourceProcedureDataGrid.IsSelected(j) == true)
                {
                    break;
                }
            }
            if (j == WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count)
            {
                MessageBox.Show("请选择至少一个存储过程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            InputContainerMappingIdForm f = new InputContainerMappingIdForm();

            if (f.ShowDialog() == DialogResult.OK)
            {
                WorkbenchSingleton.Workbench.CurrentCMPMappingFileName = WorkbenchSingleton.Workbench.DefaultFilePath + f.Tag.ToString() + ".xml";
                if (File.Exists(WorkbenchSingleton.Workbench.CurrentCMPMappingFileName))
                {
                    foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                    {
                        if (page.Tag.ToString() == WorkbenchSingleton.Workbench.CurrentCMPMappingFileName)
                        {
                            MessageBox.Show("该容器映射类文件已经打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (MessageBox.Show("已经存在容器ID为 " + f.Tag.ToString() + " 的持久性容器，是否要打开该容器所在的文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.CurrentCMPMappingFileName);

                    }
                    return;
                }
                XmlDocument doc = new XmlDocument();
                try
                {
                    CreateContainerMappingNode(doc, f.Tag.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.CurrentCMPMappingFileName);

                BindToSelectedProcedureDataGrid();

            }

        }
    }

    public class GenerateSPScriptCommand : AbstractMenuCommand
    {
        StringBuilder sb = null;
        string noneKeyString = "TABLE: {0} [UPDATE] [DELETE] [SELECT BY KEY] PROCEDURES NOT CREATE" + Environment.NewLine + "REASON: KEY NOT EXIST";

        private bool IfKeyFieldCountEqualsTotalFieldCount(XmlDocument doc)
        {
            return doc.DocumentElement.ChildNodes[0].ChildNodes.Count == doc.DocumentElement.ChildNodes[1].ChildNodes.Count;
        }
        private void AppendChildNode(XmlDocument doc, XmlNode sourceNode)
        {
            XmlNode node = doc.CreateElement("KeyNode");
            XmlNode childNode = null;
            foreach (XmlNode n in sourceNode.ChildNodes)
            {
                childNode = doc.CreateElement(n.Name);
                childNode.InnerText = n.InnerText;

                node.AppendChild(childNode);

            }
            doc.DocumentElement.AppendChild(node);

        }
        public void CreateTableMappingNodeForStoredProcedure(XmlDocument doc, string tableName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = WorkbenchSingleton.Workbench.Conn;

            cmd.CommandText = "select distinct column_name from information_schema.key_column_usage where table_name='" + tableName + "'";
            SqlDataAdapter apt = new SqlDataAdapter(cmd);
            WorkbenchSingleton.Workbench.Conn.Close();
            WorkbenchSingleton.Workbench.Conn.Open();
            DataSet ds = new DataSet();
            apt.Fill(ds);

            cmd.CommandText = "select * from INFORMATION_SCHEMA.COLUMNS where table_name = '" + tableName + "'";
            DataSet ds1 = new DataSet();
            apt.SelectCommand = cmd;
            apt.Fill(ds1);

            WorkbenchSingleton.Workbench.Conn.Close();

            XmlElement tableNode = doc.CreateElement("Table");

            XmlAttribute nameAttribute = doc.CreateAttribute("name");
            nameAttribute.Value = tableName;

            tableNode.Attributes.Append(nameAttribute);

            //1.get all the primary keys
            XmlElement primaryKeyNode = doc.CreateElement("PrimaryKey");
            if (ds.Tables.Count > 0)
            {
                XmlNode keyNode = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    keyNode = doc.CreateElement("Key");
                    keyNode.InnerText = row[0] as string;
                    primaryKeyNode.AppendChild(keyNode);
                }

            }
            tableNode.AppendChild(primaryKeyNode);

            //2.get all the columns
            XmlElement columnsNode = doc.CreateElement("Columns");
            if (ds1.Tables.Count > 0)
            {
                foreach (DataRow row in ds1.Tables[0].Rows)
                {
                    XmlNode fieldNode = doc.CreateElement("Field");

                    XmlNode nameNode = doc.CreateElement("Name");
                    nameNode.InnerText = row["COLUMN_NAME"].ToString();

                    XmlNode typeNode = doc.CreateElement("Type");
                    typeNode.InnerText = row["DATA_TYPE"].ToString();

                    XmlNode sizeNode = doc.CreateElement("Size");
                    if (typeNode.InnerText != "image" && typeNode.InnerText != "ntext" && typeNode.InnerText != "text" && typeNode.InnerText != "sql_variant" && typeNode.InnerText != "timestamp")
                    {
                        sizeNode.InnerText = row["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    }
                    fieldNode.AppendChild(nameNode);
                    fieldNode.AppendChild(typeNode);
                    fieldNode.AppendChild(sizeNode);

                    columnsNode.AppendChild(fieldNode);

                }

            }
            tableNode.AppendChild(columnsNode);

            doc.AppendChild(tableNode);

        }
        private void WriteInsertValues(StreamWriter fileStream, XmlNodeList fieldNodeList, string indent)
        {
            string fieldName = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                fieldName = fieldNode.ChildNodes[0].InnerText;

                fieldName = indent + "@" + fieldName;

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldName = fieldName + ",";
                }

                fileStream.WriteLine(fieldName);

                fieldIndex++;

            }

        }
        private void WriteParameters(StreamWriter fileStream, XmlNodeList fieldNodeList)
        {
            string fieldName = string.Empty;
            string fieldType = string.Empty;
            string fieldSize = string.Empty;
            string fieldFullString = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                //get the field name
                fieldName = fieldNode.ChildNodes[0].InnerText;
                //get the field type
                fieldType = fieldNode.ChildNodes[1].InnerText;
                //get the field size
                fieldSize = fieldNode.ChildNodes[2].InnerText;

                if (fieldSize != null && fieldSize != string.Empty)
                {
                    fieldFullString = "    @" + fieldName + " " + fieldType + "(" + fieldSize + ")";
                }
                else
                {
                    fieldFullString = "    @" + fieldName + " " + fieldType;
                }

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldFullString = fieldFullString + ",";
                }

                fileStream.WriteLine(fieldFullString);


                fieldIndex++;

            }
        }
        private void WriteParameters2(StreamWriter fileStream, XmlNodeList fieldNodeList)
        {
            string fieldName = string.Empty;
            string fieldType = string.Empty;
            string fieldSize = string.Empty;
            string fieldFullString = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                //get the field name
                fieldName = fieldNode.ChildNodes[0].InnerText;
                //get the field type
                fieldType = fieldNode.ChildNodes[1].InnerText;
                //get the field size
                fieldSize = fieldNode.ChildNodes[2].InnerText;

                if (fieldSize != null && fieldSize != string.Empty)
                {
                    fieldFullString = "    @" + fieldName + " " + fieldType + "(" + fieldSize + ")";
                }
                else
                {
                    fieldFullString = "    @" + fieldName + " " + fieldType;
                    if (fieldName == "EntityId")
                    {
                        fieldFullString += " OUTPUT";
                    }
                }

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldFullString = fieldFullString + ",";
                }

                fileStream.WriteLine(fieldFullString);


                fieldIndex++;

            }
        }
        private void WriteFieldAssignValues(StreamWriter fileStream, XmlNodeList keyFieldNodeList, XmlNodeList fieldNodeList, bool exceptKeyField, string indent)
        {
            string fieldName = string.Empty;

            string fullString = string.Empty;

            int fieldIndex = 0;
            bool isKeyField = false;

            if (exceptKeyField == false)
            {
                foreach (XmlNode fieldNode in fieldNodeList)
                {
                    fieldName = fieldNode.ChildNodes[0].InnerText;

                    fullString = indent + "[" + fieldName + "] = @" + fieldName;

                    if (fieldIndex < fieldNodeList.Count - 1)
                    {
                        fullString = fullString + ",";
                    }

                    fileStream.WriteLine(fullString);
                    fieldIndex++;

                }

                return;
            }

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                fieldName = fieldNode.ChildNodes[0].InnerText;

                fullString = indent + "[" + fieldName + "] = @" + fieldName;

                if (fieldIndex < fieldNodeList.Count - keyFieldNodeList.Count - 1)
                {
                    fullString = fullString + ",";
                }

                isKeyField = false;
                foreach (XmlNode keyFieldNode in keyFieldNodeList)
                {
                    if (fieldName == keyFieldNode.InnerText)
                    {
                        isKeyField = true;
                        break;
                    }
                }
                if (isKeyField == false)
                {
                    fileStream.WriteLine(fullString);
                    fieldIndex++;
                }

            }

        }
        private void WriteKeyFieldAssignValues(StreamWriter fileStream, XmlNodeList keyFieldNodeList, string indent)
        {
            string fieldName = string.Empty;

            string fullString = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode keyFieldNode in keyFieldNodeList)
            {
                fieldName = keyFieldNode.InnerText;

                fullString = indent + "[" + fieldName + "] = @" + fieldName;

                if (fieldIndex < keyFieldNodeList.Count - 1)
                {
                    fullString = fullString + " AND ";
                }

                fileStream.WriteLine(fullString);


                fieldIndex++;

            }

        }
        private void WriteSelectFields(StreamWriter fileStream, XmlNodeList fieldNodeList, string indent)
        {
            string fieldName = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                fieldName = fieldNode.ChildNodes[0].InnerText;

                fieldName = indent + "[" + fieldName + "]";

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldName = fieldName + ",";
                }

                fileStream.WriteLine(fieldName);

                fieldIndex++;

            }

        }
        private void WriteSelectFields2(StreamWriter fileStream, XmlNodeList fieldNodeList, string indent)
        {
            string fieldName = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                fieldName = fieldNode.ChildNodes[0].InnerText;
                if (fieldName == "EntityId")
                {
                    fieldIndex++;
                    continue;
                }
                fieldName = indent + "[" + fieldName + "]";

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldName = fieldName + ",";
                }

                fileStream.WriteLine(fieldName);

                fieldIndex++;

            }

        }
        private void WriteInsertValues2(StreamWriter fileStream, XmlNodeList fieldNodeList, string indent)
        {
            string fieldName = string.Empty;

            int fieldIndex = 0;

            foreach (XmlNode fieldNode in fieldNodeList)
            {
                fieldName = fieldNode.ChildNodes[0].InnerText;
                if (fieldName == "EntityId")
                {
                    fieldIndex++;
                    continue;
                }
                fieldName = indent + "@" + fieldName;

                if (fieldIndex < fieldNodeList.Count - 1)
                {
                    fieldName = fieldName + ",";
                }

                fileStream.WriteLine(fieldName);

                fieldIndex++;

            }

        }
        public XmlNodeList GetKeyFieldListWithDataType(XmlNodeList keyFieldNodeList, XmlNodeList fieldNodeList)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement documentNode = doc.CreateElement("KeyNodes");
            doc.AppendChild(documentNode);

            foreach (XmlNode node in fieldNodeList)
            {
                foreach (XmlNode node1 in keyFieldNodeList)
                {
                    if (node.ChildNodes[0].InnerText == node1.InnerText)
                    {
                        AppendChildNode(doc, node);
                        break;
                    }
                }
            }

            return doc.DocumentElement.ChildNodes;

        }
        private void WriteIfExistsScript(StreamWriter fileStream, XmlDocument doc)
        {
            XmlNodeList keyFieldList = doc.DocumentElement.ChildNodes[0].ChildNodes;

            string fullStringFormat = "IF EXISTS(SELECT [{0}] FROM [{1}] WHERE [{0}] = @{0})";
            string fullStringFormat1 = "IF EXISTS(SELECT * FROM [{0}] WHERE {1})";
            string whereStringFormat = "[{0}] = @{0} {1} ";
            string whereFullString = string.Empty;

            if (keyFieldList.Count == 1)
            {
                fileStream.WriteLine(string.Format(fullStringFormat, keyFieldList[0].InnerText, doc.DocumentElement.Attributes["name"].Value));
            }
            else if (keyFieldList.Count >= 2)
            {
                int count = 0;
                foreach (XmlNode keyFieldNode in keyFieldList)
                {
                    if (count < keyFieldList.Count - 1)
                    {
                        whereFullString = whereFullString + string.Format(whereStringFormat, keyFieldNode.InnerText, "AND");
                    }
                    else
                    {
                        whereFullString = whereFullString + string.Format(whereStringFormat, keyFieldNode.InnerText, string.Empty);
                    }
                    count++;
                }
                fileStream.WriteLine(string.Format(fullStringFormat1, doc.DocumentElement.Attributes["name"].Value, whereFullString));
            }

        }

        private void CreateSpecialUpdateProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("----------------------------------------------------------------------");
            fileStream.WriteLine("--Created By " + WorkbenchSingleton.Workbench.CurrentUserName);
            fileStream.WriteLine("--Created Date " + DateTime.Now.ToString());
            fileStream.WriteLine("----------------------------------------------------------------------");
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UpdateProcedureSuffix);

            WriteParameters(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes);

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");

            WriteIfExistsScript(fileStream, doc);
            fileStream.WriteLine("    RETURN 0");
            fileStream.WriteLine("ELSE");
            fileStream.WriteLine("BEGIN");

            fileStream.WriteLine("    UPDATE [" + doc.DocumentElement.Attributes["name"].Value + "] SET");

            WriteFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, doc.DocumentElement.ChildNodes[1].ChildNodes, false, "        ");

            fileStream.WriteLine("    WHERE");

            WriteKeyFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, "        ");

            fileStream.WriteLine("    RETURN 1");
            fileStream.WriteLine("END");
            fileStream.WriteLine("");
            fileStream.WriteLine("GO");

        }
        private void CreateUpdateProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UpdateProcedureSuffix);

            WriteParameters(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes);

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");
            fileStream.WriteLine("UPDATE [" + doc.DocumentElement.Attributes["name"].Value + "] SET");

            WriteFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, doc.DocumentElement.ChildNodes[1].ChildNodes, true, "    ");

            fileStream.WriteLine("WHERE");

            WriteKeyFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, "    ");

            fileStream.WriteLine("");
            fileStream.WriteLine("");
            fileStream.WriteLine("GO");

        }
        private void CreateSelectByKeyProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix);

            WriteParameters(fileStream, GetKeyFieldListWithDataType(doc.DocumentElement.ChildNodes[0].ChildNodes, doc.DocumentElement.ChildNodes[1].ChildNodes));

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");
            fileStream.WriteLine("SELECT");

            WriteSelectFields(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "    ");

            fileStream.WriteLine("FROM");
            fileStream.WriteLine("    [" + doc.DocumentElement.Attributes["name"].Value + "]");
            fileStream.WriteLine("WHERE");
            WriteKeyFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, "    ");

            fileStream.WriteLine("");
            fileStream.WriteLine("");
            fileStream.WriteLine("GO");
        }
        private void CreateListSelectProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("----------------------------------------------------------------------");
            fileStream.WriteLine("--Created By " + WorkbenchSingleton.Workbench.CurrentUserName);
            fileStream.WriteLine("--Created Date " + DateTime.Now.ToString());
            fileStream.WriteLine("----------------------------------------------------------------------");
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.CollectionSelectProcedureSuffix);

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");
            fileStream.WriteLine("SELECT");

            WriteSelectFields(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "    ");

            fileStream.WriteLine("FROM");
            fileStream.WriteLine("    [" + doc.DocumentElement.Attributes["name"].Value + "]");

            fileStream.WriteLine("");
            fileStream.WriteLine("");
            fileStream.WriteLine("GO");

        }
        private void CreateDeleteProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.DeleteProcedureSuffix);

            WriteParameters(fileStream, GetKeyFieldListWithDataType(doc.DocumentElement.ChildNodes[0].ChildNodes, doc.DocumentElement.ChildNodes[1].ChildNodes));

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");
            fileStream.WriteLine("DELETE FROM " + "[" + doc.DocumentElement.Attributes["name"].Value + "]");

            fileStream.WriteLine("WHERE");
            WriteKeyFieldAssignValues(fileStream, doc.DocumentElement.ChildNodes[0].ChildNodes, "    ");

            fileStream.WriteLine("");
            fileStream.WriteLine("");
            fileStream.WriteLine("GO");

        }
        private void CreateInsertProcedureSQLScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("");
            fileStream.WriteLine("CREATE PROCEDURE " + WorkbenchSingleton.Workbench.DatabaseOwner + "." + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.InsertProcedureSuffix);

            WriteParameters2(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes);

            fileStream.WriteLine("AS");
            fileStream.WriteLine("");
            if (doc.DocumentElement.ChildNodes[0].ChildNodes.Count > 0)
            {
                fileStream.WriteLine("    INSERT INTO [" + doc.DocumentElement.Attributes["name"].Value + "](");

                WriteSelectFields2(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "        ");

                fileStream.WriteLine("    )VALUES(");

                WriteInsertValues2(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "        ");

                fileStream.WriteLine("    )");
                fileStream.WriteLine();
                fileStream.WriteLine("    SET @EntityId = SCOPE_IDENTITY()");

                fileStream.WriteLine("");
                fileStream.WriteLine("GO");
            }
            else
            {
                fileStream.WriteLine("INSERT INTO [" + doc.DocumentElement.Attributes["name"].Value + "](");

                WriteSelectFields(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "    ");

                fileStream.WriteLine(")VALUES(");

                WriteInsertValues(fileStream, doc.DocumentElement.ChildNodes[1].ChildNodes, "    ");

                fileStream.WriteLine(")");

                fileStream.WriteLine("");
                fileStream.WriteLine("GO");
            }


        }
        private void CreateRemoveExistingProcedureScript(StreamWriter fileStream, XmlDocument doc)
        {
            fileStream.WriteLine("");

            fileStream.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.InsertProcedureSuffix + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            fileStream.WriteLine("drop procedure [" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.InsertProcedureSuffix + "]");
            fileStream.WriteLine("GO");

            fileStream.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            fileStream.WriteLine("drop procedure [" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix + "]");
            fileStream.WriteLine("GO");

            fileStream.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UpdateProcedureSuffix + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            fileStream.WriteLine("drop procedure [" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.UpdateProcedureSuffix + "]");
            fileStream.WriteLine("GO");

            fileStream.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.DeleteProcedureSuffix + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
            fileStream.WriteLine("drop procedure [" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + WorkbenchSingleton.Workbench.ProcedurePrefix + doc.DocumentElement.Attributes["name"].Value + WorkbenchSingleton.Workbench.DeleteProcedureSuffix + "]");
            fileStream.WriteLine("GO");
        }

        private void CreateSQLScriptFile(StreamWriter fileStream, XmlDocument doc)
        {
            //1.如果这个表不存在主键
            //
            if (doc.DocumentElement.ChildNodes[0].ChildNodes.Count <= 0)
            {
                //create remove existing procedure script
                CreateRemoveExistingProcedureScript(fileStream, doc);
                fileStream.WriteLine("");

                //create insert procedure script
                fileStream.WriteLine("");
                CreateInsertProcedureSQLScript(fileStream, doc);

                //create list select procedure script
                fileStream.WriteLine("");
                CreateListSelectProcedureSQLScript(fileStream, doc);

                sb.Append(string.Format(noneKeyString, doc.DocumentElement.Attributes["name"].Value));
                sb.AppendLine();

                WorkbenchSingleton.Workbench.UnCreateProcedures += 3;

                return;

            }

            //create remove existing procedure script
            CreateRemoveExistingProcedureScript(fileStream, doc);
            fileStream.WriteLine("");

            //create insert procedure script
            CreateInsertProcedureSQLScript(fileStream, doc);
            fileStream.WriteLine("");

            //create select by key procedure script
            CreateSelectByKeyProcedureSQLScript(fileStream, doc);
            fileStream.WriteLine("");

            //如果这个表中的所有字段合在一起作为主键
            //
            if (IfKeyFieldCountEqualsTotalFieldCount(doc))
            {
                //create special update procedure script
                CreateSpecialUpdateProcedureSQLScript(fileStream, doc);
                fileStream.WriteLine("");
            }
            else
            {
                //create update procedure script
                CreateUpdateProcedureSQLScript(fileStream, doc);
                fileStream.WriteLine("");
            }
            //create delete procedure script
            CreateDeleteProcedureSQLScript(fileStream, doc);
            fileStream.WriteLine("");
        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.DataSet1.Tables.Count == 0)
            {
                return;
            }

            int totalTables = 0;

            sb = new StringBuilder();
            WorkbenchSingleton.Workbench.UnCreateProcedures = 0;
            string file = WorkbenchSingleton.Workbench.DefaultFilePath + Guid.NewGuid() + ".sql";

            StreamWriter fileStream = new StreamWriter(file);

            for (int j = 0; j < WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows.Count; j++)
            {
                if (WorkbenchSingleton.Workbench.TableDataGrid.IsSelected(j) == true)
                {
                    string tableName = WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows[j][0].ToString();
                    if (File.Exists(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".sql"))
                    {
                        bool isOpen = false;
                        foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                        {
                            if (page.Tag.ToString() == WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".sql")
                            {
                                isOpen = true;
                                break;
                            }
                        }
                        if (isOpen == false)
                        {
                            WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".sql");
                        }
                        continue;
                    }

                    string className = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);
                    WorkbenchSingleton.Workbench.CurrentSQLFileName = WorkbenchSingleton.Workbench.DefaultFilePath + tableName + ".sql";
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        CreateTableMappingNodeForStoredProcedure(doc, tableName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    try
                    {
                        CreateSQLScriptFile(fileStream, doc);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    totalTables += 1;

                }
            }

            fileStream.Close();
            fileStream.Dispose();

            WorkbenchSingleton.Workbench.OpenFile(file);

            //string logFile = WorkbenchSingleton.Workbench.DefaultFilePath + WorkbenchSingleton.Workbench.ProcedureCreateLogFileName;

            //int i = 1;
            //while (File.Exists(logFile + i.ToString() + ".log") == true)
            //{
            //    i++;
            //}
            //StreamWriter fileStream1 = new StreamWriter(logFile + i.ToString() + ".log");

            //fileStream1.WriteLine("共生成 " + Convert.ToString(totalTables * 5 - WorkbenchSingleton.Workbench.UnCreateProcedures) + "个存储过程：");
            //fileStream1.Write(sb.ToString());

            //fileStream1.Close();

            //WorkbenchSingleton.Workbench.OpenFile(logFile + i.ToString() + ".log");

            //MessageBox.Show("SQL脚本执行成功，详细信息请查看日值文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }

    }

    public class GenerateBusinessClassCommand : AbstractMenuCommand
    {
        private string GetKeyParameterStringByTableName(string tableName)
        {
            XmlDocument doc = new XmlDocument();

            new GenerateSPScriptCommand().CreateTableMappingNodeForStoredProcedure(doc, tableName);

            XmlNodeList keyFieldNodeList = doc.DocumentElement.ChildNodes[0].ChildNodes;
            XmlNodeList fieldNodeList = doc.DocumentElement.ChildNodes[1].ChildNodes;

            if (keyFieldNodeList.Count == 0)
            {
                return string.Empty;
            }

            XmlNodeList keyFieldNodeListWithType = new GenerateSPScriptCommand().GetKeyFieldListWithDataType(keyFieldNodeList, fieldNodeList);

            if (keyFieldNodeListWithType.Count == 0)
            {
                return string.Empty;
            }

            string fieldName = string.Empty;
            string fieldType = string.Empty;
            string formatString = "{0} {1}";
            string keyParameterString = string.Empty;
            int index = 0;

            foreach (XmlNode keyNode in keyFieldNodeListWithType)
            {
                fieldName = keyNode.ChildNodes[0].InnerText;
                fieldType = keyNode.ChildNodes[1].InnerText;

                if (fieldType == "uniqueidentifier")
                {
                    fieldType = "Guid";
                }

                keyParameterString += string.Format(formatString, fieldType, fieldName);

                if (index < keyFieldNodeListWithType.Count - 1)
                {
                    keyParameterString += ", ";
                    index++;
                }

            }

            return keyParameterString;

        }
        private string GetKeyFieldAssignStringByTableName(string tableName)
        {
            XmlDocument doc = new XmlDocument();

            new GenerateSPScriptCommand().CreateTableMappingNodeForStoredProcedure(doc, tableName);

            XmlNodeList keyFieldNodeList = doc.DocumentElement.ChildNodes[0].ChildNodes;

            if (keyFieldNodeList.Count == 0)
            {
                return string.Empty;
            }

            string fieldName = string.Empty;
            string fieldUpperName = string.Empty;
            string formatString = "            {0}.{1} = {2};" + Environment.NewLine;
            string keyFieldAssignString = string.Empty;

            foreach (XmlNode keyNode in keyFieldNodeList)
            {
                fieldName = keyNode.InnerText;
                fieldUpperName = fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);

                keyFieldAssignString += string.Format(formatString, tableName + "Object", fieldUpperName, fieldName);

            }

            return keyFieldAssignString;

        }
        private void CreateBusinessClassFile(StreamWriter fileStream, string businessLayerNameSpace, string referenceNameSpace, string tableUpperName, string tableName, string keyParameterString, string keyFieldAssignString)
        {
            string prefix = WorkbenchSingleton.Workbench.ProcedurePrefix;
            string uniqueSelectSuffix = WorkbenchSingleton.Workbench.UniqueSelectProcedureSuffix;
            string listSelectSuffix = WorkbenchSingleton.Workbench.CollectionSelectProcedureSuffix;
            string insertSuffix = WorkbenchSingleton.Workbench.InsertProcedureSuffix;
            string updateSuffix = WorkbenchSingleton.Workbench.UpdateProcedureSuffix;
            string deleteSuffix = WorkbenchSingleton.Workbench.DeleteProcedureSuffix;

            string templateFile = WorkbenchSingleton.Workbench.TemplateFilePath + "BusinessLayerTemplate.template";

            StreamReader reader = new StreamReader(templateFile);

            string content = reader.ReadToEnd();

            reader.Close();

            content = content.Replace("BUSINESSLAYERNAMESPACE", businessLayerNameSpace);
            content = content.Replace("REFERENCENAMESPACE", referenceNameSpace);
            content = content.Replace("TABLEUPPERNAME", tableUpperName);
            content = content.Replace("TABLENAME", tableName);
            content = content.Replace("PREFIX", prefix);
            content = content.Replace("GETLISTSUFFIX", listSelectSuffix);
            content = content.Replace("GETBYKEYSUFFIX", uniqueSelectSuffix);
            content = content.Replace("INSERTSUFFIX", insertSuffix);
            content = content.Replace("UPDATESUFFIX", updateSuffix);
            content = content.Replace("DELETESUFFIX", deleteSuffix);
            content = content.Replace("KEYPARAMETERSTRING", keyParameterString);
            content = content.Replace("KEYFIELDASSIGNSTRING", keyFieldAssignString);

            fileStream.Write(content);

        }
        private void CreateSimpleBusinessClassFile(StreamWriter fileStream, string businessLayerNameSpace, string referenceNameSpace, string tableUpperName, string tableName)
        {
            string prefix = WorkbenchSingleton.Workbench.ProcedurePrefix;
            string listSelectSuffix = WorkbenchSingleton.Workbench.CollectionSelectProcedureSuffix;
            string insertSuffix = WorkbenchSingleton.Workbench.InsertProcedureSuffix;

            string templateFile = WorkbenchSingleton.Workbench.TemplateFilePath + "SimpleBusinessLayerTemplate.template";

            StreamReader reader = new StreamReader(templateFile);

            string content = reader.ReadToEnd();

            reader.Close();

            content = content.Replace("BUSINESSLAYERNAMESPACE", businessLayerNameSpace);
            content = content.Replace("REFERENCENAMESPACE", referenceNameSpace);
            content = content.Replace("TABLEUPPERNAME", tableUpperName);
            content = content.Replace("TABLENAME", tableName);
            content = content.Replace("PREFIX", prefix);
            content = content.Replace("GETLISTSUFFIX", listSelectSuffix);
            content = content.Replace("INSERTSUFFIX", insertSuffix);

            fileStream.Write(content);

        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.DataSet1.Tables.Count == 0)
            {
                return;
            }
            InputBusinessNameSpaceInfoForm f = new InputBusinessNameSpaceInfoForm();

            if (f.ShowDialog() == DialogResult.OK)
            {
                for (int j = 0; j < WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows.Count; j++)
                {
                    if (WorkbenchSingleton.Workbench.TableDataGrid.IsSelected(j) == true)
                    {
                        string tableName = WorkbenchSingleton.Workbench.DataSet1.Tables[0].Rows[j][0].ToString();
                        if (File.Exists(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + "Manager.cs"))
                        {
                            bool isOpen = false;
                            foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
                            {
                                if (page.Tag.ToString() == WorkbenchSingleton.Workbench.DefaultFilePath + tableName + "Manager.cs")
                                {
                                    //						MessageBox.Show("该表所对应的持久性对象类文件已经打开！","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                    isOpen = true;
                                    break;
                                }
                            }
                            //				if(MessageBox.Show("该表所对应的持久性对象类文件已经生成，是否要打开该文件？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                            //				{
                            if (isOpen == false)
                            {
                                WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.DefaultFilePath + tableName + "Manager.cs");
                            }
                            //				}
                            continue;
                        }

                        string tableLowerName = tableName.Substring(0, 1).ToLower() + tableName.Substring(1);
                        string tableUpperName = tableName.Substring(0, 1).ToUpper() + tableName.Substring(1);

                        WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName = WorkbenchSingleton.Workbench.DefaultFilePath + tableName + "Manager.cs";

                        StreamWriter fileStream = new StreamWriter(WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName);
                        try
                        {
                            string keyParameterString = GetKeyParameterStringByTableName(tableName);
                            string keyFieldAssingString = GetKeyFieldAssignStringByTableName(tableName);
                            if (keyParameterString == string.Empty)
                            {
                                CreateSimpleBusinessClassFile(fileStream, f.Text, f.Tag.ToString(), tableUpperName, tableLowerName);
                            }
                            else
                            {
                                CreateBusinessClassFile(fileStream, f.Text, f.Tag.ToString(), tableUpperName, tableLowerName, keyParameterString, keyFieldAssingString);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        finally
                        {
                            fileStream.Close();
                        }

                        WorkbenchSingleton.Workbench.OpenFile(WorkbenchSingleton.Workbench.CurrentStandardCSharpFileName);

                    }
                }
            }
        }
    }

    public class ExecuteSPScriptCommand : AbstractMenuCommand
    {
        private bool ExecuteSqlInFile(string pathToScriptFile)
        {
            SqlConnection connection = WorkbenchSingleton.Workbench.Conn;

            try
            {
                StreamReader _reader = null;

                string sql = "";

                if (false == System.IO.File.Exists(pathToScriptFile))
                {
                    throw new Exception("File " + pathToScriptFile + " does not exists");
                }

                _reader = new StreamReader(pathToScriptFile);

                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                while (null != (sql = ReadNextStatementFromStream(_reader)))
                {
                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }

                _reader.Close();


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in file:" + pathToScriptFile + Environment.NewLine + "Message:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private static string ReadNextStatementFromStream(StreamReader _reader)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string lineOfText;

                while (true)
                {
                    lineOfText = _reader.ReadLine();
                    if (lineOfText == null)
                    {

                        if (sb.Length > 0)
                        {
                            return sb.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }

                    if (lineOfText.TrimEnd().ToUpper() == "GO")
                    {
                        break;
                    }

                    sb.Append(lineOfText + Environment.NewLine);
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.FileTabControl.TabPages.Count == 0)
            {
                return;
            }
            new SaveAllCommand().Run();

            foreach (TabPage page in WorkbenchSingleton.Workbench.FileTabControl.TabPages)
            {
                FileInfo fileInfo = new FileInfo(page.Tag.ToString());
                if (fileInfo.Extension == ".sql")
                {
                    try
                    {
                        ExecuteSqlInFile(page.Tag.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }

            MessageBox.Show("SQL脚本执行成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }
    }

    public class ExecuteSingleSPScriptCommand : AbstractMenuCommand
    {
        private bool ExecuteSqlInFile(string pathToScriptFile)
        {
            SqlConnection connection = WorkbenchSingleton.Workbench.Conn;

            try
            {
                StreamReader _reader = null;

                string sql = "";

                if (false == System.IO.File.Exists(pathToScriptFile))
                {
                    throw new Exception("File " + pathToScriptFile + " does not exists");
                }

                _reader = new StreamReader(pathToScriptFile);

                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;

                while (null != (sql = ReadNextStatementFromStream(_reader)))
                {
                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }

                _reader.Close();


                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in file:" + pathToScriptFile + Environment.NewLine + "Message:" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private static string ReadNextStatementFromStream(StreamReader _reader)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string lineOfText;

                while (true)
                {
                    lineOfText = _reader.ReadLine();
                    if (lineOfText == null)
                    {

                        if (sb.Length > 0)
                        {
                            return sb.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }

                    if (lineOfText.TrimEnd().ToUpper() == "GO")
                    {
                        break;
                    }

                    sb.Append(lineOfText + Environment.NewLine);
                }

                return sb.ToString();
            }
            catch
            {
                return null;
            }
        }

        public override void Run()
        {
            new SaveAllCommand().Run();

            TabPage page = WorkbenchSingleton.Workbench.FileTabControl.SelectedTab;

            FileInfo fileInfo = new FileInfo(page.Tag.ToString());
            if (fileInfo.Extension == ".sql")
            {
                try
                {
                    ExecuteSqlInFile(page.Tag.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            MessageBox.Show("SQL脚本执行成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }

    public class DropProcedureCommand : AbstractMenuCommand
    {
        private void ExecuteDropProcedureScript(string procedureName)
        {
            SqlConnection connection = WorkbenchSingleton.Workbench.Conn;

            try
            {
                string sql = string.Empty;

                sql += "if exists (select * from dbo.sysobjects where id = object_id(N'[" + WorkbenchSingleton.Workbench.DatabaseOwner + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)";
                sql += Environment.NewLine;
                sql += "drop procedure [" + procedureName + "]";

                SqlCommand command = new SqlCommand();

                connection.Open();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = sql;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("删除存储过程时错误：" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        public override void Run()
        {
            if (WorkbenchSingleton.Workbench.DataSet2.Tables.Count == 0)
            {
                return;
            }
            int j = 0;
            for (j = 0; j < WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count; j++)
            {
                if (WorkbenchSingleton.Workbench.SourceProcedureDataGrid.IsSelected(j) == true)
                {
                    break;
                }
            }
            if (j == WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count)
            {
                MessageBox.Show("请选择至少一个存储过程！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("存储过程删除后不能恢复，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                for (j = 0; j < WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows.Count; j++)
                {
                    if (WorkbenchSingleton.Workbench.SourceProcedureDataGrid.IsSelected(j) == true)
                    {
                        try
                        {
                            ExecuteDropProcedureScript(WorkbenchSingleton.Workbench.DataSet2.Tables[0].Rows[j][0].ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                new InitializeCommand().Run();

            }

        }
    }

    public class DeleteFileOrFolderCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            new DeleteFileOrFolderForm().ShowDialog();
        }
    }

    public class NewEntityCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            GetEntityListForm.Instance.CreateEntity();
        }
    }

    public class CloseEntityCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            TabPage currentTab = GetEntityListForm.Instance.FileTabControl.SelectedTab;
            if (currentTab != null)
            {
                GetEntityListForm.Instance.FileTabControl.TabPages.Remove(currentTab);
            }
        }
    }


    #endregion

    public class DeleteEntityCommand : AbstractMenuCommand
    {
        public override void Run()
        {
            TabPage currentTab = GetEntityListForm.Instance.FileTabControl.SelectedTab;
            if (currentTab != null)
            {
                GetEntityListForm.Instance.FileTabControl.TabPages.Remove(currentTab);
            }
        }
    }

    public class GenerateEntityListCommand : AbstractMenuCommand
    {
        //private Hashtable extendedFieldsDBMapping = new Hashtable();
        //private Hashtable extendedFieldsTypeMapping = new Hashtable();

        //public GenerateEntityListCommand()
        //{
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        extendedFieldsDBMapping["IntField" + i.ToString()] = "SqlDbType.Int";
        //        extendedFieldsTypeMapping["IntField" + i.ToString()] = "int";
        //    }
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        extendedFieldsDBMapping["FloatField" + i.ToString()] = "SqlDbType.Float";
        //        extendedFieldsTypeMapping["FloatField" + i.ToString()] = "double";
        //    }
        //    for (int i = 1; i <= 20; i++)
        //    {
        //        extendedFieldsDBMapping["VarChar" + i.ToString()] = "SqlDbType.VarChar";
        //        extendedFieldsTypeMapping["VarChar" + i.ToString()] = "string";
        //    }
        //    for (int i = 1; i <= 2; i++)
        //    {
        //        extendedFieldsDBMapping["NText" + i.ToString()] = "SqlDbType.NText";
        //        extendedFieldsTypeMapping["NText" + i.ToString()] = "string";
        //    }
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        extendedFieldsDBMapping["DateTime" + i.ToString()] = "SqlDbType.DateTime";
        //        extendedFieldsTypeMapping["DateTime" + i.ToString()] = "DateTime";
        //    }
        //    for (int i = 1; i <= 1; i++)
        //    {
        //        extendedFieldsDBMapping["ImageField" + i.ToString()] = "SqlDbType.Image";
        //        extendedFieldsTypeMapping["ImageField" + i.ToString()] = "byte[]";
        //    }
        //    for (int i = 1; i <= 2; i++)
        //    {
        //        extendedFieldsDBMapping["UniqueIdentifier" + i.ToString()] = "SqlDbType.UniqueIdentifier";
        //        extendedFieldsTypeMapping["UniqueIdentifier" + i.ToString()] = "Guid";
        //    }
        //    for (int i = 1; i <= 2; i++)
        //    {
        //        extendedFieldsDBMapping["Binary" + i.ToString()] = "SqlDbType.Binary";
        //        extendedFieldsTypeMapping["Binary" + i.ToString()] = "long";
        //    }
        //}

        //private void CreateFile(string sourceFile, string targetFile)
        //{
        //    File.Copy(sourceFile, targetFile);
        //}

        //internal string[] CreateTargetFiles(Entity entity)
        //{
        //    string sourceDirectory = WorkbenchSingleton.Workbench.DefaultEntityTemplatesPath;//模版目录
        //    string targetDirectory = Application.StartupPath + @"\files\" + entity.EntityTypeName + @"\";//生成文件目录
        //    List<string> targetFiles = new List<string>();

        //    foreach (string file in Directory.GetFiles(sourceDirectory, "*.*", SearchOption.TopDirectoryOnly))
        //    {
        //        CreateFile(file, targetDirectory + new FileInfo(file).Name);
        //        targetFiles.Add(targetDirectory + new FileInfo(file).Name);
        //    }

        //    return targetFiles.ToArray();
        //}

        //private string GetFileContent(string file)
        //{
        //    return GetFileContent(file, 0);
        //}
        //private string GetFileContent(string file, int indent)
        //{
        //    StreamReader streamReader = new StreamReader(file, Encoding.UTF8, true);
        //    string indentString = string.Empty;
        //    for (int i = 0; i < indent; i++)
        //    {
        //        indentString += " ";
        //    }
        //    string content = streamReader.ReadToEnd();
        //    content = content.Replace(Environment.NewLine, Environment.NewLine + indentString);
        //    streamReader.Close();
        //    streamReader.Dispose();

        //    return content;
        //}

        //private string GetEntityFieldResourceListValue(Entity entity)
        //{
        //    string value = string.Empty;
        //    string format = "<resource name=\"{2}_{3}_{4}_{0}\">{1}</resource>";
        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += string.Format(format, new object[] { field.Name, field.ReourceValue, entity.PrefixString, entity.ApplicationName, entity.EntityTypeName }) + Environment.NewLine;
        //    }
        //    return value;
        //}
        //private string GetFieldControlHTMLListValue(Entity entity)
        //{
        //    string value = string.Empty;
        //    string leftSpan = "        ";
        //    string indent = "    ";
        //    string format = leftSpan + "<div class=\"FormRow\">" + Environment.NewLine;
        //    format += leftSpan + indent + "<nwap:ResourceLabel CssClass=\"FieldName\" runat=\"Server\" ControlToLabel=\"{0}{1}\" ResourceFile=\"ControlPanelResources.xml\" ResourceName=\"{3}_{4}_{5}_{2}\" />" + Environment.NewLine;
        //    format += leftSpan + indent + "<nwap:{1} Runat=\"server\" CssClass=\"InputField MiddleWidth\" id=\"{0}{1}\" />" + Environment.NewLine;
        //    format += leftSpan + "</div>";
        //    string format2 = leftSpan + "<div class=\"FormRow\">" + Environment.NewLine;
        //    format2 += leftSpan + indent + "<nwap:ResourceLabel CssClass=\"FieldName\" runat=\"Server\" ControlToLabel=\"{0}{1}\" ResourceFile=\"ControlPanelResources.xml\" ResourceName=\"{3}_{4}_{5}_{2}\" />" + Environment.NewLine;
        //    format2 += leftSpan + indent + "<nwap:{1} Runat=\"server\" SkinRelativePath=\"Skin-FCKEditor-Default.ascx\" Width=\"100%\" Height=\"250px\" id=\"{0}{1}\" />" + Environment.NewLine;
        //    format2 += leftSpan + "</div>";
        //    string format3 = leftSpan + "<div class=\"FormRow\">" + Environment.NewLine;
        //    format3 += leftSpan + indent + "<nwap:ResourceLabel CssClass=\"FieldName\" runat=\"Server\" ControlToLabel=\"{0}{1}\" ResourceFile=\"ControlPanelResources.xml\" ResourceName=\"{3}_{4}_{5}_{2}\" />" + Environment.NewLine;
        //    format3 += leftSpan + indent + "<nwap:{1} Runat=\"server\" id=\"{0}{1}\" />" + Environment.NewLine;
        //    format3 += leftSpan + "</div>";
        //    foreach (EntityField field in entity.Fields)
        //    {
        //        if (field.InputControlType == EntityInputControlType.ValuedEditor)
        //        {
        //            value += string.Format(format2, new object[] { field.PascalName, field.InputControlType.ToString(), field.Name, entity.PrefixString, entity.ApplicationName, entity.EntityTypeName }) + Environment.NewLine;
        //        }
        //        else if (field.InputControlType == EntityInputControlType.ValuedCheckBox || field.InputControlType == EntityInputControlType.ValuedDropDownList)
        //        {
        //            value += string.Format(format3, new object[] { field.PascalName, field.InputControlType.ToString(), field.Name, entity.PrefixString, entity.ApplicationName, entity.EntityTypeName }) + Environment.NewLine;
        //        }
        //        else
        //        {
        //            value += string.Format(format, new object[] { field.PascalName, field.InputControlType.ToString(), field.Name, entity.PrefixString, entity.ApplicationName, entity.EntityTypeName }) + Environment.NewLine;
        //        }
        //    }
        //    return value;
        //}
        //private string GetEntityFieldAssignValueListValue(Entity entity)
        //{
        //    string value = string.Empty;
        //    string leftSpan = "            ";
        //    string format = "{3}.{2} = this.{0}{1}.Value;";
        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += leftSpan + string.Format(format, field.PascalName, field.InputControlType.ToString(), field.Name, entity.EntityTypeName.Substring(0, 1).ToLower() + entity.EntityTypeName.Substring(1)) + Environment.NewLine;
        //    }
        //    return value;
        //}
        //private string GetEntityFieldDefineListValue(Entity entity)
        //{
        //    string value = string.Empty;
        //    string leftSpan = "        ";
        //    string format = "protected {0} {1}{0};";
        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += leftSpan + string.Format(format, field.InputControlType.ToString(), field.PascalName) + Environment.NewLine;
        //    }
        //    return value;
        //}
        //private string GetEntityFieldGetValueListValue(Entity entity)
        //{
        //    string value = string.Empty;
        //    string leftSpan = "                ";
        //    string format = "this.{0}{1}.Value = {3}.{2};";
        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += leftSpan + string.Format(format, field.PascalName, field.InputControlType.ToString(), field.Name, entity.EntityTypeName.Substring(0, 1).ToLower() + entity.EntityTypeName.Substring(1)) + Environment.NewLine;
        //    }
        //    return value;
        //}
        //private string GetEntityFieldExtendedAttributeListValue(Entity entity)
        //{
        //    string privateFormat = "private {0} {1};" + Environment.NewLine;
        //    string value = string.Empty;
        //    string format = "[ExtendedField(FieldIdent.{2}, {3})]" + Environment.NewLine;
        //    format += "        public {4} {0}" + Environment.NewLine;
        //    format += "        {{" + Environment.NewLine;
        //    format += "           get {{ return {1}; }}" + Environment.NewLine;
        //    format += "           set {{ {1} = value; }}" + Environment.NewLine;
        //    format += "        }}" + Environment.NewLine;

        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += string.Format("        " + privateFormat, extendedFieldsTypeMapping[field.DbFieldName].ToString(), field.Name.Substring(0, 1).ToLower() + field.Name.Substring(1));
        //    }

        //    value += Environment.NewLine;
        //    value += Environment.NewLine;

        //    foreach (EntityField field in entity.Fields)
        //    {
        //        value += string.Format("        " + format, field.Name, field.Name.Substring(0, 1).ToLower() + field.Name.Substring(1), field.DbFieldName, extendedFieldsDBMapping[field.DbFieldName].ToString(), extendedFieldsTypeMapping[field.DbFieldName].ToString()) + Environment.NewLine;
        //    }
        //    return value;
        //}

        //internal List<Replacer> CreateReplacers(Entity entity, string[] targetFiles)
        //{
        //    List<Replacer> replacers = new List<Replacer>();
        //    Replacer replacer = null;


        //    //创建所有的关键字替换器
        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.ApplicationName.ToString();
        //    replacer.Value = entity.ApplicationName;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.ApplicationLowerName.ToString();
        //    replacer.Value = entity.ApplicationName.ToLower();
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.ManageControlTitleFieldName.ToString();
        //    replacer.Value = entity.ManageControlTitleFieldName;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.MasterPageName.ToString();
        //    replacer.Value = entity.MasterPageName;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.MPContentID.ToString();
        //    replacer.Value = entity.MpContentID;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.PageSize.ToString();
        //    replacer.Value = entity.PageSize;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityTypeName.ToString();
        //    replacer.Value = entity.EntityTypeName;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityTypeChineseName.ToString();
        //    replacer.Value = entity.EntityTypeChineseName;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityTypeLowerName.ToString();
        //    replacer.Value = entity.EntityTypeName.ToLower();
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityTypeFirstNameLower.ToString();
        //    replacer.Value = entity.EntityTypeName.Substring(0, 1).ToLowerInvariant() + entity.EntityTypeName.Substring(1);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.PrefixLowerString.ToString();
        //    replacer.Value = entity.PrefixString.ToLower();
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.PrefixString.ToString();
        //    replacer.Value = entity.PrefixString;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.RoleNames.ToString();
        //    replacer.Value = entity.RoleNames;
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityTypeEnum.ToString();
        //    replacer.Value = string.Format("{0} = {1},    //{2}", entity.EntityTypeName, entity.EntityTypeValue, entity.EntityTypeChineseName);
        //    replacers.Add(replacer);

        //    //创建所有的List替换器
        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityFieldResourceList.ToString();
        //    replacer.Value = GetEntityFieldResourceListValue(entity);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.FieldControlHTMLList.ToString();
        //    replacer.Value = GetFieldControlHTMLListValue(entity);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.CSListMatch_ASCXCS_EntityFieldAssignValueList.ToString();
        //    replacer.Value = GetEntityFieldAssignValueListValue(entity);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.CSListMatch_ASCXCS_EntityFieldDefineList.ToString();
        //    replacer.Value = GetEntityFieldDefineListValue(entity);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.CSListMatch_ASCXCS_EntityFieldGetValueList.ToString();
        //    replacer.Value = GetEntityFieldGetValueListValue(entity);
        //    replacers.Add(replacer);

        //    replacer = new Replacer();
        //    replacer.TargetFiles = targetFiles;
        //    replacer.Key = MatchKey.EntityFieldExtendedAttributeList.ToString();
        //    replacer.Value = GetEntityFieldExtendedAttributeListValue(entity);
        //    replacers.Add(replacer);

        //    return replacers;

        //}

        //internal void ResolveFiles(Entity entity, EntityTargetInfo targetInfo)
        //{
        //    //下面将当前模块的所有相关信息整合到目标应用程序
        //    Replacer replacer = new Replacer();
        //    List<string> targetFiles = new List<string>();
        //    string sourcePath = WorkbenchSingleton.Workbench.DefaultFilePath + entity.EntityTypeName;

        //    //替换目标文件中的内容
        //    targetFiles.Add(targetInfo.ControlpanelResources);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\NavBarResources.xml", 2) + Environment.NewLine + "  <!-- INSERT POSITION -->";
        //    replacer.Replace("<!-- INSERT POSITION -->");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.ControlpanelResources);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\EntityFieldResourceList.xml", 2) + Environment.NewLine + "  <!-- INSERT POSITION -->";
        //    replacer.Replace("<!-- INSERT POSITION -->");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.Siteurls);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\\Urls.xml", 4) + Environment.NewLine + "    <!-- INSERT POSITION -->";
        //    replacer.Replace("<!-- INSERT POSITION -->");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.Navbar);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\NavBar.config", 4) + Environment.NewLine + "    <!-- INSERT POSITION -->";
        //    replacer.Replace("<!-- INSERT POSITION -->");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.Urls);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\SiteUrls.cs", 8) + Environment.NewLine + Environment.NewLine + "        //INSERT POSITION";
        //    replacer.Replace("//INSERT POSITION");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.EntityType);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\EntityType.cs") + Environment.NewLine + "        //INSERT POSITION";
        //    replacer.Replace("//INSERT POSITION");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.RequestBuilder);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\RequestBuilder.cs") + Environment.NewLine + Environment.NewLine + "        //INSERT POSITION";
        //    replacer.Replace("//INSERT POSITION");

        //    targetFiles.Clear();
        //    targetFiles.Add(targetInfo.BusinessManager);
        //    replacer.TargetFiles = targetFiles.ToArray();
        //    replacer.Value = GetFileContent(sourcePath + @"\BusinessManager.cs") + Environment.NewLine + Environment.NewLine + "        //INSERT POSITION";
        //    replacer.Replace("//INSERT POSITION");

        //    string sourceDirectory = WorkbenchSingleton.Workbench.DefaultFilePath + entity.EntityTypeName + @"\";

        //    //复制页面
        //    List<string> files = new List<string>();
        //    if (Directory.Exists(targetInfo.Pages + @"\" + entity.EntityTypeName.ToLower()))
        //    {
        //        Directory.Delete(targetInfo.Pages + @"\" + entity.EntityTypeName.ToLower(), true);
        //    }
        //    Directory.CreateDirectory(targetInfo.Pages + @"\" + entity.EntityTypeName.ToLower());
        //    files.AddRange(new string[] { sourceDirectory + "add.aspx", sourceDirectory + "edit.aspx" });
        //    foreach (string file in files)
        //    {
        //        CreateFile(file, targetInfo.Pages + @"\" + entity.EntityTypeName + @"\" + new FileInfo(file).Name);
        //    }
        //    CreateFile(sourceDirectory + "manage.aspx", targetInfo.Pages + @"\" + entity.EntityTypeName + @"\" + "manage.aspx");

        //    //复制代码
        //    if (Directory.Exists(targetInfo.Codefile + @"\" + entity.EntityTypeName))
        //    {
        //        Directory.Delete(targetInfo.Codefile + @"\" + entity.EntityTypeName, true);
        //    }
        //    Directory.CreateDirectory(targetInfo.Codefile + @"\" + entity.EntityTypeName);

        //    if (entity.HasAttachment)
        //    {
        //        CreateFile(sourceDirectory + "ListControlWithAttachment.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "ListControl.cs");
        //        CreateFile(sourceDirectory + "AttachmentEntity.cs", targetInfo.Entity + @"\" + entity.EntityTypeName + ".cs");
        //        CreateFile(sourceDirectory + "AddControlWithAttachment.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "AddControl.cs");
        //        CreateFile(sourceDirectory + "EditControlWithAttachment.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "EditControl.cs");
        //    }
        //    else
        //    {
        //        CreateFile(sourceDirectory + "ListControl.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "ListControl.cs");
        //        CreateFile(sourceDirectory + "Entity.cs", targetInfo.Entity + @"\" + entity.EntityTypeName + ".cs");
        //        CreateFile(sourceDirectory + "AddControl.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "AddControl.cs");
        //        CreateFile(sourceDirectory + "EditControl.cs", targetInfo.Codefile + @"\" + entity.EntityTypeName + @"\" + "EditControl.cs");
        //    }
        //    CreateFile(sourceDirectory + "EntityRequest.cs", targetInfo.EntityRequest + @"\" + entity.EntityTypeName + "Request.cs");
            

        //    //复制皮肤
        //    if (Directory.Exists(targetInfo.Skins + @"\" + entity.EntityTypeName))
        //    {
        //        Directory.Delete(targetInfo.Skins + @"\" + entity.EntityTypeName, true);
        //    }
        //    Directory.CreateDirectory(targetInfo.Skins + @"\" + entity.EntityTypeName);
        //    CreateFile(sourceDirectory + "Skin-ManageControl.ascx", targetInfo.Skins + @"\" + entity.EntityTypeName + @"\" + "Skin-ManageControl.ascx");
        //    if (entity.HasAttachment)
        //    {
        //        CreateFile(sourceDirectory + "Skin-AddControlWithAttachment.ascx", targetInfo.Skins + @"\" + entity.EntityTypeName + @"\" + "Skin-AddControl.ascx");
        //        CreateFile(sourceDirectory + "Skin-EditControlWithAttachment.ascx", targetInfo.Skins + @"\" + entity.EntityTypeName + @"\" + "Skin-EditControl.ascx");
        //    }
        //    else
        //    {
        //        CreateFile(sourceDirectory + "Skin-AddControl.ascx", targetInfo.Skins + @"\" + entity.EntityTypeName + @"\" + "Skin-AddControl.ascx");
        //        CreateFile(sourceDirectory + "Skin-EditControl.ascx", targetInfo.Skins + @"\" + entity.EntityTypeName + @"\" + "Skin-EditControl.ascx");
        //    }

        //}

        //private void form_OKButtonClicked(object sender, EventArgs e)
        //{
        //    GetEntityListForm getPostOrCategoryInfoForm = GetEntityListForm.Instance;

        //    List<Entity> postList = getPostOrCategoryInfoForm.EntityList;

        //    if (postList != null && postList.Count > 0)
        //    {
        //        foreach (Entity post in postList)
        //        {
        //            //生成目标目录
        //            if (Directory.Exists(WorkbenchSingleton.Workbench.DefaultFilePath + post.EntityTypeName))
        //            {
        //                Directory.Delete(WorkbenchSingleton.Workbench.DefaultFilePath + post.EntityTypeName, true);
        //            }
        //            Directory.CreateDirectory(WorkbenchSingleton.Workbench.DefaultFilePath + post.EntityTypeName);

        //            //创建所有的替换器并进行替换
        //            foreach (Replacer replacer in CreateReplacers(post, CreateTargetFiles(post)))
        //            {
        //                replacer.Replace();
        //            }
        //            new OpenCSFilesCommand().Run(post.EntityTypeName);
        //        }
        //    }

        //    if (MessageBox.Show("是否需要整合这些应用？", "询问框", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
        //    {
        //        GetEntityListTargetInfoForm f = new GetEntityListTargetInfoForm();
        //        if (f.ShowDialog() == DialogResult.OK)
        //        {
        //            EntityTargetInfo targetInfo = f.EntityTargetInfo;
        //            if (targetInfo != null)
        //            {
        //                Entity post = null;
        //                foreach (object obj in postList)
        //                {
        //                    post = obj as Entity;
        //                    ResolveFiles(post, targetInfo);
        //                }
        //                MessageBox.Show("恭喜，整合应用成功！", "恭喜", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                MessageBox.Show("请输入目标文件及目录信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            }
        //        }

        //    }

        //}
        
        public override void Run()
        {
            //GetEntityListForm form = GetEntityListForm.Instance;

            //form.OKButtonClicked -= new EventHandler(form_OKButtonClicked);
            //form.OKButtonClicked += new EventHandler(form_OKButtonClicked);

            //form.Show();

        }

    }
}