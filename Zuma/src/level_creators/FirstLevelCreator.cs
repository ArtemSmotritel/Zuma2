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
        protected override Path GetPath() => new Path(new List<Point>
        {
            new Point(0, 350),
            new Point(250, 0),
            new Point(600, 20),

            new Point(1700, 0),
            new Point(1500, 316),

            new Point(1510, 680),
            new Point(1316, 840),
            new Point(968, 930),

            new Point(550, 920),
            new Point(100, 835),
            new Point(300, 470),

            new Point(560, 325),
            new Point(835, 350),
        });
        protected override int GetEnemyCount() => 20;
    }
}
