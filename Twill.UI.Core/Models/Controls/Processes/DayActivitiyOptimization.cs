using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class DayActivitiyOptimization : BaseDayActivityAnalysis
    {

        public DayActivitiyOptimization() : base()
        {
            UserLogActivities = Range(20000.0);
        }

        public DayActivitiyOptimization(Monitor monitor) : base(monitor)
        {
            UserLogActivities = Range(20000.0);

            SyncContext.AsyncAction(list => UpDate(list), Monitor?.FilterProcessMonitor?.UserLogActivities);
        }

        protected override void UpDate()
        {
            SyncContext.AsyncAction(list => UpDate(list), Monitor?.FilterProcessMonitor?.UserLogActivities);
        }


        // 20 000 
        private const double size = 20000.0;

        private void UpDate(ICollection<ProcessActivity> list)
        {

            if (list == null)
                return;

            lock (SyncRoot)
            {
                foreach (var item in list)
                {

                    for (int 
                        i = (int)Math.Round(Tools.Math.Position.ChoisenPixel(item.Start, size)),
                        len = (int)Math.Round(Tools.Math.Position.ChoisenPixel(item.End, size)); i < len; i++)
                    {
                        UserLogActivities[i] = item;
                    }

                }
            }
        }

        private ObservableCollection<ProcessActivity> Range(double count)
        {
            var users = new ObservableCollection<ProcessActivity>();
            for (int i = 0; i < count; i++)
            {
                users.Add(new ProcessActivity());
            }
            return users;
        }

        private ObservableCollection<ProcessActivity> userLogActivities;
        public ObservableCollection<ProcessActivity> UserLogActivities
        {
            get { return userLogActivities; }
            set { Set(ref userLogActivities, value); }
        }
    }
}
