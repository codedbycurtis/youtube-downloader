using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Converts a <see cref="bool"/> value to a <see cref="Visibility"/> value.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public static BoolToVisibilityConverter Instance => new BoolToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            var param = (string)parameter;

            if (param == "Hide") { return boolValue ? Visibility.Visible : Visibility.Hidden; }
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
