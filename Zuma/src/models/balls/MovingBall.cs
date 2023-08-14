using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Zuma.models;

namespace Zuma.src.models.balls
{
    public abstract class MovingBall : IBall
    {
        public static float NormalSpeed => 0.33f;
        public static float StartingSpeed => 0.66f;

        public Point Coordinates { get; protected set; }
        public BitmapImage Sprite { get; protected set; }
        protected Path ThePath { get; set; }
        protected float Speed { get; set; }
        protected float PathTime { get; set; }

        public MovingBall(Point coordinates, Uri spriteURI, Path path)
        {
            Coordinates = coordinates;
            Sprite = new BitmapImage(spriteURI);
            ThePath = path;
            Speed = 0;
            PathTime = 0;
        }

        public void GoBackwards() => Speed = -Math.Abs(Speed);
        public void GoForwards() => Speed = Math.Abs(Speed);
        public void Freeze() => Speed = 0;
        public void ResumeNormalSpeed() => Speed = NormalSpeed;
        public void ResumeStartingSpeed() => Speed = StartingSpeed;
        public void Move()
        {
            PathTime += Speed;
            Coordinates = ThePath.GetPosition(PathTime);
        }
        public bool HasReachedDestination() => ThePath.HasReachedDestination(PathTime);
    }
}
