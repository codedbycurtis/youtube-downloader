using System;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Creates a <see cref="BitmapImage"/> that is cached in memory from a path or URL.
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public sealed class BitmapImageConverter : IValueConverter
    {
        public static BitmapImageConverter Instance => new BitmapImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var videoId = value as string;
            var path = $"{App.ThumbnailFolderPath}/{videoId}.jpg";

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }

            using (FileStream fileStream = File.OpenRead(path))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = fileStream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
