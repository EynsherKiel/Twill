using GalaSoft.MvvmLight;
using System;
using System.Linq;
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

                Fill();
            }
            else
            {
                Monitor = StorageHelperManager.Load<Monitor>();
                ReportsModel = StorageHelperManager.Load<ReportsModel>(Monitor.Date);

                DayActivityAnalysis = new DayActivityAnalysis(Monitor);
                DayActivityCommonAnalyse = new DayActivityCommonAnalyse(Monitor);

                if (IsUsingTimer)
                    return;
                IsUsingTimer = true;

                StartSaves(TimeSpan.FromSeconds(10.0));
            }
        }

        public DayMonitor(DateTime date)
        {
            Monitor = StorageHelperManager.Load<Monitor>(date);

            Fill();
        }

        private static bool IsUsingTimer = false;

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


        private void Fill()
        {
            if (Monitor == null)
            {
                DayActivityAnalysis = null;
                DayActivityCommonAnalyse = null;
                ReportsModel = new ReportsModel();
                return;
            }

            DayActivityAnalysis = new DayActivityAnalysis(Monitor);
            DayActivityCommonAnalyse = new DayActivityCommonAnalyse(Monitor);

            FillReportDayModel();
        }

        private void FillReportDayModel()
        {
            var dayreport = ReportsModel.ReportsRegulator.Load<ReportModel>(Monitor.Date);

            if (ReportsModel == null)
                ReportsModel = new ReportsModel();

            ReportsModel.UpDateReportModel(dayreport?.Reports);
        }

        public Storage.Files.Reports.DayReport<ReportModel> GetDayReport()
        {
            return new Storage.Files.Reports.DayReport<ReportModel>()
            {
                Date = Monitor.Date,
                Reports = ReportsModel.Reports.Where(report => !string.IsNullOrEmpty(report.Text)).ToList()
            };
        }

        ~DayMonitor()
        {
            Timer?.Dispose();
        }
    }
}
