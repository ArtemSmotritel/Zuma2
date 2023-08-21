using System;
using System.Collections.Generic;
using System.Geometry;
using System.Linq;
using System.Numerics;
using System.Windows;
using Zuma.src.helpers;

namespace Zuma.models
{
    public class Path
    {
        private List<Bezier> BezierCurves { get; set; }
        public int CurvesCount => BezierCurves.Count;

        private Point start;
        public Point Start => start;

        private Point end;
        public Point End => end;

        public Path(List<Point> points)
        {
            Vector2[] vectors = points
                .Select(point => GeometryCalculator.ToVector(point))
                .ToArray();
            BezierCurves = new List<Bezier> { new Bezier(vectors) };

            SetStartAndEndPoints();
        }

        public Path() { }

        public Path(Bezier bezier)
        {
            BezierCurves = new List<Bezier> { bezier };
            SetStartAndEndPoints();
        }

        public static Path CreateQuadraticBezierCurveBasedPath(List<Point> points)
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
                path.BezierCurves.Add(QuadraticBezierCurve(start, adjustent, end));
            }

            path.SetStartAndEndPoints();

            return path;
        }

        public Point GetPosition(float t)
        {
            int index = GetCurveIndex(t);
            float curveAdjustedT = t - index;
            Vector2 vector = BezierCurves[index].Position(curveAdjustedT);
            return GeometryCalculator.ToPoint(vector);
        }

        public bool HasReachedDestination(float t) => t >= BezierCurves.Count;

        private int GetCurveIndex(float t) => (int) Math.Floor(t);

        private void SetStartAndEndPoints()
        {
            start = GeometryCalculator.ToPoint(BezierCurves[0].Points[0]);

            Bezier lastCurve = BezierCurves[BezierCurves.Count - 1];
            Vector2 lastVector = lastCurve.Points[lastCurve.Points.Count - 1];
            end = GeometryCalculator.ToPoint(lastVector);
        }

        private static Bezier QuadraticBezierCurve(Point p1, Point p2, Point p3) => new Bezier(GeometryCalculator.ToVector(p1), GeometryCalculator.ToVector(p2), GeometryCalculator.ToVector(p3));
    }
}
