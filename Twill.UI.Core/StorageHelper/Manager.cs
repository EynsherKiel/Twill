using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Models.Controls.Processes;

namespace Twill.UI.Core.StorageHelper
{
    public class Manager
    {

        private Storage.Barrier.Manager BarrierManager = new Storage.Barrier.Manager();
        private object SyncRoot = new object();

        public void Save<T>(T obj, DateTime? time = null) where T : class 
        {
            BarrierManager.Save(obj, time);
        }

        public T Load<T>(DateTime? time = null) where T : class, new()
        {
            if (typeof(T) == typeof(Monitor))
            {
                if (time == null)
                {
                    lock (SyncRoot)
                    {
                        if (Tools.Architecture.StaticType<T>.Instance != null)
                            return Tools.Architecture.StaticType<T>.Instance;

                        Tools.Architecture.StaticType<Monitor>.Instance = BarrierManager.Load<Monitor>(DateTime.Now);

                        var monitor = Tools.Architecture.StaticType<Monitor>.Instance;

                        if (monitor?.ProcessMonitor?.UserLogActivities != null)
                        {
                            foreach (var userLogActivity in monitor.ProcessMonitor.UserLogActivities)
                            {
                                var process = monitor.ProcessMonitor.Processes.FirstOrDefault(p => p.Name == userLogActivity.LinkProcess.Name);
                                if (process != null)
                                    userLogActivity.LinkProcess = process;
                            }
                        }

                        monitor.StartWatch();

                        return Tools.Architecture.StaticType<T>.Instance;
                    }
                }
                else
                {
                    return BarrierManager.Load<T>(time);
                }
            }

            return BarrierManager.Load<T>(time);
        }
    }
}
