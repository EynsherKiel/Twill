using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Twill.Tools.Async;

namespace Twill.UI.Core.Models.Controls.Processes
{
    public abstract class BaseDayActivityAnalysis : ViewModelBase, IWeakEventListener
    {
        public BaseDayActivityAnalysis()
        {
            if (IsInDesignMode)
            {
                Monitor = new Monitor();
                Monitor.StartWatch();
            }
            else
            {
                Monitor = StorageHelperManager.Load<Monitor>();
            }
        }

        public BaseDayActivityAnalysis(DateTime time)
        {
            Monitor = StorageHelperManager.Load<Monitor>(time);
        }

        public BaseDayActivityAnalysis(bool isMonitorStaticInstance)
        {
            if (isMonitorStaticInstance)
            {
                Monitor = Tools.Architecture.Singleton<Monitor>.Instance;
            }
        }

        public BaseDayActivityAnalysis(Monitor monitor)
        {
            Monitor = monitor;
        }

        private StorageHelper.Manager StorageHelperManager = new StorageHelper.Manager();

        private void Monitor_UpDateEvent(object obj, EventArgs e) => UpDate();

        protected abstract void UpDate();


        protected readonly SyncContext SyncContext = new SyncContext();
        protected object SyncRoot = new object();


        private Monitor monitor;
        public Monitor Monitor
        {
            get { return monitor; }
            private set
            {
                Monitor?.UnSubscribeUpDateEvent(this);

                value?.SubscribeUpDateEvent(this);

                Set(ref monitor, value);

                //Monitor?.Filtering();

                //UpDate(true);
            }
        }
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            Monitor_UpDateEvent(sender, e);
            return true;
        }
    }
}
