using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
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
    [Persistent("OperationService")]
    [XafDisplayName("运维服务记录")]
    public class OperationService:BaseObject
    {
        public OperationService(Session s) : base(s) { }


        private DateTime _HappenDate;
        [XafDisplayName("问题发生日期")]
        public DateTime HappenDate
        {
            get { return _HappenDate; }
            set { SetPropertyValue<DateTime>(nameof(HappenDate), ref _HappenDate, value); }
        }


        private string _CName;
        [XafDisplayName("问题发起人(客户姓名)")]
        public string CName
        {
            get { return _CName; }
            set { SetPropertyValue<string>(nameof(CName), ref _CName, value); }
        }

        public enum PassMethod { 电话,微信,邮件,其他 }

        private PassMethod _PassMd;
        [XafDisplayName("问题传递方式")]
        public PassMethod PassMd
        {
            get { return _PassMd; }
            set { SetPropertyValue<PassMethod>(nameof(PassMd), ref _PassMd, value); }
        }


        private string _Description;
        [XafDisplayName("问题描述"),Size(300)]
        public string Description
        {
            get { return _Description; }
            set { SetPropertyValue<string>(nameof(Description), ref _Description, value); }
        }


        private string _Solution;
        [XafDisplayName("解决对策"),Size(300)]
        public string Solution
        {
            get { return _Solution; }
            set { SetPropertyValue<string>(nameof(Solution), ref _Solution, value); }
        }


        [Association, DevExpress.Xpo.Aggregated]
        public XPCollection<Record> Records
        {
            get { return GetCollection<Record>("Records"); }
        }

    }

    [Persistent("Record")]
    [XafDisplayName("问题解决日志记录")]
    public class Record : BaseObject
    {

        public Record(Session s) : base(s) { }


        private OperationService _OperatService;
        [Association]
        [XafDisplayName("问题解决日志记录")]
        public OperationService OperatService
        {
            get { return _OperatService; }
            set { SetPropertyValue<OperationService>(nameof(OperatService), ref _OperatService, value); }
        }

        private int _Serial;
        [XafDisplayName("问题顺序")]
        public int Serial
        {
            get { return _Serial; }
            set { SetPropertyValue<int>(nameof(Serial), ref _Serial, value); }
        }


        private string _ProblemRecord;
        [XafDisplayName("问题解决日志记录")]
        public string ProblemRecord
        {
            get { return _ProblemRecord; }
            set { SetPropertyValue<string>(nameof(ProblemRecord), ref _ProblemRecord, value); }
        }


        private int _WorkRecord;
        [XafDisplayName("工时记录(min)")]
        public int WorkRecord
        {
            get { return _WorkRecord; }
            set { SetPropertyValue<int>(nameof(WorkRecord), ref _WorkRecord, value); }
        }


        [Association, DevExpress.Xpo.Aggregated]
        [XafDisplayName("工时统计记录")]
        public XPCollection<WorkingTotal> WorkingTotals
        {
            get { return GetCollection<WorkingTotal>(nameof(WorkingTotals)); }
        }

    }


    [XafDisplayName("工时统计")]
    public class WorkingTotal : BaseObject
    {
        public WorkingTotal(Session s) : base(s) { }


        private Record _RName;
        [Association]
        [XafDisplayName("日志记录")]
        public Record RName
        {
            get { return _RName; }
            set { SetPropertyValue<Record>(nameof(RName), ref _RName, value); }
        }



        private int _TotalMinutes;
        [XafDisplayName("工时汇总(min)")]
        public int TotalMinutes
        {
            get { return _TotalMinutes; }
            set { SetPropertyValue<int>(nameof(TotalMinutes), ref _TotalMinutes, value); }
        }


        private double _TotalHours;
        [XafDisplayName("工时汇总(h)")]
        public double TotalHours
        {
            get { return _TotalHours; }
            set { SetPropertyValue<double>(nameof(TotalHours), ref _TotalHours, value); }
        }


        private string _RecordName;
        [XafDisplayName("记录人")]
        public string RecordName
        {
            get { return _RecordName; }
            set { SetPropertyValue<string>(nameof(RecordName), ref _RecordName, value); }
        }

    }
}
