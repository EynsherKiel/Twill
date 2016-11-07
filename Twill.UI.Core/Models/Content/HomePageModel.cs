using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Processes.Tracking;
using GalaSoft.MvvmLight;


namespace Twill.UI.Core.Models.Content
{
    public class HomePageModel : ViewModelBase
    {
        public HomePageModel()
        { 
        }
         

        private string text = "Welcome";
        public string Text
        {
            get { return text; }
            set { Set(nameof(Text), ref text, value); }
        } 
    }
}
