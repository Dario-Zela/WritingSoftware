using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for CustomSelectionBox.xaml
    /// </summary>
    public partial class CustomSelectionBox : ComboBox
    {
        // Dependency Proprieties of the Selection Box

        // Corner Radius of Box
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomSelectionBox));

        // Hover color of the Menu
        public SolidColorBrush MenuHoverColor
        {
            get { return (SolidColorBrush)GetValue(MenuHoverColorProperty); }
            set { SetValue(MenuHoverColorProperty, value); }
        }

        public static readonly DependencyProperty MenuHoverColorProperty =
            DependencyProperty.Register("MenuHoverColor", typeof(SolidColorBrush), typeof(CustomSelectionBox));

        // Background color of the Menu
        public SolidColorBrush MenuBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundColorProperty =
            DependencyProperty.Register("MenuBackgroundColor", typeof(SolidColorBrush), typeof(CustomSelectionBox));

        // Margin of the separation bar
        public Thickness BarMargin
        {
            get { return (Thickness)base.GetValue(BarMarginProperty); }
            set { base.SetValue(BarMarginProperty, value); }
        }

        public static DependencyProperty BarMarginProperty =
            DependencyProperty.Register("BarMargin", typeof(Thickness), typeof(CustomSelectionBox));


        public CustomSelectionBox()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}