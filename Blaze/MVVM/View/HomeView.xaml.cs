using System.Windows.Controls;
using Blaze.Core;

namespace Blaze.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        private ProjectLibrary ProjectLibrary = new ProjectLibrary();

        public HomeView()
        {
            InitializeComponent();
            foreach (Project project in ProjectLibrary.projects)
            {
                NewLinkedProject.Items.Add((MenuItem)project);
            }
        }
    }
}
