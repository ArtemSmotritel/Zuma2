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
    }
}
