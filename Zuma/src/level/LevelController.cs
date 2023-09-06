using System.Collections.Generic;
using System.Windows.Controls;
using Zuma.src.balls;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelController
    {
        public bool MoveBallss(LinkedListNode<EnemyBall> enemyBall, List<PlayerBall> playerBalls, Canvas levelCanvas, bool shouldUseStartingSpeed, Level level)
        {
            while (enemyBall != null && enemyBall.Value != null)
            {
                for (int i = 0; i < playerBalls.Count; i++)
                {
                    PlayerBall playerBall = playerBalls[i];

                    if (GeometryCalculator.AreBallsTouching(playerBall, enemyBall.Value))
                    {
                        playerBall.OnCollision(enemyBall, levelCanvas, playerBalls);
                    }
                }

                if (enemyBall.Value.IsFrozen)
                {
                    enemyBall = enemyBall.Next;
                    continue;
                }

                if (enemyBall.Previous == null || enemyBall.Previous.Value == null)
                {
                    float speeed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingSpeed() : enemyBall.Value.GetNormalSpeed();
                    float rotationSpeeed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingRotationSpeed() : enemyBall.Value.GetNormalRotationSpeed();

                    enemyBall.Value.Move(speeed, rotationSpeeed);

                    enemyBall = enemyBall.Next;
                    continue;
                }

                float speed = enemyBall.Value.GetCollisionSpeed();
                if (( enemyBall.Next == null || enemyBall.Next.Value == null ) && enemyBall.Value.HasReachedDestination(speed))
                {
                    return true;
                }

                float rotationSpeed = enemyBall.Value.GetCollisionRotationSpeed();

                if (enemyBall.Value.IsAdjusting && enemyBall.Value.HasReachedDestination(enemyBall.Value.GetStartingSpeed() * 10))
                {
                    float time = enemyBall.Previous != null && enemyBall.Previous.Value != null
                        ? enemyBall.Previous.Value.GetPathTime() + enemyBall.Previous.Value.GetStartingSpeed()
                        : enemyBall.Next.Value.GetPathTime() - enemyBall.Next.Value.GetStartingSpeed();

                    enemyBall.Value.FinishAdjustment(level.Path, time);

                    LinkedListNode<EnemyBall> prevBall = ( enemyBall.Previous?.Value?.IsAdjusting ?? false ) ? enemyBall.Previous?.Previous : enemyBall.Previous;

                    if (prevBall != null && prevBall.Value != null)
                    {
                        prevBall.Value.IsFrozen = false;
                        prevBall = prevBall.Previous;

                        while (prevBall != null && prevBall.Value != null && GeometryCalculator.AreBallsCloseEnough(prevBall.Value, prevBall.Next.Value))
                        {
                            prevBall.Value.IsFrozen = false;
                            prevBall = prevBall.Previous;
                        }
                    }

                    bool areBallsAffected = RunBallsCheckAndApplyEffect(enemyBall.List.First, levelCanvas);

                    if (areBallsAffected)
                    {
                        break;
                    }
                }

                if (enemyBall.Value.IsAdjusting)
                {
                    enemyBall.Value.Move(enemyBall.Value.GetStartingSpeed() * 10, enemyBall.Value.GetNormalRotationSpeed());
                    enemyBall = enemyBall.Next;
                    continue;
                }

                double distanceBetweenBalls = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, enemyBall.Previous.Value.Coordinates);
                if (distanceBetweenBalls < enemyBall.Value.collisionWidth)
                {
                    do
                    {
                        enemyBall.Value.Move(speed, rotationSpeed);
                    } while (GeometryCalculator.AreBallsTouching(enemyBall.Value, enemyBall.Previous.Value));
                }
                else if (distanceBetweenBalls < enemyBall.Value.width)
                {
                    enemyBall.Value.Rotate(enemyBall.Value.GetCollisionRotationSpeed());
                }

                enemyBall = enemyBall.Next;
            }

            return false;
        }

        public EnemyBall GenerateEnemyBall(Level level) => BallGenerator.GenerateEnemyBall(level);

        private bool RunBallsCheckAndApplyEffect(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas)
        {
            bool areBallsAffected = false;

            while (enemyBall != null && enemyBall.Value != null)
            {
                if (!enemyBall.Value.ShouldTriggerEffect)
                {
                    enemyBall = enemyBall.Next;
                    continue;
                }

                List<LinkedListNode<EnemyBall>> ballsToApplyEffectFor = GetBallsWithSameColor(enemyBall);

                if (ballsToApplyEffectFor.Count < 3)
                {
                    enemyBall.Value.ShouldTriggerEffect = false;
                    enemyBall = enemyBall.Next;
                    continue;
                }

                foreach (LinkedListNode<EnemyBall> ball in ballsToApplyEffectFor)
                {
                    ball.List.Remove(ball);
                    levelCanvas.Children.Remove(ball.Value.view);
                }

                areBallsAffected = true;

                enemyBall = enemyBall.Next;
            }

            return areBallsAffected;
        }

        private List<LinkedListNode<EnemyBall>> GetBallsWithSameColor(LinkedListNode<EnemyBall> enemyBall)
        {
            BallColor color = enemyBall.Value.color;

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
