using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    public class MultiBoolOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
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
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
