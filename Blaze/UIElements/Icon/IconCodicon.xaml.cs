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
    /// Interaction logic for IconCodicon.xaml
    /// </summary>
    public partial class IconCodicon : Image
    {
        // IconCodicon Kind displayed
        public PackIconCodiconsKind Kind
        {
            get { return (PackIconCodiconsKind)GetValue(KindProperty); }
            set { SetValue(KindProperty, value); }
        }

        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register("Kind", typeof(PackIconCodiconsKind), typeof(IconCodicon), new PropertyMetadata(Changed));

        // If IconCodicon changed, update value
        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconCodicon icon = (IconCodicon)d;
            icon.Source = icon.Converter.ConvertIcon(icon.Kind, icon.Foreground);
        }

        // Foreground color of the icon
        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(IconCodicon), new PropertyMetadata(Changed));

        // Converter Reference
        private CodiconToImageSourceConverter Converter = new();

        public IconCodicon()
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
