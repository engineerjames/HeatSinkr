using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HeatSinkr.UI.Utility
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts from boolean isChecked value to Collapsed or visible
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
        {
            var isChecked = (bool)value;
            var returnVal = isChecked == true ? Visibility.Visible : Visibility.Collapsed;
            return returnVal;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
