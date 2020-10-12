namespace CRM.Module.Controllers.SellManagement
{
    partial class InvoiceReceiptController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SaveReceipt = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // SaveReceipt
            // 
            this.SaveReceipt.Caption = "Save Receipt";
            this.SaveReceipt.ConfirmationMessage = null;
            this.SaveReceipt.Id = "SaveReceipt";
            this.SaveReceipt.TargetObjectType = typeof(CRM.Module.BusinessObjects.SellManagement.InvoiceReceipt);
            this.SaveReceipt.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.SaveReceipt.ToolTip = null;
            this.SaveReceipt.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.SaveReceipt.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SaveReceipt_Execute);
            // 
            // InvoiceReceiptController
            // 
            this.Actions.Add(this.SaveReceipt);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction SaveReceipt;
    }
}
