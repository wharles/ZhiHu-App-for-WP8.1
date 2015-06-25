using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace ZhiHuApp.Converters
{
    public class WidthConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var bounds = Window.Current.Bounds;
            var dpiRatio = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            var resolutionW = Math.Round(bounds.Width * dpiRatio);
            if (resolutionW == 1080)
                return 155;
            if (resolutionW == 720)
                return 145;
            if (resolutionW == 480)
                return 140;
            return 140;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
