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
        public static new T1 Deserialize<T1>(string text) where T1 : XMLReport<T>
        {
            var doc = new XmlDocument();
            doc.LoadXml(text);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T1>(Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc));
        }
        public override string Serialize() => (Newtonsoft.Json.JsonConvert.DeserializeXmlNode(Newtonsoft.Json.JsonConvert.SerializeObject(this)) as XmlDocument)?.InnerXml;
    }
}
