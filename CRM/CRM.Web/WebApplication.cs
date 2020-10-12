using System;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.Web;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.DC;

namespace CRM.Web {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Web.WebApplication
    public partial class CRMAspNetApplication : WebApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule module2;
        private CRM.Module.CRMModule module3;
        private CRM.Module.Web.CRMAspNetModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
        private DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule dashboardsAspNetModule;
        private DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule fileAttachmentsAspNetModule;
        private DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule htmlPropertyEditorAspNetModule;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule;
        private DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule notificationsAspNetModule;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
        private DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule pivotChartAspNetModule;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule pivotGridAspNetModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2 reportsAspNetModuleV2;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase;
        private DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule schedulerAspNetModule;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule treeListEditorsAspNetModule;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule validationAspNetModule;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;

        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static CRMAspNetApplication() {
			EnableMultipleBrowserTabsSupport = true;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.AllowFilterControlHierarchy = true;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxGridListEditor.MaxFilterControlHierarchyDepth = 3;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.AllowFilterControlHierarchyDefault = true;
            DevExpress.ExpressApp.Web.Editors.ASPx.ASPxCriteriaPropertyEditor.MaxHierarchyDepthDefault = 3;
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
            DevExpress.ExpressApp.BaseObjectSpace.ThrowExceptionForNotRegisteredEntityType = true;
            DevExpress.ExpressApp.Security.SecurityStrategy.EnableSecurityForActions = true;
        }
        private void InitializeDefaults() {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation = true;
        }
        #endregion
        public CRMAspNetApplication() {
            InitializeComponent();
			InitializeDefaults();
        }
        protected override IViewUrlManager CreateViewUrlManager() {
            return new ViewUrlManager();
        }
        private static NonPersistentTypeInfoSource nonPersistentTypeInfoSource;
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            //args.ObjectSpaceProvider = new XPObjectSpaceProvider(GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            //if (NonPersistentTypeInfoSource == null)
            //{
            //    nonPersistentTypeInfoSource = new NonPersistentTypeInfoSource(TypesInfo, new List<Type>()
            //       { typeof(Module.BusinessObjects.CustomLogonParameters) });
            //}
            //args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, nonPersistentTypeInfoSource));
            args.ObjectSpaceProvider = new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            System.Web.HttpApplicationState application = (System.Web.HttpContext.Current != null) ? System.Web.HttpContext.Current.Application : null;
            IXpoDataStoreProvider dataStoreProvider = null;
            if(application != null && application["DataStoreProvider"] != null) {
                dataStoreProvider = application["DataStoreProvider"] as IXpoDataStoreProvider;
            }
            else {
                dataStoreProvider = XPObjectSpaceProvider.GetDataStoreProvider(connectionString, connection, true);
                if(application != null) {
                    application["DataStoreProvider"] = dataStoreProvider;
                }
            }
			return dataStoreProvider;
        }
        private void CRMAspNetApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
        private void InitializeComponent() {
            this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
            this.module2 = new DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule();
            this.module3 = new CRM.Module.CRMModule();
            this.module4 = new CRM.Module.Web.CRMAspNetModule();
            this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
            this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
            this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
            this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
            this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
            this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
            this.dashboardsAspNetModule = new DevExpress.ExpressApp.Dashboards.Web.DashboardsAspNetModule();
            this.fileAttachmentsAspNetModule = new DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule();
            this.htmlPropertyEditorAspNetModule = new DevExpress.ExpressApp.HtmlPropertyEditor.Web.HtmlPropertyEditorAspNetModule();
            this.notificationsModule = new DevExpress.ExpressApp.Notifications.NotificationsModule();
            this.notificationsAspNetModule = new DevExpress.ExpressApp.Notifications.Web.NotificationsAspNetModule();
            this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
            this.pivotChartAspNetModule = new DevExpress.ExpressApp.PivotChart.Web.PivotChartAspNetModule();
            this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
            this.pivotGridAspNetModule = new DevExpress.ExpressApp.PivotGrid.Web.PivotGridAspNetModule();
            this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
            this.reportsAspNetModuleV2 = new DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2();
            this.schedulerModuleBase = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
            this.schedulerAspNetModule = new DevExpress.ExpressApp.Scheduler.Web.SchedulerAspNetModule();
            this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
            this.treeListEditorsAspNetModule = new DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule();
            this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
            this.validationAspNetModule = new DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule();
            this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // securityStrategyComplex1
            // 
            this.securityStrategyComplex1.AllowAnonymousAccess = false;
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.securityStrategyComplex1.PermissionsReloadMode = DevExpress.ExpressApp.Security.PermissionsReloadMode.NoCache;
            this.securityStrategyComplex1.RoleType = typeof(CRM.Module.BusinessObjects.Sys_Management.SysRole);
            this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
            this.securityStrategyComplex1.UseOptimizedPermissionRequestProcessor = false;
            this.securityStrategyComplex1.UserType = typeof(CRM.Module.BusinessObjects.Sys_Management.SysUser);
            // 
            // authenticationStandard1
            // 
            this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
            // 
            // dashboardsModule
            // 
            this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
            // 
            // notificationsModule
            // 
            this.notificationsModule.CanAccessPostponedItems = false;
            this.notificationsModule.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
            this.notificationsModule.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
            this.notificationsModule.ShowDismissAllAction = false;
            this.notificationsModule.ShowNotificationsWindow = true;
            this.notificationsModule.ShowRefreshAction = false;
            // 
            // pivotChartModuleBase
            // 
            this.pivotChartModuleBase.DataAccessMode = DevExpress.ExpressApp.CollectionSourceDataAccessMode.Client;
            this.pivotChartModuleBase.ShowAdditionalNavigation = false;
            // 
            // reportsModuleV2
            // 
            this.reportsModuleV2.EnableInplaceReports = true;
            this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
            this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
            // 
            // reportsAspNetModuleV2
            // 
            this.reportsAspNetModuleV2.ReportViewerType = DevExpress.ExpressApp.ReportsV2.Web.ReportViewerTypes.HTML5;
            // 
            // validationModule
            // 
            this.validationModule.AllowValidationDetailsAccess = true;
            this.validationModule.IgnoreWarningAndInformationRules = false;
            // 
            // CRMAspNetApplication
            // 
            this.ApplicationName = "CRM";
            this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            this.Modules.Add(this.module1);
            this.Modules.Add(this.module2);
            this.Modules.Add(this.securityModule1);
            this.Modules.Add(this.objectsModule);
            this.Modules.Add(this.conditionalAppearanceModule);
            this.Modules.Add(this.dashboardsModule);
            this.Modules.Add(this.notificationsModule);
            this.Modules.Add(this.pivotChartModuleBase);
            this.Modules.Add(this.pivotGridModule);
            this.Modules.Add(this.reportsModuleV2);
            this.Modules.Add(this.schedulerModuleBase);
            this.Modules.Add(this.treeListEditorsModuleBase);
            this.Modules.Add(this.validationModule);
            this.Modules.Add(this.viewVariantsModule);
            this.Modules.Add(this.module3);
            this.Modules.Add(this.dashboardsAspNetModule);
            this.Modules.Add(this.fileAttachmentsAspNetModule);
            this.Modules.Add(this.htmlPropertyEditorAspNetModule);
            this.Modules.Add(this.notificationsAspNetModule);
            this.Modules.Add(this.pivotChartAspNetModule);
            this.Modules.Add(this.pivotGridAspNetModule);
            this.Modules.Add(this.reportsAspNetModuleV2);
            this.Modules.Add(this.schedulerAspNetModule);
            this.Modules.Add(this.treeListEditorsAspNetModule);
            this.Modules.Add(this.validationAspNetModule);
            this.Modules.Add(this.module4);
            this.Security = this.securityStrategyComplex1;
            this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.CRMAspNetApplication_DatabaseVersionMismatch);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
