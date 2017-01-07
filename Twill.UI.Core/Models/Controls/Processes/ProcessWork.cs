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
    public class ProcessWork : Activity, IProcessWork<GroundWorkState>
    {
        
        private ObservableCollection<GroundWorkState> groundWorkStates;
        public ObservableCollection<GroundWorkState> GroundWorkStates
        {
            get { return groundWorkStates; }
            set { Set(ref groundWorkStates, value); }
        }
         
         

        private ObservableCollection<GroundWorkState> leadGroundWorkStates;
        public ObservableCollection<GroundWorkState> LeadGroundWorkStates
        {
            get { return leadGroundWorkStates; }
            set { Set(ref leadGroundWorkStates, value); }
        }
    }
}
