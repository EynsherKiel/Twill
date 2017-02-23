using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Twill.UI.Core.Models.Controls.Utils;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class DayActivityCommonAnalyse : BaseDayActivityAnalysis
    {
        public DayActivityCommonAnalyse() : base()
        {
            if (IsInDesignMode)
            {

            }
        }
        public DayActivityCommonAnalyse(Monitor monitor) : base(monitor)
        {
            UpDate(Monitor?.ProcessMonitor?.UserLogActivities);
        }

        protected override void UpDate()
        {
            SyncContext.AsyncAction(list => UpDate(list), Monitor?.FilterProcessMonitor?.UserLogActivities);
        }

        private void UpDate(ICollection<ProcessActivity> list)
        {
            if (list == null)
                return;

            lock (SyncRoot)
            {

                var names = Charts.Select(cp => cp.Process.Name).ToList();

                var processes = list.GroupBy(el => el.LinkProcess).ToList();

                if (processes.Count != names.Count || !processes.Any(el => names.Contains(el.Key.Name)))
                { 
                    Charts = new ObservableCollection<ChartElementModel>(processes.Select(process => new ChartElementModel(process.Key))); 
                }

                if (Charts.Count == 0)
                    return;

                var fullsum = processes.Select(process => process.Sum(el => el.TotalMinutesInterval)).Sum();


                var firstprocesses = processes.First();
                var chart = Charts.First(el => el.Process.Name == firstprocesses.Key.Name);

                UpDateChart(chart, fullsum, firstprocesses.Sum(el => el.TotalMinutesInterval), 0.0);

                foreach (var process in processes.Skip(1))
                {
                    var nextchart = Charts.First(el => el.Process.Name == process.Key.Name);

                    UpDateChart(nextchart, fullsum, process.Sum(el => el.TotalMinutesInterval), chart.EndAngle);

                    chart = nextchart;
                }

                var maxvalueelement = Charts.OrderByDescending(el => el.TotalTime).First();

                MostUsedAppplicationName =  maxvalueelement.Process.Name;
                MostUsedAppplicationTimeMinutes = Math.Round(maxvalueelement.TotalTime).ToString();
                AllTime = Math.Round(Charts.Sum(el => el.TotalTime)).ToString();
            }
        }

        private void UpDateChart(ChartElementModel chart, double fullsum, double chartsum, double startprocess)
        {
            chart.StartAngle = startprocess;

            var persent = chartsum / fullsum;

            chart.EndAngle = startprocess + 360.0 * persent;
            chart.TotalTime = chartsum;
        }

        private ObservableCollection<ChartElementModel> charts = new ObservableCollection<ChartElementModel>();
        public ObservableCollection<ChartElementModel> Charts
        {
            get { return charts; }
            set { Set(ref charts, value); }
        }

        private string mostUsedAppplicationName;
        public string MostUsedAppplicationName
        {
            get { return mostUsedAppplicationName; }
            set { Set(ref mostUsedAppplicationName, value); }
        }

        private string mostUsedAppplicationTimeMinutes;
        public string MostUsedAppplicationTimeMinutes
        {
            get { return mostUsedAppplicationTimeMinutes; }
            set { Set(ref mostUsedAppplicationTimeMinutes, value); }
        }

        private string allTime;
        public string AllTime
        {
            get { return allTime; }
            set { Set(ref allTime, value); }
        }
    }
}
