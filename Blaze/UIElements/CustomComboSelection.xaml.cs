using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
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
    /// Interaction logic for CustomComboSelection.xaml
    /// </summary>
    public partial class CustomComboSelection : ComboBox
    {
        // Dependency Proprieties of the Selection Box

        // Corner Radius of Box
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomComboSelection));

        // Hover color of the Menu
        public SolidColorBrush MenuHoverColor
        {
            get { return (SolidColorBrush)GetValue(MenuHoverColorProperty); }
            set { SetValue(MenuHoverColorProperty, value); }
        }

        public static readonly DependencyProperty MenuHoverColorProperty =
            DependencyProperty.Register("MenuHoverColor", typeof(SolidColorBrush), typeof(CustomComboSelection));

        // Background color of the Menu
        public SolidColorBrush MenuBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundColorProperty =
            DependencyProperty.Register("MenuBackgroundColor", typeof(SolidColorBrush), typeof(CustomComboSelection));

        // Objects to add before Text
        public object PreTextContent
        {
            get { return (object)GetValue(PreTextContentProperty); }
            set { SetValue(PreTextContentProperty, value); }
        }

        public static readonly DependencyProperty PreTextContentProperty =
            DependencyProperty.Register("PreTextContent", typeof(object), typeof(CustomComboSelection));

        // Accent Color of Text
        public SolidColorBrush AccentColor
        {
            get { return (SolidColorBrush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(CustomComboSelection));

        public CustomComboSelection()
        {
            DataContext = this;
            InitializeComponent();
        }

    }
}
