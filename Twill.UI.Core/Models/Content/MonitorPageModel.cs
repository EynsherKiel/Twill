﻿using GalaSoft.MvvmLight;
using System;
using Twill.Tools.Async;
using Twill.UI.Core.Models.Controls.Processes;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
        public MonitorPageModel()
        {
            if (IsInDesignMode)
            {
                Monitor = new Monitor();
            }
            else
            {
                Monitor = Tools.Architecture.Singleton<Monitor>.Instance;
                //StartSaves(TimeSpan.FromSeconds(10));
            }
        }

        public MonitorPageModel(DateTime time)
        {
            Monitor = BarrierManager.Load<Monitor>(time);
        }

        private void StartSaves(TimeSpan updatetime)
        {
           Timer = new System.Threading.Timer(obj =>
           {
               SyncContext.Action(e => BarrierManager.Save(Monitor), string.Empty);

           }, null, updatetime, updatetime);
        }

        private System.Threading.Timer Timer = null;
        private Storage.Barrier.Manager BarrierManager = new Storage.Barrier.Manager();

        private readonly SyncContext SyncContext = new SyncContext();

        private Monitor monitor;
        public Monitor Monitor
        {
            get { return monitor; }
            set { Set(ref monitor, value); }
        }

        private DayActivityAnalysis dayActivityAnalysis = new DayActivityAnalysis();
        public DayActivityAnalysis DayActivityAnalysis
        {
            get { return dayActivityAnalysis; }
            set { Set(ref dayActivityAnalysis, value); }
        }

        private ReportsModel reportsModel = new ReportsModel();
        public ReportsModel ReportsModel
        {
            get { return reportsModel; }
            set { Set(ref reportsModel, value); }
        }

        ~MonitorPageModel()
        {
            Timer?.Dispose();
        }
    }
}
