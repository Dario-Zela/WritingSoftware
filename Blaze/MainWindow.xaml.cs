using System;
using System.Windows;
using Blaze.Core;
using MahApps.Metro.IconPacks;

namespace Blaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isLight = true;
        public MainWindow()
        {

            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        protected void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness(6);
            }
            else if (WindowState == WindowState.Normal)
            {
                BorderThickness = new Thickness(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            if (_isLight == true)
            {
                app.ChangeTheme(0);
            }
            else
            {
                app.ChangeTheme(1);
            }
            _isLight = !_isLight;
        }
    }
}
