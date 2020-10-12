using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Security;
using CRM.Module.Controllers;

namespace TaskManagementSystem.Module.Controllers
{
    public partial class ListViewFilterPermissionController : ViewController
    {
        private IList<ListViewFilterPermissionLite> _Permissions = null;

        public ListViewFilterPermissionController()
        {
            InitializeComponent();
            RegisterActions(components);

            TargetViewType = ViewType.ListView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            if (_Permissions == null)
                _Permissions = ListViewFilterPermission.GetListViewPermissions(ObjectSpace, (ISecurityUserWithRoles)SecuritySystem.CurrentUser);

            CriteriaOperator criteria = ListViewFilterPermission.GetCriteriaOperator(ObjectSpace, (ISecurityUserWithRoles)SecuritySystem.CurrentUser, _Permissions, View.Id, View.ObjectTypeInfo.Type);
            if (criteria != null)
                ((ListView)View).CollectionSource.Criteria["ListViewFilterPermission"] = criteria;
        }
    }
}