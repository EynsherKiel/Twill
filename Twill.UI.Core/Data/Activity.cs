using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Twill.UI.Core.Data
{ 
   public class Activity : ViewModelBase
    {
        private DateTime start = DateTime.Now.Date;
        public DateTime Start
        {
            get { return start; }
            set
            {
                Set(ref start, value);
                RaisePropertyChanged(nameof(TotalMinutesInterval));
            }
        }

        private DateTime end = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        public DateTime End
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
