using System;
using System.Collections.Generic;

namespace Snake
{
    public class Ui
    {
        public event Action StartGame;
        public event Action<Direction> ChangeDirection;

        private Coordinate? _lastCoordinate;
        private Coordinate? _lastFeed;
        private Coordinate _pointsDisplay = new Coordinate(1, 1);
        public void UpdateSnake(List<Coordinate> snake)
        {
            if (_lastCoordinate != null)
            {
                var x = _lastCoordinate.Value.X;
                var y = _lastCoordinate.Value.Y;
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }

            foreach (Coordinate coordinate in snake)
            {
                Console.SetCursorPosition(coordinate.X, coordinate.Y);
                Console.Write("X");
            }

            Console.SetCursorPosition(1, 24);
            _lastCoordinate = snake[snake.Count - 1];
        }

        internal void UpdatePoints(int points)
        {
            Console.SetCursorPosition(_pointsDisplay.X, _pointsDisplay.Y);
            Console.Write($"{points} Punkte");
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
            Console.Write("o");

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
