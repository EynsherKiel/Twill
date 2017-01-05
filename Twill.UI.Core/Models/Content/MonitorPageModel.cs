using GalaSoft.MvvmLight;
using Twill.UI.Core.Models.Controls.Processes;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
         
        private Monitor monitor = Twill.Tools.Architecture.Singleton<Monitor>.Instance;
        public Monitor Monitor
        {
            get { return monitor; }
            set { Set(ref monitor, value); }
        }

        private ReportsModel reportsModel = new ReportsModel();
        public ReportsModel ReportsModel
        {
            get { return reportsModel; }
            set { Set(ref reportsModel, value); }
        } 
    }
}
