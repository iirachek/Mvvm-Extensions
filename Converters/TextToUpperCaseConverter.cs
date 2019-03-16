using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmExtensions.Converters
{
    public class TextToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inputString = (string)value;
            var charArray = inputString.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
                charArray[i] = Char.ToUpper(charArray[i]);

            return new string(charArray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
