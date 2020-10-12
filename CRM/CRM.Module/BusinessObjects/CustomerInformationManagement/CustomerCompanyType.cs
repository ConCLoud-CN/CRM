using DevExpress.ExpressApp;
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

namespace CRM.Module.BusinessObjects.CustomerInformationManagement
{
    [NavigationItem("客户信息管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("CustomerCompanyType")]
    [XafDisplayName("客户公司分类")]
    [ImageName("BO_Contact")]
    public class CustomerCompanyType : BaseObject
    {
        public CustomerCompanyType(Session s) : base(s) { }

        //公司类型编号
        
        //private string _CptCode;
        ////[RuleRequiredField("CptCodeRequired", DefaultContexts.Save)]
        ////[RuleUniqueValue("CptCodeIsUnique", DefaultContexts.Save, "CptCode已存在")]
        //[XafDisplayName("公司类型编号")]
        //public string CptCode
        //{
        //    get { return _CptCode; }
        //    set { SetPropertyValue<string>(nameof(CptCode), ref _CptCode, value); }
        //}

        //公司类型名称
        private string _CptName;
        [XafDisplayName("公司类型名称")]
        [RuleRequiredField("CptCodeRequired", DefaultContexts.Save)]
        [RuleUniqueValue("CptNameIsUnique", DefaultContexts.Save, "CptName已存在")]
        public string CptName
        {
            get { return _CptName; }
            set { SetPropertyValue<string>(nameof(CptName), ref _CptName, value); }
        }


        [Association("CustomerCompanyType-CustomerCompanies")]
        [XafDisplayName("所有公司信息")]
        public XPCollection<CustomerCompany> CustomerCompanies
        {
            get { return GetCollection<CustomerCompany>(nameof(CustomerCompanies)); }
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


    }
}
