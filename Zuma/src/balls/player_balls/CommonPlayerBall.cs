using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Zuma.src.balls.player_balls
{
    public class CommonPlayerBall : PlayerBall
    {
        public CommonPlayerBall(BallColor color) : base(color)
        {
        }

        public override void OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls)
        {
            List<LinkedListNode<EnemyBall>> enemyBallsWithSameColor = GetBallsWithSameColor(enemyBall);

            if (enemyBallsWithSameColor.Count < 2)
            {
                return;
            }


            foreach (LinkedListNode<EnemyBall> ball in enemyBallsWithSameColor)
            {
                ball.List.Remove(ball);
                levelCanvas.Children.Remove(ball.Value.view);
            }

            LinkedListNode<EnemyBall> firstNotRemovedBall = enemyBallsWithSameColor[enemyBallsWithSameColor.Count - 1].Next;
            while (firstNotRemovedBall != null && firstNotRemovedBall.Value != null)
            {
                firstNotRemovedBall.Value.IsFrozen = true;
                firstNotRemovedBall = firstNotRemovedBall.Next;
            }

            playerBalls.Remove(this);
            levelCanvas.Children.Remove(view);
        }

        private List<LinkedListNode<EnemyBall>> GetBallsWithSameColor(LinkedListNode<EnemyBall> enemyBall)
        {
            if (enemyBall.Value.color != color)
            {
                return Enumerable.Empty<LinkedListNode<EnemyBall>>().ToList();
            }

            var list = new List<LinkedListNode<EnemyBall>>
            {
                enemyBall
            };

            LinkedListNode<EnemyBall> ballBehind = enemyBall.Previous;
            while (ballBehind != null && ballBehind.Value != null && ballBehind.Value.color == color)
            {
                list.Add(ballBehind);
                ballBehind = ballBehind.Previous;
            }

            LinkedListNode<EnemyBall> ballAhead = enemyBall.Next;
            while (ballAhead != null && ballAhead.Value != null && ballAhead.Value.color == color)
            {
                list.Add(ballAhead);
                ballAhead = ballAhead.Next;
            }

            return list;
        }
    }
}
