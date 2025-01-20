using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Blaze.Core
{
    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    public class ColorIntensifier : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intensty = float.Parse((string)parameter);
            SolidColorBrush brush = (SolidColorBrush)value;
            if(brush == null)
            {
                return brush;
            }
            var changedColor = brush.Color * intensty;
            changedColor.A = brush.Color.A;
            return new SolidColorBrush(changedColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intensty = float.Parse((string)parameter);
            SolidColorBrush brush = (SolidColorBrush)value;
            if (brush == null)
            {
                return brush;
            }
            var changedColor = brush.Color * (1 / intensty);
            changedColor.A = brush.Color.A;
            return new SolidColorBrush(changedColor);
        }
    }

    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    public class ColorTranslucifier : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intensty = byte.Parse((string)parameter);
            SolidColorBrush brush = (SolidColorBrush)value;
            if (brush == null)
            {
                return brush;
            }
            var changedColor = brush.Color;
            changedColor.A = intensty;
            return new SolidColorBrush(changedColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorIntensityConverter : IValueConverter
    {
        public IValueConverter Converter1 = new ColorIntensifier();
        public IValueConverter Converter2 = new ColorTranslucifier();

        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = (parameter as string).Split('|');
            object convertedValue =
                Converter1.Convert(value, targetType, parameters[0], culture);
            return Converter2.Convert(
                convertedValue, targetType, parameters[1], culture);
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = (parameter as string).Split('|');
            object convertedValue =
                Converter1.ConvertBack(value, targetType, parameters[0], culture);
            return Converter2.ConvertBack(
                convertedValue, targetType, parameters[1], culture);
        }
    }

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public BoolToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}
