using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Search; 
using Twill.Processes.Windows;
using Twill.Tools.Architecture;

namespace Twill.Processes.Tracking
{
    public class Monitor : IDisposable  
    {
        public Monitor()
        {
            Timer = new Timer(UpDate);
        }

        public readonly Timer Timer;

        public readonly Milieu Milieu = new Milieu();

        public readonly Searcher Seracher = new Searcher();


        public event Action<Milieu> NewMilieu = delegate { };
        public event Action<Application> ChangeWorkingApplication = delegate { };


        private void UpDate(object state)
        {
            Seracher.UpDateMilieu(Milieu);
            NewMilieu?.Invoke(Milieu);

            var dic = Seracher.GetActiveProcess();
            if (dic != null)
            {
                var application = Milieu.Applications.FirstOrDefault(app => app.Name == dic["ProcessName"]);

                // Console.WriteLine($"{dic["ProcessName"]} : {dic["WindowTitleText"]}");

                if (application != null)
                {
                    if (application.LastName != dic["WindowTitleText"])
                    {
                        application.LastName = dic["WindowTitleText"];
                        ChangeWorkingApplication(application);
                    }

                }
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
                    Timer?.Dispose();
                    Milieu?.Dispose();
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
