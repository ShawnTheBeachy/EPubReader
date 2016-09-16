using System.Linq;
using Windows.Storage;

namespace ePubReader.Services
{
    public static class StorageItemHelpers
    {
        public static string GetFolderPath(this StorageFile file)
        {
            var path = file.Path.Replace('\\', '/');
            var parts = path.Split('/');
            parts = parts.Take(parts.Count() - 1).ToArray();
            return string.Join("/", parts);
        }
    }
}
