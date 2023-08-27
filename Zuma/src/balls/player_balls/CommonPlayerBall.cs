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

        public override (bool, LinkedListNode<EnemyBall>) OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls)
        {
            List<LinkedListNode<EnemyBall>> enemyBallsWithSameColor = GetBallsWithSameColor(enemyBall);

            if (enemyBallsWithSameColor.Count < 2)
            {
                return (false, null);
            }

            LinkedListNode<EnemyBall> firstNotRemovedNode = enemyBallsWithSameColor[0].Previous;
            LinkedListNode<EnemyBall> notRemovedBallAhead = enemyBallsWithSameColor[enemyBallsWithSameColor.Count - 1].Next;

            if (firstNotRemovedNode != null)
            {
                firstNotRemovedNode.Value.IsTemporarlyFirst = true;

                while (notRemovedBallAhead != null && notRemovedBallAhead.Value != null)
                {
                    notRemovedBallAhead.Value.IsFrozen = true;
                    notRemovedBallAhead = notRemovedBallAhead.Next;
                }
            }
            else
            {
                notRemovedBallAhead.Value.IsTemporarlyFirst = true;
            }

            foreach (LinkedListNode<EnemyBall> ball in enemyBallsWithSameColor)
            {
                ball.List.Remove(ball);
                levelCanvas.Children.Remove(ball.Value.view);
            }

            playerBalls.Remove(this);
            levelCanvas.Children.Remove(view);
            return (true, firstNotRemovedNode);
        }

        private List<LinkedListNode<EnemyBall>> GetBallsWithSameColor(LinkedListNode<EnemyBall> enemyBall)
        {
            if (enemyBall.Value.color != color)
            {
                return Enumerable.Empty<LinkedListNode<EnemyBall>>().ToList();
            }

            var list = new List<LinkedListNode<EnemyBall>>();

            LinkedListNode<EnemyBall> node = enemyBall;

            while (node.Previous != null && node.Previous.Value != null && node.Previous.Value.color == color)
            {
                node = node.Previous;
            }

            while (node != null && node.Value != null && node.Value.color == color)
            {
                list.Add(node);
                node = node.Next;
            }

            return list;
        }
    }
}
