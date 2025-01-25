using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ColorSelector.xaml
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        public Color ColorPicked
        {
            get { return (Color)GetValue(ColorPickedProperty); }
            set { SetValue(ColorPickedProperty, value); }
        }

        public static readonly DependencyProperty ColorPickedProperty =
            DependencyProperty.Register("ColorPicked", typeof(Color), typeof(ColorSelector), new PropertyMetadata(OnSelectionChanged));

        private static void OnSelectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var selector = (ColorSelector)d;
            selector.ColorSlider.SelectedColor = selector.ColorPicked;
        }


        public ColorSelector()
        {
            InitializeComponent();
        }

        private void ColorSlider_ColorChanged(object sender, RoutedEventArgs e)
        {
            ColorPicked = ColorSlider.SelectedColor;
        }
    }
}
