using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Module.BusinessObjects.SellManagement;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace CRM.Module.Controllers.SellManagement
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class InvoiceReceiptController : ViewController
    {
        public InvoiceReceiptController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
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

        private void SaveReceipt_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace os = View.ObjectSpace;

            os.CommitChanges();
            View.Refresh();
            decimal money = 0;
            InvoiceReceipt detail = View.CurrentObject as InvoiceReceipt;
            //detail.Name = "日常报销";
            foreach (var item in detail.PayInvoiceDetails)
            {
                money += item.InvoiceMoney;
            }
            detail.OTotal = money;
            if (money<= detail.Tax_OMoney)
            {
                os.CommitChanges();
                View.Refresh();
                
            }
            else
            {
                throw new Exception("回款金额不能大于含税金额");
            }
            
        }
    }
}
