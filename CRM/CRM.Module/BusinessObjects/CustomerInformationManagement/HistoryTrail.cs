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
using DevExpress.Office.Utils;
using DevExpress.XtraScheduler.Native;
using System.Windows.Documents;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace CRM.Module.BusinessObjects.CustomerInformationManagement
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HistoryTrail : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HistoryTrail(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}


        private CustomerCompany _CustomerCompany;

        [XafDisplayName("更新记录")]
        [Association("CustomerCompany-HistoryTrails")]
        public CustomerCompany CustomerCompany
        {
            get { return _CustomerCompany; }
            set { SetPropertyValue<CustomerCompany>(nameof(CustomerCompany), ref _CustomerCompany, value); }
        }


        private DateTime _EditTime;
        [XafDisplayName("更新时间")]
        public DateTime EditTime
        {
            get { return _EditTime; }
            set { SetPropertyValue<DateTime>(nameof(EditTime), ref _EditTime, value); }
        }



        private string _EditName;
        [XafDisplayName("公司名称")]
        public string EditName
        {
            get { return _EditName; }
            set { SetPropertyValue<string>(nameof(EditName), ref _EditName, value); }
        }


        //公司简介
        private string _EditIntroduce;
        [XafDisplayName("公司简介"), Size(300)]
        public string EditIntroduce
        {
            get { return _EditIntroduce; }
            set { SetPropertyValue<string>(nameof(EditIntroduce), ref _EditIntroduce, value); }
        }


        //法定代表人
        private string _EditlegalMan;
        [XafDisplayName("法定代表人")]
        public string EditlegalMan
        {
            get { return _EditlegalMan; }
            set { SetPropertyValue<string>(nameof(EditlegalMan), ref _EditlegalMan, value); }
        }



        //公司地址
        private string _EditAddress;
        [XafDisplayName("公司地址")]
        public string EditAddress
        {
            get { return _EditAddress; }
            set { SetPropertyValue<string>(nameof(EditAddress), ref _EditAddress, value); }
        }


        //注册资金
        private int _EditRegisteredFund;
        [XafDisplayName("注册资金(万)")]
        public int EditRegisteredFund
        {
            get { return _EditRegisteredFund; }
            set { SetPropertyValue<int>(nameof(EditRegisteredFund), ref _EditRegisteredFund, value); }
        }


        //成立日期
        private DateTime _EditReiestDate;
        [XafDisplayName("成立日期")]
        public DateTime EditReiestDate
        {
            get { return _EditReiestDate; }
            set { SetPropertyValue<DateTime>(nameof(EditReiestDate), ref _EditReiestDate, value); }
        }


        //经营范围
        private string _EditScope;
        [XafDisplayName("经营范围"),Size(300)]
        public string EditScope
        {
            get { return _EditScope; }
            set { SetPropertyValue<string>(nameof(EditScope), ref _EditScope, value); }
        }


        //备注
        private string _EditRemark;
        [XafDisplayName("备注"),Size(300)]
        public string EditRemark
        {
            get { return _EditRemark; }
            set { SetPropertyValue<string>(nameof(EditRemark), ref _EditRemark, value); }
        }



        private CustomerCompanyType _CompanyType;

        //[Association("CustomerCompanyType-CustomerCompanies")]
        [XafDisplayName("所属公司类型")]
        //[XafDisplayName("客户公司分类")]
        public CustomerCompanyType CompanyType
        {
            get { return _CompanyType; }
            set { SetPropertyValue<CustomerCompanyType>(nameof(CompanyType), ref _CompanyType, value); }
        }

    }
}