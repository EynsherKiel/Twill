using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Interfaces.Files
{
    internal interface IInteraction
    {
        string Path { get; }
        bool IsFileExist();
        void Save(string data);
        string Load();
    }
}
