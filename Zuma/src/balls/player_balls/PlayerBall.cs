using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zuma.models;
using Zuma.src.balls.enemy_balls;
using Zuma.src.models.balls;

namespace Zuma.src.balls
{
    public abstract class PlayerBall : MovingBall
    {

        public PlayerBall(BallColor color) : base(Path.EMPTY, color)
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

        public abstract (bool, LinkedListNode<EnemyBall>) OnCollision(LinkedListNode<EnemyBall> enemyBall, Canvas levelCanvas, List<PlayerBall> playerBalls);
    }
}
