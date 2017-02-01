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
        public static new T1 Deserialize<T1>(string text) where T1 : JsonReport<T> => Newtonsoft.Json.JsonConvert.DeserializeObject<T1>(text);
        public override string Serialize() => Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}
