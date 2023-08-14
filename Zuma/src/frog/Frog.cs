using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Zuma.src.models.balls;

namespace Zuma.src.frog
{
    public class Frog
    {
        public Point Coordinates { get; private set; }

        public MovingBall CurrentBall { get; private set; }

        public MovingBall NextBall { get; set; }

        public BitmapImage Sprite { get; private set; }

        public Frog(Point coordinates, Uri spriteURI)
        {
            Coordinates = coordinates;
            Sprite = new BitmapImage(spriteURI);
        }
    }
}
