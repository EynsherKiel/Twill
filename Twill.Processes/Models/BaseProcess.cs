using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Models
{
    public class BaseProcess : IEquatable<BaseProcess>
    {
        public BaseProcess(IntPtr handle)
        {
            Handle = handle;
        }

        protected Search.DesktopSearcher DesktopSearcher = new Search.DesktopSearcher();

        private IntPtr handle;
        public IntPtr Handle
        {
            get { return handle; }
            private set
            {
                handle = value;
                UpTitle();
            }
        }

        public string Title { get; private set; }

        public void UpTitle() => Title = DesktopSearcher.GetTitle(Handle);

        public bool Equals(BaseProcess other) => other == null ? false : Handle == other.Handle;
    }
}
