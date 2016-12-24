using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IGroundWorkState
    {
        string Title { get; set; }
        TimeSpan? UsingTime { get; }

        DateTime? StartWork { get; set; }
        DateTime? EndWork { get; set; }

    }
}
