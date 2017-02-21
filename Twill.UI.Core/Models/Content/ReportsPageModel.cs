using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.Models.Content
{
    public class ReportsPageModel : ViewModelBase
    {
        public ReportsPageModel()
        {
            if (IsInDesignMode)
            {
                ExistingActivitiesDates = new ObservableCollection<DateTime>() { DateTime.Now , DateTime.Now.AddDays(1)};
            }
            else
            {
                ExistingActivitiesDates = BarrierManager.GetAllActivities<ObservableCollection<DateTime>>();
            }
        }

        private Storage.Barrier.Manager BarrierManager = new Storage.Barrier.Manager();


        private ObservableCollection<DateTime> existingActivitiesDates;
        public ObservableCollection<DateTime> ExistingActivitiesDates
        {
            get { return existingActivitiesDates; }
            set { Set(ref existingActivitiesDates ,value); }
        }

        private DayMonitor dayMonitor = null;
        public DayMonitor DayMonitor
        {
            get { return dayMonitor; }
            set { Set(ref dayMonitor, value); }
        }

        private DateTime? selectedDate = null;
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (value == null)
                    DayMonitor = null;
                else
                    DayMonitor = new DayMonitor((DateTime)value);

                Set(ref selectedDate, value);
            }
        }

    }
}
