using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.ProjectManagement;
using CRM.Module.BusinessObjects.SellManagement;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CRM.Module.BusinessObjects.Sys_Management
{
    [DefaultClassOptions]
    [ImageName("BO_User")]
    [DefaultProperty("FullName")]
    [NavigationItem("系统管理")]
    [XafDisplayName("用户配置")]
    public class SysUser : BaseObject, ISecurityUser, IAuthenticationStandardUser, ISecurityUserWithRoles, IOperationPermissionProvider
    {

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            SysRoles.Add((SysRole)ReturnDefaultRole());
        }

        private SysRole ReturnDefaultRole()
        {
            SysRole defaultRole = Session.FindObject<SysRole>(new BinaryOperator("Name", "Default"));
            return defaultRole;
        }

        private Department _Department;
        private bool isActive = true;
        [XafDisplayName("是否激活")]
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                SetPropertyValue("IsActive", ref isActive, value);
            }
        }
        private string userName = String.Empty;
        [XafDisplayName("用户名")]
        [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,"用户登录名已存在")]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                SetPropertyValue("UserName", ref userName, value);
            }
        }
        //public bool IsDepartment管理员 { get; set; }

        private bool changePasswordOnFirstLogon;
        [XafDisplayName("是否第一次登录重置密码")]
        public bool ChangePasswordOnFirstLogon
        {
            get
            {
                return changePasswordOnFirstLogon;
            }
            set
            {
                SetPropertyValue("ChangePasswordOnFirstLogon", ref changePasswordOnFirstLogon, value);
            }
        }
        [Browsable(false),
        Size(SizeAttribute.Unlimited),
        Persistent,
        SecurityBrowsable]
        [XafDisplayName("保存密码")]
        protected string StoredPassword { get; set; }
        
        public bool ComparePassword(string password)
        {
            return SecurityUserBase.ComparePassword(StoredPassword, password);
        }
        
        public void SetPassword(string password)
        {
            StoredPassword = new PasswordCryptographer().GenerateSaltedPassword(password);
            OnChanged("StoredPassword");
        }
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (SysRole role in SysRoles)
                {
                    result.Add(role);
                }
                return result;
            }
        }
        IEnumerable<IOperationPermission> IOperationPermissionProvider.GetPermissions()
        {
            return new IOperationPermission[0];
        }
        IEnumerable<IOperationPermissionProvider> IOperationPermissionProvider.GetChildren()
        {
            return new EnumerableConverter<IOperationPermissionProvider, SysRole>(SysRoles);
        }

        public SysUser(DevExpress.Xpo.Session session)
            : base(session)
        {
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            if (Session.CollectReferencingObjects(this).Count > 0)
            {
                var usedby = string.Empty;
                var i = 0;
                foreach (var a in Session.CollectReferencingObjects(this))
                {
                    i++;
                    usedby = usedby + a.GetType().ToString() + ":" + a.ToString() + "; ";
                    if (i > 2)
                    {
                        break;
                    }
                }
                throw new Exception("不能删除！例如下列对象使用了要删除的内容：" + ToString() + "\r\n" + usedby);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("SysUsers-SysRoles")]
        [XafDisplayName("所属权限")]
        public XPCollection<SysRole> SysRoles
        {
            get
            {
                return GetCollection<SysRole>("SysRoles");
            }
        }
        private System.String _FullName;
        [XafDisplayName("姓名")]
        [RuleRequiredField]
        public System.String FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                SetPropertyValue("FullName", ref _FullName, value);
            }
        }

        //private SysUser _Superior;
        //public SysUser Superior
        //{
        //    get { return _Superior; }
        //    set { SetPropertyValue("Superior", ref _Superior, value); }
        //}

        private Company _Company;
        [XafDisplayName("所属公司")]
        [Association("Company-SysUsers")]
        public Company Company
        {
            get { return _Company; }
            set { SetPropertyValue("Company", ref _Company, value); }
        }

        [Association("Department-SysUsers")]
        [XafDisplayName("所属部门")]
        [DataSourceProperty("Company.Departments")]
        public Department Department
        {
            get
            {
                return _Department;
            }
            set
            {
                SetPropertyValue("Department", ref _Department, value);
            }
        }

        private Section _Section;
        [XafDisplayName("所属科室")]
        [Association("Section-SysUsers")]
        [DataSourceProperty("Department.Sections")]
        public Section Section
        {
            get { return _Section; }
            set { SetPropertyValue("Section", ref _Section, value); }
        }

        private Position _Position;

        [Association("Position-SysUsers")]
        [XafDisplayName("职务")]
        public Position Position
        {
            get { return _Position; }
            set { SetPropertyValue<Position>(nameof(Position), ref _Position, value); }
        }


        private string _Email=String.Empty;
        [RuleRequiredField("EmailRequired", DefaultContexts.Save)]
        [RuleUniqueValue("EmailIsUnique", DefaultContexts.Save,"Email已存在")]
        [XafDisplayName("邮箱")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        private string _Address;
        [XafDisplayName("联系地址")]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue("Address", ref _Address, value); }
        }

        private string _Telephone;
        [XafDisplayName("联系电话")]
        public string Telephone
        {
            get { return _Telephone; }
            set { SetPropertyValue("Telephone", ref _Telephone, value); }
        }

        private DateTime _Birthday;
        [XafDisplayName("出生日期")]
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { SetPropertyValue("Birthday", ref _Birthday, value); }
        }
        private string _OpenID;
        // [RuleUniqueValue("OPENID不能重复", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        [XafDisplayName("OpenID")]
        public string OpenID
        {
            get { return _OpenID; }
            set { SetPropertyValue<string>(nameof(OpenID), ref _OpenID, value); }
        }


        private string _ApplyStaffId;
        [XafDisplayName("员工编号")]
        [RuleUniqueValue("员工编号不能重复", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction)]
        public string ApplyStaffId
        {
            get { return _ApplyStaffId; }
            set { SetPropertyValue<string>(nameof(ApplyStaffId), ref _ApplyStaffId, value); }
        }


        private string _UnionID;
        public string UnionID
        {
            get { return _UnionID; }
            set { SetPropertyValue<string>(nameof(UnionID), ref _UnionID, value); }
        }


        private string _Comment;
        [XafDisplayName("注册附属说明")]
        public string Comment
        {
            get { return _Comment; }
            set { SetPropertyValue<string>(nameof(Comment), ref _Comment, value); }
        }




        [Association("SysUser-Customers")]
        [XafDisplayName("客户")]
        public XPCollection<Customer> Customers
        {
            get { return GetCollection<Customer>(nameof(Customers)); }
        }

        [Association("SysUser-CustomerCompanies")]
        [XafDisplayName("客户公司")]
        public XPCollection<CustomerCompany> CustomerCompanies
        {
            get { return GetCollection<CustomerCompany>(nameof(CustomerCompanies)); }
        }

        [Association("SysUser-ProjectProgresses")]
        [XafDisplayName("项目进度")]
        public XPCollection<ProjectProgress> ProjectProgresses
        {
            get { return GetCollection<ProjectProgress>(nameof(ProjectProgresses)); }
        }

        [Association("SysUser-Projects")]
        [XafDisplayName("项目信息")]
        public XPCollection<Project> Projects
        {
            get { return GetCollection<Project>(nameof(Projects)); }
        }

        [Association("SysUser-PDFFiles")]
        [XafDisplayName("附件上传")]
        public XPCollection<PDFFile> PDFFiles
        {
            get { return GetCollection<PDFFile>(nameof(PDFFiles)); }
        }

        [Association("SysUser-Orders")]
        [XafDisplayName("合同")]
        public XPCollection<Order> Orders
        {
            get { return GetCollection<Order>(nameof(Orders)); }
        }

        [Association("SysUser-InvoiceReceipts")]
        [XafDisplayName("开票收款")]
        public XPCollection<InvoiceReceipt> InvoiceReceipts
        {
            get { return GetCollection<InvoiceReceipt>(nameof(InvoiceReceipts)); }
        }


        //[Association("SysUser-SellProjects")]
        //public XPCollection<SellProject> SellProjects
        //{
        //    get { return GetCollection<SellProject>(nameof(SellProjects)); }
        //}


        //[Association("SysUser-AddProjects")]
        //public XPCollection<AddProject> AddProjects
        //{
        //    get { return GetCollection<AddProject>(nameof(AddProjects)); }
        //}



        //[Association("SysUser-AssignedTo")]
        //public XPCollection<Issue> Issues
        //{
        //    get { return GetCollection<Issue>(nameof(Issues)); }
        //}
        //[Association("SysUser-SendTo")]
        //public XPCollection<TaskManagement> TaskManagements
        //{
        //    get { return GetCollection<TaskManagement>(nameof(TaskManagements)); }
        //}



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
