using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Twill
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void WindowLoadedEvent(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
        }

        private void StateChangedEvent(object sender, EventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
                return;

            this.ShowInTaskbar = false; 
        }

        private void WindowClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TaskbarIcon.HideBalloonTip();
        }
    }
}
