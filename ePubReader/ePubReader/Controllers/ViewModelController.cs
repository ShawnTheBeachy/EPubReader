using ePubReader.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace ePubReader.Controllers
{
    public static class ViewModelController
    {
        public static async Task RefreshLibrary()
        {
            ViewModel.EPubs.Clear();
            ViewModel.EPubs.AddRange(await ImportController.GetImportedEPubsAsync());
        }

        public static async Task RefreshCollections()
        {
            StorageFile collectionsFile;
            collectionsFile = (StorageFile)await ApplicationData.Current.RoamingFolder.TryGetItemAsync("collections.json");

            if (collectionsFile == null)
            {
                collectionsFile = await ApplicationData.Current.RoamingFolder.CreateFileAsync("collections.json");
                await FileIO.WriteTextAsync(collectionsFile, JsonConvert.SerializeObject(ViewModel.Collections));
            }

            var json = await FileIO.ReadTextAsync(collectionsFile);
            ViewModel.Collections.AddRange(JArray.Parse(json).ToObject<IEnumerable<Collection>>());
        }
    }
}
