using GalaSoft.MvvmLight;
using System;
using System.Linq;
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
                Monitor = StorageHelperManager.Load<Monitor>();

                StartSaves(TimeSpan.FromSeconds(10.0));
            }
        }

        public MonitorPageModel(DateTime time)
        {
            Monitor = StorageHelperManager.Load<Monitor>(time);
        }

        private void StartSaves(TimeSpan updatetime)
        {
           Timer = new System.Threading.Timer(AsyncSaveData , null, updatetime, updatetime);
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
