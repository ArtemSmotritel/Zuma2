using System.Collections.Generic;
using System.Windows.Controls;
using Zuma.models;
using Zuma.src.balls.enemy_balls;
using Zuma.src.helpers;

namespace Zuma.src.balls.player_balls
{
    public class CommonPlayerBall : AbstractPlayerBall
    {
        public CommonPlayerBall(BallColor color) : base(color)
        {
        }

        public override (bool, LinkedListNode<AbstractEnemyBall>) OnCollision(LinkedListNode<AbstractEnemyBall> enemyBall, Canvas levelCanvas, List<AbstractPlayerBall> playerBalls)
        {
            Path adjustingPath = GetAdjustingPath(enemyBall);
            var newEnemy = new CommonEnemyBall(this, adjustingPath, 0f);

            playerBalls.Remove(this);
            levelCanvas.Children.Remove(View);

            LinkedListNode<AbstractEnemyBall> prevBall = enemyBall.Previous;
            enemyBall.List.AddAfter(enemyBall, newEnemy);

            levelCanvas.Children.Add(newEnemy.View);

            while (prevBall != null && prevBall.Value != null && GeometryCalculator.AreBallsCloseEnough(prevBall.Value, prevBall.Next.Value))
            {
                prevBall.Value.IsFrozen = true;
                prevBall = prevBall.Previous;
            }

            return (false, null);
        }

        private Path GetAdjustingPath(LinkedListNode<AbstractEnemyBall> collidedEnemyBall)
        {
            System.Windows.Point start = Coordinates;
            System.Windows.Point adjustmentPoint = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed());
            System.Windows.Point end = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed() * 1.3f);

            return new Path(new List<System.Windows.Point> {
                    start,
                    adjustmentPoint,
                    end
                });
        }
    }
}
