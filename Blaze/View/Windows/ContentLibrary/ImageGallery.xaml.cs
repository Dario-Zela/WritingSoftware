using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Blaze.View.Windows.ContentLibrary
{
    /// <summary>
    /// Interaction logic for ImageGallery.xaml
    /// </summary>
    public partial class ImageGallery : Border
    {
        // Surface Color of Gallery
        public SolidColorBrush SurfaceColor
        {
            get { return (SolidColorBrush)GetValue(SurfaceColorProperty); }
            set { SetValue(SurfaceColorProperty, value); }
        }

        public static readonly DependencyProperty SurfaceColorProperty =
            DependencyProperty.Register("SurfaceColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // On Surface Color of Gallery
        public SolidColorBrush OnSurfaceColor
        {
            get { return (SolidColorBrush)GetValue(OnSurfaceColorProperty); }
            set { SetValue(OnSurfaceColorProperty, value); }
        }

        public static readonly DependencyProperty OnSurfaceColorProperty =
            DependencyProperty.Register("OnSurfaceColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // Secondary Color of Gallery
        public SolidColorBrush SecondaryColor
        {
            get { return (SolidColorBrush)GetValue(SecondaryColorProperty); }
            set { SetValue(SecondaryColorProperty, value); }
        }

        public static readonly DependencyProperty SecondaryColorProperty =
            DependencyProperty.Register("SecondaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // On Secondary Color of Gallery
        public SolidColorBrush OnSecondaryColor
        {
            get { return (SolidColorBrush)GetValue(OnSecondaryColorProperty); }
            set { SetValue(OnSecondaryColorProperty, value); }
        }

        public static readonly DependencyProperty OnSecondaryColorProperty =
            DependencyProperty.Register("OnSecondaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        // Primary Color of Gallery 
        public SolidColorBrush PrimaryColor
        {
            get { return (SolidColorBrush)GetValue(PrimaryColorProperty); }
            set { SetValue(PrimaryColorProperty, value); }
        }

        public static readonly DependencyProperty PrimaryColorProperty =
            DependencyProperty.Register("PrimaryColor", typeof(SolidColorBrush), typeof(ImageGallery));

        public ImageGallery()
        {
            SurfaceColor = new SolidColorBrush(Colors.White);
            OnSurfaceColor = new SolidColorBrush(Colors.Black);
            SecondaryColor = new SolidColorBrush(Colors.DarkBlue);
            OnSecondaryColor = new SolidColorBrush(Colors.White);
            PrimaryColor = new SolidColorBrush(Colors.Red);
            InitializeComponent();
            DataContext = this;
        }

        public void AlphabeticalSort(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("AlphaSort");
        }

        public void RecentSort(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("RecentSort");
        }

        public void AlbumsSort(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("AlbumsSort");
        }

        public void ManualSort(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("ManualSort");
        }
    }
}
