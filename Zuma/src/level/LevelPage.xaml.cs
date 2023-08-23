using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zuma.src.balls;
using Zuma.src.frog;

namespace Zuma.src.level
{
    /// <summary>
    /// Interaction logic for LevelPage.xaml
    /// </summary>
    public partial class LevelPage : Page
    {
        public LevelViewModel ViewModel { get; private set; }
        public FrogViewModel FrogViewModel { get; private set; }
        private string fileName;
        private readonly string folderPath = "C:\\Users\\Artem\\source\\repos\\Zuma\\Zuma\\resources\\levels";

        public LevelPage(Level level)
        {
            InitializeComponent();

            ViewModel = new LevelViewModel(level, LevelCanvas);

            DataContext = ViewModel;

            InitializeFrog(new FrogViewModel(level.Frog));

            MouseMove += OnMouseMoveWithCanvas;
            MouseLeftButtonDown += OnMouseClick;
            MouseRightButtonDown += OnMouseRightClick;

            InitFileName();
        }

        private void InitFileName()
        {
            // Get all the filenames in the folder that match the pattern "level_*.txt"
            string[] fileNames = Directory.GetFiles(folderPath, "levelPath_*.txt");

            int maxNumber = -1;
            string maxFileName = null;

            foreach (string fileName in fileNames)
            {
                string[] parts = Path.GetFileNameWithoutExtension(fileName).Split('_');
                if (parts.Length == 2 && int.TryParse(parts[1], out int number))
                {
                    if (number > maxNumber)
                    {
                        maxNumber = number;
                        maxFileName = fileName;
                    }
                }
            }

            fileName = maxNumber > -1 ? $"levelPath_{maxNumber + 1}.txt" : "levelPath_1.txt";
        }

        private void InitializeFrog(FrogViewModel frogViewModel)
        {
            FrogViewModel = frogViewModel;
            var frogControl = new FrogControl(frogViewModel);

            Canvas.SetLeft(frogControl, ViewModel.FrogCoordinates.X);
            Canvas.SetTop(frogControl, ViewModel.FrogCoordinates.Y);

            LevelCanvas.Children.Add(frogControl);
        }

        private void OnMouseMoveWithCanvas(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            ViewModel.RotateFrog(currentMousePosition, FrogViewModel);
        }

        private bool hasStarted = false;

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!hasStarted)
            {
                ViewModel.Start();
                hasStarted = true;
            }
            else
            {
                Point mouseCoordinates = e.GetPosition(LevelCanvas);

                PlayerBall ball = FrogViewModel.PrepareCurrentBallForShooting(mouseCoordinates);
                ViewModel.ShootBall(mouseCoordinates, ball);

                FrogViewModel.HandleShot(mouseCoordinates);
            }
        }

        private void OnMouseRightClick(object sender, MouseEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            var writer = new StreamWriter(folderPath + "\\" + fileName, true);
            using (writer)
            {
                writer.WriteLine($"new Point({currentMousePosition.X}, {currentMousePosition.Y}),");
            }
        }
    }
}
