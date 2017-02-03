using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Interfaces.Reports
{
    public interface IReport : Twill.Processes.Interfaces.Monitor.IInterval
    {
        string Text { get; set; }
    }
}
