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

namespace Blaze.View.Windows.Theme
{
    /// <summary>
    /// Interaction logic for ThemeLibrary.xaml
    /// </summary>
    public partial class ThemeLibrary : UserControl
    {
        public List<string> themeNames => ((App)Application.Current).themes.Select(
            theme => theme.Segments.Last().Split('.')[0]).ToList();

        public ThemeLibrary()
        {
            ((App)Application.Current).InitialiseThemes();
            InitializeComponent();
            //AvailableThemes.ListView.ItemsSource = themeNames;
        }

    }
}
