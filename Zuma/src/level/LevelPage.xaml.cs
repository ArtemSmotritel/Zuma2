﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zuma.src.balls.player_balls;
using Zuma.src.frog;
using Zuma.src.level_creators;

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

        public LevelPage(LevelCreator levelCreator)
        {
            InitializeComponent();

            Level level = levelCreator.Create();

            ViewModel = new LevelViewModel(level, LevelCanvas)
            {
                View = this
            };

            DataContext = ViewModel;

            InitializeFrog(new FrogViewModel(level.Frog));

            MouseMove += OnMouseMoveWithCanvas;
            MouseLeftButtonDown += OnMouseClick;
            MouseRightButtonDown += OnMouseRightClick;
            KeyDown += OnKeyDown;

            InitFileName();
            Focus();
        }

        public void GoToLevelSelectionPage() => NavigationService.GoBack();

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                GoToLevelSelectionPage();
            }
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

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!ViewModel.IsLevelActive)
            {
                ViewModel.Start();
            }
            else if (ViewModel.CanShootBall)
            {
                Point mouseCoordinates = e.GetPosition(LevelCanvas);

                Point start = FrogViewModel.FrogRectangle.TranslatePoint(new Point(20, 20), LevelCanvas);
                AbstractPlayerBall ball = FrogViewModel.PrepareCurrentBallForShooting(start, mouseCoordinates);
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

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                ViewModel.Pause();
            }
        }
    }
}
