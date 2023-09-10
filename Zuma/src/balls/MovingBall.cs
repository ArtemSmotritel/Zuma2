using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.helpers;

namespace Zuma.src.balls
{
    public abstract class MovingBall
    {
        public abstract float GetNormalSpeed();
        public abstract float GetNormalRotationSpeed();
        protected abstract UIElement CreateView(BallColor color);

        private static int _ID = 0;
        protected static int ID
        {
            get
            {
                _ID++;
                return _ID;
            }
        }

        public readonly int id;
        public BitmapImage Sprite { get; protected set; }
        public UIElement View { get; protected set; }
        public int Height { get; protected set; }
        public int Width { get; protected set; }
        public readonly float halfHeight;
        public readonly float halfWidth;
        public readonly float collisionWidth;
        public BallColor color;
        protected Path path;

        public Point Coordinates { get; protected set; }
        protected float PathTime { get; set; }
        public float RotationAngel { get; protected set; }
        public bool IsFrozen { get; set; }
        public bool IsDisposed { get; protected set; }

        public MovingBall(Path path, BallColor color)
        {
            id = ID;

            IsFrozen = false;
            Coordinates = path.Start;
            this.path = path;
            PathTime = 0;
            this.color = color;

            View = CreateView(color);

            halfHeight = Height / 2f;
            halfWidth = Width / 2f;
            collisionWidth = Width - ( Width / 15f );
        }

        public void Move(float speed, float rotationSpeed)
        {
            if (IsFrozen)
            {
                return;
            }

            PathTime += speed;
            Coordinates = path.GetPosition(PathTime);
            Canvas.SetLeft(View, Coordinates.X);
            Canvas.SetTop(View, Coordinates.Y);

            Rotate(rotationSpeed);
        }

        public void Rotate(float rotationSpeed)
        {
            RotationAngel = Utils.AddAngels(RotationAngel, rotationSpeed);
            View.RenderTransform = new RotateTransform(RotationAngel, halfWidth, halfHeight);
        }

        public bool HasReachedDestination(float speed) => path.HasReachedDestination(PathTime + speed);

        public override string ToString() => $"[{id}] {( IsFrozen ? "FROZEN" : "" )} {Color()} ball at {Coordinates}";

        private string Color()
        {
            switch (color)
            {
                case BallColor.BLUE:
                    return "Blue";
                case BallColor.GREEN:
                    return "Green";
                case BallColor.YELLOW:
                    return "Yellow";
                case BallColor.PURPLE:
                    return "Purple";
                default:
                    throw new ArgumentException();
            }
        }

        protected static Uri GetSpritePath(BallColor ballColor)
        {
            switch (ballColor)
            {
                case BallColor.BLUE:
                    return new Uri("pack://application:,,,/resources/images/balls/blue_ball_1.png");
                case BallColor.GREEN:
                    return new Uri("pack://application:,,,/resources/images/balls/green_ball_1.png");
                case BallColor.YELLOW:
                    return new Uri("pack://application:,,,/resources/images/balls/yellow_ball_1.png");
                case BallColor.PURPLE:
                    return new Uri("pack://application:,,,/resources/images/balls/purple_ball_1.png");
                default:
                    throw new ArgumentException();
            }
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!( other is MovingBall ))
            {
                return false;
            }

            return id == ( (MovingBall) other ).id;
        }

        public override int GetHashCode() => id;
    }
}
