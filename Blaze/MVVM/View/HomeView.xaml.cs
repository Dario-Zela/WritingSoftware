using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Blaze.Core;

namespace Blaze.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            NewLinkedProject.ItemsSource = (ObservableCollection<Project>)ProjectLibrary.projects;
            ExistingProjects.ItemsSource = (ObservableCollection<Project>)ProjectLibrary.projects;

            switch (Properties.Settings.Default.SortingMethod)
            {
                case "0": SortAlphabetical.IsSelected = true; break;
                case "1": SortDate.IsSelected = true; break;
                case "2": SortManual.IsSelected = true; break;
            }

            SortAlphabetical.Selected += new RoutedEventHandler((s, o) => ProjectLibrary.SortAlphabetically());
            SortDate.Selected += new RoutedEventHandler((s, o) => ProjectLibrary.SortDateCreated());
            SortManual.Selected += new RoutedEventHandler((s, o) => ProjectLibrary.SortManualSorting());
        }

    }
}
