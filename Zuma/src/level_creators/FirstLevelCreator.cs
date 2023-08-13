using System;
using System.Collections.Generic;
using System.Windows;
using Zuma.models;
using Zuma.src.frog;

namespace Zuma.src.level_creators
{
    public class FirstLevelCreator : LevelCreator
    {
        protected override Uri GetBackgroundURI() => new Uri("pack://application:,,,/resources/images/backgrounds/beach_level.png");
        protected override Frog GetFrog() => new Frog
                (
                new Point(780, 430),
                new Uri("pack://application:,,,/resources/images/frog/frog.png")
                );
        protected override string GetName() => "First";
        protected override int GetNumber() => 1;
        protected override Path GetPath() => Path.QuadraticBezierCurveBased(new List<Point>
        {
            new Point(0, 350),
            new Point(250, 80),
            new Point(600, 80),

            new Point(1300, 80),
            new Point(1400, 416),

            new Point(1510, 680),
            new Point(1316, 740),
            new Point(968, 830),

            new Point(550, 720),
            new Point(180, 635),
            new Point(380, 470),

            new Point(560, 325),
            new Point(835, 350),
        });
    }
}
