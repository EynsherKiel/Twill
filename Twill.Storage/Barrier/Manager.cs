using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Interfaces.Monitor;
using Twill.Processes.Models.Monitor;
using Twill.Processes.Tracking;
using Twill.Tools.Async;

namespace Twill.Storage.Barrier
{
    public class Manager 
    {
        private AsyncQueue AsyncQueue = Tools.Architecture.Singleton<AsyncQueue>.Instance; 


        public void Save<T>(T obj, DateTime? time = null) where T : class
        {
            if (obj == null)
                return;

            var type = GetType<T>();
            if (type == null)
                return;

            var interaction = GetPath(type, time);
            if (interaction == null)
                return;

            AsyncSave(interaction, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }

        public T GetAllActivities<T>() where T : ICollection<DateTime>, new()
        {

            var path = Settings.Default.LaborsPath;

            var dirs = System.IO.Directory.GetDirectories(path);

            return Tools.Collections.Utils.CreateCollection<T, DateTime>(dirs.Select(dir => Tools.Text.Convert.TryParseDatTimeShort(new System.IO.DirectoryInfo(dir).Name)).OfType<DateTime>());
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

        private Interfaces.Files.IInteraction GetPath(Type type, DateTime? time = null)
        {
            if(type == typeof(BaseMonitor<,,,,>))
            {
                return new Files.Zip(Settings.Default.GetLaborFullPath(time ?? DateTime.Now));
            }

            if (type == typeof(LightProcessMonitor))
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

            if(t == typeof(LightProcessMonitor))
            {
                return typeof(LightProcessMonitor);
            }

            return null;
        }

        internal void AsyncSave(Interfaces.Files.IInteraction sysinteraction, string data)
        {
            AsyncQueue.AsyncAdd(() => sysinteraction.Save(data));
        }

        internal void Save(Interfaces.Files.IInteraction sysinteraction, string data)
        {
            AsyncQueue.Add(() => sysinteraction.Save(data));
        }

        internal string Load(Interfaces.Files.IInteraction sysinteraction)
        {
            string data = string.Empty;

            AsyncQueue.Add(() => data = sysinteraction.Load());

            return data;
        }
    }
}
