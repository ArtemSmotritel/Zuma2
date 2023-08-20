using System;
using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.level;

namespace Zuma.src.helpers
{
    public static class BallGenerator
    {
        private static readonly Random random = new Random();

        private static readonly List<BallColor> colorsWithoutBlue = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.YELLOW,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> colorsWithoutYellow = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.BLUE,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> colorsWithoutGreen = new List<BallColor>
        {
            BallColor.BLUE,
            BallColor.YELLOW,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> colorsWithoutPurple = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.YELLOW,
            BallColor.BLUE,
        };

        public static BallWithColor GenerateEnemyBall(Level level, (BallColor, BallColor) twoLastGeneratedColors)
        {
            BallColor ballColor = GetNextBallColor(twoLastGeneratedColors);

            var ball = BallWithColor.CreateBall(level.Path.Start, level.Path, ballColor);

            return ball;
        }

        private static BallColor GetNextBallColor((BallColor, BallColor) twoLastGeneratedColors)
        {
            (BallColor lastColor, BallColor beforeLastColor) = twoLastGeneratedColors;

            return lastColor == BallColor.NONE || beforeLastColor == BallColor.NONE || lastColor != beforeLastColor
                ? GetRandomBallColor()
                : GetRandomBallColorExceptOne(lastColor);
        }

        private static BallColor GetRandomBallColor()
        {
            int n = random.Next(0, 4);

            switch (n)
            {
                case 0:
                    return BallColor.BLUE;
                case 1:
                    return BallColor.GREEN;
                case 2:
                    return BallColor.YELLOW;
                case 3:
                    return BallColor.PURPLE;
                default:
                    throw new ArgumentException();
            }
        }

        private static BallColor GetRandomBallColorExceptOne(BallColor ballColorToOmit)
        {
            int index = random.Next(0, 3);

            switch (ballColorToOmit)
            {
                case BallColor.BLUE:
                    return colorsWithoutBlue[index];
                case BallColor.GREEN:
                    return colorsWithoutGreen[index];
                case BallColor.YELLOW:
                    return colorsWithoutYellow[index];
                case BallColor.PURPLE:
                    return colorsWithoutPurple[index];
                default:
                    throw new ArgumentException();
            }
        }
    }
}
