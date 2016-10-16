using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Windows
{
    public class Milieu : IDisposable
    {
        public List<Application> Applications { get; set; } = new List<Application>();


        internal void Add(IntPtr handle)
        {
            var process = Process.GetProcesses();

            var proc = process.FirstOrDefault(p => p.MainWindowHandle == handle);

            if (proc == null)
                return;

            var application = Applications.FirstOrDefault(app => app.Name == proc.ProcessName);

            if (application == null)
            {
                Applications.Add(new Application(proc) { Closed = (app) => Applications.Remove(app) });
            }
            else
            {
                application.Add(proc);
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
                    Applications?.ForEach(application => application?.Dispose());
                    Applications = null;
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