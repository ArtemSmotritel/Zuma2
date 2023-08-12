using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zuma.src.frog;

namespace Zuma.src.level
{
    /// <summary>
    /// Interaction logic for LevelPage.xaml
    /// </summary>
    public partial class LevelPage : Page
    {
        public LevelViewModel ViewModel { get; private set; }

        public LevelPage(Level level)
        {
            InitializeComponent();

            ViewModel = new LevelViewModel(level, LevelCanvas);

            DataContext = ViewModel;

            InitializeFrog();

            MouseMove += OnMouseMoveWithCanvas;
            MouseLeftButtonDown += OnMouseClick;
        }

        private void InitializeFrog()
        {
            var frog = new FrogControl(ViewModel.FrogViewModel);
            Canvas.SetLeft(frog, ViewModel.FrogCoordinates.X);
            Canvas.SetTop(frog, ViewModel.FrogCoordinates.Y);

            ViewModel.FrogControl = frog;

            LevelCanvas.Children.Add(frog);
        }

        private void OnMouseMoveWithCanvas(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            ViewModel.RotateFrog(currentMousePosition);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            ViewModel.ShootBall(currentMousePosition);
        }
    }
}
