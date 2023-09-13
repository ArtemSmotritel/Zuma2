using System.Geometry;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zuma.src.balls.player_balls;
using Zuma.src.helpers;
using Zuma.src.utils;

namespace Zuma.src.frog
{
    public class FrogViewModel : Notifier
    {
        private readonly Frog frog;

        public bool IsPaused { get; set; }
        public int Width => 100;
        public int Height => 100;

        public int RotationCenterX => Width / 2;
        public int RotationCenterY => Height / 2;
        private double rotationAngel = 0;
        public double RotationAngel
        {
            get => rotationAngel;
            set
            {
                rotationAngel = value;
                OnPropertyChanged();
            }
        }

        public Rectangle FrogRectangle { get; set; }

        public BitmapImage Sprite => frog.Sprite;
        public AbstractPlayerBall CurrentBall => frog.CurrentBall;
        public AbstractPlayerBall NextBall => frog.NextBall;
        public Point Coordinates => frog.Coordinates;

        public FrogViewModel(Frog frog)
        {
            this.frog = frog;
        }

        public void HandleShot(Point mouseCoordinates)
        {
            frog.SwapBall();
            OnPropertyChanged(nameof(CurrentBall));
            frog.NextBall = BallGenerator.GeneratePlayerBall();
            OnPropertyChanged(nameof(NextBall));
        }

        public AbstractPlayerBall PrepareCurrentBallForShooting(Point start, Point mouseCoordinates)
        {
            Bezier bezierForBall = GeometryCalculator.GetBezierPathFromAInDirectionOfB(start, mouseCoordinates);
            CurrentBall.SetPath(new Zuma.models.Path(bezierForBall));

            return CurrentBall;
        }
    }
}
