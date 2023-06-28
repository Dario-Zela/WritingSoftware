using System.Windows;
using Blaze.Core;

namespace Blaze.MVVM.ViewModel
{
    class MainViewModel : Observable
    {
        //Commands to get the home and discovery view
        //Depreciated, need to be removed
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }

        //Commands for the window controls
        public RelayCommand Close { get; set; }
        public RelayCommand Maximise { get; set; }
        public RelayCommand Minimise { get; set; }

        //Home View Model and current view
        //Depreciated
        public HomeViewModel HomeVM { get; set; }
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
            //Depreciated, need to be removed
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
            HomeViewCommand = new RelayCommand(o => { CurrentView = HomeVM; });

            //Adding methods to the commands
            Close = new RelayCommand(o => { Application.Current.Shutdown(); });
            Maximise = new RelayCommand(o => Maximise_Command());
            Minimise = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
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
