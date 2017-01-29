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

        }


        private DayMonitor dayMonitor = new DayMonitor();
        public DayMonitor DayMonitor
        {
            get { return dayMonitor; }
            set { Set(ref dayMonitor, value); }
        }
    }
}
