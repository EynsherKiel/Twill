using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Files
{
    internal class Zip : BaseInteraction, IInteraction
    {
        public Zip(string path) : base(path) { }

        public string Load()
        {
            try
            {
               return Tools.Text.Compression.UnzipText(System.IO.File.ReadAllBytes(Path));
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
                Tools.IO.Files.CheckDirectory(Path);

                System.IO.File.WriteAllBytes(Path, Twill.Tools.Text.Compression.ZipText(data));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
