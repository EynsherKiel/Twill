using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Tools.Async;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class DayMonitor : ViewModelBase
    {
        public DayMonitor()
        {
            if (IsInDesignMode)
            {
                Monitor = new Monitor();
            }
            else
            {
                Monitor = StorageHelperManager.Load<Monitor>();

                StartSaves(TimeSpan.FromSeconds(10.0));
            }

            DayActivityAnalysis = new DayActivityAnalysis(Monitor);
            DayActivityCommonAnalyse = new DayActivityCommonAnalyse(Monitor);
        }

        public DayMonitor(DateTime time)
        {
            Monitor = StorageHelperManager.Load<Monitor>(time);
            DayActivityAnalysis = new DayActivityAnalysis(Monitor);
            DayActivityCommonAnalyse = new DayActivityCommonAnalyse(Monitor);
        }

        private void StartSaves(TimeSpan updatetime)
        {
            Timer = new System.Threading.Timer(AsyncSaveData, null, updatetime, updatetime);
        }

        private void AsyncSaveData(object state)
        {
            if (!System.Threading.Monitor.TryEnter(SyncRoot))
                return;
            try
            {
                StorageHelperManager.Save(Monitor?.GetLightClone());
            }
            finally
            {
                System.Threading.Monitor.Exit(SyncRoot);
            }
        }

        public object SyncRoot = new object();
        private System.Threading.Timer Timer = null;
        private StorageHelper.Manager StorageHelperManager = new StorageHelper.Manager();

        private readonly SyncContext SyncContext = new SyncContext();

        private Monitor monitor;
        public Monitor Monitor
        {
            get { return monitor; }
            set { Set(ref monitor, value); }
        }

        private DayActivityCommonAnalyse dayActivityCommonAnalyse;
        public DayActivityCommonAnalyse DayActivityCommonAnalyse
        {
            get { return dayActivityCommonAnalyse; }
            set { Set(ref dayActivityCommonAnalyse, value); }
        }

        private DayActivityAnalysis dayActivityAnalysis;
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


        ~DayMonitor()
        {
            Timer?.Dispose();
        }
    }
}
