using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ePubReader.Views;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ePubReader.Controls
{
    public sealed partial class EPubItemContainer : UserControl
    {
        public EPubItemContainer()
        {
            this.InitializeComponent();
        }

        private void ePubItemRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FrameworkElement ancestor = sender as FrameworkElement;

            while (ancestor != null)
            {
                if (ancestor is MainPage)
                {
                    (ancestor as MainPage).ePubItemRightTapped(sender, e);
                    break;
                }

                ancestor = VisualTreeHelper.GetParent(ancestor) as FrameworkElement;
            }
        }

        private void ePubItemTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement ancestor = sender as FrameworkElement;

            while (ancestor != null)
            {
                if (ancestor is MainPage)
                {
                    (ancestor as MainPage).ePubItemTapped(sender, e);
                    break;
                }

                ancestor = VisualTreeHelper.GetParent(ancestor) as FrameworkElement;
            }
        }
    }
}
