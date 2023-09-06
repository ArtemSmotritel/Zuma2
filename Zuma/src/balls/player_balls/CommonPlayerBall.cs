using System.Collections.Generic;
using System.Windows.Controls;
using Zuma.models;
using Zuma.src.helpers;

namespace Zuma.src.balls.player_balls
{
    public class CommonPlayerBall : PlayerBall
    {
        public CommonPlayerBall(BallColor color) : base(color)
        {
        }

        public override (bool, LinkedListNode<EnemyBall>) OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls)
        {
            bool isPlayerBallPositionedMoreToTheRight = enemyBall.Value.Coordinates.X < Coordinates.X;

            Path adjustingPath = GetAdjustingPath(enemyBall, isPlayerBallPositionedMoreToTheRight);
            var newEnemy = new EnemyBall(this, adjustingPath, 0f);

            playerBalls.Remove(this);
            levelCanvas.Children.Remove(view);

            LinkedListNode<EnemyBall> prevBall = enemyBall.Previous;
            if (isPlayerBallPositionedMoreToTheRight)
            {
                enemyBall.List.AddAfter(enemyBall, newEnemy);
            }
            else
            {
                enemyBall.List.AddBefore(enemyBall, newEnemy);
                prevBall = prevBall?.Previous;
            }

            levelCanvas.Children.Add(newEnemy.view);

            while (prevBall != null && prevBall.Value != null && GeometryCalculator.AreBallsCloseEnough(prevBall.Value, prevBall.Next.Value))
            {
                prevBall.Value.IsFrozen = true;
                prevBall = prevBall.Previous;
            }

            return (false, null);
        }

        private Path GetAdjustingPath(LinkedListNode<EnemyBall> collidedEnemyBall, bool isPlayerBallPositionedMoreToTheRight)
        {
            System.Windows.Point start = Coordinates;
            System.Windows.Point adjustmentPoint = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed());
            System.Windows.Point end = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed() * 1.3f);

            if (isPlayerBallPositionedMoreToTheRight)
            {
                start = Coordinates;
                adjustmentPoint = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed());
                end = collidedEnemyBall.Value.GetPositionWithDelta(collidedEnemyBall.Value.GetStartingSpeed() * 1.3f);

                return new Path(new List<System.Windows.Point> {
                    start,
                    adjustmentPoint,
                    end
                });
            }

            start = Coordinates;
            end = collidedEnemyBall.Value.GetPositionWithDelta(-collidedEnemyBall.Value.GetStartingSpeed());

            return new Path(new List<System.Windows.Point> {
                start,
                end
            });
        }
    }
}
