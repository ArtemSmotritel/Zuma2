using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelController
    {
        public bool MoveBalls(LinkedListNode<EnemyBall> enemyBall, List<PlayerBall> playerBalls, System.Windows.Controls.Canvas levelCanvas, bool shouldUseStartingSpeed)
        {
            while (enemyBall != null && enemyBall.Value != null)
            {
                bool wereBallsRemoved = false;
                LinkedListNode<EnemyBall> firstNotRemovedBall = null;
                for (int i = 0; i < playerBalls.Count; i++)
                {
                    PlayerBall ball = playerBalls[i];
                    double distance = GeometryCalculator.DistanceBetweenPoints(ball.Coordinates, enemyBall.Value.Coordinates);
                    if (distance < ( ball.width - ( ball.width / 15f ) ))
                    {
                        (bool wereBallsRemoved2, LinkedListNode<EnemyBall> firstNotRemovedBall2) = ball.OnCollision(enemyBall, levelCanvas, playerBalls);
                        wereBallsRemoved = wereBallsRemoved2;
                        firstNotRemovedBall = firstNotRemovedBall2;
                    }
                }

                if (wereBallsRemoved)
                {
                    enemyBall = firstNotRemovedBall;
                    continue;
                }

                if (enemyBall.Value.IsFrozen)
                {
                    enemyBall = enemyBall.Previous;
                    continue;
                }

                float speed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingSpeed() : enemyBall.Value.GetNormalSpeed();
                float rotationSpeed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingRotationSpeed() : enemyBall.Value.GetNormalRotationSpeed();

                if (enemyBall.Next?.Value == null && enemyBall.Value.HasReachedDestination(speed))
                {
                    return true;
                }
                else
                {
                    if (enemyBall.Next?.Value == null)
                    {
                        enemyBall.Value.Move(speed, rotationSpeed);
                        enemyBall = enemyBall.Previous;
                        continue;
                    }

                    System.Windows.Point nextBallCoordinates = enemyBall.Next.Value.Coordinates;
                    int width = enemyBall.Value.width;

                    double distance = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, nextBallCoordinates);

                    if (enemyBall.Value.IsTemporarlyFirst)
                    {
                        enemyBall.Value.Move(speed, rotationSpeed);

                        if (distance < ( width - ( width / 15f ) ))
                        {
                            enemyBall.Value.IsTemporarlyFirst = false;
                        }

                        enemyBall = enemyBall.Previous;
                        continue;
                    }

                    if (distance < ( width - ( width / 15f ) ))
                    {
                        float collisionSpeed = enemyBall.Value.GetCollisionSpeed();
                        float collisionRotationSpeed = enemyBall.Value.GetCollisionRotationSpeed();

                        enemyBall.Value.Move(collisionSpeed, collisionRotationSpeed);

                        LinkedListNode<EnemyBall> nextBall = enemyBall.Next;
                        LinkedListNode<EnemyBall> currentBall = enemyBall;
                        while (nextBall != null && nextBall.Value != null && GeometryCalculator.DistanceBetweenPoints(currentBall.Value.Coordinates, nextBall.Value.Coordinates) < ( width - ( width / 15f ) ))
                        {
                            nextBall.Value.IsFrozen = false;
                            currentBall = nextBall;
                            nextBall = nextBall.Next;
                        }
                    }
                    else
                    {
                        while (distance >= ( width - ( width / 15f ) ) && !enemyBall.Next.Value.IsFrozen)
                        {
                            enemyBall.Value.Move(speed, rotationSpeed);
                            distance = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, nextBallCoordinates);
                        }
                    }
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
