using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake
{
    public class Ui
    {
        public event Action StartGame;
        public event Action<Direction> ChangeDirection;

        private Coordinate? _lastCoordinate;
        private Coordinate? _lastFeed;
        private Coordinate _pointsDisplay = new Coordinate(Console.WindowWidth - 15, 0);
        private Coordinate _levelDisplay = new Coordinate(Console.WindowWidth - 15, 1);

        public bool ExcludeCollision(List<Coordinate> snake)
        {
            return snake.Distinct().Count() == snake.Count;
        }

        public void UpdateSnake(List<Coordinate> snake)
        {
            if (_lastCoordinate != null)
            {
                var x = _lastCoordinate.Value.X;
                var y = _lastCoordinate.Value.Y;
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }

            Console.SetCursorPosition(snake[0].X, snake[0].Y);
            Console.Write("\u0298");
            foreach (Coordinate coordinate in snake.Skip(1))
            {
                Console.SetCursorPosition(coordinate.X, coordinate.Y);
                Console.Write("\u039F");
            }

            Console.SetCursorPosition(1, 24);
            _lastCoordinate = snake[snake.Count - 1];
        }

        internal void UpdateLevel(int level)
        {
            Console.SetCursorPosition(_levelDisplay.X, _levelDisplay.Y);
            Console.Write($"Level:  {level}");
        }

        public void PrepareConsole()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Console.Clear();
            Console.WriteLine("##################################");
            Console.WriteLine("#### Willkommen bei S N A K E ####");
            Console.WriteLine("##################################");
            Console.WriteLine();
            Console.WriteLine("Neues Spiel = N");
            Console.WriteLine("Spiel verlassen = X");
        }
        public void AbortGame()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("###################");
            Console.WriteLine("#### GAME OVER ####");
            Console.WriteLine("###################");
            Environment.Exit(0);
        }

        internal void UpdatePoints(int points)
        {
            Console.SetCursorPosition(_pointsDisplay.X, _pointsDisplay.Y);
            Console.Write($"Punkte: {points}");
        }

        public void UpdateFeed(Coordinate feed)
        {
            if (_lastFeed != null)
            {
                var x = _lastFeed.Value.X;
                var y = _lastFeed.Value.Y;
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }


            Console.SetCursorPosition(feed.X, feed.Y);
            Console.Write("\u0239");

            Console.SetCursorPosition(1, 24);
            _lastFeed = feed;
        }

        public void Run()
        {
            ConsoleKey consoleKey;
            do
            {
                consoleKey = Console.ReadKey().Key;
                switch (consoleKey)
                {
                    case ConsoleKey.DownArrow:
                        ChangeDirection(Direction.Down);
                        break;
                    case ConsoleKey.UpArrow:
                        ChangeDirection(Direction.Up);
                        break;
                    case ConsoleKey.LeftArrow:
                        ChangeDirection(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        ChangeDirection(Direction.Right);
                        break;
                    case ConsoleKey.N:
                        StartGame();
                        break;
                }
            } while (consoleKey != ConsoleKey.X);
        }
    }
}
