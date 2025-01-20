using System.Windows;

namespace Blaze.Core
{
    /// <summary>
    /// A  class to quickly add extra proprieties to wpf elements
    /// </summary>
    /// <typeparam name="T">The type of the inner dependency property</typeparam>
    /// <typeparam name="I">The class inheriting IExtention, used to allow for different proprieties with the same name</typeparam>
    public class Extention<T, I>
    {
        /// <summary>
        /// The dependecy propriety being changed
        /// </summary>
        public static DependencyProperty PropertyProperty =
            DependencyProperty.RegisterAttached("Property",
                                                typeof(T),
                                                typeof(Extention<T, I>),
                                                new PropertyMetadata(default(T)));

        //Getter and setter as required by framework
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