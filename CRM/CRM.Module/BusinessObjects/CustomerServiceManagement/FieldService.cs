using CRM.Module.BusinessObjects.CustomerInformationManagement;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.CustomerServiceManagement
{
    [NavigationItem("客户服务管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("FieldService")]
    [XafDisplayName("现场服务记录")]
    public class FieldService : BaseObject
    {
        public FieldService(Session s) : base(s) { }


        private CustomerCompany _CCpName;
        [XafDisplayName("客户名称")]
        public CustomerCompany CCpName
        {
            get { return _CCpName; }
            set { SetPropertyValue<CustomerCompany>(nameof(CCpName), ref _CCpName, value); }
        }



        private string _SysName;
        [XafDisplayName("系统名称")]
        public string SysName
        {
            get { return _SysName; }
            set { SetPropertyValue<string>(nameof(SysName), ref _SysName, value); }
        }


        private string _Contacts;
        [XafDisplayName("联系人")]
        public string Contacts
        {
            get { return _Contacts; }
            set { SetPropertyValue<string>(nameof(Contacts), ref _Contacts, value); }
        }


        private DateTime _EnterDate;
        [XafDisplayName("入场日期")]
        public DateTime EnterDate
        {
            get { return _EnterDate; }
            set { SetPropertyValue<DateTime>(nameof(EnterDate), ref _EnterDate, value); }
        }



        private string _Phone;
        [XafDisplayName("联系人电话")]
        public string Phone
        {
            get { return _Phone; }
            set { SetPropertyValue<string>(nameof(Phone), ref _Phone, value); }
        }



        private DateTime _OutDate;
        [XafDisplayName("出厂日期")]
        public DateTime OutDate
        {
            get { return _OutDate; }
            set { SetPropertyValue<DateTime>(nameof(OutDate), ref _OutDate, value); }
        }


        private string _ServiceDemand;
        [XafDisplayName("服务需求"),Size(300)]
        public string ServiceDemand
        {
            get { return _ServiceDemand; }
            set { SetPropertyValue<string>(nameof(ServiceDemand), ref _ServiceDemand, value); }
        }



        private string _ServiceRecord;
        [XafDisplayName("服务记录"),Size(300)]
        public string ServiceRecord
        {
            get { return _ServiceRecord; }
            set { SetPropertyValue<string>(nameof(ServiceRecord), ref _ServiceRecord, value); }
        }

        public enum Result { 完成,未完成,遗留课题见附件}


        private Result _RState;
        [XafDisplayName("问题处理结果")]
        public Result RState
        {
            get { return _RState; }
            set { SetPropertyValue<Result>(nameof(RState), ref _RState, value); }
        }


        private string _Memo;
        [XafDisplayName("备忘录"),Size(300)]
        public string Memo
        {
            get { return _Memo; }
            set { SetPropertyValue<string>(nameof(Memo), ref _Memo, value); }
        }

        public enum Evaluates { 很满意,满意,一般,不满意}


        private Evaluates _EGrade;
        [XafDisplayName("客户评价")]
        public Evaluates EGrade
        {
            get { return _EGrade; }
            set { SetPropertyValue<Evaluates>(nameof(EGrade), ref _EGrade, value); }
        }



        private string _Advice;
        [XafDisplayName("意见与建议"),Size(300)]
        public string Advice
        {
            get { return _Advice; }
            set { SetPropertyValue<string>(nameof(Advice), ref _Advice, value); }
        }




        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("现场服务-附件")]
        [RuleRequiredField("FSPDFFilesRequired", DefaultContexts.Save)]
        public XPCollection<FSPDFFile> FSPDFFiles
        {
            get { return GetCollection<FSPDFFile>(nameof(FSPDFFiles)); }
        }


    }

    [XafDisplayName("现场服务-附件")]
    public class FSPDFFile : FileAttachmentBase
    {
        public FSPDFFile(Session session)
            : base(session)
        {
        }


        private FieldService _FieldService;
        [Association]
        [XafDisplayName("现场服务")]
        public FieldService FieldService
        {
            get { return _FieldService; }
            set { SetPropertyValue<FieldService>(nameof(FieldService), ref _FieldService, value); }
        }



        private string _PDFName;
        [XafDisplayName("PDF文件名")]
        public string PDFName
        {
            get { return _PDFName; }
            set { SetPropertyValue<string>(nameof(PDFName), ref _PDFName, value); }
        }
    }
}

    
