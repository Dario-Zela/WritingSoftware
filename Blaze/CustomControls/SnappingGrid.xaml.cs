using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for SnappingGrid.xaml
    /// </summary>
    public partial class SnappingGrid : UserControl
    {
        public SnappingGrid()
        {
            InitializeComponent();

            //Define a resolution
            Resolution = 10;
            
            //To remove
            AddNewPannel();
            AddNewPannel();
        }

        //Stores the grid size
        public int Resolution { get; set; }

        //A clamping routine
        private double Clamp(double value, double max, double min)
        {
            return (value > max) ? max : (value < min) ? min : value;
        }

        //A rounding routine
        public Point Round(Point value, int resolution)
        {
            Point point = new Point();
            point.X = ((int)value.X) / resolution * resolution;
            point.Y = ((int)value.Y) / resolution * resolution;
            return point;
        }

        //Brings an element from the bottom of the container to the top
        private void BringToFront(UIElement element)
        {
            Container.Children.Remove(element);
            Container.Children.Add(element);
        }

        //To be changed
        //Adds a new pannel to the grid
        public void AddNewPannel()
        {
            //Adds a text pannel
            var pannel = new TextPannel();

            //Define default values
            pannel.Width = 250;
            pannel.Height = 250;
            pannel.Resolution = Resolution;
            pannel.HorizontalAlignment = HorizontalAlignment.Left;
            pannel.VerticalAlignment = VerticalAlignment.Top;
            
            //Movement method
            pannel.MovementStarted += (s, e) =>
            {
                //If the pannel starts moving, bring it to the front
                if (!pannel.AllowMovement)
                    BringToFront(pannel);

                //Get the position of the mouse and offset it to the edge of the pannel
                var position = Mouse.GetPosition(this);
                var offset = pannel.Move.TransformToAncestor(pannel).Transform(new Point(0, 0));

                //Offset it once again to the centre of the button while observing the resolution
                position.Offset(-offset.X, -offset.Y);
                position.Offset(-pannel.Move.Width / 2, -pannel.Move.Height / 2);
                position = Round(position, Resolution);

                position.X = Clamp(position.X, ActualWidth - pannel.Width, 0);
                position.Y = Clamp(position.Y, ActualHeight - pannel.Height, 0);

                //Change the marging of the pannel
                pannel.Margin = new Thickness(position.X, position.Y, 0, 0);

            };

            //Adds the pannel to the grid
            Container.Children.Add(pannel);
        }
    }
}
