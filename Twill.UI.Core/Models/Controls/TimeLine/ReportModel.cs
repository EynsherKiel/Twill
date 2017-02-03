using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Interfaces.Reports;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ReportModel : Interval, IReport
    {
        public ReportModel()
        {
            Start = TimeSpan.Zero;
            End = TimeSpan.FromHours(24);
        }

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }
    }
}