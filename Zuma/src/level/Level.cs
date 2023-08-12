using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Zuma.src.level
{
    public class Level
    {
        public string Name { get; private set; }
        public int Number { get; private set; }
        public ImageBrush Background { get; private set; }
        public LevelCoordinates Coordinates { get; private set; }
        public DispatcherTimer LevelTicker { get; private set; }

        public Level(string name, int number, Uri backgroundImageURI, LevelCoordinates coordinates)
        {
            Name = name;
            Number = number;
            Background = new ImageBrush(new BitmapImage(backgroundImageURI));

            LevelTicker = new DispatcherTimer();
            ConfigureTicker();
            Coordinates = coordinates;
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
