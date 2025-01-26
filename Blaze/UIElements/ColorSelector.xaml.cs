using Blaze.Core;
using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.UIElements
{
    [ValueConversion(typeof(HSVColor), typeof(Color))]
    public class HSVToColorConverter : IValueConverter
    {
        private double MinMax(string cond, double value)
        {
            return cond == "min" ? 0 : cond == "max" ? 1000 : value;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameters = (parameter as string).Split('|').ToList();
            HSVColor color = value as HSVColor;
            color.Hue = MinMax(parameters[0], color.Hue);
            color.Saturation = MinMax(parameters[1], color.Saturation);
            color.Value = MinMax(parameters[2], color.Value);
            color.AlphaDouble = MinMax(parameters[3], color.AlphaDouble);
            return HSXColorManager.HSVToColor(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HSVColor : Observable
    {
        public byte Alpha;
        public double AlphaDouble
        {
            get
            {
                return Alpha;
            }
            set
            {
                Alpha = (byte)value;
                OnPropertyChanged();
            }
        }
        private double hue;
        public double Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = Math.Clamp(value, 0, 360);
                OnPropertyChanged();
            }
        }
        private double saturation;
        public double Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = Math.Clamp(value, 0, 1);
                OnPropertyChanged();
            }
        }
        private double _value;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = Math.Clamp(value, 0, 1);
                OnPropertyChanged();
            }
        }

        public string ToString()
        {
            return $"{Alpha}, {Hue}, {Saturation}, {Value}";
        }
    }

    public class HSLColor : Observable
    {
        public byte Alpha;
        public double AlphaDouble
        {
            get
            {
                return Alpha;
            }
            set
            {
                Alpha = (byte)value;
                OnPropertyChanged();
            }
        }
        private double hue;
        public double Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = Math.Clamp(value, 0, 360);
                OnPropertyChanged();
            }
        }
        private double saturation;
        public double Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = Math.Clamp(value, 0, 1);
                OnPropertyChanged();
            }
        }
        private double luminance;
        public double Luminance
        {
            get
            {
                return luminance;
            }
            set
            {
                luminance = Math.Clamp(value, 0, 1);
                OnPropertyChanged();
            }
        }
        public string ToString()
        {
            return $"{Alpha}, {Hue}, {Saturation}, {Luminance}";
        }

    }

    public static class HSXColorManager
    {
        public static HSVColor ColorToHSV(Color color)
        {
            System.Drawing.Color colorHolder = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);

            int max = Math.Max(colorHolder.R, Math.Max(colorHolder.G, colorHolder.B));
            int min = Math.Min(colorHolder.R, Math.Min(colorHolder.G, colorHolder.B));

            HSVColor hsvColor = new HSVColor() { Alpha = colorHolder.A };

            hsvColor.Hue = colorHolder.GetHue();
            hsvColor.Saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            hsvColor.Value = max / 255d;

            return hsvColor;
        }

        public static HSLColor ColorToHSL(Color color)
        {
            return HSVToHSL(ColorToHSV(color));
        }

        public static HSLColor HSVToHSL(HSVColor color)
        {
            HSLColor outColor = new HSLColor() { Hue = color.Hue, Alpha = color.Alpha };
            var saturation = color.Saturation;
            var value = color.Value;

            outColor.Luminance = value * (1 - saturation / 2);
            outColor.Saturation = outColor.Luminance == 0 || outColor.Luminance == 1 ? 0 :
                (value - outColor.Luminance) / Math.Min(outColor.Luminance, 1 - outColor.Luminance);

            return outColor;
        }

        public static HSVColor HSLToHSV(HSLColor color)
        {
            HSVColor outColor = new HSVColor() { Hue = color.Hue, Alpha = color.Alpha };
            var saturation = color.Saturation;
            var luminance = color.Luminance;

            outColor.Value = luminance + saturation * Math.Min(luminance, 1 - luminance);
            outColor.Saturation = outColor.Value == 0 ? 0 : 2 * (1 - luminance / outColor.Value);

            return outColor;
        }

        public static Color HSVToColor(HSVColor color)
        {
            int hi = Convert.ToInt32(Math.Floor(color.Hue / 60)) % 6;
            double f = color.Hue / 60 - Math.Floor(color.Hue / 60);

            var Value = color.Value * 255;
            byte v = Convert.ToByte(Value);
            byte p = Convert.ToByte(Value * (1 - color.Saturation));
            byte q = Convert.ToByte(Value * (1 - f * color.Saturation));
            byte t = Convert.ToByte(Value * (1 - (1 - f) * color.Saturation));

            if (hi == 0)
                return Color.FromArgb(color.Alpha, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(color.Alpha, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(color.Alpha, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(color.Alpha, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(color.Alpha, t, p, v);
            else
                return Color.FromArgb(color.Alpha, v, p, q);
        }

        public static Color HSLToColor(HSLColor color)
        {
            return HSVToColor(HSLToHSV(color));
        }
    }

    /// <summary>
    /// Interaction logic for ColorSelector.xaml
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        public Color ColorPicked
        {
            get { return (Color)GetValue(ColorPickedProperty); }
            set { SetValue(ColorPickedProperty, value); }
        }

        public static readonly DependencyProperty ColorPickedProperty =
            DependencyProperty.Register("ColorPicked", typeof(Color), typeof(ColorSelector), new PropertyMetadata(SelectionChanged));

        private static void SelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selector = (ColorSelector)d;
            selector.HSVColorPicked = HSXColorManager.ColorToHSV((Color)e.NewValue);
        }

        private HSVColor HSVColorPickedProp = new HSVColor();
        public HSVColor HSVColorPicked
        {
            get { return HSVColorPickedProp; }
            set { HSVColorPickedProp = value; }
        }

        public HSLColor HSLColorPicked
        {
            get { return (HSLColor)HSXColorManager.ColorToHSL((Color)GetValue(ColorPickedProperty)); }
            set { SetValue(ColorPickedProperty, HSXColorManager.HSLToColor(value)); }
        }

        public ColorSelector()
        {
            InitializeComponent();
            HSVColorPickedProp.PropertyChanged += (s, e) =>
            {
                ColorPicked = HSXColorManager.HSVToColor(HSVColorPickedProp);
            };
        }
    }
}
