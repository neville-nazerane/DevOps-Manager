using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevOpsManager.MobileApp.Converters
{
    abstract class ValueConverterBase : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    abstract class ValueConverterBase<T> : ValueConverterBase
    {

        public abstract object Convert(T value, Type targetType, object parameter, CultureInfo culture);

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((T)value, targetType, parameter, culture);
        }

    }

    abstract class ValueConverterBase<TValue, TParameter> : ValueConverterBase
    {

        public abstract object Convert(TValue value, Type targetType, TParameter parameter, CultureInfo culture);

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TValue)value, targetType, (TParameter)parameter, culture);
        }

    }

}
