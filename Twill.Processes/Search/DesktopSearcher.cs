using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.Processes.Search
{
    public class DesktopSearcher
    {
        private const string ProgramManager = "Program Manager";

        public List<string> NotFindList { get; } = new List<string>() { ProgramManager };

        private bool IsBadText(string windowtext) => string.IsNullOrEmpty(windowtext) || NotFindList.Contains(windowtext);

        public List<IntPtr> FindDesktopProcess()
        {
            var list = new List<IntPtr>();

            PInvoke.User32.WNDENUMPROC WNDENUMPROC = (handle, handleParams) =>
            {

                if (!PInvoke.User32.IsWindowVisible(handle))
                    return true;

                if (!PInvoke.User32.IsWindow(handle))
                    return true;

                var windowtext = string.Empty;

                try { windowtext = PInvoke.User32.GetWindowText(handle); }
                catch (Exception) { }

                if (IsBadText(windowtext))
                    return true;

                list.Add(handle);

                return true;
            };

            try
            {
                PInvoke.User32.SafeDesktopHandle safeDesktopHandle = new PInvoke.User32.SafeDesktopHandle();
                PInvoke.User32.EnumDesktopWindows(safeDesktopHandle, WNDENUMPROC, default(IntPtr));
            }
            finally
            {
                // C++ is resident evil
                GC.KeepAlive(WNDENUMPROC);
            }

            return list;
        }
    }
}
