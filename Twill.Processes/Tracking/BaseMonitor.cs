﻿using System;
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

namespace Twill.Processes.Tracking
{ 
    public abstract class BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState>
        where TProcessMonitor :  class, IProcessMonitor<TProcessDayActivity, TProcessWork, TGroundWorkState>,new()
        where TProcessDayActivity : class, IProcessDayActivity<TProcessWork, TGroundWorkState>, new()
        where TProcessWork : class, IProcessWork<TGroundWorkState>, new()
        where TGroundWorkState : class, IGroundWorkState, new()
    {

        public BaseMonitor()
        {
            Environ.UpdateEvent += Environ_UpdateEvent;
        }

        public event Action<BaseMonitor<TProcessMonitor, TProcessDayActivity, TProcessWork, TGroundWorkState>> UpDateEvent = delegate { };

        private Environ Environ = new Environ();


        public TProcessMonitor ProcessMonitor { get; private set; } = new TProcessMonitor();
        public TProcessMonitor FilterProcessMonitor { get; private set; } = new TProcessMonitor();
        public ObservableCollection<string> FilterProcessNames { get; private set; } = new ObservableCollection<string>();


        public TimeSpan TimeUpdate
        {
            get { return Environ.TimeUpdate; }
            set { Environ.TimeUpdate = value; }
        }

        // Reload for WPF
        protected virtual void Runtime(Action action) => action();

        private void Environ_UpdateEvent(Environ environ)
        {
            Runtime(() => WriteNewData(environ));

            UpDateEvent(this);
        }

        private void Filtering()
        {
            if (FilterProcessMonitor.Processes == null)
                FilterProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            foreach (var process in ProcessMonitor.Processes)
            {
                if(!FilterProcessNames.Contains(process.Name) &&
                   !FilterProcessMonitor.Processes.Select(p => p.Name).Contains(process.Name))
                {
                    FilterProcessMonitor.Processes.Add(process);
                }
            }

            if(ProcessMonitor.Lead == null || FilterProcessNames.Contains(ProcessMonitor.Lead.Name))
            {
                FilterProcessMonitor.Lead = default(TProcessDayActivity);
            }
            else
            {
                FilterProcessMonitor.Lead = ProcessMonitor.Lead;
            }

            foreach (var name in FilterProcessNames)
            {
                var remove = FilterProcessMonitor.Processes.FirstOrDefault(p => p.Name == name);
                if (remove != null)
                {
                    FilterProcessMonitor.Processes.Remove(remove);
                }
            }
        }

        private void WriteNewData(Environ environ)
        { 

            if (ProcessMonitor == null)
                ProcessMonitor = new TProcessMonitor();

            if (ProcessMonitor.Processes == null)
                ProcessMonitor.Processes = new ObservableCollection<TProcessDayActivity>();

            if (ProcessMonitor.UserLogActivities == null)
                ProcessMonitor.UserLogActivities = new ObservableCollection<Tuple<TProcessDayActivity, TGroundWorkState>>();

            if (environ.Processes == null)
                return;

            var now = DateTime.Now;

            foreach (var process in environ.Processes)
            {
                var currentProcessDayActivity = ProcessMonitor.Processes.FirstOrDefault(p => p.Name == process.Name);

                if(currentProcessDayActivity == null)
                {
                    var work = new TProcessWork();
                    work.IsAlive = true;
                    work.Start = now;
                    work.End = now;
                    currentProcessDayActivity = new TProcessDayActivity();
                    currentProcessDayActivity.Name = process.Name; // very bad, change to activator ? >_<


                    currentProcessDayActivity.Activities = new ObservableCollection<TProcessWork>() { work };

                    ProcessMonitor.Processes.Add(currentProcessDayActivity);
                }

                // work with current
                var lastProcWork = currentProcessDayActivity.Activities.Last();
                if (!lastProcWork.IsAlive)
                {
                    lastProcWork = new TProcessWork();
                    lastProcWork.IsAlive = true;
                    lastProcWork.Start = now;
                    currentProcessDayActivity.Activities.Add(lastProcWork);
                }

                lastProcWork.End = now;

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

                lastActivity.IsAlive = false;
                lastActivity.End = now;

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
                    var groundStateWork = lastUserActivity.Item2;

                    if (groundStateWork.IsAlive)
                    {
                        groundStateWork.End = now;
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
                    var process = lastUserActivity.Item1;
                    var groundStateWork = lastUserActivity.Item2;

                    if (groundStateWork.IsAlive)
                        groundStateWork.End = now;

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
            ProcessMonitor.UserLogActivities.Add(new Tuple<TProcessDayActivity, TGroundWorkState>(ProcessMonitor.Lead, groundStateWork));

            var lastactivity = ProcessMonitor.Lead.Activities.Last();

            if (lastactivity.LeadGroundWorkStates == null)
                lastactivity.LeadGroundWorkStates = new ObservableCollection<TGroundWorkState>();

            lastactivity.LeadGroundWorkStates.Add(groundStateWork);
        }

        private TGroundWorkState NewGroundStateWork(DateTime now, string title)
        {
            var lastLeadGroundStateWork = new TGroundWorkState();

            lastLeadGroundStateWork.IsAlive = true;
            lastLeadGroundStateWork.Start = now;
            lastLeadGroundStateWork.End = now;
            lastLeadGroundStateWork.Title = title;

            return lastLeadGroundStateWork;
        }

        ~BaseMonitor()
        {
            if (Environ != null)
                Environ.UpdateEvent -= Environ_UpdateEvent;
        }
    }
}
