using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    /// <summary>
    /// Inverts <see cref="bool"/> value
    /// </summary>
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = (bool)value;
            return !booleanValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool booleanValue = (bool)value;
            return !booleanValue;
        }
    }
}
