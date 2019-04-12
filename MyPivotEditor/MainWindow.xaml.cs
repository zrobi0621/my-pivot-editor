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

namespace MyPivotEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Tool { Line, Circle}
        private Tool currentTool;

        Point start;
        Point end;

        public MainWindow()
        {
            InitializeComponent();

            //Defaul tool is Line
            LineButton.IsChecked = true;
            currentTool = Tool.Line;
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tool.Line;
        }

        private void CircleButton_Click(object sender, RoutedEventArgs e)
        {
            currentTool = Tool.Circle;
        }

        private void MainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            start = e.GetPosition(this);
        }

        private void MainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (currentTool)
            {
                case Tool.Line:
                    DrawLine();
                    break;
                case Tool.Circle:
                    DrawCircle();
                    break;
                default:
                    break;
            }
        }

        private void DrawCircle()
        {
            Ellipse newEllipse = new Ellipse
            {
                Stroke = Brushes.Green,
                Fill = Brushes.Red,
                StrokeThickness = 3,
                Height = 10,
                Width = 10
            };

            if (end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.Width = end.X - start.X;
            }
            else
            {
                newEllipse.SetValue(Canvas.LeftProperty, end.X);
                newEllipse.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newEllipse.SetValue(Canvas.TopProperty, start.Y);
                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y);
                newEllipse.Height = start.Y - end.Y;
            }
            MainCanvas.Children.Add(newEllipse);
        }

        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
            }
        }

        private void DrawLine()
        {
            Line newLine = new Line()
            {
                Stroke = Brushes.Blue,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };
            MainCanvas.Children.Add(newLine);

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
        }
    }
}
