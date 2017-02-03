using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files.Reports
{
    public class DayReport<T> where T : Interfaces.Reports.IReport
    {
        public DateTime Date { get; set; }

        public List<T> Reports { get; set; } = new List<T>();
    }
}
