using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using ZhiHuAppHelper;

namespace ZhiHuApp.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double timestamp;
            if (double.TryParse(value.ToString(), out timestamp))
            {
                var dt = UnixTimestamp.ConvertFromUnixTimestamp(timestamp);
                return dt.ToString(parameter.ToString());
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
