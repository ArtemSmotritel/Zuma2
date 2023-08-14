using Zuma.src.utils;

namespace Zuma.src.balls.enemy_ball
{
    public class EnemyBallViewModel : Notifier
    {
        public EnemyBall Model { get; private set; }
        public EnemyBallControl View { get; private set; }

        public EnemyBallViewModel(EnemyBall model, EnemyBallControl view)
        {
            Model = model;
            View = view;
        }
    }
}
