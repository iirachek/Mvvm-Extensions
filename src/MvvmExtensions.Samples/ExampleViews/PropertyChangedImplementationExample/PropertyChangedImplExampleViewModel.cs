using MvvmExtensions.Attributes;
using MvvmExtensions.PropertyChangedMonitoring;
using System.Windows.Media;

namespace MvvmExtensions.Samples.ExampleViews
{
    class PropertyChangedImplExampleViewModel : PropertyChangedImplementation
    {
        private bool useRed = false;
        private bool useGreen = false;
        private bool useBlue = false;

        public bool UseRed
        {
            get { return useRed; }
            set { Set(ref useRed, value); }
        }

        public bool UseGreen
        {
            get { return useGreen; }
            set { Set(ref useGreen, value); }
        }

        public bool UseBlue
        {
            get { return useBlue; }
            set { Set(ref useBlue, value);  }
        }

        [DependsOn(nameof(UseRed), nameof(UseGreen), nameof(UseBlue))]
        public SolidColorBrush MixedColor
        {
            get
            {
                return GetMixedColorBrush(UseRed, UseGreen, UseBlue);
            }
        }

        private SolidColorBrush GetMixedColorBrush(bool red, bool green, bool blue)
        {
            byte redb = (byte)(red ? 255 : 0);
            byte greenb = (byte)(green ? 255 : 0);
            byte blueb = (byte)(blue ? 255 : 0);

            var color = Color.FromRgb(redb, greenb, blueb);
            return new SolidColorBrush(color);
        }
    }
}
