using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Interfaces.Reports;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class Serialization
    {
        [TestMethod]
        public void IntoToDoListTest()
        {
            var todolist = new Storage.Files.Reports.To.TodoList<IReport>("test");

            todolist.Add(DateTime.Now.ToShortDateString(), "new task", 500); 
            todolist.Add(DateTime.Now.AddDays(1).ToShortDateString(), "new task", 500);

            var data = Twill.Tools.Text.XML.Serialization(todolist);

            System.IO.File.WriteAllText("2.tdl", data);
        }
    }
}
