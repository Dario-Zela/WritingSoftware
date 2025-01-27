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
    /// Interaction logic for CustomComboBox.xaml
    /// </summary>
    public partial class CustomComboBox : ComboBox
    {
        // Dependency proprieties of Custom Combobox

        // Corner Radius of Box
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomComboBox));

        // Hover color of the Menu
        public SolidColorBrush MenuHoverColor
        {
            get { return (SolidColorBrush)GetValue(MenuHoverColorProperty); }
            set { SetValue(MenuHoverColorProperty, value); }
        }

        public static readonly DependencyProperty MenuHoverColorProperty =
            DependencyProperty.Register("MenuHoverColor", typeof(SolidColorBrush), typeof(CustomComboBox));

        // Background color of the Menu
        public SolidColorBrush MenuBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundColorProperty =
            DependencyProperty.Register("MenuBackgroundColor", typeof(SolidColorBrush), typeof(CustomComboBox));

        // Objects to add before Text
        public object PreTextContent
        {
            get { return (object)GetValue(PreTextContentProperty); }
            set { SetValue(PreTextContentProperty, value); }
        }

        public static readonly DependencyProperty PreTextContentProperty =
            DependencyProperty.Register("PreTextContent", typeof(object), typeof(CustomComboBox));

        // Accent Color of Text
        public SolidColorBrush AccentColor
        {
            get { return (SolidColorBrush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(CustomComboBox));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(CustomComboBox));

        public CustomComboBox()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        // Make sure no item is ever selected
        private void UndoSelection(object sender, SelectionChangedEventArgs e)
        {
            SelectedIndex = -1;
        }
    }
}
