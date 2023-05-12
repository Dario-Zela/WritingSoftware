using System.Windows.Controls;
using Blaze.Core;

namespace Blaze.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public ProjectLibrary ProjectLibrary = new ProjectLibrary();

        public HomeView()
        {
            InitializeComponent();
            NewLinkedProject.ItemsSource = ProjectLibrary.projects;
        }
    }
}
