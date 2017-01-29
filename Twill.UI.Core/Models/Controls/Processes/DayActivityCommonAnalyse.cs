using De.TorstenMandelkow.MetroChart;
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

        }

        protected override void UpDate()
        {
            SyncContext.AsyncAction(list => UpDate(list), Monitor?.FilterProcessMonitor?.UserLogActivities);
        }

        private void UpDate(ICollection<ProcessActivity> list)
        {
            lock (SyncRoot)
            {
                var names = CommonProcesses.Select(cp => cp.ProcessName).ToList();

                var processes = list.GroupBy(el => el.LinkProcess).ToList();

                if (processes.Count != names.Count || !processes.Any(el => names.Contains(el.Key.Name))) 
                {
                    var palete = new ResourceDictionaryCollection();
                    var commonProcesses = new ObservableCollection<ChartElementModel>();

                    foreach (var item in processes)
                    {
                        var resources = new ResourceDictionary();
                        resources.Add($"Brush{palete.Count + 1}", item.Key.Brush);
                        palete.Add(resources);

                        commonProcesses.Add(new ChartElementModel() { ProcessName = item.Key.Name });
                    }

                    Palette = palete;
                    CommonProcesses = commonProcesses;
                }


                foreach (var item in processes)
                {
                    CommonProcesses.First(el => el.ProcessName == item.Key.Name).Persents = (int)item.Sum(el => el.TotalMinutesInterval);
                }

            }
        }

        private ObservableCollection<ChartElementModel> commonProcesses = new ObservableCollection<ChartElementModel>();
        public ObservableCollection<ChartElementModel> CommonProcesses
        {
            get { return commonProcesses; }
            set { Set(ref commonProcesses, value); }
        }

        private ResourceDictionaryCollection palette = new ResourceDictionaryCollection();
        public ResourceDictionaryCollection Palette
        {
            get { return palette; }
            set { Set(ref palette, value); }
        }

    }
}
