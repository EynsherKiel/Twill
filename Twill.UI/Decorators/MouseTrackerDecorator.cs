using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Twill.UI.Decorators
{
    // http://stackoverflow.com/questions/6714663/wpf-how-do-i-bind-a-controls-position-to-the-current-mouse-position
    public class MouseTrackerDecorator : Decorator
    {
        static readonly DependencyProperty MousePositionProperty;
        static MouseTrackerDecorator()
        {
            MousePositionProperty = DependencyProperty.Register("MousePosition", typeof(Point), typeof(MouseTrackerDecorator));
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (base.Child != null)
                    base.Child.MouseMove -= _controlledObject_MouseMove;
                base.Child = value;
                base.Child.MouseMove += _controlledObject_MouseMove;
            }
        }

        public Point MousePosition
        {
            get { return (Point)GetValue(MouseTrackerDecorator.MousePositionProperty); }
            set { SetValue(MouseTrackerDecorator.MousePositionProperty, value); }
        }

        private void _controlledObject_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(base.Child);

            // Here you can add some validation logic
            MousePosition = p;
        }
    }
}
