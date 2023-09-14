using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zuma.models;

namespace Zuma.src.balls.enemy_balls
{
    public class ExplodingEnemyBall : AbstractEnemyBall
    {
        public ExplodingEnemyBall(Path path, BallColor color) : base(path, color)
        {
            SpecialEffectBitmapSprite = new BitmapImage(new Uri("pack://application:,,,/resources/images/balls/effects/bomb.png"));
        }

        protected override UIElement CreateView(BallColor color)
        {
            Height = 40;
            Width = 40;
            Sprite = new BitmapImage(GetSpritePath(color));

            var viewModel = new EnemyBallViewModel(this, 40, 30);
            return new EnemyBallView(viewModel);
        }

        public override void TriggerEffect(Canvas levelCanvas, LinkedListNode<AbstractEnemyBall> currentBall)
        {
            List<LinkedListNode<AbstractEnemyBall>> ballsBefore = GetAtMostFourBallsBefore(currentBall);
            List<LinkedListNode<AbstractEnemyBall>> ballsAhead = GetAtMostFourBallsAhead(currentBall);

            TriggerEffectInBalls(levelCanvas, ballsBefore);
            TriggerEffectInBalls(levelCanvas, ballsAhead);
            RemoveFromLevel(levelCanvas, currentBall);
        }

        private List<LinkedListNode<AbstractEnemyBall>> GetAtMostFourBallsBefore(LinkedListNode<AbstractEnemyBall> currentBall)
        {
            var result = new List<LinkedListNode<AbstractEnemyBall>>(4);

            LinkedListNode<AbstractEnemyBall> prevBall = currentBall?.Previous;
            while (result.Count < 4 && prevBall != null && prevBall.Value != null)
            {
                result.Add(prevBall);
                prevBall = prevBall.Previous;
            }

            return result;
        }

        private List<LinkedListNode<AbstractEnemyBall>> GetAtMostFourBallsAhead(LinkedListNode<AbstractEnemyBall> currentBall)
        {
            var result = new List<LinkedListNode<AbstractEnemyBall>>(4);

            LinkedListNode<AbstractEnemyBall> nextBall = currentBall?.Next;
            while (result.Count < 4 && nextBall != null && nextBall.Value != null)
            {
                result.Add(nextBall);
                nextBall = nextBall.Next;
            }

            return result;
        }

        private void TriggerEffectInBalls(Canvas levelCanvas, List<LinkedListNode<AbstractEnemyBall>> balls)
        {
            foreach (LinkedListNode<AbstractEnemyBall> ball in balls)
            {
                if (ball.Value != null && !ball.Value.IsDisposed && !ball.Value.IsEffectApplying)
                {
                    ball.Value.IsEffectApplying = true;
                    ball.Value.TriggerEffect(levelCanvas, ball);
                }
            }
        }
    }
}
