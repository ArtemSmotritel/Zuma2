using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zuma.src.frog;
using Zuma.src.helpers;

namespace Zuma.src.level
{
    public class LevelViewModel : INotifyPropertyChanged
    {
        private readonly Level level;
        private readonly Canvas levelCanvas;

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

        public LevelViewModel(Level level, Canvas levelCanvas)
        {
            this.level = level;
            this.levelCanvas = levelCanvas;
            Name = $"Level {level.Number}: {level.Name}";
        }

        public void RotateFrog(Point mouseCoordinates)
        {
            double deltaX = mouseCoordinates.X - FrogCoordinates.X;
            double deltaY = mouseCoordinates.Y - FrogCoordinates.Y;

            double angel = Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI;

            //FrogViewModel.RotationAngel = angel;
            //Name = $"Mouse: {currentMousePosition};;\t Frog: {FrogPosition};;\t Angel: {angel}";
            FrogControl.SetRotationAngle(Utils.AddAngels(angel, 80));
        }

        public void ShootBall(Point mouseCoordinates)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
