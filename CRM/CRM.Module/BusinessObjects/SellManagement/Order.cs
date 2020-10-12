using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.ProjectManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.DataProcessing;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.SellManagement
{
    [NavigationItem("销售管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("Order")]
    [XafDisplayName("合同订单")]
    [DefaultProperty("COName")]
    [Appearance("ActionVisibility", AppearanceItemType = "ViewItem",TargetItems = "AddProject", Criteria = "Tp = '立项'", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("ActionVisibility", AppearanceItemType = "ViewItem", TargetItems = "AddProject", Criteria = "Tp = '立项'", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("RuleMethod1", AppearanceItemType = "ViewItem", TargetItems = "AddProject", Criteria = "Tp = '立项'")]
    public class Order: BaseObject
    {
        public Order(Session s) : base(s) { }

        protected override void OnSaving()
        {

            base.OnSaving();

        }
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
        //            flagString = "HT-" + CommUtilities.GetCurrentServerTime(Session).ToString("yyyyMMdd").Substring(2, 2);
        //        }
        //        else
        //        {
        //            flagString = "HT-" + CreateTime.ToString("yyyyMMdd").Substring(2, 2);
        //        }
        //        var indexNo = DistributedIdGeneratorHelper.Generate(this.Session.DataLayer, flagString, string.Empty);
        //        Code = flagString + indexNo.ToString().PadLeft(3, '0');

        //    }
        //}

        private string _Code;
        [XafDisplayName("合同编号")]
        public string Code
        {
            get { return _Code; }
            set { SetPropertyValue<string>(nameof(Code), ref _Code, value); }
        }
        

        private string _OName;
        [XafDisplayName("合同名称")]
        public string OName
        {
            get { return _OName; }
            set { SetPropertyValue<string>(nameof(OName), ref _OName, value); }
        }


        private string _COName;
        [XafDisplayName("合同名称")]
        public string COName
        {
            get 
            {
                if (Code!=null&& OName!=null)
                {
                    _COName = "[ " + Code + " ] " + OName;
                }
                return _COName; 
            }
            set { SetPropertyValue<string>(nameof(COName), ref _COName, value); }
        }


        private CustomerCompany _CpName;
        [XafDisplayName("公司名称")]
        [RuleRequiredField("CpNameRequired", DefaultContexts.Save)]//必填项
        public CustomerCompany CpName
        {
            get { return _CpName; }
            set { SetPropertyValue<CustomerCompany>(nameof(CpName), ref _CpName, value); }
        }


        private Project _Project;
        [XafDisplayName("项目名称")]
        [DataSourceProperty("CpName.Projects")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public Project Project
        {
            get
            {
                return _Project;
            }
            set { SetPropertyValue<Project>(nameof(Project), ref _Project, value); }
        }


        private decimal _Tax_Money;
        [XafDisplayName("金额(含税)¥")]
        public decimal Tax_Money
        {
            get { return _Tax_Money; }
            set { SetPropertyValue<decimal>(nameof(Tax_Money), ref _Tax_Money, value); }
        }


        private Dutye _DutyGrade;
        [XafDisplayName("税率")]
        public Dutye DutyGrade
        {
            get 
            { 
                return _DutyGrade;
            }
            set { SetPropertyValue<Dutye>(nameof(DutyGrade), ref _DutyGrade, value); }
        }


        private decimal _Money;
        [XafDisplayName("金额(未含税)¥")]
        public decimal Money
        {
            get
            {
                if (DutyGrade != null)
                {
                    decimal d1 = 0.06m;
                    decimal d2 = 0.13m;

                    if (DutyGrade.Type.Equals("6%"))
                    {
                        _Money = Tax_Money - (Tax_Money * d1);
                    }
                    else if (DutyGrade.Type.Equals("13%"))
                    {
                        _Money = Tax_Money - (Tax_Money * d2);
                    }
                    else
                    {
                        return _Money;
                    }

                }

                return _Money;
            }
            set { SetPropertyValue<decimal>(nameof(Money), ref _Money, value); }
        }


        private string _Address;
        [XafDisplayName("实施地")]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
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


        [Association("Order-PDFFiles")]
        [XafDisplayName("附件上传")]
        public XPCollection<PDFFile> PDFFiles
        {
            get { return GetCollection<PDFFile>(nameof(PDFFiles)); }
        }


        private SysUser _SysUser;
        [Association("SysUser-Orders")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }





        //private decimal _Money;
        //[XafDisplayName("金额(未含税)¥")]
        //public decimal Money
        //{

        //    get 
        //    {
        //        decimal d1 = 0.13m;
        //        decimal d2 = 0.16m;

        //        if (DutyGrade == 0)
        //        {
        //            _Money = Tax_Money - (Tax_Money * d1);
        //        }
        //        else
        //        {
        //            _Money = Tax_Money - (Tax_Money * d2);
        //        }

        //        return _Money;
        //    }
        //    set { SetPropertyValue<decimal>(nameof(Money), ref _Money, value); }
        //}

        //public enum Duty { 百分之十三, 百分之十六 }

        //private Duty _DutyGrade;
        //[XafDisplayName("税率")]
        //public Duty DutyGrade
        //{
        //    get { return _DutyGrade; }
        //    set { SetPropertyValue<Duty>(nameof(DutyGrade), ref _DutyGrade, value); }
        //}



        private string _Payway;
        [XafDisplayName("付款方式"),Size(300)]
        public string Payway
        {
            get { return _Payway; }
            set { SetPropertyValue<string>(nameof(Payway), ref _Payway, value); }
        }

        private DateTime _CreateTime;
        [XafDisplayName("签订时间")]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { SetPropertyValue<DateTime>(nameof(CreateTime), ref _CreateTime, value); }
        }


    }

    //[NavigationItem("销售管理")]
    [DefaultClassOptions]//默认类选项
    [XafDisplayName("税率")]
    [DefaultProperty("Type")]
    public class Dutye : BaseObject 
    {
        public Dutye(Session s) :base(s) { }


        private string _Type;
        [XafDisplayName("税率名称")]
        public string Type
        {
            get { return _Type; }
            set { SetPropertyValue<string>(nameof(Type), ref _Type, value); }
        }

    }

}
