using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Twill.Storage.Interfaces.Reports;

namespace Twill.Storage.Files.Reports.To
{
    public class XMLReport<T> : BaseReports<T> where T : IReport
    { 
        public override DayReport<T> Deserialize(string text)
        {
            var doc = new XmlDocument();
            doc.LoadXml(text);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<XMLReport<T>>(Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc))?.DayReports?.FirstOrDefault();
        }

        public override string Serialize() => Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Newtonsoft.Json.JsonConvert.SerializeObject(this))?.InnerXml;
    }
}
