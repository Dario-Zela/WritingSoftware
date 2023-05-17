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
        public MainWindow()
        {
            ProjectLibrary.InitiateLibrary();
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        protected void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness(6);

                MaximiseButton.SetValue(Extentions.Codicons.PropertyProperty, PackIconCodiconsKind.ChromeRestore);
            }
            else if (WindowState == WindowState.Normal)
            {
                BorderThickness = new Thickness(0);

                MaximiseButton.SetValue(Extentions.Codicons.PropertyProperty, PackIconCodiconsKind.ChromeMaximize);
            }
        }
    }
}
