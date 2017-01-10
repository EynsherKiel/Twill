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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Twill.UI.UserControls.Content
{
    public partial class MonitorPage : UserControl
    {
        public MonitorPage()
        {
            InitializeComponent(); 
        }

        private static Type thisType = typeof(MonitorPage);

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register(nameof(ContentHeight), typeof(double), thisType, new PropertyMetadata(20000.0));
        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateLayout();
            scrollviewer.ScrollToVerticalOffset(Tools.Math.Position.ChoisenPixel(DateTime.Now, ContentHeight)/* - ActualHeight * 0.1*/);
        }
    }
}
