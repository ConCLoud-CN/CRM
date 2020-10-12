using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;
using ListView = DevExpress.ExpressApp.ListView;
using DevExpress.Data.Filtering;
using CRM.Module.BusinessObjects.CustomerInformationManagement;
using System.Data.SqlClient;
using System.Data.Common;
using DevExpress.DataProcessing;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.DC;

namespace CRM.Module.Controllers.CustomerInformationManagement
{
    public partial class HistoryTrailAction : ViewController
    {
        public HistoryTrailAction()
        {
            InitializeComponent();
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        //private void TrailAction_Execute(object sender, DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventArgs e)
        //{
        //    IObjectSpace objectSpace = Application.CreateObjectSpace();
            
        //    string paramValue = e.ParameterCurrentValue as string;
        //    if (!string.IsNullOrEmpty(paramValue))
        //    {
        //        paramValue = "%" + paramValue + "%";
        //    }
        //    object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type, new BinaryOperator("Name", paramValue, BinaryOperatorType.Like));
        //    if (obj != null)
        //    {
        //        e.ShowViewParameters.CreatedView = Application.CreateDetailView(objectSpace, obj);
        //    }

        //    CustomerCompany customerCompany = View.CurrentObject as CustomerCompany;
            
        //    HistoryTrail trail = ObjectSpace.CreateObject<HistoryTrail>();
        //    trail.EditTime = DateTime.Now;
        //    trail.EditName = customerCompany.Name;
        //    trail.EditAddress = customerCompany.Address;
        //    trail.EditIntroduce = customerCompany.Introduce;
        //    trail.EditlegalMan = customerCompany.legalMan;
        //    trail.EditRegisteredFund = customerCompany.RegisteredFund;
        //    trail.EditReiestDate = customerCompany.ReiestDate;
        //    trail.CompanyType = customerCompany.CompanyType; 
        //    trail.EditScope = customerCompany.Scope;
        //    trail.EditRemark = customerCompany.Remark;
        //    customerCompany.HistoryTrails.Add(trail);
        //    View.ObjectSpace.CommitChanges();
        //    View.Refresh();
        //}

        private void simpleAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            //string strViewname = View.CurrentObject.ToString().Split('.')[View.CurrentObject.ToString().Split('.').Length - 1];
            //strViewname = strViewname.Substring(0, strViewname.IndexOf("("));


            //CustomerCompany customerCompany = View.CurrentObject as CustomerCompany;
            //HistoryTrail trail = ObjectSpace.CreateObject<HistoryTrail>();

            //foreach (PropertyEditor item in ((DetailView)View).GetItems<PropertyEditor>())
            //{
            //    if (item.AllowEdit)
            //    {
            //        try
            //        {
            //            trail.EditTime = DateTime.Now;
            //            trail.EditName = customerCompany.Name;
            //            trail.EditAddress = customerCompany.Address;
            //            trail.EditIntroduce = customerCompany.Introduce;
            //            trail.EditlegalMan = customerCompany.legalMan;
            //            trail.EditRegisteredFund = customerCompany.RegisteredFund;
            //            trail.EditReiestDate = customerCompany.ReiestDate;
            //            trail.CompanyType = customerCompany.CompanyType;
            //            trail.EditScope = customerCompany.Scope;
            //            trail.EditRemark = customerCompany.Remark;
            //        }
            //        catch (IntermediateMemberIsNullException)
            //        {
            //            item.Refresh();
            //        }
            //    }
            //}

            ////CustomerCompany customerCompany = View.CurrentObject as CustomerCompany;

            ////HistoryTrail trail = ObjectSpace.CreateObject<HistoryTrail>();

            //trail.EditTime = DateTime.Now;
            //trail.EditName = customerCompany.Name;
            //trail.EditAddress = customerCompany.Address;
            //trail.EditIntroduce = customerCompany.Introduce;
            //trail.EditlegalMan = customerCompany.legalMan;
            //trail.EditRegisteredFund = customerCompany.RegisteredFund;
            //trail.EditReiestDate = customerCompany.ReiestDate;
            //trail.CompanyType = customerCompany.CompanyType;
            //trail.EditScope = customerCompany.Scope;
            //trail.EditRemark = customerCompany.Remark;
            //customerCompany.HistoryTrails.Add(trail);
            //View.ObjectSpace.CommitChanges();
            //View.Refresh();
        }
    }
}
