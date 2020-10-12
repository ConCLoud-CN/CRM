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

namespace CRM.Module.BusinessObjects.Sys_Management
{
    [NavigationItem("系统管理")]
    [XafDisplayName("职务配置")]
    [DefaultClassOptions]
    [DefaultProperty("PositionName")]
     public class Position : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Position(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }


        private string _PositionName;
        [XafDisplayName("职务名称")]
        [RuleUniqueValue("PositionValue", DefaultContexts.Save, "Position已存在")]
        public string PositionName
        {
            get { return _PositionName; }
            set { SetPropertyValue("PositionName", ref _PositionName, value); }
        }

        //[Association("Posion-Subordinates"),DevExpress.Xpo.Aggregated]
        //public XPCollection<Subordinate> Subordinates
        //{
        //    get
        //    {
        //        return GetCollection<Subordinate>("Subordinates");
        //    }
        //}

        [Association("Position-SysUsers")]
        [XafDisplayName("职务下属员工信息")]
        public XPCollection<SysUser> SysUsers
        {
            get
            {
                return GetCollection<SysUser>("SysUsers");
            }
        }



        private Company _Company;
        [XafDisplayName("所属公司")]
        [Association("Company - Positions")]
        public Company Company
        {
            get { return _Company; }
            set { SetPropertyValue<Company>(nameof(Company), ref _Company, value); }
        }



    }

    //public class Subordinate : Position
    //{
    //    public Subordinate(Session session) : base(session) { }
    //    public override void AfterConstruction()
    //    {
    //        base.AfterConstruction();
    //        // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    //    }

    //    private Position _Superior;
    //    [XafDisplayName("直属领导")]
    //    [Association("Posion-Subordinates")]
    //    public Position Superior
    //    {
    //        get { return _Superior; }
    //        set { SetPropertyValue("Position", ref _Superior, value); }
    //    }


    //}
}