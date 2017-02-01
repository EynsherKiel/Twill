using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class ProcessActivity : Interval, IProcessActivity<ProcessDayActivity, ProcessWork, GroundWorkState>
    {
        public ProcessActivity()
        {
        }

        private ProcessDayActivity linkProcess;
        public ProcessDayActivity LinkProcess
        {
            get { return linkProcess; }
            set { Set(ref linkProcess, value); }
        }


        private ObservableCollection<GroundWorkState> groundWorkStates = new ObservableCollection<GroundWorkState>();
        public ObservableCollection<GroundWorkState> GroundWorkStates
        {
            get { return groundWorkStates; }
            set { Set(ref groundWorkStates, value); }
        }
         

    }
}
