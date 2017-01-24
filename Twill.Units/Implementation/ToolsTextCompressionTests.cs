using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twill.Tools.Text;

namespace Twill.Units.Implementation
{
    [TestClass]
    public class ToolsTextCompressionTests
    {
        [TestMethod]
        public void ZipCompressing()
        {
            string text = "hello!";
             
            Assert.AreEqual(text, Compression.UnzipText(Compression.ZipText(text)), "not equal...");
        }  
    }
}
