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
            set
            {
                menuModel = value;
                RaisePropertyChanged(nameof(MenuModel));
            }
        }


        private IconBehaviorModel iconBehaviorModel;
        public IconBehaviorModel IconBehaviorModel
        {
            get { return iconBehaviorModel; }
            set
            {
                iconBehaviorModel = value;
                RaisePropertyChanged(nameof(IconBehaviorModel));
            }
        }


    }
}