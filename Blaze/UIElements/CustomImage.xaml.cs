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
    /// Interaction logic for CustomImage.xaml
    /// Custom Image allows for blank images to be used as dynamic resources
    /// </summary>
    public partial class CustomImage : Image
    {
        // Source image
        public Image SourceImage
        {
            get { return (Image)GetValue(SourceImageProperty); }
            set { SetValue(SourceImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceImageProperty =
            DependencyProperty.Register("SourceImage", typeof(Image), typeof(CustomImage), new PropertyMetadata(ChangeImage));

        // Changes the image displayed
        private static void ChangeImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var image = (d as CustomImage);
            image.Source = image.SourceImage.Source;
        }

        public CustomImage()
        {
            InitializeComponent();
        }
    }
}
