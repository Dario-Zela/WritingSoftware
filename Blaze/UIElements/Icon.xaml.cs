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
using static ICSharpCode.AvalonEdit.Document.TextDocumentWeakEventManager;

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for Icon.xaml
    /// </summary>
    public partial class Icon : Image
    {
        // Icon Kind displayed
        public PackIconCodiconsKind Kind
        {
            get { return (PackIconCodiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(PackIconCodiconsKind), typeof(Icon), new PropertyMetadata(Changed));

        // If Icon changed, update value
        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Icon icon = (Icon)d;
            icon.Source = icon.Converter.ConvertIcon(icon.Kind, icon.Foreground);
        }

        // Foreground color of the icon
        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(Icon), new PropertyMetadata(Changed));

        // Converter Reference
        private IconToImageConverter Converter = new();

        public Icon()
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
