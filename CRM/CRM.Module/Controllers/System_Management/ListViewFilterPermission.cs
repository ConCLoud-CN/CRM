using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CRM.Module.Controllers
{
    [ImageName("BO_Security_Permission_Type")]
    [XafDisplayName("列表视图筛选")]
    public class ListViewFilterPermission : XPBaseObject
    {
        [Key]
        [Browsable(false)]
        public string FilterPermissionId
        {
            get { return GetPropertyValue<string>("FilterPermissionId"); }
            set { SetPropertyValue<string>("FilterPermissionId", value); }
        }
        [DevExpress.Xpo.DisplayName("角色")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("SysRole-ListViewFilterPermissions")]
        public SysRole Role
        {
            get { return GetPropertyValue<SysRole>("Role"); }
            set { SetPropertyValue<SysRole>("Role", value); }
        }

        [DevExpress.Xpo.DisplayName("数据类型")]
        [TypeConverter(typeof(SecurityStrategyTargetTypeConverter))]
        [ValueConverter(typeof(TypeToStringConverter))]
        public Type TargetType
        {
            get { return GetPropertyValue<Type>("TargetType"); }
            set { SetPropertyValue<Type>("TargetType", value); }
        }

        //[VisibleInListView(true)]
        //[Size(4000)]
        //public string Criteria
        //{
        //    get { return GetPropertyValue<string>("Criteria"); }
        //    set { SetPropertyValue<string>("Criteria", value); }
        //}
        [DevExpress.Xpo.DisplayName("筛选条件")]
        [CriteriaOptions("TargetType"), Size(4000)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        [VisibleInListView(true)]
        //[Size(4000)]
        public string Criteria
        {
            get { return GetPropertyValue<string>("Criteria"); }
            set { SetPropertyValue<string>("Criteria", value); }
        }

        [DevExpress.Xpo.DisplayName("列表视图Ids")]
        [VisibleInListView(true)]
        [Size(4000)]
        public string ListViewIds
        {
            get { return GetPropertyValue<string>("ListViewIds"); }
            set { SetPropertyValue<string>("ListViewIds", value); }
        }
        public ListViewFilterPermission(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            FilterPermissionId = Guid.NewGuid().ToString();
        }

        public static CriteriaOperator GetCriteriaOperator(IObjectSpace os, ISecurityUserWithRoles user, IList<ListViewFilterPermissionLite> permissionLites, string viewId, Type objectType)
        {
            CriteriaOperator criteria = null;
            foreach (var g in permissionLites.GroupBy(pl => pl.RoleId))
            {
                CriteriaOperator c = null;
                foreach (var permissionLite in g.ToArray())
                {
                    if ((!string.IsNullOrEmpty(permissionLite.ListViewId) && permissionLite.ListViewId == viewId) ||
                        (string.IsNullOrEmpty(permissionLite.ListViewId) && permissionLite.TargetType == objectType))
                    {
                        c = CriteriaOperator.And(c, CriteriaOperator.Parse(permissionLite.Criteria));
                    }
                }
                criteria = CriteriaOperator.Or(criteria, c);
            }
            return criteria;
        }

        public static IList<ListViewFilterPermissionLite> GetListViewPermissions(IObjectSpace os, ISecurityUserWithRoles user)
        {
            IList<ListViewFilterPermissionLite> permissionLites = new List<ListViewFilterPermissionLite>();

            List<Guid> roleIds = user.Roles.Cast<SysRole>().Select(r => r.Oid).ToList();
            CriteriaOperator criteria = new InOperator("Role.Oid", roleIds);

            IList<ListViewFilterPermission> permissions = os.GetObjects<ListViewFilterPermission>(criteria);
            foreach (var permission in permissions)
            {
                if (!string.IsNullOrEmpty(permission.ListViewIds))
                {
                    string[] listViewIds = permission.ListViewIds.Split(',');
                    foreach (string listViewId in listViewIds)
                    {
                        if (!string.IsNullOrEmpty(listViewId))
                            permissionLites.Add(new ListViewFilterPermissionLite(permission.Role.Oid, listViewId, permission.TargetType, permission.Criteria));
                    }
                }
                else
                    permissionLites.Add(new ListViewFilterPermissionLite(permission.Role.Oid, string.Empty, permission.TargetType, permission.Criteria));
            }
            return permissionLites;
        }
    }
}
