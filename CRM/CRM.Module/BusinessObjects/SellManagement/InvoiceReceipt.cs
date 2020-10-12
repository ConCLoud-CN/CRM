using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.ProjectManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.DataProcessing;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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
using static CRM.Module.BusinessObjects.SellManagement.Order;

namespace CRM.Module.BusinessObjects.SellManagement
{
    [NavigationItem("销售管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("InvoiceReceipt")]
    [XafDisplayName("开票收款")]
    [Appearance("ActionVisibility", AppearanceItemType = "ViewItem", TargetItems = "AddProject", Criteria = "OrderCode.Tp = '立项'", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RuleMethod2", AppearanceItemType = "ViewItem", TargetItems = "aPName", Criteria = "Tp = '立项'", Enabled = false)]
    [DefaultProperty("Order")]
    public class InvoiceReceipt:BaseObject
    {
        public InvoiceReceipt(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            SysUser = CommUtilities.GetCurrentUser(Session);

        }

        protected override void OnSaving()
        {

            base.OnSaving();

        }

        private Order _Order;
        [XafDisplayName("合同名称")]
        [RuleRequiredField("", DefaultContexts.Save)]//必填项
        public Order Order
        {
            get { return _Order; }
            set { SetPropertyValue<Order>(nameof(Order), ref _Order, value); }
        }


        private string _CCpName;
        [XafDisplayName("客户名称")]
        //[RuleRequiredField("", DefaultContexts.Save)]//必填项
        [ModelDefault("AllowEdit", "false")]
        public string CCpName
        {
            get
            {
                if (Order != null&& Order.CpName!=null)
                    _CCpName = Order.CpName.CPName;
                return _CCpName;
            }
            set { SetPropertyValue<string>(nameof(CCpName), ref _CCpName, value); }
        }


        private string _Project;
        [XafDisplayName("项目名称")]
        //[DataSourceProperty("CCpName.Projects", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("AllowEdit", "false")]
        public string Project
        {
            get 
            {
                if (Order!=null&&Order.Project!=null)
                {
                    _Project = Order.Project.PCName;
                }

                return _Project;  
            }
            set { SetPropertyValue<string>(nameof(Project), ref _Project, value); }
        }

        private string _Address;
        [XafDisplayName("实施地")]
        [ModelDefault("AllowEdit", "false")]
        public string Address
        {
            get 
            {
                if (Order != null)
                {
                    _Address = Order.Address;
                }
                return _Address; 
            }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }


        private string _ODuty;
        [XafDisplayName("税率")]
        [ModelDefault("AllowEdit", "false")]
        public string ODuty
        {
            get
            {
                if (Order != null&&Order.DutyGrade!=null)
                    _ODuty = Order.DutyGrade.Type;

                return _ODuty;
            }
            set { SetPropertyValue<string>(nameof(ODuty), ref _ODuty, value); }
        }


        private decimal _OMoney;
        [XafDisplayName("合同金额(未含税)¥")]
        [ModelDefault("AllowEdit", "false")]

        public decimal OMoney
        {
            get 
            {
                if (Order != null)
                    _OMoney = Order.Money;

                return _OMoney; 
            }
            set { SetPropertyValue<decimal>(nameof(OMoney), ref _OMoney, value); }
        }


        private decimal _Tax_OMoney;
        [XafDisplayName("合同金额(含税)¥")]
        [ModelDefault("AllowEdit", "false")]
        public decimal Tax_OMoney
        {
            get 
            {
                if (Order != null)
                    _Tax_OMoney = Order.Tax_Money;

                return _Tax_OMoney; 
            }
            set { SetPropertyValue<decimal>(nameof(Tax_OMoney), ref _Tax_OMoney, value); }
        }

        private decimal _OTotal;
        [XafDisplayName("回款金额(含税)¥")]
        [ModelDefault("AllowEdit", "false")]
        public decimal OTotal
        {
            get {  return _OTotal; }
            set { SetPropertyValue<decimal>(nameof(OTotal), ref _OTotal, value); }
        }

        public void UpdateOTotal(bool forceChangeEvents)
        {
            decimal? oldOTotal = _OTotal;
            decimal tempTotal = 0m;
            foreach (PayInvoiceDetail detail in PayInvoiceDetails)
                tempTotal += detail.InvoiceMoney;
            _OTotal = tempTotal;
            if (forceChangeEvents)
                OnChanged("OTotal", oldOTotal, _OTotal);
        }

        //private decimal _ReMoney;
        //[XafDisplayName("剩余金额¥")]
        //public decimal ReMoney
        //{
        //    get 
        //    {
        //        if (PayInvoiceDetails != null)
        //        {
        //            PayInvoiceDetails
        //        }

        //        return _ReMoney; 
        //    }
        //    set { SetPropertyValue<decimal>(nameof(ReMoney), ref _ReMoney, value); }
        //}

        private string _PayDetail;
        [XafDisplayName("付款明细"),Size(300)]
        public string PayDetail
        {
            get { return _PayDetail; }
            set { SetPropertyValue<string>(nameof(PayDetail), ref _PayDetail, value); }
        }

        [Association("InvoiceReceipt-PayInvoiceDetails")]
        [XafDisplayName("开票收款明细")]
        public XPCollection<PayInvoiceDetail> PayInvoiceDetails
        {
            get { return GetCollection<PayInvoiceDetail>(nameof(PayInvoiceDetails)); }
        }


        private SysUser _SysUser;
        [Association("SysUser-InvoiceReceipts")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }

    }

    //[NavigationItem("销售管理")]
    [Persistent("PayInvoiceDetail")]
    [XafDisplayName("开票收款明细")]
    public class PayInvoiceDetail : BaseObject
    {
        public PayInvoiceDetail(Session s) : base(s) { }


        private InvoiceReceipt _InvoiceReceipt;
        [Association("InvoiceReceipt-PayInvoiceDetails")]
        [XafDisplayName("对应合同编号名称")]
        public InvoiceReceipt InvoiceReceipt
        {
            get { return _InvoiceReceipt; }
            set { SetPropertyValue<InvoiceReceipt>(nameof(InvoiceReceipt), ref _InvoiceReceipt, value);  }
        }


        public enum PayTs { 第一次, 第二次, 第三次, 第四次, 第五次 }

        private PayTs _Tts;
        [XafDisplayName("付款次数")]
        public PayTs Tts
        {
            get { return _Tts; }
            set { SetPropertyValue<PayTs>(nameof(Tts), ref _Tts, value); }
        }


        private string _PayRatio;
        [XafDisplayName("付款比例%")]
        public string PayRatio
        {
            get { return _PayRatio; }
            set  { SetPropertyValue<string>(nameof(PayRatio), ref _PayRatio, value);  }
        }


        private decimal _InvoiceMoney;
        [XafDisplayName("开票金额¥")]
        public decimal InvoiceMoney
        {
            get { return _InvoiceMoney; }
            set  {SetPropertyValue<decimal>(nameof(InvoiceMoney), ref _InvoiceMoney, value);   }
        }


        private DateTime _PlanTime;
        [XafDisplayName("计划开票时间")]
        public DateTime PlanTime
        {
            get { return _PlanTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanTime), ref _PlanTime, value); }
        }


        private DateTime _RealTime;
        [XafDisplayName("实际开票时间")]
        public DateTime RealTime
        {
            get { return _RealTime; }
            set { SetPropertyValue<DateTime>(nameof(RealTime), ref _RealTime, value); }
        }


        private string _InvoiceCode;
        [XafDisplayName("发票号码")]
        public string InvoiceCode
        {
            get { return _InvoiceCode; }
            set { SetPropertyValue<string>(nameof(InvoiceCode), ref _InvoiceCode, value); }
        }


        private decimal _IsInvoiceMoney;
        [XafDisplayName("发票金额¥")]
        public decimal IsInvoiceMoney
        {
            get { return _IsInvoiceMoney; }
            set { SetPropertyValue<decimal>(nameof(IsInvoiceMoney), ref _IsInvoiceMoney, value); }
        }


        private string _IsInvoiceRatio;
        [XafDisplayName("发票比例%")]
        public string IsInvoiceRatio
        {
            get { return _IsInvoiceRatio; }
            set { SetPropertyValue<string>(nameof(IsInvoiceRatio), ref _IsInvoiceRatio, value); }
        }


        private DateTime _PlanReceiptsTime;
        [XafDisplayName("计划收款时间")]
        public DateTime PlanReceiptsTime
        {
            get { return _PlanReceiptsTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanReceiptsTime), ref _PlanReceiptsTime, value); }
        }


        private DateTime _RealReceiptsTime;
        [XafDisplayName("实际收款时间")]
        public DateTime RealReceiptsTime
        {
            get { return _RealReceiptsTime; }
            set { SetPropertyValue<DateTime>(nameof(RealReceiptsTime), ref _RealReceiptsTime, value); }
        }


        private decimal _ReceiptsMoney;
        [XafDisplayName("收款金额¥")]
        public decimal ReceiptsMoney
        {
            get { return _ReceiptsMoney; }
            set { SetPropertyValue<decimal>(nameof(ReceiptsMoney), ref _ReceiptsMoney, value); }
        }


        private string _ReceiptsRatio;
        [XafDisplayName("收款比例%")]
        public string ReceiptsRatio
        {
            get { return _ReceiptsRatio; }
            set { SetPropertyValue<string>(nameof(ReceiptsRatio), ref _ReceiptsRatio, value); }
        }
    }
}
