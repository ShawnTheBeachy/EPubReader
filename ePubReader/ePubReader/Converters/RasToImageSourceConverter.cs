using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace ePubReader.Converters
{
    public class RasToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage image = null;

            if (value != null)
            {
                if (value.GetType() != typeof(FileRandomAccessStream))
                {
                    throw new ArgumentException("Expected a StorageItemThumbnail as binding input.");
                }
                if (targetType != typeof(ImageSource))
                {
                    throw new ArgumentException("Expected ImageSource as binding output.");
                }

                if ((FileRandomAccessStream)value == null)
                {
                    return image;
                }

                image = new BitmapImage();

                using (var thumbNailClonedStream = ((FileRandomAccessStream)value).CloneStream())
                    image.SetSource(thumbNailClonedStream);
            }

            return (image);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
