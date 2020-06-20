using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    /// <summary>
    /// Converts array of <see cref="bool"/> values to single <see cref="bool"/> value by performing logical AND operation on each member
    /// </summary>
    public class MultiBoolAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return false;

            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <exception cref="NotImplementedException"/>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
