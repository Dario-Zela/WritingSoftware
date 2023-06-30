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

        //Adds a new element
        public void AddNewElement()
        {
            ListHolder.Items.Add(new ListBoxItem());
        }

        //Show the delete button
        private void AllowClosing(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button)((Grid)sender).FindName("Close")).Visibility = Visibility.Visible;
        }

        //Hide the delete button
        private void StopClosing(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ((Button)((Grid)sender).FindName("Close")).Visibility = Visibility.Hidden;
        }

        //Deletes the current ListBoxItem
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in ListHolder.Items) 
            {
                //Delete the item in listHolder that holds the sender
                if (((ListBoxItem)item).IsAncestorOf((Button)sender))
                {
                    ListHolder.Items.Remove(item); 
                    return;
                }
            }
        }
    }
}
