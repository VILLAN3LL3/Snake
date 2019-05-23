using System;
using System.Collections.Generic;

namespace Snake
{
    public static class Snakes
    {
        public static List<Coordinate> NewSnake()
        {
            return new List<Coordinate>
            {
                new Coordinate(30, 10),
                new Coordinate(31, 10),
                new Coordinate(32, 10)
            };
        }

        public static Direction InitialDirection()
        {
            return Direction.Right;
        }

        public static List<Coordinate> MoveSnake(
            List<Coordinate> snake,
            Direction direction)
        {
            Coordinate frontCoordinate = snake[0];
            Coordinate newFrontCoordinate = MoveCoordinate(frontCoordinate, direction);
            snake.Insert(0, newFrontCoordinate);
            return snake;
        }

        public static List<Coordinate> ShortenSnake(List<Coordinate> snake)
        {
            snake.RemoveAt(snake.Count - 1);
            return snake;
        }

        private static Coordinate MoveCoordinate(
            Coordinate coordinate,
            Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    coordinate.X--;
                    break;
                case Direction.Right:
                    coordinate.X++;
                    break;
                case Direction.Up:
                    coordinate.Y--;
                    break;
                case Direction.Down:
                    coordinate.Y++;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown enum value {direction}.");
            }

            return coordinate;
        }

        internal static int CalcPoints(List<Coordinate> snake, int points)
        {
            return points += snake.Count;
        }

        public static Coordinate NewFeed()
        {
            var x = new Random().Next(1, 80);
            var y = new Random().Next(1, 23);
            return new Coordinate(x, y);
        }

        public static bool SnakeIsEating(List<Coordinate> snake, Coordinate feed)
        {
            return snake[0].Equals(feed);
        }
    }
}
