using System.Windows.Controls;

namespace MvvmExtensions.Samples.ExampleViews
{
    /// <summary>
    /// Interaction logic for DependencyWatcherExampleView.xaml
    /// </summary>
    public partial class DependencyWatcherExampleView : UserControl
    {
        public DependencyWatcherExampleView()
        {
            InitializeComponent();
            DataContext = new DependencyWatcherExampleViewModel();
        }
    }
}
