using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Tools.Architecture
{
    // http://stackoverflow.com/questions/1799370/getting-attributes-of-enums-value?noredirect=1&lq=1
    public static class EnumUtils
    {
        // This extension method is broken out so you can use a similar pattern with 
        // other MetaData elements in the future. This is your base method for each.
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        // This method creates a specific call to the above method, requesting the
        // Description MetaData attribute.
        public static string GetAttributeDescription(this Enum value) => value.GetAttribute<DescriptionAttribute>()?.Description ?? value.ToString(); 
    }
}
