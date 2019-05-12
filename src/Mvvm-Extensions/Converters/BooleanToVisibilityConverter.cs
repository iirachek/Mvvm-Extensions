using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedParameter = parameter != null ? (Visibility)parameter : Visibility.Collapsed;

            return ((bool)value == true) ? Visibility.Visible : convertedParameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedParameter = parameter != null ? (Visibility)parameter : Visibility.Collapsed;

            return ((Visibility)value == Visibility.Visible) ? true : false;
        }
    }
}
