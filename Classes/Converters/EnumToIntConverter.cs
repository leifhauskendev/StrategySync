using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StrategySync.Classes.Converters
{
    public class EnumToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType().IsEnum)
            {
                return (int)value;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && targetType.IsEnum)
            {
                return Enum.ToObject(targetType, intValue);
            }
            return null;
        }
    }
}
