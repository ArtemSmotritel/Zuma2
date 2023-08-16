using System;
using System.Windows;
using Zuma.models;
using Zuma.src.models.balls;

namespace Zuma.src.balls
{
    public class EnemyBall : MovingBall
    {
        public EnemyBall(Point coordinates, Uri spriteURI, Path path) : base(coordinates, spriteURI, path)
        {
        }
    }
}
