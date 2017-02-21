using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Twill.UI.Behaviors
{
    public class CalendarAttachBehavior : DependencyObject
    {
        // Adds a collection of command bindings to a date picker's existing BlackoutDates collection, since the collections are immutable and can't be bound to otherwise.
        //
        // Usage: <DatePicker hacks:AttachedProperties.RegisterBlackoutDates="{Binding BlackoutDates}" >

        public static DependencyProperty RegisterBlackoutDatesProperty = DependencyProperty.RegisterAttached("RegisterBlackoutDates", typeof(IList<CalendarDateRange>), typeof(CalendarAttachBehavior), new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        public static void SetRegisterBlackoutDates(UIElement element, ObservableCollection<CalendarDateRange> value)
        {
            element?.SetValue(RegisterBlackoutDatesProperty, value);
        }

        public static ObservableCollection<CalendarDateRange> GetRegisterBlackoutDates(UIElement element)
        {
            return element?.GetValue(RegisterBlackoutDatesProperty) as ObservableCollection<CalendarDateRange>;
        }

        private static void OnRegisterCommandBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        { 
            dynamic element = null;

            if (sender is System.Windows.Controls.DatePicker)
                element = sender as System.Windows.Controls.DatePicker;

            if (sender is System.Windows.Controls.Calendar)
                element = sender as System.Windows.Controls.Calendar;

            if (element != null)
            {
                if (element.SelectedDate == null)
                {
                    var bindings = e.NewValue as ObservableCollection<CalendarDateRange>;
                    if (bindings != null)
                    {
                        element.BlackoutDates?.Clear();
                        foreach (var dateRange in bindings)
                        {
                            element.BlackoutDates?.Add(dateRange);
                        }

                    }
                }
            }
        }
    }
}
