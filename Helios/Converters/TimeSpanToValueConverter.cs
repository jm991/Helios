using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helios.Model;
using System.Windows.Data;

namespace Helios.Converters
{
    public class TimeSpanToValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (value != null && value is TimeSpan)
            //{
            //    TimeSpan timeSpanVal = (TimeSpan) value;
                
            //    return timeSpanVal.TotalMilliseconds / Settings.Clip.ClipDuration.TotalMilliseconds;
            //}
            //else
            //{
                throw new NotImplementedException();
            //}
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}