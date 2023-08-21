using Zuma.models;

namespace Zuma.src.balls
{
    public class PlayerBall : BallWithColor
    {
        public PlayerBall(Path path, BallColor color) : base(path, color)
        {
        }

        public override float GetNormalRotationSpeed() => 0;
        public override float GetNormalSpeed() => 0.01f;
        public override float GetStartingRotationSpeed() => GetNormalRotationSpeed();
        public override float GetStartingSpeed() => GetNormalSpeed();
    }
}
