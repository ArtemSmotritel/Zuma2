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
        public DispatcherTimer LevelTicker { get; private set; }

        public Level(string name, int number, Uri backgroundImageURI, Frog frog, Path path)
        {
            Name = name;
            Number = number;
            Background = new BitmapImage(backgroundImageURI);
            Path = path;

            LevelTicker = new DispatcherTimer();
            ConfigureTicker();
            Frog = frog;
        }

        public void Start() => LevelTicker.Start();

        public void Stop() => LevelTicker.Stop();

        private void ConfigureTicker()
        {
            LevelTicker.Interval = TimeSpan.FromMilliseconds(16);
            LevelTicker.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {

        }
    }
}
