using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twill.Processes.Tracking;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class DayActivityAnalysis : ViewModelBase
    {
        public DayActivityAnalysis()
        {
            Monitor = Tools.Architecture.Singleton<Monitor>.Instance;

            if (IsInDesignMode)
            {
                ProcessActivities.Add(new ProcessActivity());
            }
        }

        public DayActivityAnalysis(Monitor monitor)
        {
            Monitor = monitor;
        }


        private void Monitor_UpDateEvent(BaseMonitor<ProcessMonitor, ProcessDayActivity, ProcessWork, GroundWorkState, ProcessActivity> obj) => UpDate();

        private void UpDate()
        { 
            if (ContentHeight < 0)
                return;

            if (SegmentMinHeight < 0)
                return; 
        }


        private Monitor monitor;
        public Monitor Monitor
        {
            get { return monitor; }
            private set
            {
                if (Monitor != null)
                    Monitor.UpDateEvent -= Monitor_UpDateEvent;

                if (value != null)
                    value.UpDateEvent += Monitor_UpDateEvent;

                Set(ref monitor, value);
            }
        }

        private double segmentMinHeight = 100.0;
        public double SegmentMinHeight 
        {
            private get { return segmentMinHeight; }
            set { segmentMinHeight = value; ; UpDate(); }
        }

        private double contentHeight = 20000.0;
        public double ContentHeight
        {
            private get { return contentHeight; }
            set { contentHeight = value; UpDate(); }
        }

        private ObservableCollection<ProcessActivity> processActivities = new ObservableCollection<ProcessActivity>();
        public ObservableCollection<ProcessActivity> ProcessActivities
        {
            get { return processActivities; }
            set { Set(ref processActivities, value); }
        }

        ~DayActivityAnalysis()
        {
            if (Monitor != null)
                Monitor.UpDateEvent -= Monitor_UpDateEvent;
        }
    }
}
