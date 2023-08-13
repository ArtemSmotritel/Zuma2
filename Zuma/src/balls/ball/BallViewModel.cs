using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Zuma.src.balls.ball
{
    public class BallViewModel : INotifyPropertyChanged
    {
        private readonly Ball ball;

        public int Width => 25;
        public int Height => 25;
        public ImageBrush Sprite => new ImageBrush(ball.Sprite);
        public Point Coordinates
        {
            get => ball.Coordinates;
            set
            {
                ball.Coordinates = value;
                OnPropertyChanged(nameof(Coordinates));
            }
        }

        public BallViewModel(Ball ball)
        {
            this.ball = ball;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
