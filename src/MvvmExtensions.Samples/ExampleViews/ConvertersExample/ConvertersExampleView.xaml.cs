using System.Windows.Controls;

namespace MvvmExtensions.Samples.ExampleViews
{
    /// <summary>
    /// Interaction logic for ConvertersExampleView.xaml
    /// </summary>
    public partial class ConvertersExampleView : UserControl
    {
        public ConvertersExampleView()
        {
            InitializeComponent();
            DataContext = new ConvertersExampleViewModel();
        }
    }
}
