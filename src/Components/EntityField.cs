using System;
using System.Collections.Generic;
using System.Text;

namespace NetFocus.UtilityTool.CodeGenerator.Commands.Components
{
    public class EntityField
    {
        private string name = string.Empty;
        private string pascalName = string.Empty;
        private string reourceValue = string.Empty;
        private EntityInputControlType inputControlType = EntityInputControlType.ValuedTextBox;
        private string dbFieldName = string.Empty;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string PascalName
        {
            get
            {
                return this.Name.Substring(0,1).ToLower() + this.Name.Substring(1);
            }
        }
        public string ReourceValue
        {
            get
            {
                return reourceValue;
            }
            set
            {
                reourceValue = value;
            }
        }
        public EntityInputControlType InputControlType
        {
            get
            {
                return inputControlType;
            }
            set
            {
                inputControlType = value;
            }
        }
        public string DbFieldName
        {
            get
            {
                return dbFieldName;
            }
            set
            {
                dbFieldName = value;
            }
        }
    }
}
