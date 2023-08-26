using System;
using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.balls.player_balls;
using Zuma.src.level;

namespace Zuma.src.helpers
{
    public static class BallGenerator
    {
        private static readonly Random random = new Random();

        private static readonly List<BallColor> enemyColorsWithoutBlue = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.YELLOW,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> enemyColorsWithoutYellow = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.BLUE,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> enemyColorsWithoutGreen = new List<BallColor>
        {
            BallColor.BLUE,
            BallColor.YELLOW,
            BallColor.PURPLE,
        };

        private static readonly List<BallColor> enemyColorsWithoutPurple = new List<BallColor>
        {
            BallColor.GREEN,
            BallColor.YELLOW,
            BallColor.BLUE,
        };

        public static PlayerBall GeneratePlayerBall() => new CommonPlayerBall(GetRandomBallColor());

        public static EnemyBall GenerateEnemyBall(Level level, (BallColor, BallColor) twoLastGeneratedColors)
        {
            BallColor ballColor = GetNextBallColor(twoLastGeneratedColors);

            return new EnemyBall(level.Path, ballColor);
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
                    return enemyColorsWithoutBlue[index];
                case BallColor.GREEN:
                    return enemyColorsWithoutGreen[index];
                case BallColor.YELLOW:
                    return enemyColorsWithoutYellow[index];
                case BallColor.PURPLE:
                    return enemyColorsWithoutPurple[index];
                default:
                    throw new ArgumentException();
            }
        }
    }
}
