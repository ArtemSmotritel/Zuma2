using Zuma.models;

namespace Zuma.src.balls
{
    public class EnemyBall : BallWithColor
    {
        public EnemyBall(Path path, BallColor color) : base(path, color)
        {
        }

        public override float GetNormalRotationSpeed() => 6;
        public override float GetNormalSpeed() => 0.00025f;
        public float GetStartingRotationSpeed() => 15;
        public float GetStartingSpeed() => 0.007f;
        public float GetCollisionSpeed() => 0.00012f;
        public float GetCollisionRotationSpeed() => 3;
    }
}
