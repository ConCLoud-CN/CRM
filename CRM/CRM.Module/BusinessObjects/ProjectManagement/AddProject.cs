using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.ProjectManagement
{
    [NavigationItem("项目管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("AddProject")]
    [XafDisplayName("增补项目信息")]
    [DefaultProperty("CNName")]
    public class AddProject: Project
    {
        public AddProject(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            AddDate = DateTime.Now.Date;
            SysUser = CommUtilities.GetCurrentUser(Session);
        }

        private SellProject _SpCode;
        [RuleRequiredField("SpCodeRequired", DefaultContexts.Save)]
        [XafDisplayName("立项项目")]
        [Association("SellProject-AddProjects")]
        public SellProject SpCode
        {
            get { return _SpCode; }
            set 
            {
                SetPropertyValue<SellProject>(nameof(SpCode), ref _SpCode, value);
            }
        }

        private string _Code;
        [RuleUniqueValue("AddCodeIsUnique", DefaultContexts.Save, "AddCode已存在")]
        [XafDisplayName("增补项目编号")]
        [ModelDefault("AllowEdit", "true")]
        //[RuleRequiredField("CodeRequired", DefaultContexts.Save)]//必填项
        public override string Code
        {
            get 
            {
                return _Code; 
            }
            set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        }


        private string _APName;
        [XafDisplayName("增补项目名称")]
        [RuleRequiredField("APNameRequired", DefaultContexts.Save)]//必填项
        public string APName
        {
            get { return _APName; }
            set { SetPropertyValue<string>(nameof(APName), ref _APName, value); }
        }

        private ProjectType _Type;
        [XafDisplayName("项目类型")]
        public override ProjectType Type
        {
            get
            {
                return _Type;
            }
            set { SetPropertyValue<ProjectType>(nameof(Type), ref _Type, value); }

        }

        private string _Description;
        [XafDisplayName("项目描述")]
        public override  string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>(nameof(Description), ref _Description, value); }
        }


        private CustomerCompany _CCpName;
        [XafDisplayName("所属公司名称")]
        //[Association("CustomerCompany-AddProjects")]
        public override CustomerCompany CCpName
        {
            get {
                return _CCpName; 
            }
            set { SetPropertyValue<CustomerCompany>(nameof(CCpName), ref _CCpName, value); }
        }


        private DateTime _AddDate;
        [XafDisplayName("创建时间")]
        public DateTime AddDate
        {
            get { return _AddDate; }
            set { SetPropertyValue<DateTime>(nameof(AddDate), ref _AddDate, value); }
        }


        private string _APrincipal;
        [XafDisplayName("负责人")]
        [RuleRequiredField("APrincipalRequired", DefaultContexts.Save)]
        public string APrincipal
        {
            get { return _APrincipal; }
            set { SetPropertyValue<string>(nameof(APrincipal), ref _APrincipal, value); }
        }


        private string _APhone;
        [XafDisplayName("联系电话")]
        //[RuleRequiredField("APhoneRequired", DefaultContexts.Save)]
        public string APhone
        {
            get { return _APhone; }
            set { SetPropertyValue<string>(nameof(APhone), ref _APhone, value); }
        }

        private string _PCName;
        [XafDisplayName("增补项目")]
        public override string PCName
        {
            get
            {
                return _PCName;
            }
            set { SetPropertyValue<string>(nameof(PCName), ref _PCName, value); }
        }

        private SysUser _SysUser;
        [Association("SysUser-AddProjects")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }

    }
}
