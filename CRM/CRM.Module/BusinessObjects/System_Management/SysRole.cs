using System;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Utils;
using CRM.Module.Controllers;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Security;
using CRM.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.BaseImpl;

namespace CRM.Module.BusinessObjects.Sys_Management
{
    [DefaultClassOptions]
    [ImageName("BO_Role")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.None)]
    [NavigationItem("系统管理")]
    [XafDisplayName("权限配置")]
    //[MapInheritance(MapInheritanceType.ParentTable)]
    public class SysRole : DevExpress.ExpressApp.Security.Strategy.SecuritySystemRoleBase
    {
        public SysRole(DevExpress.Xpo.Session session)
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
        [VisibleInListView(true)]
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("所有用户")]
        public string Users_Included
        {
            get
            {
                string ul = "";
                foreach (SysUser u in SysUsers)
                {
                    ul = ul + u.FullName + " ";
                }
                return ul;
            }

        }
        [VisibleInListView(true)]
        [Size(SizeAttribute.Unlimited)]
        [XafDisplayName("所有权限")]
        public string TypePermissions_Included
        {
            get
            {
                string ul = "";
                foreach (SecuritySystemTypePermissionObject u in TypePermissions)
                {
                    ul = ul + u.Object.ToString() + " ";
                }
                return ul;
            }

        }

        string _Notes;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(true)]
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                SetPropertyValue(nameof(Notes), ref _Notes, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("SysUsers-SysRoles")]
        [XafDisplayName("包含用户")]
        public XPCollection<SysUser> SysUsers
        {
            get
            {
                return GetCollection<SysUser>("SysUsers");
            }
        }
        protected override IEnumerable<DevExpress.ExpressApp.Security.IOperationPermissionProvider> GetChildrenCore()
        {
            var result = new List<DevExpress.ExpressApp.Security.IOperationPermissionProvider>();
            result.AddRange(base.GetChildrenCore());
            result.AddRange(new EnumerableConverter<DevExpress.ExpressApp.Security.IOperationPermissionProvider, SysRole>(ChildRoles));
            return result;
        }
        [Association("ParentRoles-ChildRoles")]
        [XafDisplayName("子权限")]
        public XPCollection<SysRole> ChildRoles
        {
            get
            {
                return GetCollection<SysRole>("ChildRoles");
            }
        }
        [Association("ParentRoles-ChildRoles")]
        [XafDisplayName("父权限")]
        public XPCollection<SysRole> ParentRoles
        {
            get
            {
                return GetCollection<SysRole>("ParentRoles");
            }
        }
        [DevExpress.Xpo.Aggregated]
        [Association("SysRole-ListViewFilterPermissions")]
        public XPCollection<ListViewFilterPermission> ListViewFilterPermissions
        {
            get
            {
                return GetCollection<ListViewFilterPermission>("ListViewFilterPermissions");
            }
        }

        private string _HiddenNavigationItems;
        [XafDisplayName("隐藏导航菜单名称")]
        [ToolTip("使用英文逗号分隔可以输入多个")]
        public string HiddenNavigationItems
        {
            get { return _HiddenNavigationItems; }
            set { SetPropertyValue("HiddenNavigationItems", ref _HiddenNavigationItems, value); }
        }
        protected override IEnumerable<IOperationPermission> GetPermissionsCore()
        {
            List<IOperationPermission> result = new List<IOperationPermission>();
            result.AddRange(base.GetPermissionsCore());
            if (!String.IsNullOrEmpty(HiddenNavigationItems))
            {
                foreach (string hiddenNavigationItem in HiddenNavigationItems.Split(';', ','))
                {
                    result.Add(new NavigationItemPermission(hiddenNavigationItem.Trim()));
                }
            }
            return result;
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
