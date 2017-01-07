using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
   public class Activity : IActivity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double TotalMinutesInterval => (End - Start).TotalMinutes;
        public bool IsAlive { get; set; }
    }
}
