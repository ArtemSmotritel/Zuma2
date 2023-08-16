using System;
using System.Collections.Generic;
using Zuma.src.balls;
using Zuma.src.models.balls;

namespace Zuma.src.level
{
    public class LevelController
    {
        public bool MoveBalls(LinkedListNode<MovingBall> sublistHeadBall)
        {
            while (sublistHeadBall != null && sublistHeadBall.Value != null)
            {
                if (sublistHeadBall.Value.HasReachedDestination())
                {
                    return true;
                }
                else
                {
                    sublistHeadBall.Value.Move(sublistHeadBall);
                }

                sublistHeadBall = sublistHeadBall.Next;
            }

            return false;
        }

        public EnemyBall GenerateBall(Level level, LinkedList<MovingBall> MovingBalls)
        {
            (Uri spriteUri, string type) = GetRandomBallSpriteAndType();
            var ball = new EnemyBall(
                level.Path.Start,
                spriteUri,
                level.Path
                );
            MovingBalls.AddFirst(ball);
            return ball;
        }

        private (Uri, string) GetRandomBallSpriteAndType() => (new Uri("pack://application:,,,/resources/images/balls/blue_ball_1.png"), "");
    }
}
