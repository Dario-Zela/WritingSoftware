using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
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
    /// Interaction logic for ImageOrBorder.xaml
    /// </summary>
    public partial class ImageOrBorder : UserControl
    {
        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Uri), typeof(ImageOrBorder), new PropertyMetadata(SetImageSource));

        private static void SetImageSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var image = (ImageOrBorder)d;
            if (image.Source == null) return;
            try
            {
                BitmapImage sourceBitmap = new BitmapImage();

                sourceBitmap.BeginInit();
                sourceBitmap.UriSource = image.Source;
                sourceBitmap.CacheOption = BitmapCacheOption.OnLoad;
                sourceBitmap.DecodePixelWidth = (int)image.ActualWidth;
                sourceBitmap.EndInit();

                sourceBitmap.Freeze();
                image.Image.Source = sourceBitmap;
            }
            catch 
            {
                image.Image.Source = null;
            }
        }

        public ImageOrBorder()
        {
            InitializeComponent();
        }
    }
}
