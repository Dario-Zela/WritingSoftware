using System.Windows;
using Blaze.Core;

namespace Blaze.MVVM.ViewModel
{
    class MainViewModel : Observable
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }

        public RelayCommand Close { get; set; }
        public RelayCommand Maximise { get; set; }
        public RelayCommand Minimise { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });
            DiscoveryViewCommand = new RelayCommand(o => { CurrentView = DiscoveryVM; });

            Close = new RelayCommand(o => { Application.Current.Shutdown(); });
            Maximise = new RelayCommand(o => Maximise_Command());
            Minimise = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
        }

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
