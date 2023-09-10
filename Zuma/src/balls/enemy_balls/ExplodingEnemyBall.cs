using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zuma.models;

namespace Zuma.src.balls.enemy_balls
{
    public class ExplodingEnemyBall : EnemyBall
    {
        public ExplodingEnemyBall(Path path, BallColor color) : base(path, color)
        {
            SpecialEffectBitmapSprite = new BitmapImage(new Uri("pack://application:,,,/resources/images/balls/effects/bomb.png"));
        }

        public ExplodingEnemyBall(PlayerBall playerBall, Path path, float pathTime) : base(playerBall, path, pathTime)
        {
        }

        protected override UIElement CreateView(BallColor color)
        {
            Height = 40;
            Width = 40;
            Sprite = new BitmapImage(GetSpritePath(color));

            var viewModel = new EnemyBallViewModel(this, 40, 30);
            return new EnemyBallView(viewModel);
        }

        public override bool IsSpecial() => true;

        public override void TriggerEffect(Canvas levelCanvas, LinkedListNode<EnemyBall> currentBall)
        {
            List<LinkedListNode<EnemyBall>> ballsBefore = GetAtMostFourBallsBefore(currentBall);
            List<LinkedListNode<EnemyBall>> ballsAhead = GetAtMostFourBallsAhead(currentBall);

            TriggerEffectInBalls(levelCanvas, ballsBefore);
            TriggerEffectInBalls(levelCanvas, ballsAhead);
            base.TriggerEffect(levelCanvas, currentBall);
        }

        private List<LinkedListNode<EnemyBall>> GetAtMostFourBallsBefore(LinkedListNode<EnemyBall> currentBall)
        {
            var result = new List<LinkedListNode<EnemyBall>>(4);

            LinkedListNode<EnemyBall> prevBall = currentBall?.Previous;
            while (prevBall != null && prevBall.Value != null)
            {
                result.Add(prevBall);
                prevBall = prevBall.Previous;
            }

            return result;
        }

        private List<LinkedListNode<EnemyBall>> GetAtMostFourBallsAhead(LinkedListNode<EnemyBall> currentBall)
        {
            var result = new List<LinkedListNode<EnemyBall>>(4);

            LinkedListNode<EnemyBall> nextBall = currentBall?.Next;
            while (nextBall != null && nextBall.Value != null)
            {
                result.Add(nextBall);
                nextBall = nextBall.Next;
            }

            return result;
        }

        private void TriggerEffectInBalls(Canvas levelCanvas, List<LinkedListNode<EnemyBall>> balls)
        {
            foreach (LinkedListNode<EnemyBall> ball in balls)
            {
                if (ball.Value != null && !ball.Value.IsDisposed)
                {
                    ball.Value.TriggerEffect(levelCanvas, ball);
                }
            }
        }
    }
}
