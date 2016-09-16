using Template10.Mvvm;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using MyToolkit.Collections;
using ePubReader.Models;
using Windows.UI.Xaml.Controls;
using ePubReader.Controllers;
using System;
using ePubReader.Views;

namespace ePubReader.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollectionView<ePub> Library { get; set; }
        public ObservableCollectionView<Collection> Collections { get; set; }

        private int _selectedPivotIndex;
        public int SelectedPivotIndex { get { return _selectedPivotIndex; } set { Set(ref _selectedPivotIndex, value); } }

        public MainPageViewModel()
        {
            Library = new ObservableCollectionView<ePub>(ViewModel.EPubs);
            Collections = new ObservableCollectionView<Collection>(ViewModel.Collections);
        }
        
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
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

        public async void AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedPivotIndex == 0)
            {
                if (await ImportController.ImportEPubAsync())
                    await ViewModelController.RefreshLibrary();
            }

            else
                ViewModel.Collections.Add(new Collection { Id = Guid.NewGuid(), Name = "New Collection" });
        }

        public async void ChangeCoverMenuItemClick(object sender, RoutedEventArgs e) =>
            await ImportController.ChangeEPubCoverAsync((sender as MenuFlyoutItem).DataContext as ePub);

        public void ePubItemClick(object sender, RoutedEventArgs e)
        {
            var ePub = (sender as Grid).DataContext as ePub;
            SessionState.Add("reading", ePub);
            NavigationService.Navigate(typeof(ReadingPage));
        }
    }
}

