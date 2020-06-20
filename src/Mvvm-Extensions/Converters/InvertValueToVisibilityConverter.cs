using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    /// <summary>
    /// Converts <see cref="object"/> to <see cref="Visibility"/>
    /// <br></br>
    /// <br><see cref="null"/> converts to <see cref="Visibility.Visible"/></br> 
    /// <br>Any object converts to converter parameter (default is <see cref="Visibility.Collapsed"/>)</br>
    /// </summary>
    public class InvertValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedParameter = parameter != null ? (Visibility)parameter : Visibility.Collapsed;

            return value == null ? Visibility.Visible : convertedParameter;
        }

        /// <exception cref="NotImplementedException"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
