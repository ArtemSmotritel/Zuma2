using System;
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
                new LevelCoordinates()
                );
        }

        public static Level CreateSecondLevel()
        {
            return new Level
                (
                "Medium",
                2,
                new Uri("pack://application:,,,/resources/images/backgrounds/city_level.png"),
                new LevelCoordinates()
                );
        }

        public static Level CreateThirdLevel()
        {
            return new Level
                (
                "Hard",
                3,
                new Uri("pack://application:,,,/resources/images/backgrounds/temple_level.png"),
                new LevelCoordinates()
                );
        }
    }
}
