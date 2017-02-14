using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twill.UI.Core.Models.Content
{
    public class ReportsPageModel : ViewModelBase
    {
        public ReportsPageModel()
        {

        }


        private string text = nameof(ReportsPageModel);
        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }

    }
}
