using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraTreeList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM.Module.BusinessObjects.ProjectManagement
{
    [NavigationItem("项目管理")]
    [DefaultClassOptions]//~默认类选项~
    [Persistent("SellProject")]
    [XafDisplayName("立项项目信息")]
    [DefaultProperty("CNName")]
    public class SellProject: PresellProject
    {
        public SellProject(Session s) :base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //SpDate = DateTime.Now.Date;
            SysUser = CommUtilities.GetCurrentUser(Session);
            //SN_Create();

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


        private PresellProject _PspSpCode;
        [RuleUniqueValue("PspSpCodeIsUnique", DefaultContexts.Save, "PspSpCode已存在")]
        //[Association("PresellProject-SellProjects")]
        [XafDisplayName("售前项目")]
        [RuleRequiredField("PspSpCodeRequired", DefaultContexts.Save)]//必填项
        public PresellProject PspSpCode
        {
            get { return _PspSpCode; }

            set { SetPropertyValue<PresellProject>(nameof(PspSpCode), ref _PspSpCode, value); }

        }

        //internal void SN_Create()    
        //{
        //    if (string.IsNullOrEmpty(Code))
        //    {
        //        var flagString = "";
        //        if (SpDate == DateTime.MinValue)
        //        {
        //            flagString = "CC-" + CommUtilities.GetCurrentServerTime(Session).ToString("yyyyMMdd").Substring(2,2);
        //        }
        //        else
        //        {
        //            flagString = "CC-" + SpDate.ToString("yyyyMMdd").Substring(2,2);
        //        }
        //        var indexNo = DistributedIdGeneratorHelper.Generate(this.Session.DataLayer, flagString, string.Empty);
        //        Code = flagString + indexNo.ToString().PadLeft(3, '0')+"-00";

        //    }
        //}

        //private string _Code;
        //[XafDisplayName("项目编号")]
        ////[RuleUniqueValue("", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        //[ModelDefault("AllowEdit", "true")]
        //[RuleRequiredField("CodeRequired", DefaultContexts.Save)]//必填项
        //public string Code
        //{
        //    get 
        //    {
        //        return _Code;
        //    }
        //    set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        //}


        //private string _SPName;
        //[XafDisplayName("项目名称")]
        //public string SPName
        //{
        //    get { return _SPName; }

        //    set { SetPropertyValue<string>(nameof(SPName), ref _SPName, value); }
        //}


        //private string _SpDescription;
        //[XafDisplayName("项目描述"), Size(300)]
        //public string SpDescription
        //{
        //    get 
        //    {
        //        return _SpDescription; 
        //    }
        //    set { SetPropertyValue<string>(nameof(SpDescription), ref _SpDescription, value); }
        //}


        //private CustomerCompany _SpCCpName;
        //[XafDisplayName("客户公司名称")]
        //[Association("CustomerCompany-SellProjects")]
        //[ModelDefault("AllowEdit", "false")]
        //public CustomerCompany SpCCpName
        //{
        //    get {
        //        if (PspSpCode != null)
        //            _SpCCpName = PspSpCode.CCpName;
        //        return _SpCCpName; 
        //    }

        //    set { SetPropertyValue<CustomerCompany>(nameof(SpCCpName), ref _SpCCpName, value); }
        //}


        //private DateTime _SpDate;
        //[XafDisplayName("创建时间")]
        //public DateTime SpDate
        //{
        //    get { return _SpDate; }
        //    set { SetPropertyValue<DateTime>(nameof(SpDate), ref _SpDate, value); }
        //}


        //private string _SPrincipal;
        //[XafDisplayName("负责人")]
        //[RuleRequiredField("SPrincipalRequired", DefaultContexts.Save)]//必填项
        //public string SPrincipal
        //{
        //    get 
        //    {
        //        return _SPrincipal; 
        //    }

        //    set { SetPropertyValue<string>(nameof(SPrincipal), ref _SPrincipal, value); }
        //}


        //private string _SPhone;
        //[XafDisplayName("联系电话")]
        ////[RuleRequiredField("SPhoneRequired", DefaultContexts.Save)]//必填项
        //public string SPhone
        //{
        //    get 
        //    {
        //        return _SPhone; 
        //    }
        //    set { SetPropertyValue<string>(nameof(SPhone), ref _SPhone, value); }
        //}


        //private string _CNName;
        //[XafDisplayName("立项项目编号名称")]
        //public string CNName
        //{
        //    get 
        //    {
        //        if (Code!=null && SPName!=null)
        //        {
        //            _CNName = "[" + Code + "]" + SPName;
        //        }
        //        return _CNName; 
        //    }
        //    set { SetPropertyValue<string>(nameof(CNName), ref _CNName, value); }
        //}


        [Association("SellProject-AddProjects")]
        [XafDisplayName("所有增补项目")]
        public XPCollection<AddProject> AddProjects
        {
            get { return GetCollection<AddProject>(nameof(AddProjects)); }
        }


        //private SysUser _SysUser;
        //[Association("SysUser-SellProjects")]
        //public SysUser SysUser
        //{
        //    get { return _SysUser; }
        //    set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        //}

    }
}
