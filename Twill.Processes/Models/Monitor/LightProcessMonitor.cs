using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Models.Monitor
{
    public class LightProcessMonitor
    {
        public DateTime Date { get; set; }
        public List<ProcessDayActivity> Processes { get; set; }
    }
}
