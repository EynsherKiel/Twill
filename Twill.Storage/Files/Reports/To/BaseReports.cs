using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Twill.Storage.Interfaces.Reports;

namespace Twill.Storage.Files.Reports.To
{
    [JsonObject]
    [XmlRoot]
    public abstract class BaseReports<T> : IReportFormatter<T> where T : IReport
    {
        public void Add(DayReport<T> dayReport) 
        {
            
            string shortDateString = dayReport.Date.ToShortDateString();

            var data = DayReports.FirstOrDefault(el => el.Date == dayReport.Date);
            if (data == null)
                DayReports.Add(data = new DayReport<T>() { Date = dayReport.Date });

            data.Reports.AddRange(dayReport.Reports);
        }

        [XmlElement(ElementName = "Report")]
        [JsonProperty]
        public List<DayReport<T>> DayReports { get; set; } = new List<DayReport<T>>();

        public abstract string Serialize();

        public abstract DayReport<T> Deserialize(string text);
    }
}
