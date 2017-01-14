using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files
{
    public class Manager
    {


        public T Load<T>() where T : class,  new()
        {

            var path = GetPath<T>();
            if(path != null && System.IO.File.Exists(path))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(path));
            }


            return new T();
        }

        private string GetPath<T>()
        { 


            return null;
        }

    }
}
