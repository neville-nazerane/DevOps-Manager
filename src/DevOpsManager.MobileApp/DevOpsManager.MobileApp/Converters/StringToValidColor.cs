using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Converters
{
    class StringToValidColor : ValueConverterBase<string>
    {
        public override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value))
                return Color.Gray;
            else
                return Color.Blue;
        }
    }
}
