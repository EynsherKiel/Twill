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
        }

        public DayActivityAnalysis(Monitor monitor)
        {
            Monitor = monitor;
        }


        private void Monitor_UpDateEvent(BaseMonitor<ProcessMonitor, ProcessDayActivity, ProcessWork, GroundWorkState, ProcessActivity> obj) => UpDate();

        private void UpDate()
        {

            if (ContentHeight < 1.0)
                return;

            if (SegmentMinHeight < 1.0)
                return;
             
            var items = new List<ProcessActivity>();

            var first = Monitor.FilterProcessMonitor?.UserLogActivities?.FirstOrDefault();
            if (first != null)
            { 
                if (Monitor.FilterProcessMonitor.UserLogActivities.Count == 1)
                {
                    Console.WriteLine("Count == 1");
                    if (first.TotalMinutesInterval > MinTimeInterval)
                    {
                        ProcessActivities = new ObservableCollection<ProcessActivity>() { first };
                    }
                }
                else
                {
                    ProcessActivity activity = null;
                    foreach (ProcessActivity last in Monitor.FilterProcessMonitor.UserLogActivities.Skip(1))
                    {

                        // findrest
                        Console.WriteLine($"{first.LinkProcess.Name} == {last.LinkProcess.Name}");

                        // if you moved to a new tab from this process
                        if (first.LinkProcess.Name == last.LinkProcess.Name)
                        {
                            if (activity == null)
                            {
                                activity = new ProcessActivity()
                                {
                                    Start = first.Start,
                                    LinkProcess = first.LinkProcess,
                                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { first.GroundWorkStates.First() }
                                };
                            }

                            activity.GroundWorkStates.Add(last.GroundWorkStates.First());
                            activity.End = last.End;

                            if (activity.TotalMinutesInterval > MinTimeInterval)
                            {
                                if (items.LastOrDefault() != activity)
                                    items.Add(activity);
                            }

                            continue;
                        }


                        if ((first.End - last.End).TotalMinutes < MinPixTimeInterval)
                        {
                            if (activity == null || activity.LinkProcess != first.LinkProcess)
                            {
                                activity = new ProcessActivity()
                                {
                                    Start = first.Start,
                                    LinkProcess = first.LinkProcess,
                                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { first.GroundWorkStates.First() }
                                };
                            }

                            activity.End = last.End;

                            if (activity.TotalMinutesInterval > MinTimeInterval)
                            {
                                if (items.LastOrDefault() != activity)
                                    items.Add(activity);
                            }

                            continue;
                        }

                        if ((first.End - last.Start).TotalMinutes > MinTimeInterval)
                        {
                            if (activity == null || activity.LinkProcess != RestProcess)
                            {
                                activity = new ProcessActivity()
                                {
                                    Start = first.End,
                                    End = last.Start,
                                    LinkProcess = RestProcess,
                                };

                                activity.GroundWorkStates.Add(new GroundWorkState() { Title = "(￣、￣＠）Ｚｚz" });

                                items.Add(activity);
                            }
                        }


                        first = last;

                        if (last.TotalMinutesInterval > MinTimeInterval)
                        {

                            if (activity == null || activity.LinkProcess != first.LinkProcess)
                            {
                                activity = new ProcessActivity()
                                {
                                    Start = first.Start,
                                    LinkProcess = first.LinkProcess,
                                    GroundWorkStates = new ObservableCollection<GroundWorkState>() { first.GroundWorkStates.First() }
                                };
                            }

                            activity.End = last.End;

                            if (activity.TotalMinutesInterval > MinTimeInterval)
                            {
                                if (items.LastOrDefault() != activity)
                                    items.Add(activity);
                            }
                            continue;
                        }



                    }



                    if ((DateTime.Now - Monitor.FilterProcessMonitor.UserLogActivities.Last().End).TotalMinutes > MinTimeInterval && activity?.LinkProcess != RestProcess)
                    {
                    //    Console.WriteLine("Ｚｚz");
                        activity = new ProcessActivity()
                        {
                            Start = first.End,
                            End = DateTime.Now,
                            LinkProcess = RestProcess,
                        };

                        activity.GroundWorkStates.Add(new GroundWorkState() { Title = "(￣、￣＠）Ｚｚz" });

                        items.Add(activity);
                    }

                }
            } 

            ProcessActivities = new ObservableCollection<ProcessActivity>(items);
        }
         
        private double MinTimeInterval =  Tools.Math.Position.ChoisenMinute(SegmentMinHeightConstant, ContentHeightConstant);
        private double MinPixTimeInterval =  Tools.Math.Position.ChoisenMinute(MinPixSize, ContentHeightConstant);

        private void SetTimes()
        {
            MinTimeInterval =  Tools.Math.Position.ChoisenMinute(segmentMinHeight, ContentHeight);
            MinPixTimeInterval =  Tools.Math.Position.ChoisenMinute(MinPixSize, ContentHeight);
        }

        // static for uniq brush
        private static ProcessDayActivity RestProcess = new ProcessDayActivity() { Name = "Your rest" };

        private const double MinPixSize = 2.0;

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

        public const double SegmentMinHeightConstant = 3.0;
        private double segmentMinHeight = SegmentMinHeightConstant;
        public double SegmentMinHeight 
        {
            private get { return segmentMinHeight; }
            set { segmentMinHeight = value; SetTimes(); UpDate(); }
        }

        public const double ContentHeightConstant = 20000.0;
        private double contentHeight = ContentHeightConstant;
        public double ContentHeight
        {
            private get { return contentHeight; }
            set { contentHeight = value; SetTimes(); UpDate(); }
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
