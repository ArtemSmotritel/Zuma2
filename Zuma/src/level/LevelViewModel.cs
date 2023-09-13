using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zuma.models;
using Zuma.src.balls.enemy_balls;
using Zuma.src.balls.player_balls;
using Zuma.src.frog;
using Zuma.src.helpers;
using Zuma.src.utils;

namespace Zuma.src.level
{
    public class LevelViewModel : Notifier
    {
        public LevelPage View { get; set; }
        private readonly Level level;
        private readonly Canvas levelCanvas;

        public Path Path => level.Path;
        public Point FrogCoordinates => level.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(level.Background);

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public bool CanShootBall => level.CanShootBall;

        public LevelViewModel(Level level, Canvas levelCanvas)
        {
            this.level = level;
            this.levelCanvas = levelCanvas;
            Name = $"Level {level.Number}: {level.Name}";

            level.RegisterGameTickHandler(GameTick);
            level.RegisterGameTickHandler(DrawPath);
        }

        public void Pause()
        {
            level.Stop();

            MessageBoxResult result = MessageBox.Show("You've paused the game. Do you wish to exit the level?", "Are you serious?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

            if (result == MessageBoxResult.No)
            {
                level.Start();
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                View.NavigationService.GoBack();
            }
        }

        public void RotateFrog(Point mouseCoordinates, FrogViewModel frogViewModel)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogViewModel.RotationAngel = Utils.AddAngels(angel, 80);
        }

        public void Start() => level.Start();
        public bool IsLevelActive => level.IsLevelActive;

        private void RemovePlayerBall(AbstractPlayerBall playerBall, int index)
        {
            levelCanvas.Children.Remove(playerBall.View);
            level.PlayerBalls.RemoveAt(index);
        }

        private bool ShouldRemoveBall(AbstractPlayerBall playerBall)
        {
            Point coordinates = playerBall.Coordinates;
            return coordinates.X < -playerBall.Width || coordinates.Y < -playerBall.Height || coordinates.X > 1600 || coordinates.Y > 1000;
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (!finishedDrawingPath)
            {
                return;
            }

            for (int i = 0; i < level.PlayerBalls.Count; i++)
            {
                AbstractPlayerBall ball = level.PlayerBalls[i];

                if (ShouldRemoveBall(ball))
                {
                    RemovePlayerBall(ball, i);
                }
            }

            if (level.HasPlayerWon())
            {
                // add logic for game victory;
                MessageBox.Show("You've won. Congrats!", "Serious result!");
                level.HandleGameWin();
                View.NavigationService.GoBack();
                return;
            }

            bool hasAnyBallReachedDestination = level.MoveBalls(levelCanvas);

            if (hasAnyBallReachedDestination)
            {
                MessageBox.Show("You've lost. Better luck next time!", "Not so serious result!");
                level.HandleGameLose();
                View.NavigationService.GoBack();
                return;
            }

            level.MovePlayerBalls();

            if (level.ShouldGenerateEnemyBall())
            {
                AbstractEnemyBall newEnemyBall = level.GenerateEnemyBall();

                Canvas.SetLeft(newEnemyBall.View, newEnemyBall.Coordinates.X);
                Canvas.SetTop(newEnemyBall.View, newEnemyBall.Coordinates.Y);
                levelCanvas.Children.Add(newEnemyBall.View);
            }
        }

        public void ShootBall(Point mouseCoordinates, AbstractPlayerBall ball)
        {
            level.PlayerBalls.Add(ball);

            Canvas.SetLeft(ball.View, ball.Coordinates.X);
            Canvas.SetTop(ball.View, ball.Coordinates.Y);
            levelCanvas.Children.Add(ball.View);
        }

        private float pathDrawingT = 0;
        private bool finishedDrawingPath = false;
        public void DrawPath(object sender, EventArgs e)
        {
            if (pathDrawingT == 0)
            {
                Point point = Path.Start;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.LightGreen, 50, 50);
            }

            if (!Path.HasReachedDestination(pathDrawingT + 0.01f))
            {
                Point point = Path.GetPosition(pathDrawingT);
                DrawPathPoint(point, Brushes.LightGreen);
            }
            else
            {
                Point point = Path.End;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.LightGreen, 50, 50);
                level.RemoveGameTickHandler(DrawPath);
                finishedDrawingPath = true;
            }

            pathDrawingT += 0.01f;
        }
        private void DrawPathPoint(Point p, Brush brush, int heigh = 50, int width = 50)
        {
            var ellipse = new System.Windows.Shapes.Ellipse
            {
                Height = heigh,
                Width = width,
                Fill = brush,
                Stroke = brush,
                Opacity = 0.5,
            };

            Canvas.SetLeft(ellipse, p.X);
            Canvas.SetTop(ellipse, p.Y);

            levelCanvas.Children.Add(ellipse);
        }
    }
}
