using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.helpers;

namespace Zuma.src.models.balls
{
    public abstract class MovingBall : IBall
    {
        public static float NormalSpeed => 0.00025f;
        public static float NormalRotationSpeed => 10;
        public static float StartingSpeed => 0.03f;
        public static float StartingRotationSpeed => 20;

        public readonly System.Windows.Shapes.Ellipse view;
        public readonly int height = 40;
        public readonly int width = 40;
        public readonly float halfHeight;
        public readonly float halfWidth;
        private readonly BitmapImage sprite;

        protected readonly Path path;
        public Point Coordinates { get; protected set; }
        protected float Speed { get; set; }
        protected float RotationSpeed { get; set; }
        protected float PathTime { get; set; }
        protected float RotationAngle { get; set; }

        public MovingBall(Point coordinates, Uri spriteURI, Path path)
        {
            Coordinates = coordinates;
            sprite = new BitmapImage(spriteURI);
            this.path = path;
            Speed = 0;
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
            Speed = NormalSpeed;
            RotationSpeed = NormalRotationSpeed;
        }

        public void ResumeStartingSpeed()
        {
            Speed = StartingSpeed;
            RotationSpeed = StartingRotationSpeed;
        }

        public void Move(LinkedListNode<MovingBall> ball)
        {
            if (ball.Next == null)
            {
                Move();
                return;
            }

            double distance = GeometryCalculator.DistanceBetweenPoints(Coordinates, ball.Next.Value.Coordinates);
            if (distance < ( width - ( width / 15f ) ))
            {
                Speed = 0.00015f;
                RotationSpeed = 6;
                Move();
                Speed = NormalSpeed;
                RotationSpeed = NormalRotationSpeed;
            }
            else
            {
                while (GeometryCalculator.IsDistanceGreaterOrEqual(Coordinates, ball.Next.Value.Coordinates, width - ( width / 15f )))
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
