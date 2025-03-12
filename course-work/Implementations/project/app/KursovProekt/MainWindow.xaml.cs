using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VectorGraphicsEditor
{
    public partial class MainWindow : Window
    {
        private Color _currentColor = Colors.Black;
        private double _currentThickness = 3;
        private bool _isDrawing = false;
        private Point _startPoint;
        private Shape? _currentShape;

        public MainWindow()
        {
            InitializeComponent();
            DrawingCanvas.MouseDown += StartDrawing;
            DrawingCanvas.MouseMove += DrawShape;
            DrawingCanvas.MouseUp += EndDrawing;
        }

        private void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            _startPoint = e.GetPosition(DrawingCanvas);

            _currentShape = new Line
            {
                Stroke = new SolidColorBrush(_currentColor),
                StrokeThickness = _currentThickness,
                X1 = _startPoint.X,
                Y1 = _startPoint.Y,
                X2 = _startPoint.X,
                Y2 = _startPoint.Y
            };

            DrawingCanvas.Children.Add(_currentShape);
        }

        private void DrawShape(object sender, MouseEventArgs e)
        {
            if (!_isDrawing || _currentShape == null) return;

            Point endPoint = e.GetPosition(DrawingCanvas);

            if (_currentShape is Line line)
            {
                line.X2 = endPoint.X;
                line.Y2 = endPoint.Y;
            }
        }

        private void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = false;
        }

        private void ColorPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorPicker.SelectedItem is ComboBoxItem selectedItem)
            {
                string colorName = selectedItem.Tag.ToString();
                _currentColor = (Color)ColorConverter.ConvertFromString(colorName);
            }
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _currentThickness = e.NewValue;
        }
    }
}
