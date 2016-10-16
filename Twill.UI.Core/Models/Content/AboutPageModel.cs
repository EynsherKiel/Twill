using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models.Content
{
    public class AboutPageModel : Notify
    {
        public AboutPageModel()
        {
        }


        private string text = "About";
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
