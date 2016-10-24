using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Architecture
{
    public static class Singleton<T> where T : class,  new()
    {
        private static volatile T instance;
        private static object syncRoot = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                        if (instance == null)
                            instance = new T();

                return instance;
            }
        } 
    }
}
