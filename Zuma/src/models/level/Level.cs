using System;
using System.Windows.Threading;

namespace Zuma.src.models.level
{
    public class Level
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public DispatcherTimer LevelTicker { get; private set; }

        public Level(string name, int number)
        {
            Name = name;
            Number = number;

            LevelTicker = new DispatcherTimer();
            ConfigureTicker();
        }

        public void Start()
        {
            LevelTicker.Start();
        }

        public void Stop()
        {
            LevelTicker.Stop();
        }

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
