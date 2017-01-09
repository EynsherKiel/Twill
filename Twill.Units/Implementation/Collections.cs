using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Tools.Collections;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class Collections
    {
        [TestMethod]
        public void UpdateLinksWithFilter()
        {
            var newlist = new List<Tuple<int, string>>() { new Tuple<int, string>(2, "asd"), new Tuple<int, string>(17, "25"), new Tuple<int, string>(6, "42"), new Tuple<int, string>(18, "534"), };

            var list = new List<Tuple<int, string>>() { new Tuple<int, string>(5, "25"), new Tuple<int, string>(6, "42"), new Tuple<int, string>(18, "534"), };

            var filter = new List<int>() { 2, 6 };

            IList<Tuple<int, string>> l = list;
            newlist.UpdateLinksWithFilter(filter, (listelement, filterelement) => filterelement == listelement.Item1, ref l);

            Assert.IsTrue(list.Count == 2, "list contains not 2 elements");

            Assert.IsTrue(list[0].Item1 == 18 && list[0].Item2 == "534", "first element unexpected");
            Assert.IsTrue(list[1].Item1 == 17 && list[1].Item2 == "25", "second element unexpected");

        }
    }
}
