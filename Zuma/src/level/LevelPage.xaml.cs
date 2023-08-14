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
        public FrogControl FrogControl { get; private set; }

        public LevelPage(Level level)
        {
            InitializeComponent();

            ViewModel = new LevelViewModel(level, LevelCanvas);

            DataContext = ViewModel;

            InitializeFrog(new FrogViewModel(level.Frog));

            MouseMove += OnMouseMoveWithCanvas;
            MouseLeftButtonDown += OnMouseClick;
        }

        private void InitializeFrog(FrogViewModel frogViewModel)
        {
            FrogControl = new FrogControl(frogViewModel);

            Canvas.SetLeft(FrogControl, ViewModel.FrogCoordinates.X);
            Canvas.SetTop(FrogControl, ViewModel.FrogCoordinates.Y);

            LevelCanvas.Children.Add(FrogControl);
        }

        private void OnMouseMoveWithCanvas(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            ViewModel.RotateFrog(currentMousePosition, FrogControl);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            ViewModel.ShootBall(currentMousePosition);
        }
    }
}
