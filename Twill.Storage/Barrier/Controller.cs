using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Tools.Architecture;

namespace Twill.Storage.Barrier
{
    public class Controller
    {
        private FileOperationController FileOperationController = Singleton<FileOperationController>.Instance;

        public T GetLabor<T>(DateTime time) where T : class, new()
        {
            string path = Settings.Default.GetLaborFullPath(time);

            if(!File.Exists(path))
                return new T();

            var data = FileOperationController.Read(path);

            return JsonConvert.DeserializeObject<T>(data);
        }

        public void SaveLabor<T>(T data, DateTime time) where T : class
        {
            string path = Settings.Default.GetLaborFullPath(time);

            var dirpath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);

            FileOperationController.Write(path, JsonConvert.SerializeObject(data));
        }

    }
}
