using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IGroundWorkState : IActivity
    {
        string Title { get; set; }
    }
}
