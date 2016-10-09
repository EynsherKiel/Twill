using System.Windows.Input;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models.Tray
{
    public class TrayPopupModel : Notify
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