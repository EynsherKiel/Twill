using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models.Tray
{
    public class TrayToolTipModel : Notify
    {
        public TrayToolTipModel()
        {
        }


        private string text = "Twill";
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