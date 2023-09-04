using System.Collections.Generic;
using System.Windows.Controls;
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
                    if (distance < ball.collisionWidth)
                    {
                        (bool wereBallsRemoved2, LinkedListNode<EnemyBall> firstNotRemovedBall2) = ball.OnCollision(enemyBall, levelCanvas, playerBalls);
                        wereBallsRemoved = wereBallsRemoved2;
                        firstNotRemovedBall = firstNotRemovedBall2;

                        if (!wereBallsRemoved)
                        {
                            var enemy = new EnemyBall(ball, enemyBall.Value.GetPath(), enemyBall.Value.GetPathTime() - ( enemyBall.Value.GetNormalSpeed() * 2 ));
                            enemyBall.List.AddBefore(enemyBall, enemy);
                            levelCanvas.Children.Add(enemy.view);

                            float pathTime = enemy.GetPathTime();
                            System.Windows.Point position = enemy.GetPath().GetPosition(pathTime);

                            Canvas.SetTop(enemy.view, position.Y);
                            Canvas.SetLeft(enemy.view, position.X);
                        }
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

                if (enemyBall.Next?.Value == null)
                {
                    enemyBall.Value.Move(speed, rotationSpeed);
                    enemyBall = enemyBall.Previous;
                    continue;
                }

                System.Windows.Point nextBallCoordinates = enemyBall.Next.Value.Coordinates;
                float collisionWidth = enemyBall.Value.collisionWidth;

                double distanceToTheNextBall = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, nextBallCoordinates);

                if (enemyBall.Value.IsTemporarlyFirst)
                {
                    enemyBall.Value.Move(speed, rotationSpeed);

                    if (distanceToTheNextBall < collisionWidth)
                    {
                        enemyBall.Value.IsTemporarlyFirst = false;
                    }

                    enemyBall = enemyBall.Previous;
                    continue;
                }

                if (distanceToTheNextBall < collisionWidth)
                {
                    float collisionSpeed = enemyBall.Value.GetCollisionSpeed();
                    float collisionRotationSpeed = enemyBall.Value.GetCollisionRotationSpeed();

                    enemyBall.Value.Move(collisionSpeed, collisionRotationSpeed);

                    LinkedListNode<EnemyBall> nextBall = enemyBall.Next;
                    LinkedListNode<EnemyBall> currentBall = enemyBall;
                    while (nextBall != null && nextBall.Value != null && GeometryCalculator.DistanceBetweenPoints(currentBall.Value.Coordinates, nextBall.Value.Coordinates) < collisionWidth)
                    {
                        nextBall.Value.IsFrozen = false;
                        currentBall = nextBall;
                        nextBall = nextBall.Next;
                    }
                }
                else
                {
                    while (distanceToTheNextBall >= enemyBall.Value.collisionWidth && !enemyBall.Next.Value.IsFrozen)
                    {
                        enemyBall.Value.Move(speed, rotationSpeed);
                        distanceToTheNextBall = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, nextBallCoordinates);
                    }
                }

                enemyBall = enemyBall.Previous;
            }

            return false;
        }

        public EnemyBall GenerateEnemyBall(Level level, BallColor ballColor) => new EnemyBall(level.Path, ballColor);
        public EnemyBall GenerateEnemyBall(Level level) => BallGenerator.GenerateEnemyBall(level);
    }
}
