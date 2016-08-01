using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ePubReader.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ePubItemRightTapped(object sender, RoutedEventArgs e) =>
            ePubMenuFlyout.ShowAt(sender as Grid);
    }
}
