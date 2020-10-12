using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CRM.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RoleQueryController : ViewController
    {
        public RoleQueryController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            string subName = View.Caption;

            //创建一个当前用户的命名空间
            IObjectSpace newObjectSpace = Application.CreateObjectSpace(SecuritySystem.CurrentUser.GetType());
            //获取当前用户的对象
            object currentUser = newObjectSpace.GetObject(SecuritySystem.CurrentUser);

            SysUser user = (SysUser)currentUser;
            bool Role_Admin = ((SysUser)SecuritySystem.CurrentUser).IsUserInRole("Administrator");
            bool Role_Manage = ((SysUser)SecuritySystem.CurrentUser).IsUserInRole("管理员");
            bool Role_Diector = ((SysUser)SecuritySystem.CurrentUser).IsUserInRole("经理");
            bool Role_Staff = ((SysUser)SecuritySystem.CurrentUser).IsUserInRole("职员");

            if (View is ListView)
            {
                switch (subName)
                {
                    case "客户人员信息":
                    case "客户公司信息":
                    case "项目信息":
                    case "项目进度":
                    case "合同订单":
                    case "开票收款":
                        if (Role_Diector|| Role_Manage)
                        {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?",user.Company.Oid);
                        }
                        if (Role_Staff)
                        {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        break;
                    //case "客户公司信息":
                        //if (Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                        //}
                        //if (Role_Staff)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}

                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "售前项目信息":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                    //    }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "立项项目信息":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                    //    }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "增补项目信息":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                    //    }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "项目进度信息":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                    //    }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "合同订单":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    //case "开票收款":
                    //    if (Role_Diector)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Company.Oid] = ?", user.Company.Oid);
                    //    }
                    //    if (Role_Staff)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                    //    }
                        //if (!Role_Admin && !Role_Manage && !Role_Diector)
                        //{
                        //    ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[SysUser.Oid] = @CurrentUserId()");
                        //}
                        //break;
                    case "用户配置":
                        if (!Role_Admin)
                        {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[Company.Oid] =?", user.Company.Oid);
                        }
                        break;
                    case "公司配置":
                        if (!Role_Admin)
                        {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[Oid] =?", user.Company.Oid);
                        }
                        break;
                    case "部门配置":
                        if (!Role_Admin)
                        {
                            ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[Company.Oid] =?", user.Company.Oid);
                        }
                        break;
                    //case "权限配置":
                    //    if (!Role_Admin)
                    //    {
                    //        ((ListView)View).CollectionSource.Criteria["FilterByUser"] = CriteriaOperator.Parse("[Company.Oid] =?", user.Company.Oid);
                    //    }
                    //    break;
                    default:
                        break;
                }
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
