using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Models.Monitor;
using Twill.Storage.Files.Reports;
using Twill.UI.Core.Models.Content.Settings;
using Twill.UI.Core.Models.Controls.Processes;
using Twill.UI.Core.Models.Controls.TimeLine;

namespace Twill.UI.Core.StorageHelper
{
    public class Manager
    {

        public readonly ReportsRegulator ReportsRegulator = new ReportsRegulator();
        private readonly Storage.Barrier.Manager BarrierManager = new Storage.Barrier.Manager();
        private object SyncRoot = new object();

        public void Save<T>(T obj, DateTime? time = null) where T : class
        {
            BarrierManager.Save(obj, time);
        }

        public T Load<T>(DateTime? date = null) where T : class, new()
        {
            if (typeof(T) == typeof(Monitor))
            {
                if (date == null)
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

                    return new Monitor(BarrierManager.Load<LightProcessMonitor>(date)) as T;
                }
            }

            if (typeof(T) == typeof(GeneralPageModel))
            {
                lock (SyncRoot)
                {
                    if (Tools.Architecture.StaticType<T>.Instance != null)
                        return Tools.Architecture.StaticType<T>.Instance;

                    return Tools.Architecture.StaticType<T>.Instance = BarrierManager.Load<T>();
                } 
            }


            if (typeof(T) == typeof(ReportsModel))
            {
                lock (SyncRoot)
                {
                    if (Tools.Architecture.StaticType<T>.Instance != null)
                        return Tools.Architecture.StaticType<T>.Instance;

                    var dayreport = ReportsRegulator.Load<ReportModel>(date ?? DateTime.Now);

                    Tools.Architecture.StaticType<ReportsModel>.Instance = new ReportsModel();

                    Tools.Architecture.StaticType<ReportsModel>.Instance.UpDateReportModel(dayreport?.Reports);

                    return Tools.Architecture.StaticType<T>.Instance;
                }
            }


            return BarrierManager.Load<T>(date);
        }
    }
}
