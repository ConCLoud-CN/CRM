using CRM.Module.BusinessObjects.CustomerInformationManagement;
using DevExpress.ExpressApp.DC;
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
    [Persistent("CustomerComplaint")]
    [DefaultClassOptions]//默认类选项
    [XafDisplayName("客户投诉记录")]
    public class CustomerComplaint:BaseObject
    {
        public CustomerComplaint(Session s) : base(s) { }


        private CustomerCompany _CCpName;
        [XafDisplayName("客户公司名称")]
        public CustomerCompany CCpName
        {
            get { return _CCpName; }
            set { SetPropertyValue<CustomerCompany>(nameof(CCpName), ref _CCpName, value); }
        }


        private string _Name;
        [XafDisplayName("客户姓名")]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }

        public enum CSex { 男,女}

        private CSex _Sex;
        [XafDisplayName("性别")]
        public CSex Sex
        {
            get { return _Sex; }
            set { SetPropertyValue<CSex>(nameof(Sex), ref _Sex, value); }
        }

        private string _Position;
        [XafDisplayName("客户职位")]
        public string Position
        {
            get { return _Position; }
            set { SetPropertyValue<string>(nameof(Position), ref _Position, value); }
        }

        private string _Phone1;
        [XafDisplayName("联系电话")]
        public string Phone1
        {
            get { return _Phone1; }
            set { SetPropertyValue<string>(nameof(Phone1), ref _Phone1, value); }
        }

        private string _Phone2;
        [XafDisplayName("联系电话")]
        public string Phone2
        {
            get { return _Phone2; }
            set { SetPropertyValue<string>(nameof(Phone2), ref _Phone2, value); }
        }

        private string _E_mail;
        [XafDisplayName("邮箱")]
        public string E_mail
        {
            get { return _E_mail; }
            set { SetPropertyValue<string>(nameof(E_mail), ref _E_mail, value); }
        }

        private string _Content;
        [XafDisplayName("投诉内容"), Size(300)]
        public string Content
        {
            get { return _Content; }
            set { SetPropertyValue<string>(nameof(Content), ref _Content, value); }
        }

    }
}
