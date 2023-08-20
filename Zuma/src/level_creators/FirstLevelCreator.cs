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
        protected override Path GetPath()
        {
            return new Path(new List<Point>
            {
                new Point(13.6, 461.6),
                new Point(130.4, 294.4),
                new Point(292, 165.6),
                new Point(444.8, 86.4),
                new Point(644, 34.4),
                new Point(876, 31.2),
                new Point(1107.2, 71.2),
                new Point(1296, 184.8),
                new Point(1392.8, 351.2),
                new Point(1432, 551.2),
                new Point(1388.8, 708),
                new Point(1230.4, 806.4),
                new Point(1008, 841.6),
                new Point(776.8, 831.2),
                new Point(576, 772),
                new Point(423.2, 652),
                new Point(375.2, 504.8),
                new Point(431.2, 381.6),
                new Point(580, 308.8),
                new Point(784, 292),
                new Point(940.8, 340.8),
                new Point(1065.6, 472),
            });
        }

        protected override int GetEnemyCount() => 20;
    }
}
