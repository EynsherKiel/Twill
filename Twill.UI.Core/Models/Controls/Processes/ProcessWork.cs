using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using System.Collections.ObjectModel;
using Twill.UI.Core.Data;

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
         

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { Set(ref isAlive, value); }
        }
         
    }
}
