using GalaSoft.MvvmLight;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.Models.Content
{
    public class MonitorPageModel : ViewModelBase
    {
         
        private Monitor monitor = new Monitor();
        public Monitor Monitor
        {
            get { return monitor; }
            set { Set(ref monitor, value); }
        }

    }
}
