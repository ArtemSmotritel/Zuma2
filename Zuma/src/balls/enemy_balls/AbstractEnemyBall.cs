using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.balls.player_balls;

namespace Zuma.src.balls.enemy_balls
{
    public abstract class AbstractEnemyBall : AbstractBall
    {
        public AbstractEnemyBall(Path path, BallColor color) : base(path, color)
        {
        }

        public AbstractEnemyBall(AbstractPlayerBall playerBall, Path path, float pathTime) : base(path, playerBall.color)
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

        public BitmapImage SpecialEffectBitmapSprite;

        public bool IsAdjusting { get; set; }
        public bool ShouldTriggerEffect { get; set; }
        public bool IsEffectApplying { get; set; }

        public Path GetPath() => path;
        public float GetPathTime() => PathTime;

        public Point GetPositionWithDelta(float timeDelta) => path.GetPosition(PathTime + timeDelta);

        public override float GetNormalRotationSpeed() => 4;
        public override float GetNormalSpeed() => 0.0005f;
        public float GetStartingRotationSpeed() => 40;
        public float GetStartingSpeed() => 0.007f;
        public float GetCollisionSpeed() => 0.00012f;
        public float GetCollisionRotationSpeed() => 4;
        public float GetAdjustingSpeed() => 0.07f;
        public float GetAdjustingRotationSpeed() => 4;

        public virtual void TriggerEffect(Canvas levelCanvas, LinkedListNode<AbstractEnemyBall> currentBall)
        {
            IsEffectApplying = true;
            if (!IsDisposed)
            {
                currentBall.List.Remove(currentBall);
                levelCanvas.Children.Remove(View);
                IsDisposed = true;
                IsEffectApplying = false;
            }
        }
    }
}
