using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Architecture
{
    // https://msdn.microsoft.com/ru-ru/library/twcad0zb.aspx
    public static class UniversalMethods
    {
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
