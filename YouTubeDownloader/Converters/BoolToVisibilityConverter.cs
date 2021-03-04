using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YouTubeDownloader
{
    /// <summary>
    /// Converts a <see cref="bool"/> value to <see cref="Visibility.Visible"/> or <see cref="Visibility.Hidden"/>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public static BoolToVisibilityConverter Instance { get; } = new BoolToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            return boolValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibilityValue = (Visibility)value;
            return visibilityValue == Visibility.Visible;
        }
    }
}
