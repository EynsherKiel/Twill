using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.UI.Core.Models.Controls.Utils
{
    public class ChartElementModel : ViewModelBase
    {
        private string processName;
        public string ProcessName
        {
            get { return processName; }
            set { Set(ref processName, value); }
        }


        private int persents;
        public int Persents
        {
            get { return persents; }
            set { Set(ref persents, value); }
        }
    }
}
