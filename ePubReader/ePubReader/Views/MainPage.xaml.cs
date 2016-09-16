using ePubReader.ViewModels;
using Windows.UI.Xaml;
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

        public void ePubItemRightTapped(object sender, RoutedEventArgs e) =>
            ePubMenuFlyout.ShowAt(sender as Grid);

        public void ePubItemTapped(object sender, TappedRoutedEventArgs e) =>
            (DataContext as MainPageViewModel).ePubItemClick(sender, e);
    }
}
