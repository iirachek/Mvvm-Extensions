using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using MvvmExtensions.Converters;
using NUnit.Framework;

namespace MvvmExtensions.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class ConvertersTests
    {
        void PerformTwoWayConverterTest(IValueConverter converter, object input, object expectedValue, object parameter)
        {
            var culture = CultureInfo.CurrentUICulture;

            var convertedValue = converter.Convert(input, null, parameter, culture);
            var convertBackValue = converter.ConvertBack(expectedValue, null, parameter, culture);

            Assert.AreEqual(expectedValue, convertedValue);
            Assert.AreEqual(input, convertBackValue);
        }

        void PerformOneWayConverterTest(IValueConverter converter, object input, object expectedValue, object parameter)
        {
            var culture = CultureInfo.CurrentUICulture;

            var convertedValue = converter.Convert(input, null, parameter, culture);

            Assert.AreEqual(expectedValue, convertedValue);
        }

        [Test]
        [TestCase(false, Visibility.Collapsed)]
        [TestCase(false, Visibility.Collapsed, Visibility.Collapsed)]
        [TestCase(false, Visibility.Hidden, Visibility.Hidden)]
        [TestCase(true, Visibility.Visible)]
        [TestCase(true, Visibility.Visible, Visibility.Collapsed)]
        [TestCase(true, Visibility.Visible, Visibility.Hidden)]
        public void BooleanToVisibilityConverter_TwoWayConvert_Success(bool input, Visibility expectedValue, object parameter = null)
        {
            var converter = new BooleanToVisibilityConverter();
            PerformTwoWayConverterTest(converter, input, expectedValue, parameter);
        }

        [Test]
        public void InvertBooleanConverter_TwoWayConvert_Success()
        {
            var converter = new InvertBooleanConverter();
            PerformTwoWayConverterTest(converter, false, true, null);
        }

        [Test]
        [TestCase(true, Visibility.Collapsed)]
        [TestCase(true, Visibility.Collapsed, Visibility.Collapsed)]
        [TestCase(true, Visibility.Hidden, Visibility.Hidden)]
        [TestCase(false, Visibility.Visible)]
        [TestCase(false, Visibility.Visible, Visibility.Collapsed)]
        [TestCase(false, Visibility.Visible, Visibility.Hidden)]
        public void InvertBooleanToVisibilityConverter_TwoWayConvert_Success(bool input, Visibility expectedValue, object parameter = null)
        {
            var converter = new InvertBooleanToVisibilityConverter();
            PerformTwoWayConverterTest(converter, input, expectedValue, parameter);
        }

        [Test]
        [TestCase(null, Visibility.Collapsed)]
        [TestCase(null, Visibility.Collapsed, Visibility.Collapsed)]
        [TestCase(null, Visibility.Hidden, Visibility.Hidden)]
        [TestCase(1, Visibility.Visible)]
        [TestCase(1, Visibility.Visible, Visibility.Collapsed)]
        [TestCase(1, Visibility.Visible, Visibility.Hidden)]
        public void ValueToVisibilityConverter_OneWayConvert_Success(object input, Visibility expectedValue, object parameter = null)
        {
            var converter = new ValueToVisibilityConverter();
            PerformOneWayConverterTest(converter, input, expectedValue, parameter);
        }

        [Test]
        [TestCase(1, Visibility.Collapsed)]
        [TestCase(1, Visibility.Collapsed, Visibility.Collapsed)]
        [TestCase(1, Visibility.Hidden, Visibility.Hidden)]
        [TestCase(null, Visibility.Visible)]
        [TestCase(null, Visibility.Visible, Visibility.Collapsed)]
        [TestCase(null, Visibility.Visible, Visibility.Hidden)]
        public void InvertValueToVisibilityConverter_OneWayConvert_Success(object input, Visibility expectedValue, object parameter = null)
        {
            var converter = new InvertValueToVisibilityConverter();
            PerformOneWayConverterTest(converter, input, expectedValue, parameter);
        }

        [Test]
        [TestCase(null, false)]
        [TestCase(new bool[] { }, false)]
        [TestCase(new bool[] { false, false }, false)]
        [TestCase(new bool[] { true, false }, false)]
        [TestCase(new bool[] { true, true }, true)]
        public void MultiBoolAndConverter_OneWayConvert_Success(bool[] input, bool expectedValue)
        {
            // Arrange
            var converter = new MultiBoolAndConverter();
            var boxedInput = input?.Cast<object>().ToArray();

            // Act
            var convertedValue = converter.Convert(boxedInput, null, null, CultureInfo.CurrentUICulture);

            // Assert
            Assert.AreEqual(expectedValue, convertedValue);
        }

        [Test]
        [TestCase(null, false)]
        [TestCase(new bool[] { }, false)]
        [TestCase(new bool[] { false, false }, false)]
        [TestCase(new bool[] { true, false }, true)]
        [TestCase(new bool[] { true, true }, true)]
        public void MultiBoolOrConverter_OneWayConvert_Success(bool[] input, bool expectedValue)
        {
            // Arrange
            var converter = new MultiBoolOrConverter();
            var boxedInput = input?.Cast<object>().ToArray();

            // Act
            var convertedValue = converter.Convert(boxedInput, null, null, CultureInfo.CurrentUICulture);

            // Assert
            Assert.AreEqual(expectedValue, convertedValue);
        }
    }
}
