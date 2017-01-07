using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class ProcessWork : Activity, IProcessWork<GroundWorkState>
    {
        public ObservableCollection<GroundWorkState> GroundWorkStates { get; set; }
        

        public ObservableCollection<GroundWorkState> LeadGroundWorkStates { get; set; }
    }
}
