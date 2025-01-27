using Blaze.Core;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.UIElements
{

    /// <summary>
    /// Interaction logic for ChangingIcon.xaml
    /// </summary>
    public partial class ChangingIcon : Image
    {
        // Dependency Proprieties of the Control

        // Image first shown
        public PackIconCodiconsKind FirstKind
        {
            get { return (PackIconCodiconsKind)GetValue(FirstKindProperty); }
            set { SetValue(FirstKindProperty, value); }
        }

        public static readonly DependencyProperty FirstKindProperty =
            DependencyProperty.Register("FirstKind", typeof(PackIconCodiconsKind), typeof(ChangingIcon));

        // Image shown after change fires
        public PackIconCodiconsKind SecondKind
        {
            get { return (PackIconCodiconsKind)GetValue(SecondKindProperty); }
            set { SetValue(SecondKindProperty, value); }
        }

        public static readonly DependencyProperty SecondKindProperty =
            DependencyProperty.Register("SecondKind", typeof(PackIconCodiconsKind), typeof(ChangingIcon));

        // When to change the Image
        public bool Change
        {
            get { return (bool)GetValue(ChangeProperty); }
            set
            { SetValue(ChangeProperty, value); }
        }

        public static readonly DependencyProperty ChangeProperty =
            DependencyProperty.Register("Change", typeof(bool), typeof(ChangingIcon), new PropertyMetadata(ChangeIcon));

        // Appply image change
        private static void ChangeIcon(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ChangingIcon icon = (ChangingIcon)d;
            if ((bool)e.NewValue)
            {
                icon.Source = icon.Converter.ConvertIcon(icon.SecondKind, icon.ActiveForeground);
            }
            else
            {
                icon.Source = icon.Converter.ConvertIcon(icon.FirstKind, icon.Foreground);
            }
        }

        // Foreground shown when second image is shown
        public SolidColorBrush ActiveForeground
        {
            get { return (SolidColorBrush)GetValue(ActiveForegroundProperty); }
            set { SetValue(ActiveForegroundProperty, value); }
        }

        public static readonly DependencyProperty ActiveForegroundProperty =
            DependencyProperty.Register("ActiveForeground", typeof(SolidColorBrush), typeof(ChangingIcon));

        // Foreground shown when first image is shown
        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(ChangingIcon));

        // Image Converter reference
        private CodiconToImageSourceConverter Converter = new();

        public ChangingIcon()
        {
            InitializeComponent();
            this.Loaded += InitialiseImage;
        }

        // Initialises the image when loaded
        private void InitialiseImage(object sender, RoutedEventArgs e)
        {
            if (Change)
            {
                Source = Converter.ConvertIcon(SecondKind, ActiveForeground);
            }
            else
            {
                Source = Converter.ConvertIcon(FirstKind, Foreground);
            }
        }
    }
}
