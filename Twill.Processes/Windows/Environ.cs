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
        private TimeSpan timeUpdate = TimeSpan.FromMilliseconds(1000);

        public readonly Timer Timer;
        public readonly Searcher Searcher = new Searcher();
         
        public event EventHandler<EventArgs> Event = delegate { };

        public ProcessDocker Lead { get; private set; }
        public List<ProcessDocker> ProcessDockers { get; } = new List<ProcessDocker>();
         

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
                    ProcessDockers.Add(new ProcessDocker(group.Key, group.ToList()));
                }
            }

            ProcessDockers.RemoveAll(pd => pd.IsTerminated || !groupsNewestProcesses.Any(gnp => gnp.Key == pd.Name) );

            Lead = ProcessDocker.SetLead(ProcessDockers, selectedprocess);

            ProcessDockers.ForEach(pd => pd.UpTitles()); 
        }


        private void UpDate(object state)
        {
            if (!Monitor.TryEnter(SyncRoot))
                return;
            try
            {
                UpDate(); 

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
