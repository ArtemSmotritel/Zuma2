using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zuma.models;

namespace Zuma.src.balls.enemy_balls
{
    public class SlowingEnemyBall : AbstractEnemyBall
    {
        public SlowingEnemyBall(Path path, BallColor color) : base(path, color)
        {
            SpecialEffectBitmapSprite = new BitmapImage(new Uri("pack://application:,,,/resources/images/balls/effects/sand_clock.png"));
        }

        public override void TriggerEffect(Canvas levelCanvas, LinkedListNode<AbstractEnemyBall> currentBall)
        {
            currentBall.List.First.Value.IsSlowed = true;
            RemoveFromLevel(levelCanvas, currentBall);
        }

        protected override UIElement CreateView(BallColor color)
        {
            Height = 40;
            Width = 40;
            Sprite = new BitmapImage(GetSpritePath(color));

            var viewModel = new EnemyBallViewModel(this, 25, 25);
            return new EnemyBallView(viewModel);
        }
    }
}
