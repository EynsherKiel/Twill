using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models;
using Twill.Processes.WMI;
using Twill.Processes.Interfaces;
using Twill.Processes.Interfaces.WMI;

namespace Twill.Processes.Windows
{
    public class Process : BaseProcess, IProcess
    {
        public Process(IntPtr handle) : base(handle)
        { 
            WMIManager.UpDateProcess(this, DesktopSearcher.GetProcessId(handle));
        }

        private WMI.Manager WMIManager = new WMI.Manager();

        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public TimeSpan WorkTime => DateTime.Now - CreationDate;

    }
}
