using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Point point = new Point
            {
                X = ((int)value.X) / resolution * resolution,
                Y = ((int)value.Y) / resolution * resolution
            };
            return point;
        }

        //Brings an element from the bottom of the container to the top
        private void BringToFront(UIElement element)
        {
            Container.Children.Remove(element);
            Container.Children.Add(element);
        }

        private Thickness AddThickness(Thickness thickness1, Thickness thickness2)
        {
            return new Thickness(thickness1.Left + thickness2.Left, thickness1.Top + thickness2.Top,
                                 thickness1.Right + thickness2.Right, thickness1.Bottom + thickness2.Bottom);
        }

        //To be changed
        //Adds a new pannel to the grid
        public TextPannel AddNewPannel()
        {
            //Adds a text pannel
            var pannel = new TextPannel
            {
                //Define default values
                Width = 250,
                Height = 250,
                Resolution = Resolution,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

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

            pannel.DeletePannel += (s, e) =>
            {
                Container.Children.Remove(pannel);
            };

            pannel.DuplicatePannel += (s, e) =>
            {
                var duplicate = AddNewPannel();
                duplicate.Title = pannel.Title;
                duplicate.Resolution = pannel.Resolution;
                duplicate.Margin = AddThickness(pannel.Margin, new Thickness(10, 10, 0, 0));
                duplicate.Height = pannel.Height;
                duplicate.Width = pannel.Width;

                duplicate.CopyFrom(pannel);
            };

            //Adds the pannel to the grid
            Container.Children.Add(pannel);

            return pannel;
        }
    }
}
