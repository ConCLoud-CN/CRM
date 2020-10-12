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
using CRM.Module.BusinessObjects.SellManagement;
using CRM.Module.BusinessObjects.Sys_Management;

namespace CRM.Module.BusinessObjects.CustomerInformationManagement
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).

    public class PDFFile : FileAttachmentBase
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PDFFile(Session session)
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
        [XafDisplayName("公司名称")]
        [Association("CustomerCompany-PDFFiles")]
        public CustomerCompany CustomerCompany
        {
            get { return _CustomerCompany; }
            set { SetPropertyValue<CustomerCompany>(nameof(CustomerCompany), ref _CustomerCompany, value); }
        }


        private string _PDFName;
        [XafDisplayName("附件名")]
        public string PDFName
        {
            get { return _PDFName; }
            set { SetPropertyValue<string>(nameof(PDFName), ref _PDFName, value); }
        }


        private Order _Order;
        [Association("Order-PDFFiles")]
        [XafDisplayName("合同")]
        public Order Order
        {
            get { return _Order; }
            set { SetPropertyValue<Order>(nameof(Order), ref _Order, value); }
        }


        private SysUser _SysUser;
        [Association("SysUser-PDFFiles")]
        [XafDisplayName("合同")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }
    }
}