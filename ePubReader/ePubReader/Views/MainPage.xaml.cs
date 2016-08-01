using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ePubReader.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public void HandleRightTap(object sender, RightTappedRoutedEventArgs e)
        {
            ePubMenuFlyout.ShowAt(ePubGridView);
        }
    }
}
