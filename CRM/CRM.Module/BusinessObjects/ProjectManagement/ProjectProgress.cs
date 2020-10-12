using CRM.Module.BusinessObjects.CustomerInformationManagement;
using CRM.Module.BusinessObjects.SellManagement;
using CRM.Module.BusinessObjects.Sys_Management;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CRM.Module.BusinessObjects.ProjectManagement
{
    [NavigationItem("项目管理")]
    [DefaultClassOptions]//默认类选项
    [Persistent("ProjectProgress")]
    [XafDisplayName("项目进度")]
    //[Appearance("ActionVisibility", AppearanceItemType = "ViewItem", TargetItems = "AddProject", Criteria = "Tp = '立项'", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("RuleMethod2", AppearanceItemType = "ViewItem", TargetItems = "aPName", Criteria = "Tp = '立项'", Enabled = false)]
    [Appearance("Appearance1", AppearanceItemType = "ViewItem", Context = "ListView", TargetItems = "StateName", Criteria = "StateName = '延期'", BackColor = "red")]
    public class ProjectProgress : BaseObject
    {

        public ProjectProgress(Session s) : base(s) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            SysUser = CommUtilities.GetCurrentUser(Session);

        }

        protected override void OnSaving()
        {

            base.OnSaving();
        }

        private Project _Project;
        [XafDisplayName("项目名称")]
        public Project Project
        {
            get { return _Project; }
            set { SetPropertyValue<Project>(nameof(Project), ref _Project, value); }
        }


        private string _CCpName;

        [XafDisplayName("客户名称")]
        public string CCpName
        {
            get 
            {
                if (Project != null && Project.CCpName != null)
                {
                    _CCpName = Project.CCpName.CPName;
                }

                return _CCpName; 
            }
            set { SetPropertyValue<string>(nameof(CCpName), ref _CCpName, value); }
        }
        public enum State { 未延期, 延期 }


        private string _Address;
        [XafDisplayName("实施地")]
        public string Address
        {
            get { return _Address; }
            set { SetPropertyValue<string>(nameof(Address), ref _Address, value); }
        }


        private State _StateName;
        [XafDisplayName("进展状态")]
        public State StateName
        {
            get { return _StateName; }
            set { SetPropertyValue<State>(nameof(StateName), ref _StateName, value); }
        }



        [Association("ProjectProgress-ProgressDetails")]
        [XafDisplayName("进度明细")]
        public XPCollection<ProgressDetail> ProgressDetails
        {
            get { return GetCollection<ProgressDetail>(nameof(ProgressDetails)); }
        }


        private XPCollection<AuditDataItemPersistent> changeHistory;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [XafDisplayName("更改记录")]
        public XPCollection<AuditDataItemPersistent> ChangeHistory
        {
            get
            {
                if (changeHistory == null)
                {
                    changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return changeHistory;
            }
        }


        private SysUser _SysUser;
        [Association("SysUser-ProjectProgresses")]
        public SysUser SysUser
        {
            get { return _SysUser; }
            set { SetPropertyValue<SysUser>(nameof(SysUser), ref _SysUser, value); }
        }

    }

    [DefaultClassOptions]//默认类选项
    [Persistent("ProgressDetail")]
    [XafDisplayName("进度明细")]
    public class ProgressDetail : BaseObject
    {
        public ProgressDetail(Session s) : base(s) { }


        private ProjectProgress _ProjectProgress;
        [Association("ProjectProgress-ProgressDetails")]
        [XafDisplayName("项目进度")]
        public ProjectProgress ProjectProgress
        {
            get { return _ProjectProgress; }
            set { SetPropertyValue<ProjectProgress>(nameof(ProjectProgress), ref _ProjectProgress, value); }
        }



        private DateTime _PlanDesignTime;
        [XafDisplayName("方案设计计划时间")]
        public DateTime PlanDesignTime
        {
            get { return _PlanDesignTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanDesignTime), ref _PlanDesignTime, value); }
        }


        private DateTime _RealDesignTime;
        [XafDisplayName("方案设计实际时间")]
        public DateTime RealDesignTime
        {
            get
            {
                return _RealDesignTime;
            }
            set { SetPropertyValue<DateTime>(nameof(RealDesignTime), ref _RealDesignTime, value); }
        }


        private DateTime _PlanSysTime;
        [XafDisplayName("计划系统开发时间")]
        public DateTime PlanSysTime
        {
            get { return _PlanSysTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanSysTime), ref _PlanSysTime, value); }
        }



        private DateTime _RealSysTime;
        [XafDisplayName("实际系统开发时间")]
        public DateTime RealSysTime
        {
            get { return _RealSysTime; }
            set { SetPropertyValue<DateTime>(nameof(RealSysTime), ref _RealSysTime, value); }
        }


        private DateTime _PlanDebugTime;
        [XafDisplayName("计划现场调试时间")]
        public DateTime PlanDebugTime
        {
            get { return _PlanDebugTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanDebugTime), ref _PlanDebugTime, value); }
        }


        private DateTime _RealDebugTime;
        [XafDisplayName("实际现场调试时间")]
        public DateTime RealDebugTime
        {
            get { return _RealDebugTime; }
            set { SetPropertyValue<DateTime>(nameof(RealDebugTime), ref _RealDebugTime, value); }
        }


        private DateTime _PlanPlRunTime;
        [XafDisplayName("计划试运行时间")]
        public DateTime PlanPlRunTime
        {
            get { return _PlanPlRunTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanPlRunTime), ref _PlanPlRunTime, value); }
        }


        private DateTime _RealPlRunTime;
        [XafDisplayName("实际试运行时间")]
        public DateTime RealPlRunTime
        {
            get { return _RealPlRunTime; }
            set { SetPropertyValue<DateTime>(nameof(RealPlRunTime), ref _RealPlRunTime, value); }
        }


        private DateTime _PlanTrainTime;
        [XafDisplayName("计划培训时间")]
        public DateTime PlanTrainTime
        {
            get { return _PlanTrainTime; }
            set { SetPropertyValue<DateTime>(nameof(PlanTrainTime), ref _PlanTrainTime, value); }
        }



        private DateTime _RealTrainTime;
        [XafDisplayName("实际培训时间")]
        public DateTime RealTrainTime
        {
            get { return _RealTrainTime; }
            set { SetPropertyValue<DateTime>(nameof(RealTrainTime), ref _RealTrainTime, value); }
        }
    }
}
