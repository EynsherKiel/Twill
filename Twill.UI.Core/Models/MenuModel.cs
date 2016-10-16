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
            currentViewModel = MonitorPageModel;

            HomePageViewCommand = new RelayCommand(HomePageViewMethod);
            AboutPageViewCommand = new RelayCommand(AboutPageViewMethod);
            MonitorPageViewCommand = new RelayCommand(MonitorPageViewMethod);
            SettingsPageViewCommand = new RelayCommand(SettingsPageViewMethod);
        }


        public HomePageModel HomePageModel { get; set; } = new HomePageModel();
        public AboutPageModel AboutPageModel { get; set; } = new AboutPageModel();
        public MonitorPageModel MonitorPageModel { get; set; } = new MonitorPageModel();
        public SettingsPageModel SettingsPageModel { get; set; } = new SettingsPageModel();

        public ICommand HomePageViewCommand { get; }
        public ICommand AboutPageViewCommand { get; }
        public ICommand MonitorPageViewCommand { get; }
        public ICommand SettingsPageViewCommand { get; }

        public void HomePageViewMethod() => CurrentViewModel = HomePageModel;
        public void AboutPageViewMethod() => CurrentViewModel = AboutPageModel;
        public void MonitorPageViewMethod() => CurrentViewModel = MonitorPageModel;
        public void SettingsPageViewMethod() => CurrentViewModel = SettingsPageModel;


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
