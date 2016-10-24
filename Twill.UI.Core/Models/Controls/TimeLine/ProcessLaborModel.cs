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
                Headers = new ObservableCollection<string>() { "Twill", "Twill develop", "Fate Series「AMV」- The Beginning ᴴᴰ - YouTube" };

                Start = DateTime.Now.AddMinutes(200);
                End = DateTime.Now.AddMinutes(300);
            }
        }

        private ObservableCollection<string> headers = new ObservableCollection<string>();
        public ObservableCollection<string> Headers
        {
            get { return headers; }
            set
            {
                headers = value;
                RaisePropertyChanged(nameof(Headers));
            }
        }
    }

}
