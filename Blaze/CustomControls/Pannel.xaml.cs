using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for Pannel.xaml
    /// </summary>
    partial class Pannel : UserControl
    {
        public Pannel()
        {
            InitializeComponent();
            DataContext = this;
            Title = "Title";
        }

        public string Title { get; set; }

        public bool AllowMovement
        {
            get { return (bool)GetValue(AllowMovementProperty); }
            set { SetValue(AllowMovementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowMovement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowMovementProperty =
            DependencyProperty.Register("AllowMovement", typeof(bool), typeof(Pannel), new PropertyMetadata(false));

        public static readonly RoutedEvent MovementStartedEvent =
            EventManager.RegisterRoutedEvent("MovementStarted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pannel));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler MovementStarted
        {
            add { AddHandler(MovementStartedEvent, value); }
            remove { RemoveHandler(MovementStartedEvent, value); }
        }

        private void RaiseMovementEvent()
        {
            // Create a RoutedEventArgs instance.
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(MovementStartedEvent);

            // Raise the event, which will bubble up through the element tree.
            RaiseEvent(routedEventArgs);
        }

        private void AllowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Visible;
        }

        private void DisallowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Hidden;
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RaiseMovementEvent();
            AllowMovement = true;
        }

        private void Move_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RaiseMovementEvent();
            AllowMovement = false;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (AllowMovement)
            {
                RaiseMovementEvent();
            }
        }
    }

    public class TextPannel : Pannel
    {
        public TextPannel()
            : base()
        {
            var richText = new RichTextBox() { Margin = new Thickness(10, 0, 10, 5), BorderThickness = new Thickness(0) };
            Container.Children.Add(richText);
            Grid.SetRow(richText, 1);
        }
    }

}
