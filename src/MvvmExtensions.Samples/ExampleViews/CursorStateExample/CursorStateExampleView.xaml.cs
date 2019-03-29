using System.Windows.Controls;

namespace MvvmExtensions.Samples.ExampleViews
{
    /// <summary>
    /// Interaction logic for CursorStateExampleView.xaml
    /// </summary>
    public partial class CursorStateExampleView : UserControl
    {
        public CursorStateExampleView()
        {
            InitializeComponent();
            DataContext = new CursorStateExampleViewModel();
        }
    }
}
