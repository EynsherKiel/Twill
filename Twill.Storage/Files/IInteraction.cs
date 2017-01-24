using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files
{
    internal interface IInteraction
    {
        string Path { get; }
        bool IsFileExist();
        void Save(string data);
        string Load();
    }
}
