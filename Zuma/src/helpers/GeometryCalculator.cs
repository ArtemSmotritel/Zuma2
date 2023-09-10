using System;
using System.Geometry;
using System.Numerics;
using System.Windows;
using Zuma.src.models.balls;

namespace Zuma.src.helpers
{
    public static class GeometryCalculator
    {
        public static double GetAngelBetweenTwoPoints(Point p1, Point p2)
        {
            double deltaX = p1.X - p2.X;
            double deltaY = p1.Y - p2.Y;

            double angel = Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI;

            return angel;
        }

        public static bool IsDistanceGreaterOrEqual(Point point1, Point point2, int length)
        {
            double distance = DistanceBetweenPoints(point1, point2);
            return distance >= length;
        }

        public static double DistanceBetweenPoints(Point point1, Point point2)
        {
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;

            return Math.Sqrt(( deltaX * deltaX ) + ( deltaY * deltaY ));
        }

        public static bool AreBallsTouching(MovingBall ball1, MovingBall ball2)
        {
            Point c1 = ball1.Coordinates;
            Point c2 = ball2.Coordinates;

            return DistanceBetweenPoints(c1, c2) < ball1.collisionWidth;
        }

        public static bool AreBallsCloseEnough(MovingBall ball1, MovingBall ball2)
        {
            Point c1 = ball1.Coordinates;
            Point c2 = ball2.Coordinates;

            return DistanceBetweenPoints(c1, c2) < ball1.Width;
        }

        public static Bezier GetBezierPathFromAInDirectionOfB(Point A, Point B, double distance = 1500)
        {
            Vector2 vectorA = ToVector(A);
            Vector2 vectorB = ToVector(B);

            var vectorAB = Vector2.Normalize(vectorB - vectorA);

            Vector2 vectorC = vectorA + ( (float) distance * vectorAB );

            return new Bezier(vectorA, vectorC);
        }

        public static Point ToPoint(Vector2 vector) => new Point(vector.X, vector.Y);
        public static Vector2 ToVector(Point point) => new Vector2((float) point.X, (float) point.Y);
    }
}
