using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaintApp
{
    /*
     * Ramy Huffman
     * Paint Application
     */

    // Main Window class that creates Canvas and Draws Shapes onto the Canvas
    public partial class MainWindow : Window
    {

        // Enum that selects the draws the appropriate shape when selected
        private enum myShape
        {
            Line,
            Ellipse,
            Rectangle
        }

        // Defaults to black line drawn
        private myShape shapeSelect = myShape.Line;
        private byte redColor = 0;
        private byte blueColor = 0;
        private byte greenColor = 0;

        // Declare Coordinates (double) of Starting and Ending Points when mouse is clicked
        private Point clickStart;
        private Point clickEnd;

        // Initialize the Main Window Form
        public MainWindow()
        {
            InitializeComponent();
        }

        // When Line button is pressed, set shape to Line
        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelect = myShape.Line;
        }

        // When Ellipse button is pressed, set shape to Ellipse
        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelect = myShape.Ellipse;
        }

        // When Rectangle button is pressed, set shape to Rectnagle
        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            shapeSelect = myShape.Rectangle;
        }

        // Get value from the Red Slider and change get corresponding R value
        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            redColor = (byte)RedSlider.Value;
        }

        // Get value from the Green Slider and change get corresponding G value
        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            greenColor = (byte)GreenSlider.Value;
        }

        // Get value from the Blue Slider and change get corresponding B value
        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            blueColor = (byte)BlueSlider.Value;
        }

        // When mouse pressed down, get starting coordinate
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickStart = e.GetPosition(this);
        }

        // As mouse is moving, update ending coordinate
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                clickEnd = e.GetPosition(this);
            }
        }

        // When mouse release...
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Shape obj = null;                       // Create an object from Shape class
            Shapes ss = new Shapes();               // Create new instance of Shapes class
            
            // Depending on what shape is selected, use DYNAMIC BONDING and POLYMORPHISM to call method
            // from Shapes called "drawColoredShape" that takes one type, starting coordinates, ending coordinates,
            // and the corresponding color code using RGB sliders.
            switch (shapeSelect)
            {
                case myShape.Line:
                     obj = ss.drawColoredShape("Line", clickStart.X, clickStart.Y, clickEnd.X,clickEnd.Y,
                                               redColor, greenColor, blueColor);
                    break;
                case myShape.Ellipse:
                    obj = ss.drawColoredShape("Ellipse", clickStart.X, clickStart.Y, clickEnd.X, clickEnd.Y,
                          redColor, greenColor, blueColor);
                    break;
                case myShape.Rectangle:
                    obj = ss.drawColoredShape("Rectangle", clickStart.X, clickStart.Y, clickEnd.X, clickEnd.Y,
                          redColor, greenColor, blueColor);
                    break;
                default:
                    break;
            }

            MyCanvas.Children.Add(obj);             // Add object to the Canvas in the WPF
        }
    }

    // Shapes class that takes inputs and sets properties for the rendered shape
    public class Shapes 
    {
        // Universal method shared to be applied whatever shape is selected
        public Shape drawColoredShape(String s, double x1, double y1, double x2, double y2, byte r, byte g, byte b)
        {
            Shape renderedShape = null;            // Create an empty object of Shape type

            // Create a custom color to be applied based on values from RGB Sliders
            Color customColor = Color.FromRgb(r, g, b);
            SolidColorBrush mySCB = new SolidColorBrush(customColor);

            // Depending shape selected, set the empty Shape object to the appropriate class using dynamic bonding
            // and set appropriate attributes based on inputs
            if (s == "Line")
            {
                renderedShape = new Line()
                {
                    Stroke = mySCB,
                    X1 = x1,
                    Y1 = y1 - 50,
                    X2 = x2,
                    Y2 = y2 - 50
                };
            } 
            else if (s == "Ellipse")
            {
                renderedShape = new Ellipse()
                {
                    Stroke = mySCB,
                    StrokeThickness = 1
                };
                // Width Dimensions
                if (x2 >= x1)
                {
                    renderedShape.SetValue(Canvas.LeftProperty, x1);
                    renderedShape.Width = x2 - x1;
                }
                else
                {
                    renderedShape.SetValue(Canvas.LeftProperty, x2);
                    renderedShape.Width = x1 - x2;
                }

                // Height Dimensions
                if (y2 >= y1)
                {
                    renderedShape.SetValue(Canvas.TopProperty, y1 - 50);
                    renderedShape.Height = y2 - y1;
                }
                else
                {
                    renderedShape.SetValue(Canvas.TopProperty, y2 - 50);
                    renderedShape.Height = y1 - y2;
                }
            } 
            else if (s == "Rectangle")
            {
                renderedShape = new Rectangle()
                {
                    Stroke = mySCB,
                    StrokeThickness = 1
                };
                // Width Dimensions
                if (x2 >= x1)
                {
                    renderedShape.SetValue(Canvas.LeftProperty, x1);
                    renderedShape.Width = x2 - x1;
                }
                else
                {
                    renderedShape.SetValue(Canvas.LeftProperty, x2);
                    renderedShape.Width = x1 - x2;
                }

                // Height Dimensions
                if (y2 >= y1)
                {
                    renderedShape.SetValue(Canvas.TopProperty, y1 - 50);
                    renderedShape.Height = y2 - y1;
                }
                else
                {
                    renderedShape.SetValue(Canvas.TopProperty, y2 - 50);
                    renderedShape.Height = y1 - y2;
                }
            }

            return renderedShape;                 // Return the rendered shape with appropriate shape properties
        }

    }
}
