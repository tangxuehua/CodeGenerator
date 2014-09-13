using System;
using System.Collections.Generic;
using System.Text;

namespace NetFocus.UtilityTool.CodeGenerator.Commands.Components
{
    /// <summary>
    /// 存放默认的目标目录或文件
    /// </summary>
    public class DefaultTargetInfo
    {
        private string siteurls = string.Empty;
        private string navbar = string.Empty;
        private string controlpanelResources = string.Empty;
        private string postType = string.Empty;
        private string urls = string.Empty;
        private string skins = string.Empty;
        private string codefile = string.Empty;
        private string pages = string.Empty;
        private string requestBuilder = string.Empty;
        private string businessManager = string.Empty;
        private string entityRequest = string.Empty;
        private string entity = string.Empty;

        private static DefaultTargetInfo instance = null;

        //Singleton Design Pattern
        private DefaultTargetInfo()
        { }

        //Provide the unique access point
        public static DefaultTargetInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultTargetInfo();
                }
                return instance;
            }
        }

        public string Siteurls
        {
            get
            {
                return siteurls;
            }
            set
            {
                siteurls = value;
            }
        }
        public string Navbar
        {
            get
            {
                return navbar;
            }
            set
            {
                navbar = value;
            }
        }
        public string ControlpanelResources
        {
            get
            {
                return controlpanelResources;
            }
            set
            {
                controlpanelResources = value;
            }
        }
        public string EntityType
        {
            get
            {
                return postType;
            }
            set
            {
                postType = value;
            }
        }
        public string Urls
        {
            get
            {
                return urls;
            }
            set
            {
                urls = value;
            }
        }
        public string Skins
        {
            get
            {
                return skins;
            }
            set
            {
                skins = value;
            }
        }
        public string Codefile
        {
            get
            {
                return codefile;
            }
            set
            {
                codefile = value;
            }
        }
        public string Pages
        {
            get
            {
                return pages;
            }
            set
            {
                pages = value;
            }
        }
        public string RequestBuilder
        {
            get
            {
                return requestBuilder;
            }
            set
            {
                requestBuilder = value;
            }
        }
        public string BusinessManager
        {
            get
            {
                return businessManager;
            }
            set
            {
                businessManager = value;
            }
        }
        public string EntityRequest
        {
            get
            {
                return entityRequest;
            }
            set
            {
                entityRequest = value;
            }
        }
        public string Entity
        {
            get
            {
                return entity;
            }
            set
            {
                entity = value;
            }
        }

    }
}
