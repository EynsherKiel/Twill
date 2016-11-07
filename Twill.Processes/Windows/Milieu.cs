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


        internal void Add(Process process)
        {  
            var application = Applications.ToList().FirstOrDefault(app => app.Name == process.ProcessName);

            if (application == null)
            {
                Applications.Add(new Application(process) { Closed = (app) => Applications.Remove(app) });
            }
            else
            {
                application.Add(process);
            }
        }

        public void CheckApplications()
        {
            foreach (var application in Applications.ToList())
            {
                application.CheckClose();
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