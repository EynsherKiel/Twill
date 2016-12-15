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
        DateTime UsingTime { get; set; }
    }
}
