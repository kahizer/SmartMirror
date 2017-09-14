using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv
{
    using Windows.UI.Xaml.Data;
    public class CalendarIconValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var itemType = value.ToString();
            if (itemType == "personal") return $"Resources/calendar-24.png";

            return $"Resources/public-calendar-24.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
