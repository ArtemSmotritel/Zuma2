using System;
using System.Windows;
using Zuma.models;
using Zuma.src.models.balls;

namespace Zuma.src.balls
{
    public class BallWithColor : MovingBall
    {
        public readonly BallColor color;

        public static BallWithColor CreateBall(Point coordinates, Path path, BallColor ballColor)
        {
            Uri spritePath = GetSpritePath(ballColor);

            var ball = new BallWithColor(coordinates, spritePath, path, ballColor);

            return ball;
        }

        private static Uri GetSpritePath(BallColor ballColor)
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

        private BallWithColor(Point coordinates,
                         Uri spriteURI,
                         Path path,
                         BallColor color) : base(coordinates, spriteURI, path)
        {
            this.color = color;
        }

        public override string ToString() => $"{Color()} ball at {Coordinates}";

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
