using Blaze.Core;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for IconMaterial.xaml
    /// </summary>
    public partial class IconMaterial : Image
    {
        // IconMaterial Kind displayed
        public PackIconMaterialKind Kind
        {
            get { return (PackIconMaterialKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(PackIconMaterialKind), typeof(IconMaterial), new PropertyMetadata(Changed));

        // If IconMaterial changed, update value
        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconMaterial icon = (IconMaterial)d;
            icon.Source = icon.Converter.ConvertIcon(icon.Kind, icon.Foreground);
        }

        // Foreground color of the icon
        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(IconMaterial), new PropertyMetadata(Changed));

        // Converter Reference
        private MaterialIconToImageConverter Converter = new();

        public IconMaterial()
        {
            InitializeComponent();
            Loaded += FirstUpdate;
        }

        // Initial Setup of icon
        private void FirstUpdate(object sender, RoutedEventArgs e)
        {
            Source = Converter.ConvertIcon(Kind, Foreground);
        }
    }
}
