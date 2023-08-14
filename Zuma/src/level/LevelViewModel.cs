using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.balls.ball;
using Zuma.src.frog;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelViewModel : INotifyPropertyChanged
    {
        private readonly Level level;
        private readonly Canvas levelCanvas;

        public Path Path => level.Path;
        public Point FrogCoordinates => level.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(level.Background);

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public LevelViewModel(Level level, Canvas levelCanvas)
        {
            this.level = level;
            this.levelCanvas = levelCanvas;
            Name = $"Level {level.Number}: {level.Name}";
        }

        public void RotateFrog(Point mouseCoordinates, FrogControl frogControl)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogControl.SetRotationAngle(Utils.AddAngels(angel, 80));
            Name = $"X = {mouseCoordinates.X};\t pathDrawingT Y = {mouseCoordinates.Y}";
        }

        public void ShootBall(Point mouseCoordinates)
        {
            var ballStartingPoint = new Point(FrogCoordinates.X + 50, FrogCoordinates.Y + 50);

            var ballSprite = new BitmapImage(new System.Uri("pack://application:,,,/resources/images/balls/blue_ball_1.png"));
            var ball = new Ball(ballSprite, ballStartingPoint);
            var ballControl = new BallControl(new BallViewModel(ball));

            Canvas.SetLeft(ballControl, ballStartingPoint.X);
            Canvas.SetTop(ballControl, ballStartingPoint.Y);

            levelCanvas.Children.Add(ballControl);
            level.LevelTicker.Tick += DrawPath;
            level.LevelTicker.Start();
        }

        private float pathDrawingT = 0;
        public void DrawPath(object sender, EventArgs e)
        {
            if (pathDrawingT == 0)
            {
                Point point = Path.Start;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.Red, 30, 30);
            }

            if (!Path.HasReachedDestination(pathDrawingT))
            {
                Point point = Path.GetPosition(pathDrawingT);
                DrawPathPoint(point, Brushes.DimGray);
                pathDrawingT += 0.02f;
            }
            else
            {
                Point point = Path.End;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.Red, 30, 30);
            }
        }
        private void DrawPathPoint(Point p, Brush brush, int heigh = 15, int width = 15)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Height = heigh,
                Width = width,
                Fill = brush,
                Stroke = brush,
            };

            Canvas.SetLeft(rect, p.X);
            Canvas.SetTop(rect, p.Y);

            levelCanvas.Children.Add(rect);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
