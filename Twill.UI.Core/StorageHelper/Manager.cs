using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models.Monitor;
using Twill.UI.Core.Models.Content.Settings;
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

                        var lightMonitor = BarrierManager.Load<LightProcessMonitor>(DateTime.Now);

                        Tools.Architecture.StaticType<Monitor>.Instance = new Monitor(lightMonitor);

                        Tools.Architecture.StaticType<Monitor>.Instance.StartWatch();

                        return Tools.Architecture.StaticType<T>.Instance;
                    }
                }
                else
                {
                    return BarrierManager.Load<T>(time);
                }
            }

            if (typeof(T) == typeof(GeneralPageModel))
            {
                // todo alarm!
                lock (SyncRoot)
                {
                    if (Tools.Architecture.StaticType<T>.Instance != null)
                        return Tools.Architecture.StaticType<T>.Instance;

                    return Tools.Architecture.StaticType<T>.Instance = BarrierManager.Load<T>();
                }

            }
            return BarrierManager.Load<T>(time);
        }
    }
}
