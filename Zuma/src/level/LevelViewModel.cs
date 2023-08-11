using System.ComponentModel;
using System.Windows.Media;

namespace Zuma.src.level
{
    public class LevelViewModel : INotifyPropertyChanged
    {
        private Level LevelModel { get; set; }

        public ImageBrush Background => LevelModel.Background;

        public LevelViewModel(Level level)
        {
            LevelModel = level;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
