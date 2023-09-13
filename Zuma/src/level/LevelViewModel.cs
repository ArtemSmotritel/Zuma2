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
        private readonly Level model;
        private readonly Canvas levelCanvas;

        public Path Path => model.Path;
        public Point FrogCoordinates => model.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(model.Background);

        public int Score => model.Score;
        public string ScoreLabel => $"Score: {Score}";

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

        public bool CanShootBall => model.CanShootBall;

        public LevelViewModel(Level level, Canvas levelCanvas)
        {
            model = level;
            this.levelCanvas = levelCanvas;
            Name = $"Level {level.Number}: {level.Name}";

            level.RegisterGameTickHandler(GameTick);
            level.RegisterGameTickHandler(DrawPath);
        }

        public void Pause()
        {
            model.Stop();

            MessageBoxResult result = MessageBox.Show("You've paused the game. Do you wish to exit the model?", "Are you serious?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);

            if (result == MessageBoxResult.No)
            {
                model.Start();
                return;
            }

            if (result == MessageBoxResult.Yes)
            {
                View.GoToLevelSelectionPage();
            }
        }

        public void RotateFrog(Point mouseCoordinates, FrogViewModel frogViewModel)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogViewModel.RotationAngel = Utils.AddAngels(angel, 90);
        }

        public void Start() => model.Start();
        public bool IsLevelActive => model.IsLevelActive;

        private void RemovePlayerBall(AbstractPlayerBall playerBall, int index)
        {
            levelCanvas.Children.Remove(playerBall.View);
            model.PlayerBalls.RemoveAt(index);
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

            for (int i = 0; i < model.PlayerBalls.Count; i++)
            {
                AbstractPlayerBall ball = model.PlayerBalls[i];

                if (ShouldRemoveBall(ball))
                {
                    RemovePlayerBall(ball, i);
                }
            }

            if (model.HasPlayerWon())
            {
                MessageBox.Show($"You've won. Congrats!\nYour Score: {Score}", "Serious result!");
                model.HandleGameWin();
                View.GoToLevelSelectionPage();
                return;
            }

            int scoreBefore = Score;
            bool hasAnyBallReachedDestination = model.MoveBalls(levelCanvas);
            if (scoreBefore != Score)
            {
                OnPropertyChanged(nameof(ScoreLabel));
            }

            if (hasAnyBallReachedDestination)
            {
                MessageBox.Show($"You've lost. Better luck next time!\nYour Score: {Score}", "Not so serious result!");
                model.HandleGameLose();
                View.GoToLevelSelectionPage();
                return;
            }

            model.MovePlayerBalls();

            if (model.ShouldGenerateEnemyBall())
            {
                AbstractEnemyBall newEnemyBall = model.GenerateEnemyBall();

                Canvas.SetLeft(newEnemyBall.View, newEnemyBall.Coordinates.X);
                Canvas.SetTop(newEnemyBall.View, newEnemyBall.Coordinates.Y);
                levelCanvas.Children.Add(newEnemyBall.View);
            }
        }

        public void ShootBall(Point mouseCoordinates, AbstractPlayerBall ball)
        {
            model.PlayerBalls.Add(ball);

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
                model.RemoveGameTickHandler(DrawPath);
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
