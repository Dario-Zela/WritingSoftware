using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Blaze.CustomControls
{
    /// <summary>
    /// Interaction logic for ListBoxElement.xaml
    /// </summary>
    public partial class ListBoxElement : UserControl
    {
        public ListBoxElement()
        {
            InitializeComponent();
        }

        public void AddNewElement()
        {
            ListHolder.Items.Add(new ListBoxItem());
        }

        object CurrentListItem = null;

        private void AllowClosing(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button)((Grid)sender).FindName("Close")).Visibility = System.Windows.Visibility.Visible;
            CurrentListItem = sender;
        }

        private void StopClosing(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CurrentListItem = null;
            ((Button)((Grid)sender).FindName("Close")).Visibility = System.Windows.Visibility.Hidden;
        }

        private void Close_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach(var item in ListHolder.Items) 
            {
                if (((ListBoxItem)item).IsAncestorOf((Button)sender))
                {
                    ListHolder.Items.Remove(item); 
                    return;
                }
            }
        }
    }
}
