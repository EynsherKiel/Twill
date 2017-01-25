using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models.Monitor;
using Twill.Processes.Tracking;
using Twill.Tools.Async;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class Monitor : BaseMonitor<ProcessMonitor, ProcessDayActivity, ProcessWork, GroundWorkState, ProcessActivity>
    {
        public Monitor() { }
        public Monitor(LightProcessMonitor lightProcessMonitor) : base(lightProcessMonitor) { }

        private readonly SyncContext SyncContext = new SyncContext();

        protected override void Runtime(Action action)
        {
            SyncContext.Action(e => action(), string.Empty);
        }
    }
}
