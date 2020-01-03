using DevOpsManager.MobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DevOpsManager.MobileApp.Converters
{
    class BoolToStarConverter : ValueConverterBase<bool>
    {
        public override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value) return "star_on.png".GetSharedImage();
            else return "star_off.png".GetSharedImage();
        }
    }
}
