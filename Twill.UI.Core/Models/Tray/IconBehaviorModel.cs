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
            ShowWindowCommand = new RelayCommand<Window>(ShowWindowMethod);
            TrayPopupModel.IconBehaviorModel = this;
        }

        
        public TrayToolTipModel TrayToolTipModel { get; set; } = new TrayToolTipModel();
        public TrayPopupModel TrayPopupModel { get; set; } = new TrayPopupModel();


        public ICommand ShowWindowCommand { get; }


        public void ShowWindowMethod(Window window)
        {
            if (window == null)
                return;
             

            if (window.WindowState != WindowState.Minimized)
                return;

            window.ShowInTaskbar = true;

            window.WindowState = WindowState.Normal;

            window.Topmost = true;
            window.Topmost = false;
        }
    }
}
