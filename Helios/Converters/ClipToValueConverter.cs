using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helios.Model;
using System.Windows.Data;

namespace Helios.Converters
{
    public class ClipToValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = ((string)parameter).ToLower();

            if (value != null && value is Clip)
            {
                Clip clipVal = value as Clip;

                switch (param)
                {
                    case "start":
                        return clipVal.StartTime.TotalMilliseconds / clipVal.ClipDuration.TotalMilliseconds;
                    case "end":
                        return clipVal.EndTime.TotalMilliseconds / clipVal.ClipDuration.TotalMilliseconds;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}