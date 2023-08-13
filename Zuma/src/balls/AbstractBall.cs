using System.Windows;
using System.Windows.Media.Imaging;

namespace Zuma.src.balls
{
    public abstract class AbstractBall
    {
        public Point Coordinates { get; set; }
        public BitmapImage Sprite { get; protected set; }
    }
}
