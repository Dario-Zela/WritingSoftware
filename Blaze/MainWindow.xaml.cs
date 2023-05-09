using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using MahApps.Metro.IconPacks;

namespace Blaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized) 
            {
                BorderThickness = new Thickness(6);

                MaximiseButton.SetValue(Core.Icon.ExtendedProperty, PackIconCodiconsKind.ChromeRestore);
            }
            else if (WindowState == WindowState.Normal) 
            {
                BorderThickness = new Thickness(0);

                MaximiseButton.SetValue(Core.Icon.ExtendedProperty, PackIconCodiconsKind.ChromeMaximize);
            }
        }
    }
}
