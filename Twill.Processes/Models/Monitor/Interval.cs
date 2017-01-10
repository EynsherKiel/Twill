using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Models.Monitor
{
    public class Interval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double TotalMinutesInterval => (End - Start).TotalMinutes;
    }
}
