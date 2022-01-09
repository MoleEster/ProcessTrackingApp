using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProcessTrackigWPF.Shared
{
    public class Converters: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value.GetType();
            if(type == typeof(DateTime) )
                return ((DateTime)value).ToString("dd.MM.yyyy");
            else
            {
                var a = ((TimeSpan)value);
                return a.ToString("hh:mm:ss");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
