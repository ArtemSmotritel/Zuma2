using System;
using Zuma.models;
using Zuma.src.balls;
using Zuma.src.balls.enemy_balls;
using Zuma.src.balls.player_balls;
using Zuma.src.level;

namespace Zuma.src.helpers
{
    public static class BallGenerator
    {
        private static readonly Random random = new Random();

        public static AbstractPlayerBall GeneratePlayerBall() => new CommonPlayerBall(GetRandomBallColor());

        public static AbstractEnemyBall GenerateEnemyBall(Level level)
        {
            BallColor ballColor = GetRandomBallColor();

            return GenerateRandomEnemyBall(level.Path, ballColor);
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

        private static AbstractEnemyBall GenerateRandomEnemyBall(Path path, BallColor ballColor)
        {
            int r = random.Next(0, 100);

            return r < 95 ? new CommonEnemyBall(path, ballColor) : (AbstractEnemyBall) new ExplodingEnemyBall(path, ballColor);
        }
    }
}
