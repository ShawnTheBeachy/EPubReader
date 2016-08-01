using Template10.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using MyToolkit.Collections;
using ePubReader.Models;
using Windows.UI.Xaml.Controls;
using ePubReader.Views;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;

namespace ePubReader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        MtObservableCollection<ePub> _importedEPubs { get; set; }
        public ObservableCollectionView<ePub> ImportedEPubs { get; set; }

        public MainPageViewModel()
        {
            _importedEPubs = new MtObservableCollection<ePub>();
            ImportedEPubs = new ObservableCollectionView<ePub>(_importedEPubs);
        }
        
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            _importedEPubs.AddRange(await Controllers.ImportController.GetImportedEPubsAsync());
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async void OpenEPubButtonClick(object sender, RoutedEventArgs e)
        {
            if (await Controllers.ImportController.ImportEPubAsync())
            {
                _importedEPubs.Clear();
                _importedEPubs.AddRange(await Controllers.ImportController.GetImportedEPubsAsync());
            }
        }

        public async void ChangeCoverMenuItemClick(object sender, RoutedEventArgs e) =>
            await Controllers.ImportController.ChangeEPubCoverAsync((sender as MenuFlyoutItem).DataContext as ePub);
    }
}

