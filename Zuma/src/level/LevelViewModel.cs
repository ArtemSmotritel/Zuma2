using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zuma.models;
using Zuma.src.balls;
using Zuma.src.balls.enemy_balls;
using Zuma.src.frog;
using Zuma.src.helpers;
using Zuma.src.utils;

namespace Zuma.src.level
{
    public class LevelViewModel : Notifier
    {
        private readonly Level level;
        private readonly Canvas levelCanvas;
        private readonly LevelController levelController = new LevelController();

        private EnemyBall lastGeneratedEnemyBall;

        public Path Path => level.Path;
        public Point FrogCoordinates => level.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(level.Background);
        public LinkedList<EnemyBall> EnemyBalls { get; private set; }
        public List<PlayerBall> PlayerBalls { get; private set; }
        public bool CanShootBall => PlayerBalls.Count == 0;

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

        public LevelViewModel(Level level, Canvas levelCanvas)
        {
            this.level = level;
            this.levelCanvas = levelCanvas;
            Name = $"Level {level.Number}: {level.Name}";
            EnemyBalls = new LinkedList<EnemyBall>();
            PlayerBalls = new List<PlayerBall>();

            level.RegisterGameTickHandler(GameTick);
        }

        public void Pause() => level.Stop();

        public void RotateFrog(Point mouseCoordinates, FrogViewModel frogViewModel)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogViewModel.RotationAngel = Utils.AddAngels(angel, 80);
        }

        public void Start() => level.Start();
        public bool IsLevelActive => level.IsLevelActive;

        private bool MoveEnemyBalls()
        {
            LinkedListNode<EnemyBall> theLastBall = EnemyBalls.First;
            return levelController.MoveBalls(theLastBall, PlayerBalls, levelCanvas, level.ShouldContinueGenerateWithStartingSpeed(), level);
        }

        private void MovePlayerBalls()
        {
            for (int i = 0; i < PlayerBalls.Count; i++)
            {
                PlayerBall ball = PlayerBalls[i];
                ball.Move(ball.GetNormalSpeed(), ball.GetNormalRotationSpeed());
            }
        }

        private void RemovePlayerBall(PlayerBall playerBall, int index)
        {
            levelCanvas.Children.Remove(playerBall.View);
            PlayerBalls.RemoveAt(index);
        }

        private bool ShouldRemoveBall(PlayerBall playerBall)
        {
            Point coordinates = playerBall.Coordinates;
            return coordinates.X < -playerBall.Width || coordinates.Y < -playerBall.Height || coordinates.X > 1600 || coordinates.Y > 1000;
        }

        private void GameTick(object sender, EventArgs e)
        {
            for (int i = 0; i < PlayerBalls.Count; i++)
            {
                PlayerBall ball = PlayerBalls[i];

                if (ShouldRemoveBall(ball))
                {
                    RemovePlayerBall(ball, i);
                }
            }

            if (EnemyBalls.Count == 0 && level.HadGeneratedEnoughBalls())
            {
                // add logic for game victory;
                MessageBox.Show("You've won. Congrats!", "Serious result!");
                level.HandleGameWin();
                return;
            }

            bool hasAnyBallReachedDestination = MoveEnemyBalls();

            if (hasAnyBallReachedDestination)
            {
                MessageBox.Show("You've lost. Better luck next time!", "Not so serious result!");
                level.HandleGameLose();
                return;
            }

            if (PlayerBalls.Count > 0)
            {
                MovePlayerBalls();
            }

            if (lastGeneratedEnemyBall == null || ( level.ShouldGeneratedMoreBalls() && IsLastGeneratedBallFarEnough() ))
            {
                lastGeneratedEnemyBall = levelController.GenerateEnemyBall(level);
                EnemyBalls.AddFirst(lastGeneratedEnemyBall);
                Canvas.SetLeft(lastGeneratedEnemyBall.View, lastGeneratedEnemyBall.Coordinates.X);
                Canvas.SetTop(lastGeneratedEnemyBall.View, lastGeneratedEnemyBall.Coordinates.Y);
                levelCanvas.Children.Add(lastGeneratedEnemyBall.View);
                level.GeneratedEnemyBallsTotalCount++;
            }
        }

        private bool IsLastGeneratedBallFarEnough() => GeometryCalculator.IsDistanceGreaterOrEqual(lastGeneratedEnemyBall.Coordinates, level.Path.Start, lastGeneratedEnemyBall.Width);

        public void ShootBall(Point mouseCoordinates, PlayerBall ball)
        {
            PlayerBalls.Add(ball);

            Canvas.SetLeft(ball.View, ball.Coordinates.X);
            Canvas.SetTop(ball.View, ball.Coordinates.Y);
            levelCanvas.Children.Add(ball.View);
        }

        private float pathDrawingT = 0;
        public void DrawPath(object sender, EventArgs e)
        {
            if (pathDrawingT == 0)
            {
                Point point = Path.Start;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.Blue, 30, 30);
            }

            if (!Path.HasReachedDestination(pathDrawingT))
            {
                Point point = Path.GetPosition(pathDrawingT);
                DrawPathPoint(point, Brushes.DimGray);
                pathDrawingT += 0.02f;
            }
            else
            {
                Point point = Path.End;
                point.Y -= 15;
                DrawPathPoint(point, Brushes.Red, 30, 30);
            }
        }
        private void DrawPathPoint(Point p, Brush brush, int heigh = 15, int width = 15)
        {
            var rect = new System.Windows.Shapes.Rectangle
            {
                Height = heigh,
                Width = width,
                Fill = brush,
                Stroke = brush,
            };

            Canvas.SetLeft(rect, p.X);
            Canvas.SetTop(rect, p.Y);

            levelCanvas.Children.Add(rect);
        }
    }
}
