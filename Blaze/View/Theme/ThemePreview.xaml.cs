using Blaze.Model;
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

namespace Blaze.View.Theme
{
    /// <summary>
    /// Interaction logic for ThemePreview.xaml
    /// </summary>
    public partial class ThemePreview : UserControl
    {
        public ThemePreview()
        {
            InitializeComponent();
            // If data context is not theme, hide preview
            Loaded += (s, e) =>
            {
                if (DataContext is not Blaze.Model.Theme)
                {
                    Visibility = Visibility.Hidden;
                }
            };

            DataContextChanged += (s, e) =>
            {
                if (DataContext is not Blaze.Model.Theme)
                {
                    Visibility = Visibility.Hidden;
                }
                else
                {
                    Visibility = Visibility.Visible;
                }
            };
        }
    }
}
