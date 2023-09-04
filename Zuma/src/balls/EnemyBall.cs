using Zuma.models;

namespace Zuma.src.balls
{
    public class EnemyBall : BallWithColor
    {
        public EnemyBall(Path path, BallColor color) : base(path, color)
        {
        }

        public EnemyBall(PlayerBall playerBall, Path path, float pathTime) : base(path, playerBall.color)
        {
            PathTime = pathTime;
        }

        public bool IsTemporarlyFirst { get; set; }

        public Path GetPath() => path;
        public float GetPathTime() => PathTime;

        public override float GetNormalRotationSpeed() => 8;
        public override float GetNormalSpeed() => 0.0003f;
        public float GetStartingRotationSpeed() => 40;
        public float GetStartingSpeed() => 0.007f;
        public float GetCollisionSpeed() => 0.00012f;
        public float GetCollisionRotationSpeed() => 3;
    }
}
