using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Twill.Processes.Search;

namespace Twill.Processes.Windows
{
    public class Environ
    {
        public Environ()
        {
            Timer = new Timer(UpDate, null, 0, 1000);
        }

        public readonly Timer Timer;
        public readonly Searcher Searcher = new Searcher();

        public event Action<Environ> UpDateEvent = delegate { };

        public List<Process> Processes { get; private set; } = new List<Process>();
        public Process LeadProcess { get; set; }

        private object SyncRoot = new object();


        private void UpDate(object state)
        {
            lock (SyncRoot)
            {
                var results = Searcher.FindAllProcess();

                var selectedprocess = results.Item1;
                var handles = results.Item2;

                var newList = new List<Process>();
                var clonelist = Processes.ToList();

                handles.ForEach(handle => newList.Add(clonelist.FirstOrDefault(proc => proc.Handle == handle) ?? new Process(handle)));

                Processes = newList;
                LeadProcess = newList.First(proc => proc.Handle == selectedprocess.Handle);
            }

            UpDateEvent(this);
        }

        ~Environ()
        {
            // <_< i'm know what this is baaaaaaaaaaaaaaaaaaaad
            Timer?.Dispose();
        }
    }
}
