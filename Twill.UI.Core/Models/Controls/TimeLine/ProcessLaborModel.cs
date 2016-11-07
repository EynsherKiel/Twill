using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Data;


namespace Twill.UI.Core.Models.Controls.TimeLine
{
    public class ProcessLaborModel : Labor
    {
        public ProcessLaborModel()
        {
            if (IsInDesignMode)
            {
                Headers = new ObservableCollection<ReportModel>() {
                    new ReportModel() { Text = "Twill" },
                    new ReportModel() { Text = "Twill develop" },
                    new ReportModel() { Text = "Fate Series「AMV」- The Beginning ᴴᴰ - YouTube"  }};

                Start = DateTime.Now.AddMinutes(200);
                End = DateTime.Now.AddMinutes(300);
            }
        }

        private ObservableCollection<ReportModel> headers = new ObservableCollection<ReportModel>();
        public ObservableCollection<ReportModel> Headers
        {
            get { return headers; }
            set { Set(nameof(Headers), ref headers, value); }
        }
    }

}
