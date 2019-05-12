using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedParameter = parameter != null ? (Visibility)parameter : Visibility.Collapsed;

            return value == null ? convertedParameter : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
