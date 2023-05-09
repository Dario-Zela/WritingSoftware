using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;

namespace Blaze.Core.Extentions
{
    public class IExtention<T, I>
    {
        public static DependencyProperty ExtendedProperty =
            DependencyProperty.RegisterAttached("Property",
                                                typeof(T),
                                                typeof(IExtention<T, I>),
                                                new PropertyMetadata(default(T)));

        public static T GetProperty(DependencyObject target)
        {
            return (T)target.GetValue(ExtendedProperty);
        }

        public static void SetProperty(DependencyObject target, T value)
        {
            target.SetValue(ExtendedProperty, value);
        }
    }
}
