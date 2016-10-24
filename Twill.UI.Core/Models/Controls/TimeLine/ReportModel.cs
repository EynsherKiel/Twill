using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Data;

namespace Twill.UI.Core.Models.Controls.TimeLine
{ 
   public class ReportModel : Labor
    {
        public ReportModel()
        {
        } 

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged(nameof(Text));
            }
        } 
    }

}
