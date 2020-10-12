using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using CRM.Module.BusinessObjects.ProjectManagement;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CRM.Module.Controllers.ProjectManagement
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ProjectController : ViewController
    {
        //private SimpleAction writeMailAction;
        public ProjectController()
        {

            InitializeComponent();

        }
        protected override void OnActivated()
        {
            base.OnActivated();
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

    //    private void AddProjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
    //    {

    //        //SellProject sProject = View.CurrentObject as SellProject;
    //        //SellProject aProject = ObjectSpace.CreateObject<SellProject>();

    //        foreach (PropertyEditor item in ((DetailView)View).GetItems<PropertyEditor>())
    //        {
    //            if (item.AllowEdit)
    //            {
    //                try
    //                {
    //                    aProject.Code = sProject.Code;
    //                    //aProject.SPName = sProject.SPName;
    //                    //aProject.SpDescription = sProject.SpDescription;
    //                    //aProject.SpCCpName = sProject.SpCCpName;
    //                    //aProject.SpDate = DateTime.Now;
    //                    //aProject.SPrincipal = sProject.SPrincipal;
    //                    //aProject.SPhone = sProject.SPhone;

    //                }
    //                catch (IntermediateMemberIsNullException)
    //                {
    //                    item.Refresh();
    //                }
    //            }
    //        }

    //    }


    //    private void SellSave_Execute(object sender, SimpleActionExecuteEventArgs e)
    //    {
    //        //SellProject sProject = View.CurrentObject as SellProject;

    //        //using (UnitOfWork uow = new UnitOfWork())
    //        //{
    //        //    if (sProject.PspSpCode!=null)
    //        //    {
    //        //        sProject.Oid = sProject.PspSpCode.Oid;
    //        //    }
    //        //}



    //    }
    //}
//}
