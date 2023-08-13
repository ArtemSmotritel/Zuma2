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
        private readonly Random random = new Random();

        private readonly Level level;
        private readonly Canvas levelCanvas;

        public Path Path => level.Path;

        public FrogControl FrogControl { get; set; }
        public FrogViewModel FrogViewModel => new FrogViewModel(level.Frog);
        public Point FrogCoordinates => FrogViewModel.Coordinates;

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

        public void RotateFrog(Point mouseCoordinates)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            FrogControl.SetRotationAngle(Utils.AddAngels(angel, 80));
            Name = $"X = {mouseCoordinates.X};\t Y = {mouseCoordinates.Y}";
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

        private float t = 0;
        public void DrawPath(object sender, EventArgs e)
        {
            if (!Path.HasReachedDestination(t))
            {
                Point point = Path.GetPosition(t);
                DrawPathPoint(point, Brushes.Red);
                t += 0.03f;
            }
        }

        private void DrawPathPoint(Point p, Brush brush)
        {
            var p1C = new System.Windows.Shapes.Rectangle
            {
                Height = 3,
                Width = 3,
                Fill = brush,
                Stroke = brush,
            };

            Canvas.SetLeft(p1C, p.X);
            Canvas.SetTop(p1C, p.Y);

            levelCanvas.Children.Add(p1C);
        }

        private Brush GetRandomColor()
        {
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);

            var randomColor = Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
            return new SolidColorBrush(randomColor);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
