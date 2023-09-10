using System.Windows.Media.Imaging;
using Zuma.src.utils;

namespace Zuma.src.balls.enemy_balls
{
    public class EnemyBallViewModel : Notifier
    {
        private readonly EnemyBall model;
        public int SpecialEffectSpriteHeight { get; set; }
        public int SpecialEffectSpriteWidth { get; set; }

        public EnemyBallViewModel(EnemyBall enemyBall, int specialEffectSpriteWidth, int specialEffectSpriteHeight)
        {
            model = enemyBall;
            SpecialEffectSpriteHeight = specialEffectSpriteHeight;
            SpecialEffectSpriteWidth = specialEffectSpriteWidth;
        }

        public int Height => model.Height;
        public int Width => model.Width;
        public BitmapImage Sprite => model.Sprite;

        public BitmapImage SpecialEffectSprite => model.SpecialEffectBitmapSprite;

        public int RotationCenterX => model.Width / 2;
        public int RotationCenterY => model.Height / 2;
        public double RotationAngel => model.RotationAngel;

    }
}
