using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Twill.UI.Core.Models.Content;

namespace Twill.UI.Core.Models
{
    public class MenuModel : ViewModelBase
    {
        public MenuModel()
        {
            currentViewModel = MonitorPageModel;
        }


        public HomePageModel HomePageModel { get; set; } = new HomePageModel();
        public AboutPageModel AboutPageModel { get; set; } = new AboutPageModel();
        public MonitorPageModel MonitorPageModel { get; set; } = new MonitorPageModel();
        public SettingsPageModel SettingsPageModel { get; set; } = new SettingsPageModel();

        public ICommand HomePageViewCommand => new RelayCommand(() => CurrentViewModel = HomePageModel);
        public ICommand AboutPageViewCommand => new RelayCommand(() => CurrentViewModel = AboutPageModel);
        public ICommand MonitorPageViewCommand => new RelayCommand(() => CurrentViewModel = MonitorPageModel);
        public ICommand SettingsPageViewCommand => new RelayCommand(() => CurrentViewModel = SettingsPageModel);
        

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(ref currentViewModel, value); GC.Collect();  }
        }
    }
}
