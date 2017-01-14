using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Search;
using Twill.Processes.Windows;
using Twill.Tools.Architecture;
using Twill.Tools.Collections;
using Twill.Tools.Events;

namespace Twill.Processes.Tracking
{ 
    public abstract class BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity> : IWeakEventListener, ISmartWeakEventManager
        where TProcessMonitor :  class, IProcessMonitor<TProcessDayActivity, TProcessWork, TGroundWorkState, TProcessActivity>,new()
        where TProcessDayActivity : class, IProcessDayActivity<TProcessWork, TGroundWorkState>, new()
        where TProcessWork : class, IProcessWork<TGroundWorkState>, new()
        where TGroundWorkState : class, IGroundWorkState, new()
        where TProcessActivity : class, IProcessActivity<TProcessDayActivity, TProcessWork, TGroundWorkState>, new()
    {

        public BaseMonitor()
        {
            Environ.SubscribeUpDateEvent(this);
        } 
         

        private Environ Environ = new Environ();

        public TProcessMonitor ProcessMonitor { get; private set; } = new TProcessMonitor();
        public TProcessMonitor FilterProcessMonitor { get; private set; } = new TProcessMonitor();
        public ObservableCollection<string> FilterProcessNames { get; private set; } = new ObservableCollection<string>();


        public TimeSpan TimeUpdate
        {
            get { return Environ.TimeUpdate; }
            set { Environ.TimeUpdate = value; }
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

        private void Filtering()
        {
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

            if (ProcessMonitor == null)
                ProcessMonitor = new TProcessMonitor();

            if (ProcessMonitor.Processes == null)
                ProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            if (ProcessMonitor.UserLogActivities == null)
                ProcessMonitor.UserLogActivities = new ObservableCollection<TProcessActivity>();

            if (environ.Processes == null)
                return;

            var now = DateTime.Now;

            foreach (var process in environ.Processes)
            {
                var currentProcessDayActivity = ProcessMonitor.Processes.FirstOrDefault(p => p.Name == process.Name);

                if(currentProcessDayActivity == null)
                {
                    var work = CreateIActivity<TProcessWork>(now);
                    currentProcessDayActivity = new TProcessDayActivity();
                    currentProcessDayActivity.Name = process.Name; // very bad, change to activator ? >_<


                    currentProcessDayActivity.Activities = new ObservableCollection<TProcessWork>() { work };

                    ProcessMonitor.Processes.Add(currentProcessDayActivity);
                }

                // work with current
                var lastProcWork = currentProcessDayActivity.Activities.Last();
                if (!lastProcWork.IsAlive)
                {
                    lastProcWork = CreateIActivity<TProcessWork>(now);
                    currentProcessDayActivity.Activities.Add(lastProcWork);
                }
                else
                {
                    lastProcWork.End = now;
                }

                if (lastProcWork.GroundWorkStates == null)
                    lastProcWork.GroundWorkStates = new ObservableCollection<TGroundWorkState>();


                var lastGroundState = lastProcWork.GroundWorkStates.LastOrDefault();

                if (lastGroundState != null)
                {
                    if (lastGroundState.Title == process.Title)
                        continue;

                    lastGroundState.End = now;

                }

                //if(string.IsNullOrEmpty(process.Title))
                //{
                //    MessageBox.Show(string.Format("process.Title is null or empty : {0}", process.Title));
                //}
                 
                lastProcWork.GroundWorkStates.Add(NewGroundStateWork(now, process.Title));
            }

            // delete was ended process
            foreach (var process in ProcessMonitor.Processes.Where(proc => !environ.Processes.Select(p => p.Name).Contains(proc.Name)))
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
                    lastActivity.End = now;
                }

                var lastgroundworkstate = lastActivity.GroundWorkStates.LastOrDefault();
                if (lastgroundworkstate != null)
                {
                    lastgroundworkstate.End = now;
                    lastgroundworkstate.IsAlive = false;
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
                        lastUserActivity.End = now;
                        groundStateWork.IsAlive = false;
                    }
                }
            }
            else
            {
                ProcessMonitor.Lead = ProcessMonitor.Processes.First(p => p.Name == environ.Lead.Name);

                if (lastUserActivity == null)
                { 
                    FillingUserLogActivitys(NewGroundStateWork(now, environ.Lead.Title));
                }
                else
                {
                    var process = lastUserActivity.LinkProcess;
                    var groundStateWork = lastUserActivity.GroundWorkStates.First();

                    if (groundStateWork.IsAlive)
                        lastUserActivity.End = now;

                    if (groundStateWork.Title != environ.Lead.Title ||
                        process.Name != ProcessMonitor.Lead.Name ||
                        !groundStateWork.IsAlive)
                    {
                        groundStateWork.IsAlive = false;

                        FillingUserLogActivitys(NewGroundStateWork(now, environ.Lead.Title));
                    }
                }
            }


            Filtering();
        }

        private void FillingUserLogActivitys(TGroundWorkState groundStateWork)
        {
            var data = new TProcessActivity();

            data.Start = groundStateWork.Start;
            data.End = groundStateWork.End;

            data.LinkProcess = ProcessMonitor.Lead;
            data.GroundWorkStates = new ObservableCollection<TGroundWorkState>() { groundStateWork };

            ProcessMonitor.UserLogActivities.Add(data);

            var lastactivity = ProcessMonitor.Lead.Activities.Last();

            if (lastactivity.LeadGroundWorkStates == null)
                lastactivity.LeadGroundWorkStates = new ObservableCollection<TGroundWorkState>();

            lastactivity.LeadGroundWorkStates.Add(groundStateWork);
        }

        private T CreateIActivity<T>(DateTime now) where T : class, IActivity, new()
        {
            var activity = new T();

            activity.IsAlive = true;
            activity.Start = now;
            activity.End = now;

            return activity;
        }

        private TGroundWorkState NewGroundStateWork(DateTime now, string title)
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
