using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using System.Collections.ObjectModel;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class ProcessMonitor : ViewModelBase, IProcessMonitor<ProcessDayActivity, ProcessWork, GroundWorkState>
    {
        private ProcessDayActivity lead;
        public ProcessDayActivity Lead
        {
            get { return lead; }
            set
            {
                Set(ref lead, value);
                RaisePropertyChanged(nameof(LeadTitle));
            }
        }

        public string LeadTitle => Lead?.Activities?.LastOrDefault()?.GroundWorkStates?.LastOrDefault()?.Title ?? string.Empty;


        private ObservableCollection<ProcessDayActivity> processes;
        public ObservableCollection<ProcessDayActivity> Processes
        {
            get { return processes; }
            set { Set(ref processes, value); }
        }
    }
}
