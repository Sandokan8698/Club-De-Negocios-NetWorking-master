using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFPresentation.Converters
{
    class VentaNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ceroReplacerCounter = 7 - value.ToString().Length;

            for (int i = 0; i < ceroReplacerCounter; i++)
            {
               value = value.ToString().Insert(0, "0");
            }

            return "VENTA No. " + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
