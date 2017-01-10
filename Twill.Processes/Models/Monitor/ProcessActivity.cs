using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class ProcessActivity : Interval, IProcessActivity<ProcessDayActivity, ProcessWork, GroundWorkState>
    {
        public ObservableCollection<GroundWorkState> GroundWorkStates { get; set; } 

        public ProcessDayActivity LinkProcess { get; set; }
    }
}
