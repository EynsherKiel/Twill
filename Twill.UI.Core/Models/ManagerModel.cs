using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Twill.UI.Core.Models.Tray;
using GalaSoft.MvvmLight.Command;


namespace Twill.UI.Core.Models
{
    public class ManagerModel :ViewModelBase
    {
        public ManagerModel()
        { 
            if (IconBehaviorModel.TrayPopupModel != null)
                IconBehaviorModel.TrayPopupModel.MenuModel = MenuModel;
        }

        public MenuModel MenuModel { get; set; } = new MenuModel();
        public IconBehaviorModel IconBehaviorModel { get; set; } = new IconBehaviorModel();

        
    }
}
