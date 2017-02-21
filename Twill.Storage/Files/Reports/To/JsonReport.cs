using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Interfaces.Reports;

namespace Twill.Storage.Files.Reports.To
{
    public class JsonReport<T> : BaseReports<T> where T : IReport
    {
        public override DayReport<T> Deserialize(string text) => Newtonsoft.Json.JsonConvert.DeserializeObject<JsonReport<T>>(text)?.DayReports?.FirstOrDefault();

        public override string Serialize() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
