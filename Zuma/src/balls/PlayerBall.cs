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
        public override float GetNormalSpeed() => 0.01f;
        public override float GetStartingRotationSpeed() => GetNormalRotationSpeed();
        public override float GetStartingSpeed() => GetNormalSpeed();
        public override float GetCollisionSpeed() => GetNormalSpeed();
        public override float GetCollisionRotationSpeed() => GetNormalRotationSpeed();

        public void SetPath(Path path)
        {
            this.path = path;
            Coordinates = path.Start;
        }

        public abstract void OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls);
    }
}
