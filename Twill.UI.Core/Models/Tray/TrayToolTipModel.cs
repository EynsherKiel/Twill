
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Twill.UI.Core.Models.Tray
{
    public class TrayToolTipModel : ViewModelBase
    {
        public TrayToolTipModel()
        {
        }


        private string text = "Twill";
        public string Text
        {
            get { return text; }
            set { Set(nameof(Text), ref text, value); }
        }

    }
}