using System;
using System.Windows;
using Zuma.src.frog;
using Zuma.src.level;

namespace Zuma.src.controllers
{
    public static class LevelCreator
    {
        public static Level CreateFirstLevel() => new Level
                (
                "Simple",
                1,
                new Uri("pack://application:,,,/resources/images/backgrounds/beach_level.png"),
                CreateFrog()
                );

        public static Level CreateSecondLevel() => new Level
                (
                "Medium",
                2,
                new Uri("pack://application:,,,/resources/images/backgrounds/city_level.png"),
                CreateFrog()
                );

        public static Level CreateThirdLevel() => new Level
                (
                "Hard",
                3,
                new Uri("pack://application:,,,/resources/images/backgrounds/temple_level.png"),
                CreateFrog()
                );

        private static Frog CreateFrog() => new Frog
                (
                new Point(500, 500),
                new Uri("pack://application:,,,/resources/images/frog/frog.png")
                );
    }
}
