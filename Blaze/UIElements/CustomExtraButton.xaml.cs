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

namespace Blaze.UIElements
{
    /// <summary>
    /// Interaction logic for CustomExtraButton.xaml
    /// </summary>
    public partial class CustomExtraButton : ComboBox
    {
        // Dependency proprieties of Custom Combobox

        // Corner Radius of Box
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomExtraButton));

        // Hover color of the Menu
        public SolidColorBrush MenuHoverColor
        {
            get { return (SolidColorBrush)GetValue(MenuHoverColorProperty); }
            set { SetValue(MenuHoverColorProperty, value); }
        }

        public static readonly DependencyProperty MenuHoverColorProperty =
            DependencyProperty.Register("MenuHoverColor", typeof(SolidColorBrush), typeof(CustomExtraButton));

        // Background color of the Menu
        public SolidColorBrush MenuBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundColorProperty =
            DependencyProperty.Register("MenuBackgroundColor", typeof(SolidColorBrush), typeof(CustomExtraButton));

        // Kind of icon displayed
        public PackIconMaterialKind HeaderKind
        {
            get { return (PackIconMaterialKind)GetValue(HeaderKindProperty); }
            set { SetValue(HeaderKindProperty, value); }
        }

        public static readonly DependencyProperty HeaderKindProperty =
            DependencyProperty.Register("HeaderKind", typeof(PackIconMaterialKind), typeof(CustomExtraButton));

        // Accent Color of Text
        public SolidColorBrush AccentColor
        {
            get { return (SolidColorBrush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(SolidColorBrush), typeof(CustomExtraButton));

        public CustomExtraButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UndoSelection(object sender, SelectionChangedEventArgs e)
        {
            SelectedIndex = -1;
        }
    }
}
