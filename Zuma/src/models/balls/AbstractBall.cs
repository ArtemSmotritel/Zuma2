using System.Windows;
using Zuma.models;

namespace Zuma.src.models.balls
{
    public abstract class AbstractBall : IBall
    {
        public Point Coordinates { get; protected set; }
        public Path ThePath { get; protected set; }

        public abstract void Dissapear();
        public abstract void Freeze();
        public abstract void Move();
    }
}
