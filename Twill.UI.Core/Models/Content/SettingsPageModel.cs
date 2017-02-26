using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Twill.UI.Core.Models.Content.Settings;

namespace Twill.UI.Core.Models.Content
{
    public class SettingsPageModel : ViewModelBase
    {
        public SettingsPageModel()
        {
            if (IsInDesignMode)
            {
                GeneralPageModel = new GeneralPageModel();
                ActionPlannerPageModel = new ActionPlannerPageModel();
            }
            else
            {
                GeneralPageModel = StorageHelperManager.Load<GeneralPageModel>();
                ActionPlannerPageModel = StorageHelperManager.Load<ActionPlannerPageModel>();
            }

            currentViewModel = ActionPlannerPageModel;
        }

        private StorageHelper.Manager StorageHelperManager = new StorageHelper.Manager();


        public ICommand GeneralPageViewCommand => new RelayCommand(() => CurrentViewModel = GeneralPageModel);
        public ICommand ActionPlannerPageViewCommand => new RelayCommand(() => CurrentViewModel = ActionPlannerPageModel);

        private GeneralPageModel generalPageModel;
        public GeneralPageModel GeneralPageModel
        {
            get { return generalPageModel; }
            set { Set(ref generalPageModel, value); }
        }

        private ActionPlannerPageModel actionPlannerPageModel;
        public ActionPlannerPageModel ActionPlannerPageModel
        {
            get { return actionPlannerPageModel; }
            set { Set(ref actionPlannerPageModel, value); }
        }

        private ObservableObject currentViewModel;
        public ObservableObject CurrentViewModel
        {
            get { return currentViewModel; }
            set { Set(ref currentViewModel, value); GC.Collect(); }
        }
    }
} 
