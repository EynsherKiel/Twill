using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Twill.Tools.Events
{
    public class SmartWeakEventManager<T> : WeakEventManager where T : class, ISmartWeakEventManager
    {
        static SmartWeakEventManager<T> CurrentManager
        {
            get
            {
                Type managerType = typeof(SmartWeakEventManager<T>);
                SmartWeakEventManager<T> currentManager =
                    (SmartWeakEventManager<T>)GetCurrentManager(managerType);
                if (currentManager == null)
                {
                    currentManager = new SmartWeakEventManager<T>();
                    SetCurrentManager(managerType, currentManager);
                }
                return currentManager;
            }
        }

        public static void AddListener(T source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(T source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }
        protected override void StartListening(object source)
        {
            var obj = source as T;
            if (obj == null)
                return;

            obj.Event += EventHandler;
        }
        protected override void StopListening(object source)
        {
            var obj = source as T;
            if (obj == null)
                return;

            obj.Event -= EventHandler;
        }

        void EventHandler(object sender, EventArgs e)
        {
            base.DeliverEvent(sender, e);
        }
         
    }
}
