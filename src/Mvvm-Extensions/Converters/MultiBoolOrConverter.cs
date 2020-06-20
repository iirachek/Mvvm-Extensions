using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    /// <summary>
    /// Converts array of <see cref="bool"/> values to single <see cref="bool"/> value by performing logical OR operation on each member
    /// </summary>
    public class MultiBoolOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return false;

            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <exception cref="NotImplementedException"/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
