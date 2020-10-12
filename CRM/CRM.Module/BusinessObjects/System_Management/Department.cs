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
    [NavigationItem("系统管理")]
    [XafDisplayName("部门配置")]
    [DefaultClassOptions]
   
    [DefaultProperty("Name")]
    public class Department : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Department(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization SN here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        // Fields...
        private Company _Company;
        private string _Number;
        private string _Name;

        [Association("Company-Departments")]
        [XafDisplayName("所属公司")]
        public Company Company
        {
            get
            {
                return _Company;
            }
            set
            {
                SetPropertyValue("Company", ref _Company, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("部门名称")]
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


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue("Number不能重复", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("部门编号")]
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
        [Association("Department-SysUsers")]
        [XafDisplayName("部门下属员工信息")]
        public XPCollection<SysUser> SysUsers
        {
            get
            {
                return GetCollection<SysUser>("SysUsers");
            }
        }

        [Association("Department-Sections")]
        [XafDisplayName("部门下属科室信息")]
        public XPCollection<Section> Sections
        {
            get
            {
                return GetCollection<Section>("Sections");
            }
        }


        private XPCollection<AuditDataItemPersistent> changeHistory;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [XafDisplayName("更改记录")]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                if (changeHistory == null)
                {
                    changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return changeHistory;
            }
        }

    }

}