using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.helpers;

namespace Zuma.src.models.balls
{
    public abstract class MovingBall
    {
        public abstract float GetNormalSpeed();
        public abstract float GetNormalRotationSpeed();
        public abstract float GetStartingSpeed();
        public abstract float GetStartingRotationSpeed();

        public readonly System.Windows.Shapes.Ellipse view;
        public readonly int height = 40;
        public readonly int width = 40;
        public readonly float halfHeight;
        public readonly float halfWidth;
        protected BitmapImage sprite;

        protected readonly Path path;
        public Point Coordinates { get; protected set; }
        protected float Speed { get; set; }
        protected float RotationSpeed { get; set; }
        protected float PathTime { get; set; }
        protected float RotationAngle { get; set; }

        public MovingBall(Path path, Uri spriteUri)
        {
            Coordinates = path.Start;
            sprite = new BitmapImage(spriteUri);
            this.path = path;
            ResumeNormalSpeed();
            PathTime = 0;
            view = new System.Windows.Shapes.Ellipse
            {
                Width = width,
                Height = height,
                Fill = new ImageBrush(sprite),
            };
            halfHeight = height / 2f;
            halfWidth = width / 2f;
        }

        public void GoBackwards()
        {
            Speed = -Math.Abs(Speed);
            RotationSpeed = -Math.Abs(RotationSpeed);
        }

        public void GoForwards()
        {
            Speed = Math.Abs(Speed);
            RotationSpeed = Math.Abs(RotationSpeed);
        }

        public void Freeze()
        {
            Speed = 0;
            RotationSpeed = 0;
        }

        public void ResumeNormalSpeed()
        {
            Speed = GetNormalSpeed();
            RotationSpeed = GetNormalRotationSpeed();
        }

        public void ResumeStartingSpeed()
        {
            Speed = GetStartingSpeed();
            RotationSpeed = GetStartingRotationSpeed();
        }

        public void Move(MovingBall ball, MovingBall nextBall)
        {
            if (nextBall == null)
            {
                Move();
                return;
            }

            double distance = GeometryCalculator.DistanceBetweenPoints(Coordinates, nextBall.Coordinates);
            if (distance < ( width - ( width / 15f ) ))
            {
                Speed = 0.00015f;
                RotationSpeed = 6;
                Move();
                ResumeNormalSpeed();
            }
            else
            {
                while (GeometryCalculator.IsDistanceGreaterOrEqual(Coordinates, nextBall.Coordinates, width - ( width / 15f )))
                {
                    Move();
                }
            }
        }

        public void Move()
        {
            PathTime += Speed;
            Coordinates = path.GetPosition(PathTime);
            Canvas.SetLeft(view, Coordinates.X);
            Canvas.SetTop(view, Coordinates.Y);

            RotationAngle = Utils.AddAngels(RotationAngle, RotationSpeed);
            view.RenderTransform = new RotateTransform(RotationAngle, halfWidth, halfHeight);
        }

        public bool HasReachedDestination() => path.HasReachedDestination(PathTime + Speed);
    }
}
