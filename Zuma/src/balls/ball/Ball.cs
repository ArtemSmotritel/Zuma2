using System.Windows;
using System.Windows.Media.Imaging;

namespace Zuma.src.balls.ball
{
    public class Ball : AbstractBall
    {
        public Ball(BitmapImage sprite, Point coordinates)
        {
            Sprite = sprite;
            Coordinates = coordinates;
        }
    }
}
