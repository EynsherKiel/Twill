﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class ProcessMonitor : IProcessMonitor<ProcessDayActivity, ProcessWork, GroundWorkState, ProcessActivity>
    {
        public ObservableCollection<ProcessDayActivity> Processes { get; set; }
        public ProcessDayActivity Lead { get; set; }
        public ObservableCollection<ProcessActivity> UserLogActivities { get; set; }

        public string LeadTitle => UserLogActivities?.Last()?.GroundWorkStates?.Last()?.Title ?? string.Empty;

         
    }
}
