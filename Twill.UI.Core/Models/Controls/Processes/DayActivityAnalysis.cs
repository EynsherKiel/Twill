using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twill.Processes.Tracking;
using Twill.Tools.Async;
using Twill.Tools.Collections;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public class DayActivityAnalysis : ViewModelBase, IWeakEventListener
    {
        public DayActivityAnalysis()
        {
            if (IsInDesignMode)
            {
                Monitor = new Monitor();
            }
            else
            {
                Monitor = Tools.Architecture.Singleton<Monitor>.Instance;
            }
        }


        public DayActivityAnalysis(bool isMonitorStaticInstance)
        {
            if (isMonitorStaticInstance)
            {
                Monitor = Tools.Architecture.Singleton<Monitor>.Instance;
            }
        }

        public DayActivityAnalysis(Monitor monitor)
        {
            Monitor = monitor;
        }

        private void Monitor_UpDateEvent(object obj, EventArgs e) => UpDate();

        private void UpDate(bool isFullUpdate = false)
        {
            lock (SyncRoot)
            {
                if (ContentHeight < 1.0)
                    return;

                if (SegmentMinHeight < 1.0)
                    return;

                Analyse(Monitor?.FilterProcessMonitor?.UserLogActivities, isFullUpdate);
            }
        }

        public void Analyse(ICollection<ProcessActivity> list, bool isFullUpdate = false)
        {
            if (list == null)
                return;

            if (list.Count == 0)
                return;

            var processlist = new List<ProcessActivity>() { list.First() };

            foreach (var last in list.Skip(1))
            {
                var itemslast = processlist.Last();
                if (this.MinutesBetween(itemslast, last) > MinTimeInterval)
                {
                    processlist.Add(new ProcessActivity()
                    {
                        LinkProcess = RestProcess,
                        Start = itemslast.End,
                        End = last.Start,
                        GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = "(￣、￣＠）Ｚｚz" } }
                    });
                }
                processlist.Add(last);
            }

            if ((DateTime.Now - processlist.Last().End).TotalMinutes > MinTimeInterval)
            {
                processlist.Add(new ProcessActivity()
                {
                    LinkProcess = RestProcess,
                    Start = processlist.Last().End,
                    End = DateTime.Now,
                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { new GroundWorkState() { Title = "(￣、￣＠）Ｚｚz" } }
                });
            }

            var _processes = processlist.OrderBy(ac => ac.Start).GroupBy(el => el.LinkProcess.Name).Select(activities =>
            {
                var items = new List<ProcessActivity>() { ProcessActivityClone(activities.First()) };

                foreach (var last in activities.Skip(1))
                {
                    var first = items.Last();
                    if (MinutesBetween(first, last) < MinPixTimeInterval)
                    {
                        if (first.GroundWorkStates.Count == 0 || first.GroundWorkStates.Last().Title != last.GroundWorkStates.First().Title)
                            first.GroundWorkStates.Add(last.GroundWorkStates.First());

                        first.End = last.End;
                    }
                    else
                    {
                        items.Add(ProcessActivityClone(last));
                    }
                }

                return items.Where(item => item.TotalMinutesInterval > MinTimeInterval).ToList();

            }).SelectMany(el => el).OrderBy(ac => ac.Start).ToList();

            SyncContext.Action(processes =>
                {

                    if (isFullUpdate)
                    {
                        ProcessActivities = new ObservableCollection<ProcessActivity>(processes);
                        return;
                    }

                    for (int i = 0; i < ProcessActivities.Count; i++)
                    {
                        if (ProcessActivities[i].LinkProcess.Name == processes[i].LinkProcess.Name)
                        {
                            for (int j = 0; j < ProcessActivities[i].GroundWorkStates.Count; j++)
                            {
                                if (ProcessActivities[i].GroundWorkStates[j].Title != processes[i].GroundWorkStates[j].Title)
                                {
                                    ProcessActivities[i].GroundWorkStates[j].Title = processes[i].GroundWorkStates[j].Title;
                                }
                            }

                            if (ProcessActivities[i].GroundWorkStates.Count < processes[i].GroundWorkStates.Count)
                            {
                                foreach (var gws in processes[i].GroundWorkStates.Skip(ProcessActivities[i].GroundWorkStates.Count))
                                {
                                    ProcessActivities[i].GroundWorkStates.Add(gws);
                                }
                            }

                            ProcessActivities[i].Start = processes[i].Start;
                            ProcessActivities[i].End = processes[i].End;
                        }
                        else
                        {
                            ProcessActivities = new ObservableCollection<ProcessActivity>(processes);
                            return;
                        }
                    }

                    foreach (var process in processes.Skip(ProcessActivities.Count))
                    {
                        ProcessActivities.Add(process);
                    }
                }, _processes);
        }


        private double MinutesBetween(ProcessActivity first, ProcessActivity last) => (last.Start - first.End).TotalMinutes;

        private ProcessActivity ProcessActivityClone(ProcessActivity activity) =>
            new ProcessActivity()
            {
                Start = activity.Start,
                End = activity.End,
                LinkProcess = activity.LinkProcess,

                GroundWorkStates = new ObservableCollection<GroundWorkState>(activity.GroundWorkStates.Select(gws => new GroundWorkState() { Title = gws.Title }))
            };


        private double MinTimeInterval = Tools.Math.Position.ChoisenMinute(SegmentMinHeightConstant, ContentHeightConstant);
        private double MinPixTimeInterval = Tools.Math.Position.ChoisenMinute(MinPixSize, ContentHeightConstant);

        private void SetTimes()
        {
            MinTimeInterval = Tools.Math.Position.ChoisenMinute(segmentMinHeight, ContentHeight);
            MinPixTimeInterval = Tools.Math.Position.ChoisenMinute(MinPixSize, ContentHeight);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            Monitor_UpDateEvent(sender, e);
            return true;
        }

        private readonly SyncContext SyncContext = new SyncContext();
        private object SyncRoot = new object();
        // static for uniq brush
        private static ProcessDayActivity RestProcess = new ProcessDayActivity() { Name = "Your rest" };

        private const double MinPixSize = 4.0;

        private Monitor monitor;
        public Monitor Monitor
        {
            get { return monitor; }
            private set
            {
                Monitor?.UnSubscribeUpDateEvent(this);

                value?.SubscribeUpDateEvent(this);

                Set(ref monitor, value);
            }
        } 

        public const double SegmentMinHeightConstant = 47.0;
        private double segmentMinHeight = SegmentMinHeightConstant;
        public double SegmentMinHeight
        {
            private get { return segmentMinHeight; }
            set { segmentMinHeight = value; SetTimes(); UpDate(true); }
        }

        public const double ContentHeightConstant = 20000.0;
        private double contentHeight = ContentHeightConstant;
        public double ContentHeight
        {
            private get { return contentHeight; }
            set { contentHeight = value; SetTimes(); UpDate(true); }
        }

        private ObservableCollection<ProcessActivity> processActivities = new ObservableCollection<ProcessActivity>();
        public ObservableCollection<ProcessActivity> ProcessActivities
        {
            get { return processActivities; }
            set { Set(ref processActivities, value); }
        } 
    }
}
