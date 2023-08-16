using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zuma.models;
using Zuma.src.frog;
using Zuma.src.models.balls;

namespace Zuma.src.level
{
    public class Level
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public BitmapImage Background { get; private set; }
        public Frog Frog { get; private set; }
        public Path Path { get; private set; }
        public int EnemyBallsTotalCount { get; private set; }
        public ReadOnlyCollection<MovingBall> EnemyBalls { get; private set; }
        public DispatcherTimer LevelTicker { get; private set; }

        public Level(string name, int number, Uri backgroundImageURI, Frog frog, Path path, int enemyBallsTotalCount)
        {
            Name = name;
            Number = number;
            Background = new BitmapImage(backgroundImageURI);
            Path = path;

            LevelTicker = new DispatcherTimer();
            ConfigureTicker();
            Frog = frog;
            EnemyBallsTotalCount = enemyBallsTotalCount;
        }

        public void Start() => LevelTicker.Start();

        public void Stop() => LevelTicker.Stop();

        private void ConfigureTicker() => LevelTicker.Interval = TimeSpan.FromMilliseconds(16);
    }
}
