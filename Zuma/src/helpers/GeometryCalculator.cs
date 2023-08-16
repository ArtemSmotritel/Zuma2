using System;
using System.Windows;

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
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;

            double distance = Math.Sqrt(( deltaX * deltaX ) + ( deltaY * deltaY ));

            return distance >= length;
        }

        public static bool IsDistanceGreaterOrEqual(Point point1, Point point2, float length)
        {
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;

            double distance = Math.Sqrt(( deltaX * deltaX ) + ( deltaY * deltaY ));

            return distance >= length;
        }

        public static double DistanceBetweenPoints(Point point1, Point point2)
        {
            double deltaX = point2.X - point1.X;
            double deltaY = point2.Y - point1.Y;

            return Math.Sqrt(( deltaX * deltaX ) + ( deltaY * deltaY ));
        }
    }
}
