using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Interfaces.Files;

namespace Twill.Storage.Files
{
    internal class Ordinary : BaseInteraction, IInteraction
    {
        public Ordinary(string path) : base(path) { }

        public Encoding Encoding = Encoding.UTF8;

        public string Load()
        {
            try
            {
                return System.IO.File.ReadAllText(Path, Encoding);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Save(string data)
        {
            try
            {
                System.IO.File.WriteAllText(Path, data, Encoding);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
