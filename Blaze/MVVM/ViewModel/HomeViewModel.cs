using Blaze.Core;

namespace Blaze.MVVM.ViewModel
{
    class HomeViewModel : Observable
    {
        //New Projects command
        public RelayCommand NewProject { get; set; }

        public HomeViewModel()
        {

            NewProject = new RelayCommand(o => ProjectLibrary.NewProject());
        }
    }
}
