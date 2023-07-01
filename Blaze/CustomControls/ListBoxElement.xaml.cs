using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Blaze.CustomControls
{
    public class ListElement
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Interaction logic for ListBoxElement.xaml
    /// </summary>
    public partial class ListBoxElement : UserControl
    {
        public ObservableCollection<ListElement> content;

        public ListBoxElement()
        {
            InitializeComponent();
            DataContext = this;
            content = new ObservableCollection<ListElement>();

            ListHolder.DataContext = content;
            ListHolder.ItemsSource = content;
        }


        //Adds a new element
        public void AddNewElement()
        {
            ListElement element = new ListElement();
            content.Add(element);
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
            content.Remove((ListElement)((Button)sender).DataContext);
        }

        public void Load(ListElement[] list)
        {
            content.Clear();
            foreach (ListElement element in list)
            {
                content.Add(element);
            }
        }

        public ListElement[] GetElements()
        {
            ListElement[] elements = new ListElement[content.Count];
            content.CopyTo(elements, 0);
            return elements;
        }
    }
}
