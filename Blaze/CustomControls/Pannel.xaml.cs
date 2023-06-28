using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Blaze.Extentions;

namespace Blaze.CustomControls
{
    /// <summary>
    /// Base class for all pannel types
    /// </summary>
    partial class Pannel : UserControl
    {
        public Pannel()
        {
            InitializeComponent();
            DataContext = this;
            //Initialise title to make sure an initial value always exists
            Title = "Title";
        }

        //Text on top of the pannel
        public string Title { get; set; }
        //Resolution of the grid
        public int Resolution { get; set; }

        private const int MIN_HEIGHT = 150;
        private const int MIN_WIDTH = 150;



        //Current movement state of the pannel
        public bool AllowMovement
        {
            get { return (bool)GetValue(AllowMovementProperty); }
            set { SetValue(AllowMovementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowMovement.  
        public static readonly DependencyProperty AllowMovementProperty =
            DependencyProperty.Register("AllowMovement", typeof(bool), typeof(Pannel), new PropertyMetadata(false));



        //Current resizing state of the pannel
        public bool AllowResize
        {
            get { return (bool)GetValue(AllowResizeProperty); }
            set { SetValue(AllowResizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowResize. 
        public static readonly DependencyProperty AllowResizeProperty =
            DependencyProperty.Register("AllowResize", typeof(bool), typeof(Pannel), new PropertyMetadata(false));




        //Event handler for starting and stopping the movement of the pannel
        public static readonly RoutedEvent MovementStartedEvent =
            EventManager.RegisterRoutedEvent("MovementStarted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pannel));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler MovementStarted
        {
            add { AddHandler(MovementStartedEvent, value); }
            remove { RemoveHandler(MovementStartedEvent, value); }
        }

        //Raises the movement event flag
        private void RaiseMovementEvent()
        {
            // Create a RoutedEventArgs instance.
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(MovementStartedEvent);

            // Raise the event, which will bubble up through the element tree.
            RaiseEvent(routedEventArgs);
        }



        //Change the visibility of the move button on the pannel


        private void AllowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Visible;
        }

        private void DisallowMoving(object sender, MouseEventArgs e)
        {
            Move.Visibility = Visibility.Hidden;
        }


        //Mouse conditions

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //If button pressed allow movement
            RaiseMovementEvent();
            AllowMovement = true;
        }

        private void Move_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //If button released, stop movement
            RaiseMovementEvent();
            AllowMovement = false;
        }



        //A rounding routine
        public double Round(double value, int resolution)
        {
            return ((int)value) / resolution * resolution;
        }



        //Mouse movement
        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            //If the pannel is moving
            if (AllowMovement)
            {
                //Let the container know
                RaiseMovementEvent();
            }
            //If pannel is resizing
            if (AllowResize)
            {
                //Change the size of the pannel with a minimum size
                Vector drag = e.GetPosition(this) - Resize.TransformToAncestor(this).Transform(new Point(0, 0));
                drag -= new Vector(Resize.Width / 2, Resize.Height / 2);
                SetValue(HeightProperty, Round(Math.Max(ActualHeight + drag.Y, MIN_HEIGHT), Resolution));
                SetValue(WidthProperty, Round(Math.Max(ActualWidth + drag.X, MIN_WIDTH), Resolution));
            }
        }



        //If the mouse presses on the resize button
        private void Resize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Allow resize
            AllowResize = true;
            //Sanity check the height and width proprieties
            SetValue(HeightProperty, ActualHeight);
            SetValue(WidthProperty, ActualWidth);
        }

        //If the mouse is released from the resize button
        private void Resize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllowResize = false;
        }
    }

    //Pannel with a textbox
    public class TextPannel : Pannel
    {
        public TextPannel()
            : base()
        {
            //Add a textbox in slot
            var richText = new RichTextBox() { Margin = new Thickness(10, 0, 10, 5), BorderThickness = new Thickness(0) };
            Container.Children.Add(richText);
            Grid.SetRow(richText, 1);
        }
    }

    //Pannel with a textbox
    public class ListPannel : Pannel
    {
        ListBoxElement ListHolder;

        public ListPannel()
            : base()
        {
            //Add a textbox in slot
            ListHolder = new ListBoxElement()
            {
                Margin = new Thickness(10, 0, 10, 5),
                BorderThickness = new Thickness(0),
            };
            Container.Children.Add(ListHolder);
            Grid.SetRow(ListHolder, 1);

            ListHolder.AddNewElement();
            ListHolder.AddNewElement();

            //Add Add Button
            var addButton = new Button()
            {
                Width = 15,
                Height = 15,
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                Style = Application.Current.FindResource("ButtonWithIcon2") as Style,
            };

            addButton.Click += new RoutedEventHandler((s, e) => AddListItem());

            Coolicons.SetProperty(addButton, MahApps.Metro.IconPacks.PackIconCooliconsKind.Plus);
            IsMouseOverColor.SetProperty(addButton, Brushes.DarkGray);
            IsPressedColor.SetProperty(addButton, Brushes.Gray);
            IconHeight.SetProperty(addButton, 8);


            Container.Children.Add(addButton);
            Grid.SetRow(addButton, 1);
            Panel.SetZIndex(addButton, 1);
        }

        public void AddListItem()
        {
            ListHolder.AddNewElement();
        }
    }
}
