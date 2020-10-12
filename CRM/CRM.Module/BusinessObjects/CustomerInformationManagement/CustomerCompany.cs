using CRM.Module.BusinessObjects.ProjectManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Utils.Drawing;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.CustomerInformationManagement
{
    [NavigationItem("客户信息管理")]
    [Persistent("CustomerCompany")]
    [DefaultClassOptions]//默认类选项
    [XafDisplayName("客户公司信息")]
    [DefaultProperty("CPName")]
    public class CustomerCompany:BaseObject
    {
        public CustomerCompany(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            SysUser = CommUtilities.GetCurrentUser(Session);
        }

        //公司编号
        //private string _Code;
        //[XafDisplayName("公司编号")]
        //public string Code
        //{
        //    get { return _Code; }
        //    set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        //}

        //公司编号
        private string _CPName;
        [XafDisplayName("公司名称")]
        [RuleRequiredField("CPNameRequired", DefaultContexts.Save)]//必填项
        public string CPName
        {
            get { return _CPName; }
            set { SetPropertyValue<string>(nameof(CPName), ref _CPName, value); }
        }

        //公司类型编号
        //[XafDisplayName("公司类型编号")]
        //private string _CptpCode;
        //public string CptpCode
        //{
        //    get { return _CptpCode; }
        //    set { SetPropertyValue<string>(nameof(CptpCode), ref _CptpCode, value); }
        //}


        //公司简介
        private string _Introduce;
        [XafDisplayName("公司简介"),Size(300)]
        public string Introduce
        {
            get { return _Introduce; }
            set { SetPropertyValue<string>(nameof(Introduce), ref _Introduce, value); }
        }


        //法定代表人
        private string _legalMan;
        [XafDisplayName("法定代表人")]
        public string legalMan
        {
            get { return _legalMan; }
            set { SetPropertyValue<string>(nameof(legalMan), ref _legalMan, value); }
        }



        //公司地址
        private string _Address;
        [XafDisplayName("公司地址")]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }


        //注册资金
        private int _RegisteredFund;
        [XafDisplayName("注册资金(万)")]
        [RuleRange(0,10000)]
        public int RegisteredFund
        {
            get { return _RegisteredFund; }
            set { SetPropertyValue<int>(nameof(RegisteredFund), ref _RegisteredFund, value); }
        }


        //成立日期
        private DateTime _ReiestDate;
        [XafDisplayName("成立日期")]
        public DateTime ReiestDate
        {
            get { return _ReiestDate; }
            set { SetPropertyValue<DateTime>(nameof(ReiestDate), ref _ReiestDate, value); }
        }


        //经营范围
        private string _Scope;
        [XafDisplayName("经营范围"),Size(300)]
        public string Scope
        {
            get { return _Scope; }
            set { SetPropertyValue<string>(nameof(Scope), ref _Scope, value); }
        }


        //备注
        private string _Remark;
        [XafDisplayName("备注"),Size(300)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue<string>(nameof(Remark), ref _Remark, value); }
        }



        private CustomerCompanyType _CompanyType;

        [Association("CustomerCompanyType-CustomerCompanies")]
        [XafDisplayName("所属公司类型")]
        [RuleRequiredField("CompanyTypeRequired", DefaultContexts.Save)]//必填项
        public CustomerCompanyType CompanyType
        {
            get { return _CompanyType; }
            set { SetPropertyValue<CustomerCompanyType>(nameof(CompanyType), ref _CompanyType, value); }
        }


        [Association("CustomerCompany-Customers")]
        [XafDisplayName("所有员工信息")]
        public XPCollection<Customer> Customers
        {
            get { return GetCollection<Customer>(nameof(Customers)); }
        }



        //private Image _ImageName;
        //[XafDisplayName("图片")]
        //[ValueConverterAttribute(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        //public Image ImageName
        //{
        //    get { return _ImageName; }
        //    set { SetPropertyValue<Image>(nameof(ImageName), ref _ImageName, value); }
        //}


        [Association("CustomerCompany-PDFFiles")]
        [XafDisplayName("附件上传")]
        public XPCollection<PDFFile> PDFFiles
        {
            get { return GetCollection<PDFFile>(nameof(PDFFiles)); }
        }

        [Association("CustomerCompany-HistoryTrails")]
        [XafDisplayName("更改记录")]
        public XPCollection<HistoryTrail> HistoryTrails
        {
            get { return GetCollection<HistoryTrail>(nameof(HistoryTrails)); }
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

        [Association("CustomerCompany-Projects")]
        [XafDisplayName("所有项目信息")]
        public XPCollection<Project> Projects
        {
            get { return GetCollection<Project>(nameof(Projects)); }
        }


        private SysUser _SysUser;
        [Association("SysUser-CustomerCompanies")]
        [XafDisplayName("姓名")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }



    }

}
