using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.IconPacks.Converter;
using System.Windows.Controls;
using System.Diagnostics;

namespace Blaze.Core
{
    /// <summary>
    /// The collections of converters used in the solution
    /// </summary>


    // Changes the intensity of a binded color.
    // Will change between intensifying and dimming color depending on the
    // Intensity of the color.
    [ValueConversion(typeof(SolidColorBrush), typeof(SolidColorBrush))]
    public class ColorIntensifier : IValueConverter
    {
        // Check Intensity
        private double CheckIntensity(Color color)
        {
            return ((double)color.R + (double)color.G + (double)color.B) / (265 * 3);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intensty = float.Parse((string)parameter);
            SolidColorBrush brush = (SolidColorBrush)value;
            if (brush == null)
            {
                return brush;
            }
            if (CheckIntensity(brush.Color) < 0.5)
            {
                intensty = intensty < 1 ? 1 / intensty : intensty;
            }
            else
            {
                intensty = intensty > 1 ? 1 / intensty : intensty;
            }
            var changedColor = brush.Color * intensty;
            changedColor.A = brush.Color.A;
            return new SolidColorBrush(changedColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Changes the alpha value of a binded color.
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

    // Inverts the value of a boolean
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }

    // Converts color to solid color brush
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converts SolidColorBursh to Brush
    [ValueConversion(typeof(SolidColorBrush), typeof(Brush))]
    public class ColorBrushToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((SolidColorBrush)value as Brush);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Brush)value as SolidColorBrush);
        }
    }

    // Combines a color intenifier and a translucifier
    public class ColorIntensityConverter : IValueConverter
    {
        public IValueConverter Converter1 = new ColorIntensifier();
        public IValueConverter Converter2 = new ColorTranslucifier();

        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Paramters are gotten in the form intensity|alpha
            var parameters = (parameter as string).Split('|');
            object convertedValue =
                Converter1.Convert(value, targetType, parameters[0], culture);
            return Converter2.Convert(
                convertedValue, targetType, parameters[1], culture);
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Converts a boolean to a specified visibility
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

    // Adds a value to a double
    [ValueConversion(typeof(Double), typeof(Double))]
    public class Adder : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var adder = Double.Parse((string)parameter);
            return (Double)value + adder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var adder = Double.Parse((string)parameter);
            return (Double)value - adder;
        }
    }

    // Multiplies a value to a double
    [ValueConversion(typeof(Double), typeof(Double))]
    public class Multiplier : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mult = Double.Parse((string)parameter);
            return (Double)value * mult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mult = Double.Parse((string)parameter);
            return (Double)value / mult;
        }
    }

    // Converts an icon to a image source
    public class IconToImageConverter : PackIconCodiconsKindToImageConverter
    {
        public ImageSource ConvertIcon(object iconKind, Brush foregroundBrush)
        {
            return CreateImageSource(iconKind, foregroundBrush);
        }
    }
}
