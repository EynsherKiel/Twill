using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Tracking;
using Twill.Tools.Async;

namespace Twill.Storage.Barrier
{
    public class Manager 
    {
        private AsyncQueue AsyncQueue = Tools.Architecture.Singleton<AsyncQueue>.Instance; 


        public void Save<T>(T obj, DateTime? time = null) where T : class
        {
            var type = GetType<T>();
            if (type == null)
                return;

            var interaction = GetPath(type, time);
            if (interaction == null)
                return;

            Save(interaction, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
         
        public T Load<T>(DateTime? time = null) where T : class,  new()
        {

            var type = GetType<T>();
            if (type == null)
                return new T();

            var interaction = GetPath(type, time);
            if (interaction?.IsFileExist() == true)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Load(interaction));
            }

            return new T();
        }

        private Files.IInteraction GetPath(Type type, DateTime? time = null)
        {
            if(type == typeof(BaseMonitor<,,,,>))
            {
                return new Files.Zip(Settings.Default.GetLaborFullPath(time ?? DateTime.Now));
            }

            return null;
        }


        private Type GetType<T>()
        {
            var t = typeof(T);

            var baset = t.BaseType;

            if (baset.IsGenericType && baset.GetGenericTypeDefinition() == typeof(BaseMonitor<,,,,>))
            {
                return typeof(BaseMonitor<,,,,>);
            }

            return null;
        }

        private void Save(Files.IInteraction sysinteraction, string data)
        {
            AsyncQueue.AsyncAdd(() => sysinteraction.Save(data));
        }

        private string Load(Files.IInteraction sysinteraction)
        {
            string data = string.Empty;

            AsyncQueue.Add(() => data = sysinteraction.Load());

            return data;
        }
    }
}
