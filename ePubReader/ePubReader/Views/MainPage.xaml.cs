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

        private void ePubItemRightTapped(object sender, RightTappedRoutedEventArgs e) =>
            ePubMenuFlyout.ShowAt(ePubGridView);
    }
}
