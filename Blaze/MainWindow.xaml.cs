using System;
using System.Diagnostics;
using System.Windows;
using Blaze.Core;
using Blaze.View.Windows.Theme;
using MahApps.Metro.IconPacks;

namespace Blaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: Remove
        private bool _isLight = true;

        // Main Window instance
        public static MainWindow Instance { get; private set; }

        // Checks if window is maximised
        public bool IsMaximised
        {
            get { return (bool)GetValue(IsMaximisedProperty); }
            set { SetValue(IsMaximisedProperty, value); }
        }

        public static readonly DependencyProperty IsMaximisedProperty =
            DependencyProperty.Register("IsMaximised", typeof(bool), typeof(MainWindow));

        //Commands for the window controls
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand Maximise { get; set; }
        public RelayCommand Minimise { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            IsMaximised = false;
            DataContext = this;
            Instance = this;
            Holder.Children.Add(new ThemeLibrary());

            //Adding methods to the commands
            CloseWindow = new RelayCommand(o => { Application.Current.Shutdown(); });
            Maximise = new RelayCommand(o => Maximise_Command());
            Minimise = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
        }

        // Change the currently displayed view
        public static void ChangeScene(UIElement element)
        {
            MainWindow.Instance.Holder.Children.Clear();
            MainWindow.Instance.Holder.Children.Add(element);
        }

        // Fixes Window problems when maximised
        protected void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                BorderThickness = new Thickness(6);
                IsMaximised = true;
            }
            else if (WindowState == WindowState.Normal)
            {
                BorderThickness = new Thickness(0);
                IsMaximised = false;
            }
        }

        // TODO: Remove
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

        //Maximise method
        private void Maximise_Command()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
