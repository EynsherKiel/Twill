using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Twill.Processes.Search;
using Twill.Tools.Events;

namespace Twill.Processes.Windows
{
    public class Environ : ISmartWeakEventManager
    {
        public Environ()
        {
            Timer = new Timer(UpDate, null, TimeSpan.Zero, TimeUpdate);
        }

        private object SyncRoot = new object();

        public readonly Timer Timer;
        public readonly Searcher Searcher = new Searcher();
         
        public event EventHandler<EventArgs> Event = delegate { };

        public List<ProcessDocker> ProcessDockers { get; private set; } = new List<ProcessDocker>();

        public List<Process> Processes { get; private set; } = new List<Process>();
        private List<Process> RealProcesses = new List<Process>();
        public Process Lead { get; set; }


        private TimeSpan timeUpdate = TimeSpan.FromMilliseconds(1000 );

        public TimeSpan TimeUpdate
        {
            get { return timeUpdate; }
            set
            {
                if (value == TimeSpan.Zero)
                    return;

                if (Timer == null)
                    return;

                Timer.Change(TimeSpan.Zero, value);

                timeUpdate = value;
            }
        }
        
        private void UpDate()
        {
            var results = Searcher.FindAllProcess();

            var selectedprocess = results.Item1;
            var handles = results.Item2;

            var allprocesses = handles.Select(handle => ProcessDockers.Select(pd => pd.FirstOrDefault(handle)).Where(process => process != null).FirstOrDefault() ?? new Process(handle)).ToList();

            var groupsNewestProcesses = allprocesses.GroupBy(process => process.Name).ToList();

            foreach (var group in groupsNewestProcesses)
            {
                var processDocker = ProcessDockers.FirstOrDefault(pd => pd.Name == group.Key);
                if (processDocker != null)
                {
                    processDocker.Up(group.ToList());
                }
                else
                {
                    ProcessDockers.Add(new ProcessDocker(group.ToList()));
                }
            }

            ProcessDockers.RemoveAll(pd => pd.IsTerminated || !groupsNewestProcesses.Any(gnp => gnp.Key == pd.Name) );

            var lead = ProcessDocker.SetLead(ProcessDockers, selectedprocess);

            ProcessDockers.ForEach(pd => pd.UpTitles()); 
        }




        private void UpDate(object state)
        {
            if (!Monitor.TryEnter(SyncRoot))
                return;
            try
            {
                UpDate();


                var results = Searcher.FindAllProcess();

                var selectedprocess = results.Item1;
                var handles = results.Item2;

                var newList = new List<Process>();
                var clonelist = RealProcesses.ToList();

                handles.ForEach(handle => newList.Add(RealProcesses.FirstOrDefault(proc => proc.Handle == handle) ?? new Process(handle)));

                RealProcesses = newList;
                newList = newList.GroupBy(p => p.Name).Select(group => group.First()).ToList();

                newList.ForEach(el => el.UpTitle());

                Processes = newList;

                Lead = selectedprocess == null ? null : newList.FirstOrDefault(proc => proc.Handle == selectedprocess.Handle);

                Lead?.UpTitle();


                Event(this, EventArgs.Empty);
            }
            finally
            {
                Monitor.Exit(SyncRoot);
            }
        }

        public void SubscribeUpDateEvent(IWeakEventListener obj) => SmartWeakEventManager<Environ>.AddListener(this, obj);
        public void UnSubscribeUpDateEvent(IWeakEventListener obj) => SmartWeakEventManager<Environ>.AddListener(this, obj);


        ~Environ()
        {
            // <_< i'm know what this is baaaaaaaaaaaaaaaaaaaad
            Timer?.Dispose();
        }
    }
}
