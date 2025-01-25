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
    /// Interaction logic for CustomCombobox.xaml
    /// </summary>
    public partial class CustomCombobox : ComboBox
    {
        public CornerRadius ButtonCornerRadius
        {
            get { return (CornerRadius)GetValue(ButtonCornerRadiusProperty); }
            set { SetValue(ButtonCornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ButtonCornerRadiusProperty =
            DependencyProperty.Register("ButtonCornerRadius", typeof(CornerRadius), typeof(CustomCombobox));

        public SolidColorBrush ToggleColor
        {
            get { return (SolidColorBrush)GetValue(ToggleColorProperty); }
            set { SetValue(ToggleColorProperty, value); }
        }

        public static readonly DependencyProperty ToggleColorProperty =
            DependencyProperty.Register("ToggleColor", typeof(SolidColorBrush), typeof(CustomCombobox));

        public SolidColorBrush CurrentForegroundColor
        {
            get { return (SolidColorBrush)GetValue(CurrentForegroundColorProperty); }
            set { SetValue(CurrentForegroundColorProperty, value); }
        }

        public static readonly DependencyProperty CurrentForegroundColorProperty =
            DependencyProperty.Register("CurrentForegroundColor", typeof(SolidColorBrush), typeof(CustomCombobox));

        public SolidColorBrush MenuHoverColor
        {
            get { return (SolidColorBrush)GetValue(MenuHoverColorProperty); }
            set { SetValue(MenuHoverColorProperty, value); }
        }

        public static readonly DependencyProperty MenuHoverColorProperty =
            DependencyProperty.Register("MenuHoverColor", typeof(SolidColorBrush), typeof(CustomCombobox));

        public SolidColorBrush MenuBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(MenuBackgroundColorProperty); }
            set { SetValue(MenuBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty MenuBackgroundColorProperty =
            DependencyProperty.Register("MenuBackgroundColor", typeof(SolidColorBrush), typeof(CustomCombobox));

        public CustomCombobox()
        {
            InitializeComponent();
            this.DropDownOpened += OnDropdown;
            this.DropDownClosed += OnDropdown;
            this.Loaded += (s,e) => { OnDropdown(s, e); };
        }

        private void OnDropdown(object sender, EventArgs e)
        {
            if (IsDropDownOpen)
            {
                CurrentForegroundColor = ToggleColor;
            }
            else
            {
                CurrentForegroundColor = (SolidColorBrush)Foreground;
            }
        }
    }
}
