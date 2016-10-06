using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models.Tray
{
    public class IconBehaviorModel : Notify
    {
        public IconBehaviorModel()
        {
            DoubleClickNotifyIcon = new RelayCommand<object>(DoubleClickNotifyIconEvent);
        }


        public TrayPopupModel TrayPopupModel { get; set; } = new TrayPopupModel();
        public TrayToolTipModel TrayToolTipModel { get; set; } = new TrayToolTipModel();


        public ICommand DoubleClickNotifyIcon { get; }


        private void DoubleClickNotifyIconEvent(object obj)
        {
            if (obj == null)
                return;


            var window = obj as Window;

            if (window.WindowState != WindowState.Minimized)
                return;

            window.ShowInTaskbar = true;

            window.WindowState = WindowState.Normal;

            window.Topmost = true;
            window.Topmost = false;
        }
    }
}
