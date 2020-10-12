using AI_IMS.Module.BusinessObjects.Sys_Management;
using AI_IMS.Module.BusinessObjects.Task_Management;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Windows.Forms;

namespace AI_IMS.Module.Controllers.Approval
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ApprovalAction : ViewController
    {
        private ListViewProcessCurrentObjectController processCurrentObjectController;
        public ApprovalAction()
        {
            InitializeComponent();
            
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.CustomProcessSelectedItem+= processCurrentObjectController_CustomProcessSelectedItem;
            }
                
            // Perform various tasks depending on the target View.
        }
        private void processCurrentObjectController_CustomProcessSelectedItem(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            e.Handled = true;
            //Accept.DoExecute();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            // 拒绝按钮不可点
            Refuse.Enabled.SetItemValue("ObjectsCriteria", false);
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void Submit_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            MessageBox.Show(View.CurrentObject.ToString());
            string strViewname = View.CurrentObject.ToString().Split('.')[View.CurrentObject.ToString().Split('.').Length - 1];
            strViewname = strViewname.Substring(0, strViewname.IndexOf("("));
           
            Update_Approval(strViewname,true);

            


        }

        private void Update_Approval(string strViewname,bool bActive)
        {
            
            
            
            
            switch (strViewname)
            {
                case "TaskInitiation"://任务发起
                    
                    TaskInitiation comm = View.CurrentObject as TaskInitiation;
                    SysUser user = CommUtilities.GetCurrentUser(comm.Session);
                    string strTaskName = comm.TaskName;
                    ApprovalRecords approval = ObjectSpace.CreateObject<ApprovalRecords>();
                    if (bActive)
                    {
                        approval.Operations = ApprovalRecords.Operation.提交;

                    }
                    else
                        approval.Operations = ApprovalRecords.Operation.拒绝;
                    approval.OperateTime = DateTime.Now;
                    approval.Operator = user;
                    approval.RecieveTime = DateTime.Now;
                    Accept.Enabled.SetItemValue("ObjectsCriteria", false);
                    approval.Process = ApprovalRecords.TaskProcess.发起;
                    TaskSummary task = View.CurrentObject as TaskSummary;
                    TaskSummary taskSummary = ObjectSpace.CreateObject<TaskSummary>();
                    taskSummary.TaskName = comm.TaskName;
                    taskSummary.TaskNum = comm.TaskNum;
                    taskSummary.AnalysisPerson = comm.NextPerson;
                    taskSummary.EndTime = comm.EndTime;
                    taskSummary.StartTime = comm.StartTime;
                    taskSummary.Process = TaskSummary.TaskProcess.发起;
                    taskSummary.InitiationPerson = user;
                     taskSummary.TaskDescribe = comm.TaskDescribe;
                    comm.ApprovalRecords.Add(approval);
                    comm.TaskSummaryNavigation.Add(taskSummary);
                    View.ObjectSpace.CommitChanges();
                    View.Refresh();
                    //IObjectSpace objectSpace = this.Application.CreateObjectSpace(typeof(TaskAnalysis));
                    
                  

                    //CriteriaOperator op = CriteriaOperator.Parse("TaskName = ?", strTaskName);
                    //int qw = comm.Session.ExecuteNonQuery("INSERT into TaskAnalysis (TaskName) Values('00000')");

                    //using (UnitOfWork uow = new UnitOfWork())
                    //{

                    //    TaskAnalysis analysis1 = new TaskAnalysis(uow);
                    //    //user = CommUtilities.GetCurrentUser(analysis1.Session);
                    //    analysis1.AnalysisPerson = loc.NextPerson;
                    //    analysis1.EndTime = loc.EndTime;
                    //    analysis1.StartTime = loc.StartTime;
                    //    analysis1.TaskName = loc.TaskName;
                    //    analysis1.TaskNum = loc.TaskNum;


                    //    uow.CommitChanges();
                    //}



                    break;
                case "TaskAnalysis"://任务分析
                    TaskAnalysis analysis = View.CurrentObject as TaskAnalysis;
                    //TaskInitiation comm1 = View.CurrentObject as TaskInitiation;
                    //using (UnitOfWork uow = new UnitOfWork())
                    //{
                    //    CriteriaOperator op = CriteriaOperator.Parse("TaskName= ?", "dasda");
                    //    TaskSummary task1 = uow.FindObject<TaskSummary>(op);
                    //    if (task1 == null) return;
                    //    task1.Process =  TaskSummary.TaskProcess.分析;

                       
                    //    uow.CommitChanges();
                    //}

                    break;
                case "TaskExecution"://任务执行
                    Accept.Enabled.SetItemValue("ObjectsCriteria", false);
                    Refuse.Enabled.SetItemValue("ObjectsCriteria", false);
                     
                    break;
                case "TaskConfirmation"://任务确认
                    Accept.Enabled.SetItemValue("ObjectsCriteria", false);
                    Refuse.Enabled.SetItemValue("ObjectsCriteria", false);
                    
                    break;
                default:
                    break;
            }
            
        }
        private void Refuse_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

        }
    }
}
