using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;


namespace Twill.UI.Core.Models.Content
{
    public class SettingsPageModel : ViewModelBase
    {
        public SettingsPageModel()
        {

        }


        private string text = "Settings";
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }


    }
} 
