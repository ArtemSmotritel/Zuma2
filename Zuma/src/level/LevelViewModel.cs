using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zuma.models;
using Zuma.src.balls;
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

        private int generatedBallsCount = 0;
        private EnemyBall lastGeneratedEnemyBall;

        public Path Path => level.Path;
        public Point FrogCoordinates => level.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(level.Background);
        public LinkedList<EnemyBall> EnemyBalls { get; private set; }
        public List<PlayerBall> PlayerBalls { get; private set; }
        public List<PlayerBall> PlayerBallsToRemove { get; private set; }
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
            PlayerBallsToRemove = new List<PlayerBall>();

            level.LevelTicker.Tick += GameTick;
            Start();
        }

        public void RotateFrog(Point mouseCoordinates, FrogViewModel frogViewModel)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogViewModel.RotationAngel = Utils.AddAngels(angel, 80);
        }

        public void Start() => level.Start();

        private bool MoveEnemyBalls()
        {
            LinkedListNode<EnemyBall> theLastBall = EnemyBalls.Last;
            return levelController.MoveBalls(theLastBall, PlayerBalls, levelCanvas);
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
            levelCanvas.Children.Remove(playerBall.view);
            PlayerBalls.RemoveAt(index);
        }

        private bool ShouldRemoveBall(PlayerBall playerBall)
        {
            Point coordinates = playerBall.Coordinates;
            return coordinates.X < -playerBall.width || coordinates.Y < -playerBall.height || coordinates.X > 1600 || coordinates.Y > 1000;
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

            if (EnemyBalls.Count == 0 && generatedBallsCount >= level.EnemyBallsTotalCount)
            {
                // add logic for game victory;
                MessageBox.Show("You've won. Congrats!", "Serious result!");
                level.LevelTicker.Stop();
                return;
            }

            bool hasAnyBallReachedDestination = MoveEnemyBalls();

            if (hasAnyBallReachedDestination)
            {
                MessageBox.Show("You've lost. Better luck next time!", "Not so serious result!");
                level.LevelTicker.Stop();
                return;
            }

            if (PlayerBalls.Count > 0)
            {
                MovePlayerBalls();
            }

            if (lastGeneratedEnemyBall == null || ( generatedBallsCount < level.EnemyBallsTotalCount && IsLastGeneratedBallFarEnough() ))
            {
                lastGeneratedEnemyBall = levelController.GenerateBall(level, EnemyBalls);
                EnemyBalls.AddFirst(lastGeneratedEnemyBall);
                Canvas.SetLeft(lastGeneratedEnemyBall.view, lastGeneratedEnemyBall.Coordinates.X);
                Canvas.SetTop(lastGeneratedEnemyBall.view, lastGeneratedEnemyBall.Coordinates.Y);
                levelCanvas.Children.Add(lastGeneratedEnemyBall.view);
                generatedBallsCount++;
            }
        }

        private bool IsLastGeneratedBallFarEnough() => GeometryCalculator.IsDistanceGreaterOrEqual(lastGeneratedEnemyBall.Coordinates, level.Path.Start, lastGeneratedEnemyBall.width);

        public void ShootBall(Point mouseCoordinates, PlayerBall ball)
        {
            PlayerBalls.Add(ball);

            Canvas.SetLeft(ball.view, ball.Coordinates.X);
            Canvas.SetTop(ball.view, ball.Coordinates.Y);
            levelCanvas.Children.Add(ball.view);
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
