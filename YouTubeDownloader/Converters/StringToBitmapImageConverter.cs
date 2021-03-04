using System;
using System.Globalization;
using System.Windows.Data;
using System.IO;
using System.Windows.Media.Imaging;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts the contents of a specified <see cref="string"/> path to a <see cref="BitmapImage"/> that is cached in memory.
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public sealed class StringToBitmapImageConverter : IValueConverter
    {
        public static StringToBitmapImageConverter Instance { get; } = new StringToBitmapImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var videoId = (string)value;
            var path = $"{Globals.ThumbnailFolderPath}\\{videoId}.jpg";
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return null; }
            using (var fileStream = File.OpenRead(path))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = fileStream;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
