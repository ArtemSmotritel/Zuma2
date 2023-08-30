using System;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zuma.models;
using Zuma.src.frog;

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
        public int GeneratedEnemyBallsTotalCount { get; set; }
        private DispatcherTimer LevelTicker { get; set; }

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
            StartEnemyBallsCount = (int) Math.Floor(enemyBallsTotalCount * 1f);
        }

        public void RegisterGameTickHandler(EventHandler handler) => LevelTicker.Tick += handler;

        public bool IsLevelActive => LevelTicker.IsEnabled;

        public void Start() => LevelTicker.Start();

        public void Stop() => LevelTicker.Stop();

        public void HandleGameWin() => Stop();
        public void HandleGameLose() => Stop();

        public bool ShouldContinueGenerateWithStartingSpeed() => GeneratedEnemyBallsTotalCount < StartEnemyBallsCount;
        public bool HadGeneratedEnoughBalls() => GeneratedEnemyBallsTotalCount >= EnemyBallsTotalCount;
        public bool ShouldGeneratedMoreBalls() => GeneratedEnemyBallsTotalCount < EnemyBallsTotalCount;

        private void ConfigureTicker() => LevelTicker.Interval = TimeSpan.FromMilliseconds(20);
    }
}
