using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files
{
    internal class Json : BaseInteraction, IInteraction
    {
        public Json(string path) : base(path) { }

        public string Load()
        {
            throw new NotImplementedException();
        }

        public void Save(string data)
        {
            throw new NotImplementedException();
        }
    }
}
