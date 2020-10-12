using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.ProjectManagement
{
    [NavigationItem("项目管理")]
    [XafDisplayName("项目类型")]
    [DefaultProperty("类型名称")]
    public class ProjectType:BaseObject
    {
        public ProjectType(Session s) : base(s) { }


        private string _TName;
        [XafDisplayName("类型名称")]
        public string TName
        {
            get { return _TName; }
            set { SetPropertyValue<string>(nameof(TName), ref _TName, value); }
        }

    }
}
