using System;
using Zuma.models;
using Zuma.src.models.balls;

namespace Zuma.src.balls
{
    public abstract class BallWithColor : MovingBall
    {
        public readonly BallColor color;
        protected static Uri GetSpritePath(BallColor ballColor)
        {
            switch (ballColor)
            {
                case BallColor.BLUE:
                    return new Uri("pack://application:,,,/resources/images/balls/blue_ball_1.png");
                case BallColor.GREEN:
                    return new Uri("pack://application:,,,/resources/images/balls/green_ball_1.png");
                case BallColor.YELLOW:
                    return new Uri("pack://application:,,,/resources/images/balls/yellow_ball_1.png");
                case BallColor.PURPLE:
                    return new Uri("pack://application:,,,/resources/images/balls/purple_ball_1.png");
                default:
                    throw new ArgumentException();
            }
        }

        public BallWithColor(Path path, BallColor color) : base(path, spriteUri: GetSpritePath(color))
        {
            this.color = color;
        }

        public override string ToString() => $"[{id}] {( IsFrozen ? "FROZEN" : "" )} {Color()} ball at {Coordinates}";

        private string Color()
        {
            switch (color)
            {
                case BallColor.BLUE:
                    return "Blue";
                case BallColor.GREEN:
                    return "Green";
                case BallColor.YELLOW:
                    return "Yellow";
                case BallColor.PURPLE:
                    return "Purple";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
