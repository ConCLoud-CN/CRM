using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.ExpressApp.DC;
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

namespace CRM.Module.BusinessObjects.CustomerInformationManagement
{
    [NavigationItem("客户信息管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("Customer")]
    [XafDisplayName("客户人员信息")]
    //[DefaultProperty("Name")]
    public class Customer:BaseObject
    {
        public Customer(Session s) : base(s) { }

        private string _Name;
        [XafDisplayName("客户姓名")]
        [RuleRequiredField("NameRequired", DefaultContexts.Save)]//必填项
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue<string>(nameof(Name), ref _Name, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            SysUser = CommUtilities.GetCurrentUser(Session);
        }

        public enum Sex { 男, 女 }

        private Sex _Sex;
        [XafDisplayName("客户性别")]
        public Sex CSex
        {
            get { return _Sex; }
            set { SetPropertyValue<Sex>(nameof(Sex), ref _Sex, value); }
        }


        private string _Position;
        [XafDisplayName("客户职位")]
        public string Position
        {
            get { return _Position; }
            set { SetPropertyValue<string>(nameof(Position), ref _Position, value); }
        }




        private string _Phone1;
        [XafDisplayName("联系电话1")]
        //[RuleRequiredField("Phone1Required", DefaultContexts.Save)]//必填项
        public string Phone1
        {
            get { return _Phone1; }
            set { SetPropertyValue<string>(nameof(Phone1), ref _Phone1, value); }
        }




        private string _Phone2;
        [XafDisplayName("联系电话2")]
        public string Phone2
        {
            get { return _Phone2; }
            set { SetPropertyValue<string>(nameof(Phone2), ref _Phone2, value); }
        }



        private string _Adress;
        [XafDisplayName("住址")]
        public string Adress
        {
            get { return _Adress; }
            set { SetPropertyValue<string>(nameof(Adress), ref _Adress, value); }
        }



        private string _Origo;
        [XafDisplayName("籍贯")]
        public string Origo
        {
            get { return _Origo; }
            set { SetPropertyValue<string>(nameof(Origo), ref _Origo, value); }
        }



        private string _E_mail;
        [XafDisplayName("邮箱")]
        public string E_mail
        {
            get { return _E_mail; }
            set { SetPropertyValue<string>(nameof(E_mail), ref _E_mail, value); }
        }



        private string _Interest;
        [XafDisplayName("兴趣"),Size(300)]
        public string Interest
        {
            get { return _Interest; }
            set { SetPropertyValue<string>(nameof(Interest), ref _Interest, value); }
        }






        private string _Remark;
        [XafDisplayName("备注"), Size(300)]
        public string Remark
        {
            get { return _Remark; }
            set { SetPropertyValue<string>(nameof(Remark), ref _Remark, value); }
        }



        
        private CustomerCompany _CustomerCompany;
        [Association("CustomerCompany-Customers")]
        [XafDisplayName("公司名称")]
        [RuleRequiredField("CustomerCompanyRequired", DefaultContexts.Save)]//必填项
        public CustomerCompany CustomerCompany
        {
            get { return _CustomerCompany; }
            set { SetPropertyValue<CustomerCompany>(nameof(CustomerCompany), ref _CustomerCompany, value); }
        }


        private SysUser _SysUser;
        [Association("SysUser-Customers")]
        [XafDisplayName("姓名")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }


    }
}
