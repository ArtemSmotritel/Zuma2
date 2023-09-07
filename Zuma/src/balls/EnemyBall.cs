using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            IsAdjusting = true;
        }

        public void FinishAdjustment(Path path, float pathTime)
        {
            this.path = path;
            PathTime = pathTime;

            IsAdjusting = false;
            ShouldTriggerEffect = true;
        }

        public bool IsAdjusting { get; set; }
        public bool ShouldTriggerEffect { get; set; }

        public Path GetPath() => path;
        public float GetPathTime() => PathTime;

        public Point GetPositionWithDelta(float timeDelta) => path.GetPosition(PathTime + timeDelta);

        public override float GetNormalRotationSpeed() => 8;
        public override float GetNormalSpeed() => 0.0003f;
        public float GetStartingRotationSpeed() => 40;
        public float GetStartingSpeed() => 0.007f;
        public float GetCollisionSpeed() => 0.00012f;
        public float GetCollisionRotationSpeed() => 3;

        public void TriggerEffect(Canvas levelCanvas, LinkedListNode<EnemyBall> currentBall)
        {
            currentBall.List.Remove(currentBall);
            levelCanvas.Children.Remove(view);
        }
    }
}
