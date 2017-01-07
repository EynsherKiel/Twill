using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ReportModel : Activity
    {
        public ReportModel()
        {
            Start = DateTime.Now.Date;
            End = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }
    }
}