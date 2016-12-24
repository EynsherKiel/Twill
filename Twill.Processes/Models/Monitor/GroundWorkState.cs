using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;

namespace Twill.Processes.Models.Monitor
{
    public class GroundWorkState : IGroundWorkState
    {

        public DateTime? StartWork { get; set; }

        public string Title { get; set; }
        public DateTime? EndWork { get; set; }


        public TimeSpan? UsingTime => StartWork == null || EndWork == null ? null : EndWork - StartWork;
    }
}
