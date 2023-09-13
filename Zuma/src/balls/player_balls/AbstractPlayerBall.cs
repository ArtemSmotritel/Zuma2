using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.balls.enemy_balls;

namespace Zuma.src.balls.player_balls
{
    public abstract class AbstractPlayerBall : AbstractBall
    {
        public AbstractPlayerBall(BallColor color) : base(Path.EMPTY, color)
        {
        }

        public override float GetNormalRotationSpeed() => 0;
        public override float GetNormalSpeed() => 0.02f;
        protected override UIElement CreateView(BallColor color)
        {
            Height = 40;
            Width = 40;
            Sprite = new BitmapImage(GetSpritePath(color));

            return new System.Windows.Shapes.Ellipse()
            {
                Height = Height,
                Width = Width,
                Fill = new ImageBrush(Sprite)
            };
        }

        public void SetPath(Path path)
        {
            this.path = path;
            Coordinates = path.Start;
        }

        public abstract (bool, LinkedListNode<AbstractEnemyBall>) OnCollision(LinkedListNode<AbstractEnemyBall> enemyBall, Canvas levelCanvas, List<AbstractPlayerBall> playerBalls);
    }
}
