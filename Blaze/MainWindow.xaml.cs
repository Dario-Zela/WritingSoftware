using System;
using System.Windows;
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
