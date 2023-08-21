using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelController
    {
        public bool MoveBalls(LinkedListNode<EnemyBall> sublistTailBall)
        {
            while (sublistTailBall != null && sublistTailBall.Value != null)
            {
                if (sublistTailBall.Value.HasReachedDestination())
                {
                    return true;
                }
                else
                {
                    sublistTailBall.Value.Move(sublistTailBall.Value, sublistTailBall.Next?.Value);
                }

                sublistTailBall = sublistTailBall.Previous;
            }

            return false;
        }

        public EnemyBall GenerateBall(Level level, LinkedList<EnemyBall> enemyBalls)
        {
            BallColor lastGeneratedColor = enemyBalls.First?.Value?.color ?? BallColor.NONE;
            BallColor beforeLastGeneratedColor = enemyBalls.First?.Next?.Value?.color ?? BallColor.NONE;
            EnemyBall ball = BallGenerator.GenerateEnemyBall(level, (lastGeneratedColor, beforeLastGeneratedColor));

            return ball;
        }
    }
}
