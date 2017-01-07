using System;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IActivity
    {
        DateTime Start { get; set; }
        DateTime End { get; set; }
        bool IsAlive { get; set; }
    }
}