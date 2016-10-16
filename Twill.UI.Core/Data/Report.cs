using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Data
{
    public class Report : Notify
    {
        public const double MinetsInDay = 1440.0;

        private DateTime start = DateTime.Now.Date;
        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                RaisePropertyChanged(nameof(Start));
            }
        }

        private DateTime end = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        public DateTime End
        {
            get { return end; }
            set
            {
                end = value;
                RaisePropertyChanged(nameof(End));
            }
        }
         

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }

        public double GetInterval() => (End - Start).TotalMinutes;
    }
}
