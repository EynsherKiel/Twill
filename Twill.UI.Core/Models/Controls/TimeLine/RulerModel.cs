using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class RulerModel : ObservableObject
    {
        public RulerModel()
        {
            UpDate();
        }


        private double actualHeight = 20000.0;
        public double ActualHeight
        {
            private get { return actualHeight; }
            set
            {
                if (actualHeight != value)
                { 
                    Set(ref actualHeight, value);
                    UpDate();
                }
            }
        }

        private   ObservableCollection<TimeSpan> times = new ObservableCollection<TimeSpan>();
        public ObservableCollection<TimeSpan> Times
        {
            get { return times; }
            private set { Set(ref times, value); }
        }


        private void UpDate()
        {
            var forconstr = ActualHeight / 60.0;

            var additinaltime = Tools.Math.Position.MinutesInDay / forconstr;

            var timespan = TimeSpan.Zero;

            var list = new List<TimeSpan>()
            {
                timespan
            };

            double time = additinaltime;

            for (int i = 1, len = (int)forconstr; i < len; time += additinaltime, i++)
            {
                list.Add(TimeSpan.FromSeconds(list.Last().TotalSeconds).Add(TimeSpan.FromMinutes(additinaltime)));
            }

            Times = new ObservableCollection<TimeSpan>(list);
        }

    }
}
