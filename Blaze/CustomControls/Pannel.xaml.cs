using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Blaze.Extentions;
using ICSharpCode.AvalonEdit.Highlighting;

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

        private const int MIN_HEIGHT = 250;
        private const int MIN_WIDTH = 250;

        //Current movement state of the pannel
        public bool AllowMovement
        {
            get { return (bool)GetValue(AllowMovementProperty); }
            set { SetValue(AllowMovementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowMovement.  
        public static readonly DependencyProperty AllowMovementProperty =
            DependencyProperty.Register("AllowMovement", typeof(bool), typeof(Pannel), new PropertyMetadata(false));


        //Event handler for deleting a pannel
        public static readonly RoutedEvent DeletePannelEvent =
            EventManager.RegisterRoutedEvent("DeletePannel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pannel));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler DeletePannel
        {
            add { AddHandler(DeletePannelEvent, value); }
            remove { RemoveHandler(DeletePannelEvent, value); }
        }

        //Event handler for duplicating a pannel
        public static readonly RoutedEvent DuplicatePannelEvent =
            EventManager.RegisterRoutedEvent("DuplicatePannel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Pannel));

        // Provide CLR accessors for assigning an event handler.
        public event RoutedEventHandler DuplicatePannel
        {
            add { AddHandler(DuplicatePannelEvent, value); }
            remove { RemoveHandler(DuplicatePannelEvent, value); }
        }


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

        private void DeletePannel_Click(object sender, RoutedEventArgs e)
        {
            Extra.IsChecked = false;
            RaiseEvent(new RoutedEventArgs(DeletePannelEvent));
        }

        private void DuplicatePannel_Click(object sender, RoutedEventArgs e)
        {
            Extra.IsChecked = false;
            RaiseEvent(new RoutedEventArgs(DuplicatePannelEvent));
        }

        //
        public virtual void CopyFrom(Pannel pannel) 
        {
            if (GetType() != pannel.GetType()) return;
        }
    }

    //Pannel with a textbox
    public class TextPannel : Pannel
    {
        RichTextBox richText;
        public TextPannel()
            : base()
        {
            //Add a textbox in slot
            richText = new RichTextBox() { Margin = new Thickness(10, 0, 10, 5), BorderThickness = new Thickness(0) };
            Container.Children.Add(richText);
            Grid.SetRow(richText, 1);
        }


        public override void CopyFrom(Pannel pannel)
        {
            base.CopyFrom(pannel);
            richText.Load(((TextPannel)pannel).richText.GetBlocks());
        }
    }

    //Pannel with a list
    public class ListPannel : Pannel
    {
        //Reference to the listbox
        ListBoxElement ListHolder;

        public ListPannel()
            : base()
        {
            //Add a listbox in the slot
            ListHolder = new ListBoxElement()
            {
                Margin = new Thickness(10, 0, 10, 5),
                BorderThickness = new Thickness(0),
            };
            Container.Children.Add(ListHolder);
            Grid.SetRow(ListHolder, 1);

            ListHolder.AddNewElement();
            ListHolder.AddNewElement();

            //Add the Add Button
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

            Coolicons.SetProperty(addButton, MahApps.Metro.IconPacks.PackIconCooliconsKind.Plus);
            IsMouseOverColor.SetProperty(addButton, Brushes.DarkGray);
            IsPressedColor.SetProperty(addButton, Brushes.Gray);
            IconHeight.SetProperty(addButton, 8);

            //Add click event
            addButton.Click += new RoutedEventHandler((s, e) => AddListItem());

            //Add to container
            Container.Children.Add(addButton);
            Grid.SetRow(addButton, 1);
            Panel.SetZIndex(addButton, 1);
        }

        //Add a list item
        public void AddListItem()
        {
            ListHolder.AddNewElement();
        }

        public override void CopyFrom(Pannel pannel)
        {
            base.CopyFrom(pannel);
            ListHolder.Load(((ListPannel)pannel).ListHolder.GetElements());
        }
    }

    //Pannel with a list
    public class StatisticsPannel : Pannel
    {
        //Reference to the listbox
        StatisticsElement ListHolder;

        public StatisticsPannel()
            : base()
        {
            //Add a listbox in the slot
            ListHolder = new StatisticsElement()
            {
                Margin = new Thickness(10, 0, 10, 5),
                BorderThickness = new Thickness(0),
            };
            Container.Children.Add(ListHolder);
            Grid.SetRow(ListHolder, 1);

            ListHolder.AddNewElement();
            ListHolder.AddNewElement();

            //Add the Add Button
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

            Coolicons.SetProperty(addButton, MahApps.Metro.IconPacks.PackIconCooliconsKind.Plus);
            IsMouseOverColor.SetProperty(addButton, Brushes.DarkGray);
            IsPressedColor.SetProperty(addButton, Brushes.Gray);
            IconHeight.SetProperty(addButton, 8);

            //Add click event
            addButton.Click += new RoutedEventHandler((s, e) => AddListItem());

            //Add to container
            Container.Children.Add(addButton);
            Grid.SetRow(addButton, 1);
            Panel.SetZIndex(addButton, 1);
        }

        //Add a list item
        public void AddListItem()
        {
            ListHolder.AddNewElement();
        }

        public override void CopyFrom(Pannel pannel)
        {
            base.CopyFrom(pannel);
            ListHolder.Load(((StatisticsPannel)pannel).ListHolder.GetElements());
        }
    }

    //Pannel with a list
    public class TablePannel : Pannel
    {
        //Reference to the table
        TableElement table;

        //Reference to the two add buttons
        Button addColumn;
        Button addRow;

        public TablePannel()
            : base()
        {
            //Add a table in the slot
            table = new TableElement()
            {
                Margin = new Thickness(10, 0, 10, 5),
            };

            Container.Children.Add(table);
            Grid.SetRow(table, 1);

            //Add the Add Column Button
            addColumn = new Button()
            {
                Width = 25,
                Height = 25,
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                Style = Application.Current.FindResource("ButtonWithIcon2") as Style,
                Name = "AddColumn"
            };

            Coolicons.SetProperty(addColumn, MahApps.Metro.IconPacks.PackIconCooliconsKind.AddColumn);
            IsMouseOverColor.SetProperty(addColumn, Brushes.DarkGray);
            IsPressedColor.SetProperty(addColumn, Brushes.Gray);
            IconHeight.SetProperty(addColumn, 10);

            //Add click event
            addColumn.Click += new RoutedEventHandler((s, e) => AddColumn());

            //Add to container
            Container.Children.Add(addColumn);
            Grid.SetRow(addColumn, 1);
            Panel.SetZIndex(addColumn, 1);

            //Add the Add Row Button
            addRow = new Button()
            {
                Width = 25,
                Height = 25,
                Cursor = Cursors.Hand,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                Style = Application.Current.FindResource("ButtonWithIcon2") as Style,
                Margin = new Thickness(20, 0, 0, 0),
                Name = "AddRow"
            };

            Coolicons.SetProperty(addRow, MahApps.Metro.IconPacks.PackIconCooliconsKind.AddRow);
            IsMouseOverColor.SetProperty(addRow, Brushes.DarkGray);
            IsPressedColor.SetProperty(addRow, Brushes.Gray);
            IconHeight.SetProperty(addRow, 10);

            //Add click event
            addRow.Click += new RoutedEventHandler((s, e) => AddRow());

            //Add to container
            Container.Children.Add(addRow);
            Grid.SetRow(addRow, 1);
            Panel.SetZIndex(addRow, 1);

            //Add the Table Settings Button
            var settingsTable = new Button()
            {
                Style = Application.Current.FindResource("MenuButton") as Style,
            };
            Header.SetProperty(settingsTable, "Edit Rows & Columns");

            //Add click event
            settingsTable.Click += new RoutedEventHandler((s, e) => OpenSettingsMenu());

            //Add to popup menu
            ExtraMenu.Children.Insert(1, settingsTable);

            //Change the visibility of the buttons in pair with the table settings
            table.TableSettings.IsVisibleChanged += ToggleButtons;
        }

        //Toggles the visibility of the buttons
        private void ToggleButtons(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Switch on the button visibility
            switch(addColumn.Visibility)
            {
                case Visibility.Hidden:
                    addColumn.Visibility = Visibility.Visible;
                    addRow.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                    addColumn.Visibility = Visibility.Hidden;
                    addRow.Visibility = Visibility.Hidden;
                    break;
            }
        }

        //Add a column
        public void AddColumn()
        {
            table.AddColumn();
        }

        //Add a row
        public void AddRow()
        {
            table.AddRow();
        }

        //Opens the Settings menu
        public void OpenSettingsMenu()
        {
            Extra.IsChecked = false;
            table.OpenSettingsMenu();
        }

        //Override copyFrom
        public override void CopyFrom(Pannel pannel)
        {
            base.CopyFrom(pannel);
            table.Load(((TablePannel)pannel).table.data.Copy());
            table.Cancel();
        }
    }
}
