using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IProcessMonitor<T1, T2, T3>
        where T1 : IProcessDayActivity<T2, T3>
        where T2 : IProcessWork<T3>
        where T3 : IGroundWorkState
    {
        ObservableCollection<T1> Processes { get; set; }
        T1 Lead { get; set; }
        string LeadTitle { get; }
    }
}
