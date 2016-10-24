using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks; 

namespace Twill.Tools.Architecture
{
    public class Timer : IDisposable
    {
        public Timer(TimerCallback callback)
        {
            Callback = callback;
            StartTimer();
        }

        private TimerCallback Callback;

        private System.Threading.Timer STTimer;

        private TimeSpan period = TimeSpan.FromSeconds(15);

        public TimeSpan Period
        {
            get { return period; }
            set
            {
                period = value;
                STTimer.Change(TimeSpan.Zero, Period);
            }
        }

        private void StartTimer() => STTimer = new System.Threading.Timer(Callback, null, TimeSpan.Zero, Period); 

        private void StopTimer() => STTimer?.Dispose(); 


        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    StopTimer();
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



