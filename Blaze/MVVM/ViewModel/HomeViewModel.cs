using System.Windows;
using Blaze.Core;
using MahApps.Metro.IconPacks;

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
