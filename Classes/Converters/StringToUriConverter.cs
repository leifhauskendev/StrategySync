using System;
using System.Globalization;
using System.Windows.Data;

namespace StrategySync.Classes.Converters
{
    /// <summary>
    /// Converts a string to a Uri. If the string is null or invalid, returns null.
    /// </summary>
    public class StringToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string uriString && Uri.TryCreate(uriString, UriKind.Absolute, out var uri))
            {
                return uri;
            }
            return null; // Or handle as needed, e.g., return a default Uri
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }
    }
}
