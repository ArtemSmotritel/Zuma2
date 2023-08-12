using System;
using System.Windows.Media.Imaging;
using Zuma.src.models.balls;

namespace Zuma.src.frog
{
    public class Frog
    {
        public AbstractBall CurrentBall { get; private set; }

        public AbstractBall NextBall { get; set; }

        public BitmapImage Sprite { get; private set; }

        public Frog(Uri spriteURI)
        {
            Sprite = new BitmapImage(spriteURI);
        }
    }
}
