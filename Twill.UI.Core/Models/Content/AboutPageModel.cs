using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Twill.UI.Core.Models.Content
{
    public class AboutPageModel : ViewModelBase
    {
        public AboutPageModel()
        {
        }


        private string text = "About";
        public string Text
        {
            get { return text; }
            set { Set(nameof(Text), ref text, value); }
        }


    }
}
