using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blaze.CustomControls
{
    /// <summary>
    /// Interaction logic for StatisticsElement.xaml
    /// </summary>
    public partial class StatisticsElement : UserControl
    {
        public StatisticsElement()
        {
            InitializeComponent();
        }

        //Adds a new element
        public void AddNewElement()
        {
            ListHolder.Items.Add(new ListBoxItem());
        }

        Grid CurrentlyActiveStatistics;

        //Show the extra button
        private void AllowExtra(object sender, MouseEventArgs e)
        {
            CurrentlyActiveStatistics = (Grid)sender;

            ((ToggleButton)(CurrentlyActiveStatistics).FindName("Extra")).Visibility = Visibility.Visible;

        }

        //Hide the extra button
        private void StopExtra(object sender, MouseEventArgs e)
        {
            ((ToggleButton)(CurrentlyActiveStatistics).FindName("Extra")).Visibility = Visibility.Hidden;
        }

        //Toggle the visibility of the description textbox
        private void ToggleDescription(object sender, RoutedEventArgs e)
        {
            TextBox textBox = ((TextBox)CurrentlyActiveStatistics.FindName("Description"));

            switch (textBox.Visibility)
            {
                case Visibility.Visible:
                    textBox.Visibility = Visibility.Collapsed;
                    Extentions.Header.SetProperty(((Button)sender), "Show Description");
                    break;

                case Visibility.Collapsed:
                    textBox.Visibility = Visibility.Visible;
                    Extentions.Header.SetProperty(((Button)sender), "Hide Description");
                    break;
            }
        }

        //Toggle the visibility of the units textbox
        private void ToggleUnits(object sender, RoutedEventArgs e)
        {
            TextBox textBox = ((TextBox)CurrentlyActiveStatistics.FindName("Units"));

            switch (textBox.Visibility)
            {
                case Visibility.Visible:
                    textBox.Visibility = Visibility.Collapsed;
                    Extentions.Header.SetProperty(((Button)sender), "Show Units");
                    break;

                case Visibility.Collapsed:
                    textBox.Visibility = Visibility.Visible;
                    Extentions.Header.SetProperty(((Button)sender), "Hide Units");
                    break;
            }
        }

        //Delete the current list item
        private void Delete(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListHolder.Items)
            {
                //Delete the item in listHolder that holds the sender
                if (((ListBoxItem)item).IsAncestorOf(CurrentlyActiveStatistics))
                {
                    ListHolder.Items.Remove(item);
                    return;
                }
            }
        }
    }
}
