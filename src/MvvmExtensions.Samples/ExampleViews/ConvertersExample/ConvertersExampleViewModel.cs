using MvvmExtensions.PropertyChangedMonitoring;

namespace MvvmExtensions.Samples.ExampleViews
{
    class ConvertersExampleViewModel : PropertyChangedImplementation
    {
        bool _boolSwitch = true;
        public bool BoolSwitch
        {
            get
            {
                return _boolSwitch;
            }
            set
            {
                if (_boolSwitch != value)
                {
                    _boolSwitch = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
