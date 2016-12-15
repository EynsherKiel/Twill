using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IProcessDayActivity<T1, T2> 
        where T1 : IProcessWork<T2>
        where T2 : IGroundWorkState
    {

        string ProcessName { get; }

        ObservableCollection<T1> Activities { get; set; }

    }
}
