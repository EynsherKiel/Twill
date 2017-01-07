using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class GroundWorkState : Activity, IGroundWorkState
    {
        public string Title { get; set; }  
    }
}
