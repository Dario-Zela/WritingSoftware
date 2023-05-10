using System.Windows;

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
