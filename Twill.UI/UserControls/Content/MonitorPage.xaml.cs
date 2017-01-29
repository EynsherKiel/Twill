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
using System.Windows.Media.Animation;
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

            OpenAnimation = this.Resources[nameof(OpenAnimation)] as Storyboard;
            HideAnimation = this.Resources[nameof(HideAnimation)] as Storyboard; 
        }

        private Storyboard OpenAnimation;
        private Storyboard HideAnimation; 

        private static Type thisType = typeof(MonitorPage);

        private const double DefaultColumnHeight = 1.0;
        public double ColumnHeight
        {
            get { return (double)GetValue(ColumnHeightProperty); }
            set { SetValue(ColumnHeightProperty, value); }
        }
        public static readonly DependencyProperty ColumnHeightProperty = DependencyProperty.Register(nameof(ColumnHeight), typeof(double), thisType, new PropertyMetadata(DefaultColumnHeight));

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            
            if(checkbox.IsChecked == true)
            { 
                this.Resources["Height"] = ColumnHeight;

                OpenAnimation.Begin();
            }
            else
            {
                this.Resources["Height"] = this.ActualHeight * 0.7;
                HideAnimation.Begin();
            }
        }

        private void OpenAnimationCompleted(object sender, EventArgs e)
        { 
            OpenAnimation.Stop();
            ColumnHeight = DefaultColumnHeight;
        }

        private void HideAnimationCompleted(object sender, EventArgs e)
        { 
            HideAnimation.Stop();
            ColumnHeight = ColumnHeight;
        }
    }
}
