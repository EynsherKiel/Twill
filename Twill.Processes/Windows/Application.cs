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
            Add(process);
        }

        public Action<Application> Closed;

        public List<Process> Processes = new List<Process>();

        public string Name { get { return Processes.FirstOrDefault()?.ProcessName; } }


        public void Add(Process process)
        {
            AddProcess(process);
            UpDateProcesses();
            DeleteTerminatedProcess();
        }

        private void DeleteTerminatedProcess()
        {
            var processes = Processes.Where(process =>
            {
                try { return process.HasExited; }
                catch { return true; }
            }).ToList();

            foreach (var process in processes)
                using(process)
                    Processes.Remove(process);

            CheckClose();
        }

        private void AddProcess(Process process)
        {
            if (Processes.Where(proc => proc.Id == process.Id).Count() > 0)
                return;

            try  {  process.EnableRaisingEvents = true; }
            catch  {

                if (process == null)
                    return;

                using (process)
                    return;
            }

            process.Exited += CloseProcess;

            Processes.Add(process);
        }

        private void CloseProcess(object sender, EventArgs e)
        {
            var process = sender as Process;
            if (process == null)
                return;

            process.Exited -= CloseProcess;

            using (process)
                Processes.Remove(process);

            CheckClose();

        }

        private void CheckClose()
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
        private object A;

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
