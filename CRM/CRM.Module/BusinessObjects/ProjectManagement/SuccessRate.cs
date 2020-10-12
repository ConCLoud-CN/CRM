using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Module.BusinessObjects.ProjectManagement
{
    //[NavigationItem("项目管理")]
    //[DefaultClassOptions]//默认类选项
    //[Persistent("SuccessRate")]
    [XafDisplayName("销售承接率")]
    public class SuccessRate:BaseObject
    {
        public SuccessRate(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private double _SRate;
        [XafDisplayName("销售承接率")]
        [RuleRequiredField("SRateRequired", DefaultContexts.Save)]//必填项
        public double SRate
        {
            get 
            { 

                return _SRate; 
            }
            set { SetPropertyValue<double>(nameof(SRate), ref _SRate, value); }
        }


        private DateTime _CreateTime;
        [XafDisplayName("创建时间")]
        [RuleRequiredField("CreateTimeRequired", DefaultContexts.Save)]//必填项
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { SetPropertyValue<DateTime>(nameof(CreateTime), ref _CreateTime, value); }
        }



    }
}
