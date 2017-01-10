using System;

namespace Twill.Processes.Interfaces.Monitor
{
    public interface IActivity : IInterval
    { 
        bool IsAlive { get; set; }
    }
}