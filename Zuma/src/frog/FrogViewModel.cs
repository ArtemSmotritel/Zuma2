using System.Geometry;
using System.Windows;
using System.Windows.Media;
using Zuma.src.balls;
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

        public ImageBrush Sprite => new ImageBrush(frog.Sprite);
        public PlayerBall CurrentBall => frog.CurrentBall;
        public PlayerBall NextBall => frog.NextBall;
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

        public PlayerBall PrepareCurrentBallForShooting(Point mouseCoordinates)
        {
            Bezier bezierForBall = GeometryCalculator.GetBezierPathFromAInDirectionOfB(Coordinates, mouseCoordinates);
            CurrentBall.SetPath(new Zuma.models.Path(bezierForBall));

            return CurrentBall;
        }
    }
}
