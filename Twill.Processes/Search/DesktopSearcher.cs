using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;
using Twill.Processes.Models;

namespace Twill.Processes.Search
{
    public class DesktopSearcher
    {
        private const string ProgramManager = "Program Manager";
        private const string RusTaskManager = "Диспетчер задач";

        public List<string> NotFindList { get; } = new List<string>() { ProgramManager, RusTaskManager };

        private bool IsBadText(string windowtext) => string.IsNullOrEmpty(windowtext) || NotFindList.Contains(windowtext);

          

        public List<IntPtr> FindDesktopProcess()
        {
            var list = new List<IntPtr>();

            User32.WNDENUMPROC WNDENUMPROC = (handle, handleParams) =>
            {
                if (!User32.IsWindowVisible(handle))
                    return true;

                if (!User32.IsWindow(handle))
                    return true;

                string windowtext; 

                if (!TryGetWindowText(handle, out windowtext))
                    return true;

                if (IsBadText(windowtext))
                    return true;

                list.Add(handle);

                return true;
            };

            try
            {
                using (var safeDesktopHandle = new User32.SafeDesktopHandle())
                {
                    User32.EnumDesktopWindows(safeDesktopHandle, WNDENUMPROC, default(IntPtr));
                } 
            }
            finally
            {
                // C++ is resident evil
                GC.KeepAlive(WNDENUMPROC);
            }

            return list;
        }

        public bool TryGetWindowText(IntPtr handle, out string windowtext)
        {
            var chars = new char[1024];
            var findWindowTextLenght = User32.GetWindowText(handle, chars, chars.Length);
            if (findWindowTextLenght == 0)
            {
                windowtext = null;
                return false;
            }
            windowtext = new string(chars, 0, findWindowTextLenght);
            return true;
        }

        public string GetTitle(IntPtr handle)
        {
            string windowTitleText;
            if (!TryGetWindowText(handle, out windowTitleText))
                return null;

            return windowTitleText;
        }

        public IntPtr GetActiveHandle() => User32.GetForegroundWindow(); 

        public Process GetActiveProcess(IntPtr hwnd) => Process.GetProcessById(GetProcessId(hwnd).ToInt32());


        public IntPtr GetProcessId(IntPtr hwnd)
        {
            int pid;
            PInvoke.User32.GetWindowThreadProcessId(hwnd, out pid);
            return new IntPtr(pid);
        }

        public BaseProcess GetActiveWindow()
        {
            var handle = GetActiveHandle();

            string WindowTitleText;
            if (!TryGetWindowText(handle, out WindowTitleText))
                return null;

            return new BaseProcess(handle);
        }
    }
}
