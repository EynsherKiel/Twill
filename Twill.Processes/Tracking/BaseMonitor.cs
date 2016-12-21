using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Search;
using Twill.Processes.Windows;
using Twill.Tools.Architecture;

namespace Twill.Processes.Tracking
{
    // abstract ?
    public class BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState>
        where TProcessMonitor : IProcessMonitor<TProcessDayActivity, TProcessWork, TGroundWorkState>, new()
        where TProcessDayActivity : IProcessDayActivity<TProcessWork, TGroundWorkState>, new()
        where TProcessWork : IProcessWork<TGroundWorkState>, new()
        where TGroundWorkState : IGroundWorkState, new()
    {

        public BaseMonitor()
        {
            Environ.UpdateEvent += Environ_UpdateEvent;
        }

        public event Action<BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState>> UpDateEvent = delegate { };

        private Environ Environ = new Environ();


        public TProcessMonitor ProcessMonitor { get; private set; } = new TProcessMonitor();

        public TimeSpan TimeUpdate
        {
            get { return Environ.TimeUpdate; }
            set { Environ.TimeUpdate = value; }
        }


        private void Environ_UpdateEvent(Environ obj)
        {



            UpDateEvent(this);
        }


        ~BaseMonitor()
        {
            if (Environ != null)
                Environ.UpdateEvent += Environ_UpdateEvent;
        }
    }
}
