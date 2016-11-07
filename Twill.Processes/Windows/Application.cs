using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Windows
{
    public class Application : IDisposable
    {
        public Application(Process process)
        {
            Name = process.ProcessName;
            Add(process);
        }

        private object SyncRoot = new object();

        public Action<Application> Closed;

        public List<Process> Processes = new List<Process>();

        public string Name { get; set; }

        public string LastName { get; set; } = string.Empty;

        public DateTime? StartWork { get { return Processes.ToList().Min(process => process?.StartTime); } }


        public void Add(Process process)
        {
            lock (SyncRoot)
            {
                AddProcess(process);
                UpDateProcesses();
                CheckClose();
            }
        }

        private void AddProcess(Process process)
        {

            if (Processes.Where(proc => proc.Id == process.Id).Count() > 0)
                return;

            Processes.Add(process);
        }
        

        public void CheckClose()
        {
            if (Processes.Count == 0)
                Closed?.Invoke(this);
        }

        private void UpDateProcesses()
        {
            foreach (var process in Process.GetProcessesByName(Name))
            {
                AddProcess(process);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Processes?.ForEach(process => process?.Dispose());
                    Processes.Clear();
                    Processes = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
