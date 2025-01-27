using Blaze.Core;
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

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for CustomNumericalBox.xaml
    /// </summary>
    public partial class CustomNumericalBox : TextBox
    {
        // Dependency Proprieties of the Numerical Box

        // Corner Radius of Box
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CustomNumericalBox));

        // Corner Radius of buttons
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomNumericalBox));

        // Currently Displayed Value
        public uint Value
        {
            get { return uint.Parse((string)GetValue(ValueProperty)); }
            set { SetValue(ValueProperty, value.ToString()); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(CustomNumericalBox), new PropertyMetadata("0", ValueSet));

        // Update Value
        private static void ValueSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustomNumericalBox box = (CustomNumericalBox)d;
            uint value;
            if (uint.TryParse((string)e.NewValue, out value))
            {
                box.Value = value;
            }
            else
            {
                box.Value = uint.Parse((string)e.OldValue);
            }
        }

        // Width of Buttons
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(CustomNumericalBox));

        // Commands
        public RelayCommand Add { get; set; }
        public RelayCommand Subtract { get; set; }

        public CustomNumericalBox()
        {
            InitializeComponent();
            Subtract = new RelayCommand(o => { if (Value > 0) Value--; });
            Add = new RelayCommand(o => { if (Value < uint.MaxValue) Value++; });
            DataContext = this;
        }
    }
}
