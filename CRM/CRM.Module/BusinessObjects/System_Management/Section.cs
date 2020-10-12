using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace CRM.Module.BusinessObjects.Sys_Management
{
    [DefaultClassOptions]
    [NavigationItem("系统管理")]
    [XafDisplayName("科室配置")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    public class Section : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Section(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private Company _Company;
        [XafDisplayName("科室所属公司")]
        public Company Company
        {
            get { return _Company; }
            set { SetPropertyValue("Company", ref _Company, value); }
        }


        private Department _Department;
        [XafDisplayName("科室所属部门")]
        [Association("Department-Sections")]
        [DataSourceProperty("Company.Departments")]
        public Department Department
        {
            get { return _Department; }
            set { SetPropertyValue("Department", ref _Department, value); }
        }

        private string _Name;
        [XafDisplayName("科室名称")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        private string _Number;
        [XafDisplayName("科室编号")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Number
        {
            get
            {
                return _Number;
            }
            set
            {
                SetPropertyValue("Number", ref _Number, value);
            }
        }


        [Association("Section-SysUsers")]
        [XafDisplayName("科室下属员工信息")]
        public XPCollection<SysUser> SysUsers
        {
            get
            {
                return GetCollection<SysUser>("SysUsers");
            }
        }


    }
}