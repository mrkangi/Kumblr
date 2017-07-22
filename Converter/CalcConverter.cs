using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace DownloadManager.Converter
{
    class CalcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double val = (double)value;
            return val / double.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
