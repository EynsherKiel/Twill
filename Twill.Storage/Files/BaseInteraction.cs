using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files
{
   internal class BaseInteraction
    {
        public BaseInteraction(string path)
        {
            Tools.IO.Files.CheckDirectory(path);

            Path = path;
        }

        public string Path { get; }

        public bool IsFileExist() => System.IO.File.Exists(Path);
    }
}
