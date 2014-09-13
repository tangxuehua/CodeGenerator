using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using NetFocus.UtilityTool.CodeGenerator.Commands.Components;

namespace NetFocus.UtilityTool.CodeGenerator.Gui
{
    public partial class GetEntityInfoControl : UserControl
    {
        private Entity entity = null;
        private TabControl parentTabControl = new TabControl();

        public string EntityTypeName
        {
            get
            {
                return this.entityTypeNameTextBox.Text.Trim();
            }
        }
        public TabControl ParentTabControl
        {
            get
            {
                return this.parentTabControl;
            }
            set
            {
                this.parentTabControl = value;
            }
        }

        public GetEntityInfoControl()
        {
            InitializeComponent();
        }

        private class SortByTabIndex : IComparer<Control>
        {
            public int Compare(Control a, Control b)
            {
                if (a.TabIndex > b.TabIndex)
                {
                    return 1;
                }
                else if (a.TabIndex < b.TabIndex)
                {
                    return -1;
                }
                return 0;
            }
        }

        private class ControlNameValue
        {
            private string name = string.Empty;
            private string value = string.Empty;

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }
            public string Value
            {
                get
                {
                    return this.value;
                }
                set
                {
                    this.value = value;
                }
            }

            public ControlNameValue(string name, string value)
            {
                this.name = name;
                this.value = value;
            }

        }

        private class DbField
        {
            private string name = string.Empty;

            public string Name
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            public DbField(string name)
            {
                this.name = name;
            }

        }

        public Entity Entity
        {
            get
            {
                return this.entity;
            }
            set
            {
                this.entity = value;
            }
        }

        private bool AssertValue()
        {
            List<Control> controls = new List<Control>();

            foreach (Control control in this.basicEntityInfoGroupBox.Controls)
            {
                if (control.GetType().Name == "TextBox")
                {
                    controls.Add(control);
                }
            }
            controls.Sort(new SortByTabIndex());
            foreach (Control control in controls)
            {
                if (((TextBox)control).Text.Trim() == "" && control.Tag.ToString() != "ParentEntityType")
                {
                    MessageBox.Show(control.Tag.ToString() + "的值不能为空！");
                    control.Focus();
                    return false;
                }
            }

            if (this.attachmentCheckBox.Checked)
            {
                foreach (DataGridViewRow row in this.fieldsDataGridView.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null || row.Cells[3].Value == null)
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) || string.IsNullOrEmpty(row.Cells[1].Value.ToString()) || string.IsNullOrEmpty(row.Cells[2].Value.ToString()) || string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                        {
                            continue;
                        }
                        if (row.Cells["dbFieldColumn"].Value.ToString().ToLower() == "varchar1" || row.Cells["dbFieldColumn"].Value.ToString().ToLower() == "intfield1" || row.Cells["dbFieldColumn"].Value.ToString().ToLower() == "imagefield1")
                        {
                            MessageBox.Show("如果实体带有附件，则不能用VarChar1,IntField1,ImageField1作为数据库字段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                            row.Cells["dbFieldColumn"].Selected = true;
                            return false;
                        }
                    }
                }
            }

            return true;

        }

        public Entity GetData()
        {
            //验证数据有效性
            if (!AssertValue())
            {
                return null;
            }

            Entity entity = new Entity();

            entity.ApplicationName = this.applicationNameTextBox.Text.Trim();
            entity.ManageControlTitleFieldName = this.manageControlTitleFieldNameTextBox.Text.Trim();
            entity.MasterPageName = this.masterPageNameTextBox.Text.Trim();
            entity.MpContentID = this.mpContentIDTextBox.Text.Trim();
            entity.PageSize = this.pageSizeTextBox.Text.Trim();
            entity.EntityTypeChineseName = this.entityTypeChineseNameTextBox.Text.Trim();
            entity.EntityTypeName = this.entityTypeNameTextBox.Text.Trim();
            entity.PrefixString = this.prefixStringTextBox.Text.Trim();
            entity.RoleNames = this.roleNamesTextBox.Text.Trim();
            entity.EntityTypeValue = this.entityTypeValueTextBox.Text.Trim();
            entity.HasAttachment = this.attachmentCheckBox.Checked;

            foreach (DataGridViewRow row in this.fieldsDataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null || row.Cells[2].Value == null || row.Cells[3].Value == null)
                    {
                        continue;                    
                    }
                    if (string.IsNullOrEmpty(row.Cells[0].Value.ToString()) || string.IsNullOrEmpty(row.Cells[1].Value.ToString()) || string.IsNullOrEmpty(row.Cells[2].Value.ToString()) || string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                    {
                        continue;
                    }
                    EntityField field = new EntityField();
                    field.Name = row.Cells["fieldNameColumn"].Value.ToString();
                    field.DbFieldName = row.Cells["dbFieldColumn"].Value.ToString();
                    field.ReourceValue = row.Cells["resourceValueColumn"].Value.ToString();
                    field.InputControlType = (EntityInputControlType)Enum.Parse(typeof(EntityInputControlType), row.Cells["inputControlTypeColumn"].Value.ToString());
                    entity.Fields.Add(field);
                }
            }

            return entity;
        }

        private void GetEntityInfoControl_Load(object sender, EventArgs e)
        {
            List<ControlNameValue> dataSource = new List<ControlNameValue>();
            dataSource.Add(new ControlNameValue("TextBox", "ValuedTextBox"));
            dataSource.Add(new ControlNameValue("DropDownList", "ValuedDropDownList"));
            dataSource.Add(new ControlNameValue("CheckBox", "ValuedCheckBox"));
            dataSource.Add(new ControlNameValue("Editor", "ValuedEditor"));
            this.inputControlTypeColumn.DataSource = dataSource;
            this.inputControlTypeColumn.ValueMember = "Value";
            this.inputControlTypeColumn.DisplayMember = "Name";

            List<DbField> dataSource2 = new List<DbField>();

            for (int i = 1; i <= 20; i++)
            {
                dataSource2.Add(new DbField("IntField" + i.ToString()));
            }
            for (int i = 1; i <= 10; i++)
            {
                dataSource2.Add(new DbField("FloatField" + i.ToString()));
            }
            for (int i = 1; i <= 20; i++)
            {
                dataSource2.Add(new DbField("VarChar" + i.ToString()));
            }
            for (int i = 1; i <= 2; i++)
            {
                dataSource2.Add(new DbField("NText" + i.ToString()));
            }
            for (int i = 1; i <= 5; i++)
            {
                dataSource2.Add(new DbField("DateTime" + i.ToString()));
            }
            for (int i = 1; i <= 1; i++)
            {
                dataSource2.Add(new DbField("ImageField" + i.ToString()));
            }
            for (int i = 1; i <= 2; i++)
            {
                dataSource2.Add(new DbField("UniqueIdentifier" + i.ToString()));
            }
            for (int i = 1; i <= 2; i++)
            {
                dataSource2.Add(new DbField("Binary" + i.ToString()));
            }

            this.dbFieldColumn.DataSource = dataSource2;
            this.dbFieldColumn.ValueMember = "Name";
            this.dbFieldColumn.DisplayMember = "Name";

            this.applicationNameTextBox.Text = ConfigurationManager.AppSettings["ApplicationName"];
            this.manageControlTitleFieldNameTextBox.Focus();
        }

    }
}
