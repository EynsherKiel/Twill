using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Storage.Enums
{
    public enum ToType
    {
        [Description("tdl")]
        ToDoList,
        [Description("xml")] // timespan serialization bug
        XML,
        [Description("json")]
        JSON 
        //, [Description("xls")] Excel
    }
}
