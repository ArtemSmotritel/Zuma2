using System.Collections.Generic;
using System.Windows.Controls;
using Zuma.models;

namespace Zuma.src.balls
{
    public abstract class PlayerBall : BallWithColor
    {
        private PlayerBall(Path path, BallColor color) : base(path, color)
        {
        }

        public PlayerBall(BallColor color) : base(Path.EMPTY, color)
        {
        }

        public override float GetNormalRotationSpeed() => 0;
        public override float GetNormalSpeed() => 0.02f;

        public void SetPath(Path path)
        {
            this.path = path;
            Coordinates = path.Start;
        }

        public abstract (bool, LinkedListNode<EnemyBall>) OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls);
    }
}
