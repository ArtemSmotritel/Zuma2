using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.balls.player_balls;

namespace Zuma.src.balls.enemy_balls
{
    public class CommonEnemyBall : AbstractEnemyBall
    {
        public CommonEnemyBall(Path path, BallColor color) : base(path, color)
        {
        }

        public CommonEnemyBall(AbstractPlayerBall playerBall, Path path, float pathTime) : base(playerBall, path, pathTime)
        {
        }

        public override void TriggerEffect(Canvas levelCanvas, LinkedListNode<AbstractEnemyBall> currentBall) => RemoveFromLevel(levelCanvas, currentBall);

        protected override UIElement CreateView(BallColor color)
        {
            Height = 40;
            Width = 40;
            Sprite = new BitmapImage(GetSpritePath(color));

            return new System.Windows.Shapes.Ellipse()
            {
                Height = Height,
                Width = Width,
                Fill = new System.Windows.Media.ImageBrush(Sprite)
            };
        }
    }
}
