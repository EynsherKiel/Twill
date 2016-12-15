using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Search;
//using Twill.Processes.Windows;
using Twill.Tools.Architecture;

namespace Twill.Processes.Tracking
{
    public class BaseMonitor<T1, T2, T3, T4> 
        where T1 : IProcessMonitor<T2,T3,T4>
        where T2 : IProcessDayActivity<T3, T4>
        where T3 : IProcessWork<T4>
        where T4 : IGroundWorkState
    {

        public T1 ProcessMonitor { get; set; }


    }
}
