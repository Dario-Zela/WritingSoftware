using System.Windows;

namespace Blaze.Core.Extentions
{
    public class IExtention<T, I>
    {
        public static DependencyProperty PropertyProperty =
            DependencyProperty.RegisterAttached("Property",
                                                typeof(T),
                                                typeof(IExtention<T, I>),
                                                new PropertyMetadata(default(T)));

        public static T GetProperty(DependencyObject target)
        {
            if (PropertyProperty == null)
                return default;

            return (T)target.GetValue(PropertyProperty);
        }

        public static void SetProperty(DependencyObject target, T value)
        {
            target.SetValue(PropertyProperty, value);
        }
    }
}
