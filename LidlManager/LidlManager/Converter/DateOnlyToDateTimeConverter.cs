using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LidlManager.Converter
{
    public class DateOnlyToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            else if (value is DateOnly)
            {
                DateOnly dateOnlyValue = (DateOnly)value;
                return new DateTime(dateOnlyValue.Year, dateOnlyValue.Month, dateOnlyValue.Day);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
