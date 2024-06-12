using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Blaze.CustomControls
{
    public class Statistic
    {
        public string Name { get; set; }
        private double _value;
        public string Value
        {
            get => _value.ToString();
            set
            {
                //Add's it if it is a valid number and raises the event
                if (double.TryParse(value, out double num))
                {
                    _value = num;
                }
            }
        }
        public string Units { get; set; }
        public string Description { get; set; }
    }


    /// <summary>
    /// Interaction logic for StatisticsElement.xaml
    /// </summary>
    public partial class StatisticsElement : UserControl
    {
        ObservableCollection<Statistic> Statistics;

        public StatisticsElement()
        {
            InitializeComponent();
            Statistics = new ObservableCollection<Statistic>();
            DataContext = this;

            ListHolder.DataContext = Statistics;
            ListHolder.ItemsSource = Statistics;
        }

        //Adds a new element
        public void AddNewElement()
        {
            Statistics.Add(new Statistic());
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

            ((ToggleButton)(CurrentlyActiveStatistics).FindName("Extra")).IsChecked = false;
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

            ((ToggleButton)(CurrentlyActiveStatistics).FindName("Extra")).IsChecked = false;
        }

        //Delete the current list item
        private void Delete(object sender, RoutedEventArgs e)
        {
            Statistics.Remove((Statistic)((Button)sender).DataContext);
            ((ToggleButton)(CurrentlyActiveStatistics).FindName("Extra")).IsChecked = false;
        }

        public void Load(Statistic[] list)
        {
            Statistics.Clear();
            foreach (Statistic element in list)
            {
                Statistics.Add(element);
            }
        }

        public Statistic[] GetElements()
        {
            Statistic[] elements = new Statistic[Statistics.Count];
            Statistics.CopyTo(elements, 0);
            return elements;
        }
    }
}
