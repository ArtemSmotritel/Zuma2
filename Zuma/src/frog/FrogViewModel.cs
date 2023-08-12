﻿using System.ComponentModel;
using System.Windows.Media;
using Zuma.src.models.balls;

namespace Zuma.src.frog
{
    public class FrogViewModel : INotifyPropertyChanged
    {
        private readonly Frog frog;

        public bool IsPaused { get; set; }
        public int Width => 100;
        public int Height => 100;

        public int RotationCenterX => Width / 2;
        public int RotationCenterY => Height / 2;
        private double rotationAngel = 0;
        public double RotationAngel
        {
            get => rotationAngel;
            set
            {
                rotationAngel = value;
                OnPropertyChanged(nameof(RotationAngel));
            }
        }

        public ImageBrush Sprite => new ImageBrush(frog.Sprite);
        public AbstractBall CurrentBall => frog.CurrentBall;
        public AbstractBall NextBall => frog.NextBall;

        public FrogViewModel(Frog frog)
        {
            this.frog = frog;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
