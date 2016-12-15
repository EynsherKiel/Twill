using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Interfaces.WMI
{
    public interface IProcess
    {
        string Name { get; set; }
        DateTime CreationDate { get; set; }
    }
}
