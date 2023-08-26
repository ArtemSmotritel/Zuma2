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
        private static int _ID = 0;
        protected static int ID
        {
            get
            {
                _ID++;
                return _ID;
            }
        }
        public abstract float GetNormalSpeed();
        public abstract float GetNormalRotationSpeed();

        public readonly int id;
        public readonly System.Windows.Shapes.Ellipse view;
        public readonly int height = 40;
        public readonly int width = 40;
        public readonly float halfHeight;
        public readonly float halfWidth;
        public BitmapImage Sprite { get; protected set; }

        protected Path path;
        public Point Coordinates { get; protected set; }
        protected float PathTime { get; set; }
        protected float RotationAngle { get; set; }
        public bool IsFrozen { get; set; }

        public MovingBall(Path path, Uri spriteUri)
        {
            id = ID;
            IsFrozen = false;
            Coordinates = path.Start;
            Sprite = new BitmapImage(spriteUri);
            this.path = path;
            PathTime = 0;
            view = new System.Windows.Shapes.Ellipse
            {
                Width = width,
                Height = height,
                Fill = new ImageBrush(Sprite),
            };
            halfHeight = height / 2f;
            halfWidth = width / 2f;
        }

        public void Move(MovingBall ball, MovingBall nextBall, float speed, float rotationSpeed, float collisisonSpeed, float collisionRotationSpeed)
        {
            if (IsFrozen)
            {
                return;
            }

            if (nextBall == null)
            {
                Move(speed, rotationSpeed);
                return;
            }

            double distance = GeometryCalculator.DistanceBetweenPoints(Coordinates, nextBall.Coordinates);
            if (distance < ( width - ( width / 15f ) ))
            {
                Move(collisisonSpeed, collisionRotationSpeed);
            }
            else
            {
                while (GeometryCalculator.IsDistanceGreaterOrEqual(Coordinates, nextBall.Coordinates, width - ( width / 15f )))
                {
                    Move(speed, rotationSpeed);
                }
            }
        }

        public void Move(float speed, float rotationSpeed)
        {
            if (IsFrozen)
            {
                return;
            }

            PathTime += speed;
            Coordinates = path.GetPosition(PathTime);
            Canvas.SetLeft(view, Coordinates.X);
            Canvas.SetTop(view, Coordinates.Y);

            RotationAngle = Utils.AddAngels(RotationAngle, rotationSpeed);
            view.RenderTransform = new RotateTransform(RotationAngle, halfWidth, halfHeight);
        }

        public bool HasReachedDestination(float speed) => path.HasReachedDestination(PathTime + speed);
    }
}
