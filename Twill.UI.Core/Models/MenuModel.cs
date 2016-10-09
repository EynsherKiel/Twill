using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Twill.UI.Core.Models.Content;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models
{
    public class MenuModel : Notify
    {
        public MenuModel()
        {
            currentViewModel = HomePageModel;

            HomePageViewCommand = new RelayCommand(HomePageViewMethod);
            SettingsPageViewCommand = new RelayCommand(SettingsPageViewMethod);
        }


        public HomePageModel HomePageModel { get; set; } = new HomePageModel();
        public SettingsPageModel SettingsPageModel { get; set; } = new SettingsPageModel();


        public ICommand HomePageViewCommand { get; }
        public ICommand SettingsPageViewCommand { get; }

        public void SettingsPageViewMethod() => CurrentViewModel = SettingsPageModel;
        public void HomePageViewMethod() => CurrentViewModel = HomePageModel;


        private Notify currentViewModel;
        public Notify CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                if (currentViewModel == value)
                    return;
                currentViewModel = value;
                RaisePropertyChanged(nameof(CurrentViewModel));
            }
        }


    }
}
