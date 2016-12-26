using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Tracking;
using Twill.Tools.Async;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class Monitor : BaseMonitor<ProcessMonitor, ProcessDayActivity, ProcessWork, GroundWorkState>
    {

        private readonly SyncContext SyncContext = new SyncContext();

        protected override void Runtime(Action action)
        {
            SyncContext.Action(e => action(), string.Empty);
        }
    }
}
