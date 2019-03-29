using System.Windows.Controls;

namespace MvvmExtensions.Samples.ExampleViews
{
    /// <summary>
    /// Interaction logic for PropertyChangedImplView.xaml
    /// </summary>
    public partial class PropertyChangedImplExampleView : UserControl
    {
        public PropertyChangedImplExampleView()
        {
            InitializeComponent();
            DataContext = new PropertyChangedImplExampleViewModel();
        }
    }
}
