using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class Interval : ObservableObject, IInterval
    {
        private TimeSpan start;
        public TimeSpan Start
        {
            get { return start; }
            set
            {
                Set(ref start, value);
                RaisePropertyChanged(nameof(TotalMinutesInterval));
            }
        }

        private TimeSpan end;
        public TimeSpan End
        {
            get { return end; }
            set
            {
                Set(ref end, value);
                RaisePropertyChanged(nameof(TotalMinutesInterval));
            }
        }

        public double TotalMinutesInterval => (End - Start).TotalMinutes;

    }
}
