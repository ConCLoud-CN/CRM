using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Dashboards;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using System;
using System.IO;
using System.Reflection;
// using DevExpress.ExpressApp.Dashboards;
// using TaskManagementSystem.Module.Properties;

namespace CRM.Module.DatabaseUpdate
{
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater 
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) 
        {
        }
        public override void UpdateDatabaseAfterUpdateSchema() 
        {
            base.UpdateDatabaseAfterUpdateSchema();

            //DashboardsModule.AddDashboardData<DashboardData>(
            //ObjectSpace, "任务看板", Resources.Issue_Kanban);
            //// ...
            //ObjectSpace.CommitChanges();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            // 系统自带权限设置
            //PermissionPolicyUser sampleUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User"));
            //if(sampleUser == null) {
            //    sampleUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
            //    sampleUser.UserName = "User";
            //    sampleUser.SetPassword("");
            //}
            //PermissionPolicyRole defaultRole = CreateDefaultRole();
            //sampleUser.Roles.Add(defaultRole);

            //PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
            //if(userAdmin == null) {
            //    userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
            //    userAdmin.UserName = "Admin";
            //    // Set a password if the standard authentication type is used
            //    userAdmin.SetPassword("");
            //}
            //// If a role with the Administrators name doesn't exist in the database, create this role
            //PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            //if(adminRole == null) {
            //    adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
            //    adminRole.Name = "Administrators";
            //}
            //adminRole.IsAdministrative = true;
            //userAdmin.Roles.Add(adminRole);
            // 系统自带权限设置
            CreateDefaultRole();
            SysRole adminRole = ObjectSpace.FindObject<SysRole>(new BinaryOperator("Name", SecurityStrategy.AdministratorRoleName));
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<SysRole>();
                adminRole.Name = SecurityStrategy.AdministratorRoleName;
                adminRole.IsAdministrative = true;
            }
            SysUser userAdmin = ObjectSpace.FindObject<SysUser>(new BinaryOperator("UserName", "Admin"));
            if (userAdmin == null)
            {
                userAdmin = ObjectSpace.CreateObject<SysUser>();
                userAdmin.UserName = "Admin";

                userAdmin.SetPassword(string.Empty);
            }
            SysRole exportRole = ObjectSpace.FindObject<SysRole>(new BinaryOperator("Name", "导出权限"));
            if (exportRole == null)
            {
                exportRole = ObjectSpace.CreateObject<SysRole>();
                exportRole.Name = "导出权限";
            }
            userAdmin.SysRoles.Add(adminRole);
            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() 
        {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}


            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CRM.Module.DashBoard.开票收款报表.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string dbxml = reader.ReadToEnd();
                    DashboardsModule.AddDashboardData<DashboardData>(ObjectSpace, "开票收款报表", dbxml);
                }
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CRM.Module.DashBoard.销售承接率报表.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string dbxml = reader.ReadToEnd();
                    DashboardsModule.AddDashboardData<DashboardData>(ObjectSpace, "销售承接率报表", dbxml);
                }
            }

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CRM.Module.DashBoard.销售金额报表.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string dbxml = reader.ReadToEnd();
                    DashboardsModule.AddDashboardData<DashboardData>(ObjectSpace, "销售金额报表", dbxml);
                }
            }
        }
        // 系统自带权限设置
        //private PermissionPolicyRole CreateDefaultRole() {
        //    PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
        //    if(defaultRole == null) {
        //        defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
        //        defaultRole.Name = "Default";

        //		defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        //      defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        //		defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        //		defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        //      defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
        //      defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
        //      defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
        //		defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
        //        defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
        //    }
        //    return defaultRole;
        //}
        // 系统自带权限设置
        private SysRole CreateDefaultRole()
        {
            SysRole defaultRole = ObjectSpace.FindObject<SysRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<SysRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectAccessPermission<SecuritySystemUser>("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess);
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("ChangePasswordOnFirstLogon", SecurityOperations.Write, "[Oid] = CurrentUserId()");
                defaultRole.AddMemberAccessPermission<SecuritySystemUser>("StoredPassword", SecurityOperations.Write, "[Oid] = CurrentUserId()");
                defaultRole.SetTypePermissionsRecursively<SecuritySystemRole>(SecurityOperations.Read, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
                defaultRole.SetTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow);
            }
            return defaultRole;
        }
    }
}
