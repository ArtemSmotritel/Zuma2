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
        public override float GetStartingRotationSpeed() => 0.03f;
        public override float GetStartingSpeed() => 15;
    }
}
