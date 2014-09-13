using System;
using System.Collections.Generic;
using System.Text;

namespace NetFocus.UtilityTool.CodeGenerator.Commands.Components
{
    /// <summary>
    /// 表示要生成的一个Entity的管理模块
    /// </summary>
    public class Entity
    {
        private string applicationName = string.Empty;
        private string manageControlTitleFieldName = string.Empty;
        private string masterPageName = string.Empty;
        private string mpContentID = string.Empty;
        private string pageSize = string.Empty;
        private string postTypeName = string.Empty;
        private string postTypeChineseName = string.Empty;
        private string prefixString = string.Empty;
        private string roleNames = string.Empty;
        private bool hasAttachment = false;
        private string postTypeValue = string.Empty;
        private List<EntityField> fields = new List<EntityField>();

        public string ApplicationName
        {
            get
            {
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }
        public string ManageControlTitleFieldName
        {
            get
            {
                return manageControlTitleFieldName;
            }
            set
            {
                manageControlTitleFieldName = value;
            }
        }
        public string MasterPageName
        {
            get
            {
                return masterPageName;
            }
            set
            {
                masterPageName = value;
            }
        }
        public string MpContentID
        {
            get
            {
                return mpContentID;
            }
            set
            {
                mpContentID = value;
            }
        }
        public string PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        public string EntityTypeName
        {
            get
            {
                return postTypeName;
            }
            set
            {
                postTypeName = value;
            }
        }
        public string EntityTypeChineseName
        {
            get
            {
                return postTypeChineseName;
            }
            set
            {
                postTypeChineseName = value;
            }
        }
        public string PrefixString
        {
            get
            {
                return prefixString;
            }
            set
            {
                prefixString = value;
            }
        }
        public string RoleNames
        {
            get
            {
                return roleNames;
            }
            set
            {
                roleNames = value;
            }
        }
        public bool HasAttachment
        {
            get
            {
                return hasAttachment;
            }
            set
            {
                hasAttachment = value;
            }
        }
        public string EntityTypeValue
        {
            get
            {
                return postTypeValue;
            }
            set
            {
                postTypeValue = value;
            }
        }
        public List<EntityField> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
            }
        }
    }
}
