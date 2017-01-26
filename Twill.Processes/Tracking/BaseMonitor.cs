using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Windows;
using Twill.Tools.Collections;
using Twill.Tools.Events;

namespace Twill.Processes.Tracking
{
    public abstract class BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity> : IWeakEventListener, ISmartWeakEventManager
        where TProcessMonitor : class, IProcessMonitor<TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity>, new()
        where TProcessDayActivity : class, IProcessDayActivity<TProcessWork, TGroundWorkState>, new()
        where TProcessWork : class, IProcessWork<TGroundWorkState>, new()
        where TGroundWorkState : class, IGroundWorkState, new()
        where TProcessActivity : class, IProcessActivity<TProcessDayActivity, TProcessWork, TGroundWorkState>, new()
    {
        public BaseMonitor() { }

        public BaseMonitor(Models.Monitor.LightProcessMonitor lightProcessMonitor)
        {
            SetLightClone(lightProcessMonitor);
        }

        private Environ Environ;
        private object SyncRoot = new object();

        public TProcessMonitor ProcessMonitor { get; private set; } = new TProcessMonitor();
        public TProcessMonitor FilterProcessMonitor { get; private set; } = new TProcessMonitor();
        public ObservableCollection<string> FilterProcessNames { get; private set; } = new ObservableCollection<string>();

        public DateTime Date { get; private set; } = DateTime.Now.Date;

        public TimeSpan TimeUpdate
        {
            get { return Environ?.TimeUpdate ?? TimeSpan.Zero; }
            set { if (Environ != null) Environ.TimeUpdate = value; }
        }

        public event EventHandler<EventArgs> Event = delegate { };

        public void SubscribeUpDateEvent(IWeakEventListener obj) =>
            SmartWeakEventManager<BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity>>.AddListener(this, obj);

        public void UnSubscribeUpDateEvent(IWeakEventListener obj) =>
            SmartWeakEventManager<BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity>>.RemoveListener(this, obj);


        // Reload for WPF
        protected virtual void Runtime(Action action) => action();

        private void Environ_UpdateEvent(object sender, EventArgs e)
        {
            var environ = sender as Environ;
            if (environ == null)
                return;

            Runtime(() => WriteNewData(environ));

            Event(this, EventArgs.Empty);
        }
        public void StartWatch()
        {
            lock (SyncRoot)
            {
                if (Environ == null)
                {
                    Environ = new Environ();
                    Environ.SubscribeUpDateEvent(this);
                }
            }
        }

        public void StopWatch()
        {
            lock (SyncRoot)
            {
                if (Environ != null)
                {
                    Environ.UnSubscribeUpDateEvent(this);
                    Environ = null;
                }
            }
        }

        public Models.Monitor.LightProcessMonitor GetLightClone()
        {
            if (ProcessMonitor?.Processes == null)
                return null;

            var list = new List<Models.Monitor.ProcessDayActivity>();

            foreach (var process in this.ProcessMonitor.Processes)
            {
                var tpda = new Models.Monitor.ProcessDayActivity();
                tpda.Name = process.Name;
                tpda.Activities = new ObservableCollection<Models.Monitor.ProcessWork>();

                foreach (var activity in process.Activities)
                {
                    var processwork = new Models.Monitor.ProcessWork();

                    processwork.End = activity.End;
                    processwork.Start = activity.Start;

                    processwork.LeadGroundWorkStates = new ObservableCollection<Models.Monitor.GroundWorkState>();
                    if (activity.LeadGroundWorkStates != null)
                    {
                        foreach (var lead in activity.LeadGroundWorkStates)
                        {
                            var lgws = new Models.Monitor.GroundWorkState();

                            lgws.End = lead.End;
                            lgws.Start = lead.Start;
                            lgws.Title = lead.Title;

                            processwork.LeadGroundWorkStates.Add(lgws);
                        }
                    }

                    tpda.Activities.Add(processwork);
                }
                list.Add(tpda);
            }
            return new Models.Monitor.LightProcessMonitor() { Date = this.Date, Processes = list };

        }

        private void SetLightClone(Models.Monitor.LightProcessMonitor lightProcessMonitor)
        {
            ProcessMonitor = new TProcessMonitor();
            ProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            Date = lightProcessMonitor.Date;

            var leads = new List<Tuple<TProcessDayActivity, TGroundWorkState>>();

            if (lightProcessMonitor?.Processes == null)
                return;

            foreach (var process in lightProcessMonitor.Processes)
            {
                var tpda = new TProcessDayActivity();
                tpda.Name = process.Name;
                tpda.Activities = new ObservableCollection<TProcessWork>();

                foreach (var activity in process.Activities)
                {
                    var processwork = new TProcessWork();

                    processwork.End = activity.End;
                    processwork.Start = activity.Start;

                    processwork.LeadGroundWorkStates = new ObservableCollection<TGroundWorkState>();
                    if (activity.LeadGroundWorkStates != null)
                    {
                        foreach (var lead in activity.LeadGroundWorkStates)
                        {
                            var lgws = new TGroundWorkState();

                            lgws.End = lead.End;
                            lgws.Start = lead.Start;
                            lgws.Title = lead.Title;

                            leads.Add(new Tuple<TProcessDayActivity, TGroundWorkState>(tpda, lgws));

                            processwork.LeadGroundWorkStates.Add(lgws);
                        }
                    }

                    tpda.Activities.Add(processwork);
                }
                ProcessMonitor.Processes.Add(tpda);
            }

            ProcessMonitor.UserLogActivities = new ObservableCollection<TProcessActivity>(leads.OrderBy(lead => lead.Item2.Start).Select(lead => ProcessActivityFromGroundAndLink(lead.Item2, lead.Item1)));
        }


        public void Filtering()
        {
            if (FilterProcessMonitor == null)
                FilterProcessMonitor = new TProcessMonitor();

            if (FilterProcessMonitor.Processes == null)
                FilterProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            if (FilterProcessMonitor.UserLogActivities == null)
                FilterProcessMonitor.UserLogActivities = new ObservableCollection<TProcessActivity>();

            // filtering processes
            IList<TProcessDayActivity> processes = FilterProcessMonitor.Processes;
            ProcessMonitor.Processes.UpdateLinksWithFilter(FilterProcessNames, (listelement, filterelement) => listelement.Name == filterelement, ref processes);


            // filtering userlogactivities
            IList<TProcessActivity> userLogActivities = FilterProcessMonitor.UserLogActivities;
            ProcessMonitor.UserLogActivities.UpdateLinksWithFilter(FilterProcessNames, (listelement, filterelement) => listelement.LinkProcess.Name == filterelement, ref userLogActivities);


            // filtering lead
            if (ProcessMonitor.Lead == null || FilterProcessNames.Contains(ProcessMonitor.Lead.Name))
            {
                FilterProcessMonitor.Lead = default(TProcessDayActivity);
            }
            else
            {
                FilterProcessMonitor.Lead = ProcessMonitor.Lead;
            }
        }

        private void WriteNewData(Environ environ)
        {
            var dateNow = DateTime.Now;
            var time = dateNow.TimeOfDay;


            if (ProcessMonitor == null)
                ProcessMonitor = new TProcessMonitor();

            if (ProcessMonitor.Processes == null)
                ProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            if (ProcessMonitor.UserLogActivities == null)
                ProcessMonitor.UserLogActivities = new ObservableCollection<TProcessActivity>();

            if (environ.ProcessDockers == null)
                return;

            if (Date != dateNow.Date)
            {
                Date = dateNow.Date;

                ProcessMonitor.Processes.Clear();
                ProcessMonitor.Lead = null;
                ProcessMonitor.UserLogActivities.Clear();
            }

            foreach (var process in environ.ProcessDockers)
            {
                var currentProcessDayActivity = ProcessMonitor.Processes.FirstOrDefault(p => p.Name == process.Name);

                if (currentProcessDayActivity == null)
                {
                    var work = CreateIActivity<TProcessWork>(time);
                    currentProcessDayActivity = new TProcessDayActivity();
                    currentProcessDayActivity.Name = process.Name; // very bad, change to activator ? >_<


                    currentProcessDayActivity.Activities = new ObservableCollection<TProcessWork>() { work };

                    ProcessMonitor.Processes.Add(currentProcessDayActivity);
                }

                // work with current
                var lastProcWork = currentProcessDayActivity.Activities.Last();
                if (!lastProcWork.IsAlive)
                {
                    lastProcWork = CreateIActivity<TProcessWork>(time);
                    currentProcessDayActivity.Activities.Add(lastProcWork);
                }
                else
                {
                    lastProcWork.End = time;
                }
            }

            // delete was ended process
            foreach (var process in ProcessMonitor.Processes.Where(proc => !environ.ProcessDockers.Select(p => p.Name).Contains(proc.Name)))
            {
                var lastActivity = process.Activities.LastOrDefault();
                if (lastActivity == null)
                {
                    // miiistake (scrubs^_^)
                    MessageBox.Show(string.Format("{0} not work", "BaseMonitor"));
                    continue;
                }

                if (lastActivity.IsAlive)
                {
                    lastActivity.IsAlive = false;
                    lastActivity.End = time;
                }
            }


            var lastUserActivity = ProcessMonitor.UserLogActivities.LastOrDefault();

            if (environ.Lead == null)
            {
                ProcessMonitor.Lead = default(TProcessDayActivity);

                if (lastUserActivity != null)
                {
                    var groundStateWork = lastUserActivity.GroundWorkStates.First();

                    if (groundStateWork.IsAlive)
                    {
                        lastUserActivity.End = time;
                        groundStateWork.End = time;
                        groundStateWork.IsAlive = false;
                    }
                }
            }
            else
            {
                ProcessMonitor.Lead = ProcessMonitor.Processes.First(p => p.Name == environ.Lead.Name);

                if (lastUserActivity == null)
                {
                    FillingUserLogActivitys(NewGroundStateWork(time, environ.Lead.Lead.Title));
                }
                else
                {
                    var process = lastUserActivity.LinkProcess;
                    var groundStateWork = lastUserActivity.GroundWorkStates.First();


                    if (groundStateWork.IsAlive)
                    {
                        if (time - lastUserActivity.End < TimeSpan.FromSeconds(10))
                        {
                            lastUserActivity.End = time;
                            groundStateWork.End = time;
                        }
                        else
                        {
                            groundStateWork.IsAlive = false;
                        }
                    }


                    if (groundStateWork.Title != environ.Lead.Lead.Title ||
                        process.Name != ProcessMonitor.Lead.Name ||
                        !groundStateWork.IsAlive)
                    {
                        groundStateWork.IsAlive = false;

                        FillingUserLogActivitys(NewGroundStateWork(time, environ.Lead.Lead.Title));
                    }
                }
            }


            Filtering();
        }

        private void FillingUserLogActivitys(TGroundWorkState groundStateWork)
        {
            TProcessActivity data = ProcessActivityFromGroundAndLink(groundStateWork, ProcessMonitor.Lead);

            ProcessMonitor.UserLogActivities.Add(data);

            var lastactivity = ProcessMonitor.Lead.Activities.Last();

            if (lastactivity.LeadGroundWorkStates == null)
                lastactivity.LeadGroundWorkStates = new ObservableCollection<TGroundWorkState>();

            lastactivity.LeadGroundWorkStates.Add(groundStateWork);
        }

        private TProcessActivity ProcessActivityFromGroundAndLink(TGroundWorkState groundStateWork, TProcessDayActivity process)
        {
            var data = new TProcessActivity();

            data.Start = groundStateWork.Start;
            data.End = groundStateWork.End;

            data.LinkProcess = process;
            data.GroundWorkStates = new ObservableCollection<TGroundWorkState>() { groundStateWork };

            return data;
        }

        private T CreateIActivity<T>(TimeSpan now) where T : class, IActivity, new()
        {
            var activity = new T();

            activity.IsAlive = true;
            activity.Start = now;
            activity.End = now;

            return activity;
        }

        private TGroundWorkState NewGroundStateWork(TimeSpan now, string title)
        {
            var lastLeadGroundStateWork = CreateIActivity<TGroundWorkState>(now);
            lastLeadGroundStateWork.Title = title;
            return lastLeadGroundStateWork;
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            Environ_UpdateEvent(sender, e);
            return true;
        }
    }
}