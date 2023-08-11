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
                new Uri("Resources/images/beach_level.png", UriKind.Relative)
                );
        }
    }
}
