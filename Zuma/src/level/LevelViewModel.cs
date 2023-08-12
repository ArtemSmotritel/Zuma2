using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Zuma.src.frog;

namespace Zuma.src.level
{
    public class LevelViewModel : INotifyPropertyChanged
    {
        private readonly Level level;

        public FrogControl FrogControl { get; set; }
        public FrogViewModel FrogViewModel => new FrogViewModel(level.Frog);
        public Point FrogCoordinates => level.Coordinates.Frog;

        public ImageBrush Background => new ImageBrush(level.Background);

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public LevelViewModel(Level level)
        {
            this.level = level;
            Name = $"Level {level.Number}: {level.Name}";
        }

        public void RotateFrog(Point mouseCoordinates)
        {
            double deltaX = mouseCoordinates.X - FrogCoordinates.X;
            double deltaY = mouseCoordinates.Y - FrogCoordinates.Y;

            double angel = Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI;

            //FrogViewModel.RotationAngel = angel;
            //Name = $"Mouse: {currentMousePosition};;\t Frog: {FrogPosition};;\t Angel: {angel}";
            FrogControl.SetRotationAngle(AddAngels(angel, 80));
        }

        private double AddAngels(double a1, double a2)
        {
            double sum = a1 + a2;
            return Math.Abs(sum) <= 360 ? sum : sum > 0 ? sum - 360 : sum + 360;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
