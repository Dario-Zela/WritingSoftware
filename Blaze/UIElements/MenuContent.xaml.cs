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
    /// Interaction logic for MenuContent.xaml
    /// </summary>
    public partial class MenuContent : ListView
    {
        // Corner Radius of Menu Content
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MenuContent));

        // Marging of items displayed
        public Thickness ItemMargin
        {
            get { return (Thickness)base.GetValue(ItemMarginProperty); }
            set { base.SetValue(ItemMarginProperty, value); }
        }

        public static DependencyProperty ItemMarginProperty = 
            DependencyProperty.Register("ItemMargin", typeof(Thickness), typeof(MenuContent));

        // Hover color of Menu
        public SolidColorBrush HoverColor
        {
            get { return (SolidColorBrush)GetValue(HoverColorProperty); }
            set { SetValue(HoverColorProperty, value); }
        }

        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register("HoverColor", typeof(SolidColorBrush), typeof(MenuContent));

        public MenuContent()
        {
            InitializeComponent();
        }
    }
}
