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
    [Persistent("PresellProject")]
    [XafDisplayName("售前项目信息")]
    [DefaultProperty("PSCName")]
    public class PresellProject:Project
    {
        public PresellProject(Session s) :base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
            SysUser = CommUtilities.GetCurrentUser(Session);

            //SN_Create();
        }

        //internal void SN_Create()
        //{
        //    if (string.IsNullOrEmpty(Code))
        //    {
        //        var flagString = "";
        //        if (CreateTime == DateTime.MinValue)
        //        {
        //            flagString = "PS-" + CommUtilities.GetCurrentServerTime(Session).ToString("yyyyMMdd");
        //        }
        //        else
        //        {
        //            flagString = "PS-" + CreateTime.ToString("yyyyMMdd");
        //        }
        //        var indexNo = DistributedIdGeneratorHelper.Generate(this.Session.DataLayer, flagString, string.Empty);
        //        Code = flagString+ indexNo.ToString().PadLeft(3, '0');

        //    }
        //}

        private string _Code;
        //[RuleRequiredField("CodeRequired", DefaultContexts.Save)]
        //[RuleUniqueValue("CodeIsUnique", DefaultContexts.Save, "Code已存在")]
        [XafDisplayName("项目编号")]
        [RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
       // [ModelDefault("AllowEdit", "false")]
        public override string Code
        {
            get { return _Code; }
            set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        }

        private string _PName;
        [XafDisplayName("项目名称")]
        [RuleRequiredField("PSNameRequired", DefaultContexts.Save)]//必填项
        public override string PName
        {
            get { return _PName; }
            set { SetPropertyValue<string>(nameof(PName), ref _PName, value); }
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
        public override string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>(nameof(Description), ref _Description, value); }
        }


        private CustomerCompany _CCpName;
        [XafDisplayName("客户公司名称")]
        [RuleRequiredField("CCpNameRequired", DefaultContexts.Save)]//必填项
        public override CustomerCompany CCpName
        {
            get { return _CCpName; }
            set { SetPropertyValue<CustomerCompany>(nameof(CCpName), ref _CCpName, value); }
        }


        private SysUser _SysUser;
        [Association("SysUser-PresellProjects")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }


    }
}
