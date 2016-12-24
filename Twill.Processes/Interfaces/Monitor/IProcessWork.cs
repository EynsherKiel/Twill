using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IProcessWork<T> where T : IGroundWorkState
    {
        DateTime StartWork { get; set; }
        DateTime? EndWork { get; set; }
        
        ObservableCollection<T> GroundWorkStates { get; set; }
    }
}
