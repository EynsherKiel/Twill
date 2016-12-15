using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class ProcessDayActivity : IProcessDayActivity<ProcessWork, GroundWorkState>
    {
        public string ProcessName { get; }

        public ObservableCollection<ProcessWork> Activities { get; set; }

    }
}
