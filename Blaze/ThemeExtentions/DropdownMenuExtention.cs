using System;
using System.Globalization;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Blaze.Core.Extentions;
using MahApps.Metro.IconPacks;

namespace Blaze.Extentions
{
    //Propriety extentions for the drop down menu

    public class Header : IExtention<string, Header> { }
    public class IconToPlacementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.GetType() != typeof(PackIconCodiconsKind)) return null;

            PackIconCodiconsKind icon = (PackIconCodiconsKind)value;

            switch (icon)
            {
                case PackIconCodiconsKind.ChevronDown: return PlacementMode.Bottom;
                case PackIconCodiconsKind.ChevronUp: return PlacementMode.Top;
                case PackIconCodiconsKind.ChevronLeft: return PlacementMode.Right;
                case PackIconCodiconsKind.ChevronRight: return PlacementMode.Left;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value.GetType() != typeof(PlacementMode)) return null;

            PlacementMode icon = (PlacementMode)value;

            switch (icon)
            {
                case PlacementMode.Bottom: return PackIconCodiconsKind.ChevronDown;
                case PlacementMode.Top: return PackIconCodiconsKind.ChevronUp;
                case PlacementMode.Left: return PackIconCodiconsKind.ChevronLeft;
                case PlacementMode.Right: return PackIconCodiconsKind.ChevronRight;
            }

            return null;
        }
    }

    //Converter to get the negative of the input
    public class NegateValueConverter : IValueConverter
    {
        private static bool IsNumber(object obj)
        {
            if (Equals(obj, null))
            {
                return false;
            }

            Type objType = obj.GetType();
            objType = Nullable.GetUnderlyingType(objType) ?? objType;

            if (objType.IsPrimitive)
            {
                return objType != typeof(bool) &&
                    objType != typeof(char) &&
                    objType != typeof(IntPtr) &&
                    objType != typeof(UIntPtr);
            }

            return objType == typeof(decimal);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!IsNumber(value)) return null;
            else return -(double)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!IsNumber(value)) return null;
            else return -(double)value;
        }
    }
    public class Placement : IExtention<PlacementMode, Placement> { }
    public class MaterialDesignIcon : IExtention<PackIconMaterialDesignKind, MaterialDesignIcon> { }

}
