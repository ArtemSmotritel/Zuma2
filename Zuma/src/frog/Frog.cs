using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Zuma.src.balls.player_balls;
using Zuma.src.helpers;

namespace Zuma.src.frog
{
    public class Frog
    {
        public Point Coordinates { get; private set; }

        public AbstractPlayerBall CurrentBall { get; set; }

        public AbstractPlayerBall NextBall { get; set; }

        public BitmapImage Sprite { get; private set; }

        public Frog(Point coordinates, Uri spriteURI)
        {
            Coordinates = coordinates;
            Sprite = new BitmapImage(spriteURI);
            CurrentBall = BallGenerator.GeneratePlayerBall();
            NextBall = BallGenerator.GeneratePlayerBall();
        }

        public void SwapBall() => CurrentBall = NextBall;
    }
}
