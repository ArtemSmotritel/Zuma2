using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zuma.models;
using Zuma.src.balls.enemy_balls;
using Zuma.src.balls.player_balls;
using Zuma.src.frog;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class Level
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public BitmapImage Background { get; private set; }
        public Frog Frog { get; private set; }
        public Path Path { get; private set; }
        private int EnemyBallsTotalCount { get; set; }
        private int StartEnemyBallsCount { get; set; }
        private int GeneratedEnemyBallsTotalCount { get; set; }
        private DispatcherTimer LevelTicker { get; set; }
        private LevelController LevelController { get; set; }

        private AbstractEnemyBall LastGeneratedEnemyBall { get; set; }
        public LinkedList<AbstractEnemyBall> EnemyBalls { get; private set; }
        public List<AbstractPlayerBall> PlayerBalls { get; private set; }

        public Level(string name, int number, Uri backgroundImageURI, Frog frog, Path path, int enemyBallsTotalCount)
        {
            Name = name;
            Number = number;
            Background = new BitmapImage(backgroundImageURI);
            Path = path;

            LevelTicker = new DispatcherTimer(DispatcherPriority.Normal);
            ConfigureTicker();
            Frog = frog;
            EnemyBallsTotalCount = enemyBallsTotalCount;
            GeneratedEnemyBallsTotalCount = 0;
            StartEnemyBallsCount = (int) Math.Floor(enemyBallsTotalCount * 0.2f);

            LevelController = new LevelController();
            EnemyBalls = new LinkedList<AbstractEnemyBall>();
            PlayerBalls = new List<AbstractPlayerBall>(2);
        }

        public void RegisterGameTickHandler(EventHandler handler) => LevelTicker.Tick += handler;
        public void RemoveGameTickHandler(EventHandler handler) => LevelTicker.Tick -= handler;

        public bool IsLevelActive => LevelTicker.IsEnabled;

        public void Start() => LevelTicker.Start();

        public void Stop() => LevelTicker.Stop();

        public void HandleGameWin() => Stop();
        public void HandleGameLose() => Stop();

        public AbstractEnemyBall GenerateEnemyBall()
        {
            AbstractEnemyBall ball = LevelController.GenerateEnemyBall(this);
            LastGeneratedEnemyBall = ball;
            GeneratedEnemyBallsTotalCount++;
            EnemyBalls.AddFirst(ball);

            return ball;
        }

        public bool ShouldGenerateEnemyBall() => LastGeneratedEnemyBall == null || ( ShouldGeneratedMoreBalls() && IsLastGeneratedBallFarEnough() );

        public bool ShouldContinueGenerateWithStartingSpeed() => GeneratedEnemyBallsTotalCount < StartEnemyBallsCount;

        public bool HasPlayerWon() => EnemyBalls.Count == 0 && GeneratedEnemyBallsTotalCount >= EnemyBallsTotalCount;

        public bool CanShootBall => PlayerBalls.Count == 0;

        public bool MoveBalls(Canvas levelCanvas)
        {
            LinkedListNode<AbstractEnemyBall> firstBall = EnemyBalls.First;
            return LevelController.MoveBalls(firstBall, PlayerBalls, levelCanvas, ShouldContinueGenerateWithStartingSpeed(), this);
        }

        public void MovePlayerBalls()
        {
            for (int i = 0; i < PlayerBalls.Count; i++)
            {
                AbstractPlayerBall ball = PlayerBalls[i];
                ball.Move(ball.GetNormalSpeed(), ball.GetNormalRotationSpeed());
            }
        }

        private bool ShouldGeneratedMoreBalls() => GeneratedEnemyBallsTotalCount < EnemyBallsTotalCount;
        private void ConfigureTicker() => LevelTicker.Interval = TimeSpan.FromMilliseconds(20);
        private bool IsLastGeneratedBallFarEnough() => LastGeneratedEnemyBall.IsDisposed || GeometryCalculator.IsDistanceGreaterOrEqual(LastGeneratedEnemyBall.Coordinates, Path.Start, LastGeneratedEnemyBall.Width);

    }
}
