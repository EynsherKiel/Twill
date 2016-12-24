using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class ProcessWork : IProcessWork<GroundWorkState>
    {
       
        public DateTime? EndWork { get; set; }
        public DateTime StartWork { get; set; }

        public ObservableCollection<GroundWorkState> GroundWorkStates { get; set; }

    }
}
