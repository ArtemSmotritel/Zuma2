using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Zuma.src.balls;
using Zuma.src.balls.enemy_balls;
using Zuma.src.balls.player_balls;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelController
    {
        public AbstractEnemyBall GenerateEnemyBall(Level level) => BallGenerator.GenerateEnemyBall(level);

        public bool MoveBalls(LinkedListNode<AbstractEnemyBall> enemyBall, List<AbstractPlayerBall> playerBalls, Canvas levelCanvas, bool shouldUseStartingSpeed, Level level)
        {
            while (enemyBall != null && enemyBall.Value != null)
            {
                for (int i = 0; i < playerBalls.Count; i++)
                {
                    AbstractPlayerBall playerBall = playerBalls[i];

                    if (GeometryCalculator.AreBallsTouching(playerBall, enemyBall.Value))
                    {
                        playerBall.OnCollision(enemyBall, levelCanvas, playerBalls);
                        enemyBall.List.First.Value.IsSlowed = false;
                    }
                }

                if (enemyBall.Value.IsFrozen)
                {
                    enemyBall = enemyBall.Next;
                    continue;
                }

                float speed;
                float rotationSpeed;
                if (enemyBall.Previous == null || enemyBall.Previous.Value == null)
                {
                    speed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingSpeed() : enemyBall.Value.GetNormalSpeed();
                    rotationSpeed = shouldUseStartingSpeed ? enemyBall.Value.GetStartingRotationSpeed() : enemyBall.Value.GetNormalRotationSpeed();

                    if (enemyBall.Value.IsSlowed)
                    {
                        speed = enemyBall.Value.GetSlowedSpeed();
                        rotationSpeed = enemyBall.Value.GetSlowedRotationSpeed();
                    }

                    enemyBall.Value.Move(speed, rotationSpeed);

                    enemyBall = enemyBall.Next;
                    continue;
                }

                speed = enemyBall.Value.GetCollisionSpeed();
                rotationSpeed = enemyBall.Value.GetCollisionRotationSpeed();
                if (( enemyBall.Next == null || enemyBall.Next.Value == null ) && enemyBall.Value.HasReachedDestination(speed))
                {
                    return true;
                }

                if (enemyBall.Value.IsAdjusting && enemyBall.Value.HasReachedDestination(enemyBall.Value.GetAdjustingSpeed()))
                {
                    bool areBallsAffected = FinishBallAdjustment(enemyBall, levelCanvas, level);

                    if (areBallsAffected)
                    {
                        return false;
                    }
                }

                if (enemyBall.Value.IsAdjusting)
                {
                    enemyBall.Value.Move(enemyBall.Value.GetAdjustingSpeed(), enemyBall.Value.GetAdjustingRotationSpeed());
                    enemyBall = enemyBall.Next;
                    continue;
                }

                double distanceBetweenBalls = GeometryCalculator.DistanceBetweenPoints(enemyBall.Value.Coordinates, enemyBall.Previous.Value.Coordinates);
                if (distanceBetweenBalls < enemyBall.Value.collisionWidth)
                {
                    do
                    {
                        enemyBall.Value.Move(speed, rotationSpeed);
                        if (( enemyBall.Next == null || enemyBall.Next.Value == null ) && enemyBall.Value.HasReachedDestination(speed))
                        {
                            return true;
                        }
                    } while (GeometryCalculator.AreBallsTouching(enemyBall.Value, enemyBall.Previous.Value));
                }
                else if (distanceBetweenBalls < enemyBall.Value.Width)
                {
                    enemyBall.Value.Rotate(enemyBall.Value.GetCollisionRotationSpeed());
                }

                enemyBall = enemyBall.Next;
            }

            return false;
        }

        private bool FinishBallAdjustment(LinkedListNode<AbstractEnemyBall> enemyBall, Canvas levelCanvas, Level level)
        {
            float time = enemyBall.Previous != null && enemyBall.Previous.Value != null
                    ? enemyBall.Previous.Value.GetPathTime() + enemyBall.Previous.Value.GetStartingSpeed()
                    : enemyBall.Next.Value.GetPathTime() - enemyBall.Next.Value.GetStartingSpeed();

            enemyBall.Value.FinishAdjustment(level.Path, time);

            LinkedListNode<AbstractEnemyBall> prevBall = ( enemyBall.Previous?.Value?.IsAdjusting ?? false ) ? enemyBall.Previous?.Previous : enemyBall.Previous;

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

            int score = RunBallsCheckAndApplyEffect(enemyBall.List.First, levelCanvas);
            level.IncreaseScore(score);

            return score != 0;
        }

        private int RunBallsCheckAndApplyEffect(LinkedListNode<AbstractEnemyBall> enemyBall, Canvas levelCanvas)
        {
            int totalScore = 0;

            while (enemyBall != null && enemyBall.Value != null)
            {
                int scoreFromBall = CheckBallAndApplyEffect(enemyBall, levelCanvas);
                totalScore += scoreFromBall;

                enemyBall = enemyBall.Next;
            }

            return totalScore;
        }

        private int CheckBallAndApplyEffect(LinkedListNode<AbstractEnemyBall> enemyBall, Canvas levelCanvas)
        {
            if (!enemyBall.Value.ShouldTriggerEffect)
            {
                return 0;
            }

            List<LinkedListNode<AbstractEnemyBall>> ballsToApplyEffectFor = GetBallsWithSameColor(enemyBall);
            enemyBall.Value.ShouldTriggerEffect = false;

            if (ballsToApplyEffectFor.Count < 3)
            {
                return 0;
            }

            foreach (LinkedListNode<AbstractEnemyBall> ball in ballsToApplyEffectFor)
            {
                ball.Value.TriggerEffect(levelCanvas, ball);
            }

            return (int) Math.Pow(2, ballsToApplyEffectFor.Count);
        }

        private List<LinkedListNode<AbstractEnemyBall>> GetBallsWithSameColor(LinkedListNode<AbstractEnemyBall> enemyBall)
        {
            BallColor color = enemyBall.Value.color;

            var list = new List<LinkedListNode<AbstractEnemyBall>>();
            LinkedListNode<AbstractEnemyBall> node = enemyBall;

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
