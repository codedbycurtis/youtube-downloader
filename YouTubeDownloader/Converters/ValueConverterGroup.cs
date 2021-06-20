using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace YouTubeDownloader.Converters
{
    /// <summary>
    /// Supports the chaining of multiple converters to be used as one.
    /// </summary>
    public sealed class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
