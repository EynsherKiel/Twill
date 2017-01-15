using GalaSoft.MvvmLight;
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
            }
        }

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
    }
}
