using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ePubReader.Models;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;
using ePubReader.Views;
using Windows.Storage;
using System.Linq;

namespace ePubReader.ViewModels
{
    public class ReadingPageViewModel : ViewModelBase
    {
        WebView ContentWebView { get; set; }
        ePub Reading { get; set; }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Reading = SessionState["reading"] as ePub;
            ContentWebView = (NavigationService.Content as ReadingPage).FindName("ContentWebView") as WebView;
            ContentWebView.NavigateToString(await LoadHtmlFromManifestItem(Reading.Manifest.First()));
            await Task.CompletedTask;
        }

        async Task<string> LoadHtmlFromManifestItem(ManifestItem item)
        {
            var file = await StorageFile.GetFileFromPathAsync(item.Path.Replace('/', '\\'));
            return await FileIO.ReadTextAsync(file);
        }
    }
}
