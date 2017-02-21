using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Text
{
    public static class Convert
    {
        public static DateTime? TryParseDatTimeShort(string text)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
                return time;
            return null;
        }
    }
}
