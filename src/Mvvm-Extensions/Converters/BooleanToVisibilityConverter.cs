using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    /// <summary>
    /// Converts <see cref="bool"/> to <see cref="Visibility"/>
    /// <br></br>
    /// <br>True converts to <see cref="Visibility.Visible"/></br> 
    /// <br>False converts to converter parameter (default is <see cref="Visibility.Collapsed"/>)</br>
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedParameter = parameter != null ? (Visibility)parameter : Visibility.Collapsed;

            return ((bool)value == true) ? Visibility.Visible : convertedParameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Visibility)value == Visibility.Visible) ? true : false;
        }
    }
}
