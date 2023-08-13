using System;
using System.Collections.Generic;
using System.Geometry;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace Zuma.models
{
    public class Path
    {
        private List<Bezier> BezierCurves { get; set; }
        public int CurvesCount => BezierCurves.Count;

        public Path(List<Point> points)
        {
            Vector2[] vectors = points
                .Select(point => new Vector2((float) point.X, (float) point.Y))
                .ToArray();
            BezierCurves = new List<Bezier> { new Bezier(vectors) };
        }

        public Path() { }

        public static Path QuadraticBezierCurveBased(List<Point> points)
        {
            var path = new Path
            {
                BezierCurves = new List<Bezier>()
            };

            for (int i = 0; i < points.Count - 2; i += 2)
            {
                Point start = points[i];
                Point adjustent = points[i + 1];
                Point end = points[i + 2];
                path.BezierCurves.Add(B3(start, adjustent, end));
            }

            return path;
        }

        public Point GetPosition(float t)
        {
            int index = GetCurveIndex(t);
            float curveAdjustedT = t - index;
            System.Numerics.Vector2 vector = BezierCurves[index].Position(curveAdjustedT);
            return new Point(vector.X, vector.Y);
        }

        public bool HasReachedDestination(float t) => t >= BezierCurves.Count;

        private int GetCurveIndex(float t) => (int) Math.Floor(t);

        private static Bezier B3(Point p1, Point p2, Point p3)
        {
            return new Bezier(
                new Vector2((float) p1.X, (float) p1.Y),
                new Vector2((float) p2.X, (float) p2.Y),
                new Vector2((float) p3.X, (float) p3.Y)
                );
        }
    }
}
