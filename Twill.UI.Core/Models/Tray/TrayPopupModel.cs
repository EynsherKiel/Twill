using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;


namespace Twill.UI.Core.Models.Tray
{
    public class TrayPopupModel : ViewModelBase
    {
        public TrayPopupModel()
        { 

        }
        

        private MenuModel menuModel;
        public MenuModel MenuModel
        {
            get { return menuModel; } 
            set { Set(nameof(MenuModel), ref menuModel, value); } 
        }


        private IconBehaviorModel iconBehaviorModel;
        public IconBehaviorModel IconBehaviorModel
        {
            get { return iconBehaviorModel; }
            set { Set(nameof(IconBehaviorModel), ref iconBehaviorModel, value); }
        }
    }
}