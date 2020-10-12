using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
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
    [DefaultClassOptions]//默认类选项
    [Persistent("Project")]
    [XafDisplayName("项目信息")]
    [DefaultProperty("PCName")]
    public class Project:BaseObject
    {
        public Project(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            SysUser = CommUtilities.GetCurrentUser(Session);
            CreateTime = DateTime.Now;
        }

        private string _Code;
        //[RuleRequiredField("CodeRequired", DefaultContexts.Save)]
        //[RuleUniqueValue("CodeIsUnique", DefaultContexts.Save, "Code已存在")]
        [XafDisplayName("项目编号")]
        [RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
       // [ModelDefault("AllowEdit", "false")]
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        }

        private string _Name;
        [XafDisplayName("项目名称")]
        [RuleRequiredField("", DefaultContexts.Save)]//必填项
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }


        private string _PCName;
        [XafDisplayName("项目名称")]
        public string PCName
        {
            get
            {
                if (Code!=null&&Name!=null)
                {
                    _PCName = "[ " + Code + " ] " + Name;
                }
                return _PCName;
            }
            set { SetPropertyValue<string>(nameof(PCName), ref _PCName, value); }

        }

        private ProjectType _Type;
        [XafDisplayName("项目状态")]
        public ProjectType Type
        {
            get
            {
                return _Type;
            }
            set { SetPropertyValue<ProjectType>(nameof(Type), ref _Type, value); }

        }


        private   string _Description;
        [XafDisplayName("项目描述"), Size(300)]
        public  string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>(nameof(Description), ref _Description, value); }
        }


        private CustomerCompany _CCpName;
        [XafDisplayName("客户公司名称")]
        [RuleRequiredField("", DefaultContexts.Save)]//必填项
        [Association("CustomerCompany-Projects")]
        public CustomerCompany CCpName
        {
            get { return _CCpName; }
            set { SetPropertyValue<CustomerCompany>(nameof(CCpName), ref _CCpName, value); }
        }


        private string _Address;
        [XafDisplayName("实施地")]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }


        private string _Principal;
        [XafDisplayName("负责人")]
        //[RuleRequiredField("PrincipalRequired", DefaultContexts.Save)]//必填项
        public  string Principal
        {
            get
            {
                return _Principal;
            }
            set { SetPropertyValue<string>(nameof(Principal), ref _Principal, value); }
        }


        private string _Phone;
        [XafDisplayName("联系电话")]
        //[RuleRequiredField("PhoneRequired", DefaultContexts.Save)]//必填项
        public string Phone
        {
            get { return _Phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
        }


        private DateTime _CreateTime;
        [XafDisplayName("创建时间")]
        [ModelDefault("AllowEdit", "false")]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { SetPropertyValue<DateTime>(nameof(CreateTime), ref _CreateTime, value); }
        }


        private SysUser _SysUser;
        [Association("SysUser-Projects")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
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
