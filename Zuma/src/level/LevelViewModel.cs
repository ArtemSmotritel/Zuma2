using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zuma.models;
using Zuma.src.frog;
using Zuma.src.helpers;
using Zuma.src.models.balls;
using Zuma.src.utils;

namespace Zuma.src.level
{
    public class LevelViewModel : Notifier
    {
        private readonly Level level;
        private readonly Canvas levelCanvas;
        private readonly LevelController levelController = new LevelController();

        private int generatedBallsCount = 0;
        private MovingBall lastGeneratedEnemyBall;

        public Path Path => level.Path;
        public Point FrogCoordinates => level.Frog.Coordinates;
        public ImageBrush Background => new ImageBrush(level.Background);
        public LinkedList<MovingBall> MovingBalls { get; private set; }

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
            MovingBalls = new LinkedList<MovingBall>();
        }

        public void RotateFrog(Point mouseCoordinates, FrogControl frogControl)
        {
            double angel = GeometryCalculator.GetAngelBetweenTwoPoints(mouseCoordinates, FrogCoordinates);
            frogControl.SetRotationAngle(Utils.AddAngels(angel, 80));
        }

        public void Start() => level.Start();

        private bool MoveBalls()
        {
            LinkedListNode<MovingBall> theFirstBall = MovingBalls.First;
            return levelController.MoveBalls(theFirstBall);
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (MovingBalls.Count == 0 && generatedBallsCount >= level.EnemyBallsTotalCount)
            {
                // add logic for game victory;
                MessageBox.Show("You've won. Congrats!", "Serious result!");
                level.LevelTicker.Stop();
                return;
            }

            bool hasAnyBallReachedDestination = MoveBalls();

            if (hasAnyBallReachedDestination)
            {
                MessageBox.Show("You've lost. Better luck next time!", "Not so serious result!");
                level.LevelTicker.Stop();
                return;
            }

            if (lastGeneratedEnemyBall == null || ( generatedBallsCount < level.EnemyBallsTotalCount && IsLastGeneratedBallFarEnough() ))
            {
                lastGeneratedEnemyBall = levelController.GenerateBall(level, MovingBalls);
                Canvas.SetLeft(lastGeneratedEnemyBall.view, lastGeneratedEnemyBall.Coordinates.X);
                Canvas.SetTop(lastGeneratedEnemyBall.view, lastGeneratedEnemyBall.Coordinates.Y);
                levelCanvas.Children.Add(lastGeneratedEnemyBall.view);
                generatedBallsCount++;
            }
        }

        private bool IsLastGeneratedBallFarEnough()
        {
            return lastGeneratedEnemyBall != null
            && GeometryCalculator.IsDistanceGreaterOrEqual(lastGeneratedEnemyBall.Coordinates, level.Path.Start, lastGeneratedEnemyBall.width);
        }

        public void ShootBall(Point mouseCoordinates)
        {
            //var ballStartingPoint = new Point(FrogCoordinates.X + 50, FrogCoordinates.Y + 50);

            //var ballSprite = new BitmapImage(new System.Uri("pack://application:,,,/resources/images/balls/blue_ball_1.png"));
            //var ball = new Ball(ballSprite, ballStartingPoint);
            //var ballControl = new BallControl(new BallViewModel(ball));

            //Canvas.SetLeft(ballControl, ballStartingPoint.X);
            //Canvas.SetTop(ballControl, ballStartingPoint.Y);

            //levelCanvas.Children.Add(ballControl);
            //level.LevelTicker.Tick += DrawPath;
            level.LevelTicker.Tick += GameTick;
            Start();
            string p = "";
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
