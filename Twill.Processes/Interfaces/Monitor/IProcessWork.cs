using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IProcessWork<T> : IActivity where T : IGroundWorkState
    { 
        bool IsAlive { get; set; }
        ObservableCollection<T> GroundWorkStates { get; set; }
        ObservableCollection<T> LeadGroundWorkStates { get; set; }
    }
}
