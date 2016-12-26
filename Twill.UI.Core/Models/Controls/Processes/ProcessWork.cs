using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using System.Collections.ObjectModel;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class ProcessWork : ViewModelBase, IProcessWork<GroundWorkState>
    {
        private DateTime? endWork;
        public DateTime? EndWork
        {
            get { return endWork; }
            set{ Set(ref endWork, value);}
        }

        private ObservableCollection<GroundWorkState> groundWorkStates;
        public ObservableCollection<GroundWorkState> GroundWorkStates
        {
            get { return groundWorkStates; }
            set { Set(ref groundWorkStates, value); }
        }

        private DateTime startWork;
        public DateTime StartWork
        {
            get { return startWork; }
            set { Set(ref startWork, value); }
        }
    }
}
