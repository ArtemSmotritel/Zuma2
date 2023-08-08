using System.Windows;
using Zuma.models;

namespace Zuma.src.models.balls
{
    public abstract class AbstractBall : IBall
    {
        public Point Coordinates { get; private set; }
        public Path ThePath { get; private set; }

        public abstract void Dissapear();
        public abstract void Freeze();
        public abstract void Move();
    }
}
