using System;
using System.Windows;
using Zuma.src.frog;
using Zuma.src.level;

namespace Zuma.src.controllers
{
    public static class LevelCreator
    {
        public static Level CreateFirstLevel()
        {
            return new Level
                (
                "Simple",
                1,
                new Uri("pack://application:,,,/resources/images/backgrounds/beach_level.png"),
                new LevelCoordinates(new Point(500, 500)),
                CreateFrog()
                );
        }

        public static Level CreateSecondLevel()
        {
            return new Level
                (
                "Medium",
                2,
                new Uri("pack://application:,,,/resources/images/backgrounds/city_level.png"),
                new LevelCoordinates(),
                CreateFrog()
                );
        }

        public static Level CreateThirdLevel()
        {
            return new Level
                (
                "Hard",
                3,
                new Uri("pack://application:,,,/resources/images/backgrounds/temple_level.png"),
                new LevelCoordinates(),
                CreateFrog()
                );
        }

        private static Frog CreateFrog()
        {
            return new Frog
                (
                new Uri("pack://application:,,,/resources/images/frog/frog.png")
                );
        }
    }
}
