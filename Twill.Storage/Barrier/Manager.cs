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

        // bool = is using datetime
        private readonly List<Tuple<Type, string, bool>> PathsByTypes = new List<Tuple<Type, string, bool>>()
        {
            new Tuple<Type, string, bool>(typeof(BaseMonitor<,,,,>), null, true)
        };
         

        public void Save<T>(T obj, DateTime? time = null) where T : class
        {
            var type = GetType<T>();
            if (type == null)
                return;

            var path = GetPath(type, time);
            if (path == null)
                return;

            Write(path, Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
         
        public T Load<T>(DateTime? time = null) where T : class,  new()
        {

            var type = GetType<T>();
            if (type == null)
                return new T();

            var path = GetPath(type, time);
            if (System.IO.File.Exists(path))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Read(path));
            }

            return new T();
        }

        private string GetPath(Type type, DateTime? time = null)
        {
            var tuple = PathsByTypes.FirstOrDefault(el => el.Item1 == type);
            if (tuple == null)
                return null;

            if(tuple.Item3)
            {
                return Settings.Default.GetLaborFullPath(time ?? DateTime.Now);
            }

            return tuple.Item2;
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
        private void Write(string path, string data)
        {
            AsyncQueue.AsyncAdd(() =>
            {
                try
                {
                    Tools.IO.Files.CheckDirectory(path);

                    System.IO.File.WriteAllText(path, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private string Read(string path)
        {
            string data = string.Empty;

            AsyncQueue.Add(() =>
            {
                try
                {
                    data = System.IO.File.ReadAllText(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            return data;
        }
    }
}
