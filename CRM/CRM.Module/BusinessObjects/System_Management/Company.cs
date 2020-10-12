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
    [XafDisplayName("公司配置")]
   
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    public class Company : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Company(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization SN here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
      
        // Fields...
        private string _Telephone;
        private string _Address;
        private string _Number;
        private string _Name;

        [RuleRequiredField(DefaultContexts.Save)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleUniqueValue("公司名称不能重复", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [XafDisplayName("公司名称")]
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
        [RuleUniqueValue("公司编号不能重复", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [RuleRequiredField(DefaultContexts.Save)]
        
        [XafDisplayName("公司编号")]
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
        [Association("Company-Departments")]
        [XafDisplayName("公司所属部门信息")]
        public XPCollection<Department> Departments
        {
            get
            {
                return GetCollection<Department>("Departments");
            }
        }

        [Association("Company-SysUsers")]
        [XafDisplayName("公司所属员工信息")]
        public XPCollection<SysUser> SysUsers
        {
            get
            {
                return GetCollection<SysUser>("SysUsers");
            }
        }



        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("公司地址")]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                SetPropertyValue("Address", ref _Address, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("公司联系电话")]
        public string Telephone
        {
            get
            {
                return _Telephone;
            }
            set
            {
                SetPropertyValue("Telephone", ref _Telephone, value);
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



        [Association("Company - Positions")]
        public XPCollection<Position> Positions
        {
            get { return GetCollection<Position>(nameof(Positions)); }
        }

    }
}