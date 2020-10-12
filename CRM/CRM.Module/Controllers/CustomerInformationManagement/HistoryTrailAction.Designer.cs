namespace CRM.Module.Controllers.CustomerInformationManagement
{
    partial class HistoryTrailAction
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
            this.simpleAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction
            // 
            this.simpleAction.Caption = "保存";
            this.simpleAction.ConfirmationMessage = null;
            this.simpleAction.Id = "simpleAction";
            this.simpleAction.TargetObjectType = typeof(CRM.Module.BusinessObjects.CustomerInformationManagement.CustomerCompany);
            this.simpleAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.simpleAction.ToolTip = null;
            this.simpleAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.simpleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction_Execute);
            // 
            // HistoryTrailAction
            // 
            this.Actions.Add(this.simpleAction);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction;
    }
}
