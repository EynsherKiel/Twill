using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Files.Reports;

namespace Twill.Storage.Interfaces.Reports
{
    internal interface IReportFormatter<T> where T : IReport
    {
        void Add(DayReport<T> dayReport);
        string Serialize(); 
    }
}
