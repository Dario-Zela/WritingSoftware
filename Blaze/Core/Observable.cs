using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Blaze.Core
{
    /// <summary>
    /// Observable interface, used for objects that need to inherit INotifyPropertyChanged to reduce code
    /// </summary>
    public class Observable : DependencyObject, INotifyPropertyChanged
    {
        //Inner event
        public event PropertyChangedEventHandler PropertyChanged;
        
        //Event raiser
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
