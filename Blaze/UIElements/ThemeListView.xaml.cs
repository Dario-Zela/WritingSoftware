using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for ThemeListView.xaml
    /// </summary>
    public partial class ThemeListView : UserControl
    {
        // UI Dependencies
        public static DependencyProperty ItemMarginProperty = DependencyProperty.Register(
            name: "ItemMargin",
            propertyType: typeof(Thickness),
            ownerType: typeof(ThemeListView));

        public Thickness ItemMargin
        {
            get { return (Thickness)base.GetValue(ItemMarginProperty); }
            set { base.SetValue(ItemMarginProperty, value); }
        }

        public static DependencyProperty ItemMouseOverColorProperty = DependencyProperty.Register(
            name: "ItemMouseOverColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata(((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"])));

        public SolidColorBrush ItemMouseOverColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemMouseOverColorProperty); }
            set { base.SetValue(ItemMouseOverColorProperty, value); }
        }

        public static DependencyProperty ItemSelectedActiveColorProperty = DependencyProperty.Register(
            name: "ItemSelectedActiveColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata(((SolidColorBrush)Application.Current.Resources["ApplicationPrimaryBrush"])));


        public SolidColorBrush ItemSelectedActiveColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemSelectedActiveColorProperty); }
            set { base.SetValue(ItemSelectedActiveColorProperty, value); }
        }

        public static DependencyProperty ItemInactiveColorProperty = DependencyProperty.Register(
            name: "ItemInactiveColor",
            propertyType: typeof(SolidColorBrush),
            ownerType: typeof(ThemeListView),
            typeMetadata: new PropertyMetadata(((SolidColorBrush)Application.Current.Resources["ApplicationSurface"])));


        public SolidColorBrush ItemInactiveColor
        {
            get { return (SolidColorBrush)base.GetValue(ItemInactiveColorProperty); }
            set { base.SetValue(ItemInactiveColorProperty, value); }
        }

        // List View

        public ListView ListView => ContainerListView;

        public ThemeListView()
        {
            InitializeComponent();
        }

        private void PopupMenu_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Opened");
        }
    }
}
