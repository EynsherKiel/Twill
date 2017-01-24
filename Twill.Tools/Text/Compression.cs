using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Text
{
    // http://stackoverflow.com/questions/9830428/sharpziplib-to-compress-a-string
    public static class Compression
    {
        public static byte[] ZipText(string text)
        {
            if (text == null)
                return null;

            using (Stream memOutput = new MemoryStream())
            {
                using (var zipOut = new GZipOutputStream(memOutput))
                {
                    using (StreamWriter writer = new StreamWriter(zipOut))
                    {
                        writer.Write(text);

                        writer.Flush();
                        zipOut.Finish();

                        byte[] bytes = new byte[memOutput.Length];
                        memOutput.Seek(0, SeekOrigin.Begin);
                        memOutput.Read(bytes, 0, bytes.Length);

                        return bytes;
                    }
                }
            }
        }

        public static string UnzipText(byte[] bytes)
        {
            if (bytes == null)
                return null;

            using (Stream memInput = new MemoryStream(bytes))
            using (var zipInput = new GZipInputStream(memInput))
            {
                using (StreamReader reader = new StreamReader(zipInput))
                {
                    string text = reader.ReadToEnd();

                    return text;
                }
            }

        }
    }
}
