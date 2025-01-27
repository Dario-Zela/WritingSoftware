using Blaze.Core;
using ColorPicker;
using ColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        // Color being selected by the control
        public Color ColorPicked
        {
            get { return (Color)GetValue(ColorPickedProperty); }
            set { SetValue(ColorPickedProperty, value); }
        }

        // Color picked property
        public static readonly DependencyProperty ColorPickedProperty =
            DependencyProperty.Register("ColorPicked", typeof(Color), typeof(ColorSelector));

        public ColorSelector()
        {
            InitializeComponent();
        }
    }
}
