using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Blaze.Core
{
    public class Observable : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
