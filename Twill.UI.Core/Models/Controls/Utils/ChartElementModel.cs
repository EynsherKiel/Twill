using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.Models.Controls.Utils
{
    public class ChartElementModel : ViewModelBase
    {
        public ChartElementModel(ProcessDayActivity process)
        {
            Process = process;
        }

        public ProcessDayActivity Process { get; }
        

        private double startAngle;
        public double StartAngle
        {
            get { return startAngle; }
            set { Set(ref startAngle, value); }
        }

        private double endAngle;
        public double EndAngle
        {
            get { return endAngle; }
            set { Set(ref endAngle, value); }
        }


        private double totalTime;
        public double TotalTime
        {
            get { return totalTime; }
            set { Set(ref totalTime, value); }
        }
    }
}
