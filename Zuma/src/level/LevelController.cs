using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelController
    {
        public bool MoveBalls(LinkedListNode<EnemyBall> enemyBall, List<PlayerBall> playerBalls, System.Windows.Controls.Canvas levelCanvas)
        {
            while (enemyBall != null && enemyBall.Value != null)
            {
                float speed = 0;
                float rotationSpeed = 0;

                if (enemyBall.Value.HasReachedDestination(speed))
                {
                    return true;
                }
                else
                {
                    speed = enemyBall.Value.GetNormalSpeed();
                    rotationSpeed = enemyBall.Value.GetNormalRotationSpeed();
                    enemyBall.Value.Move(enemyBall.Value, enemyBall.Next?.Value, speed, rotationSpeed, enemyBall.Value.GetCollisionSpeed(), enemyBall.Value.GetCollisionRotationSpeed());
                }

                enemyBall = enemyBall.Previous;
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
